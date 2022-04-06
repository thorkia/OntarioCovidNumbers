using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using CsvHelper;
using Microsoft.Extensions.Logging;
using OntarioCovidNumber.Core;

namespace OntarioCovidNumber.OntarioOData
{
	public class OntarioODataRepository : ICovidRepository
	{
		public static DateTime FirstDate = new DateTime(2020, 02, 07);
		public static string DateFormat = "yyyy-MM-dd";

		private static string CASE_DATA_SOURCE = "https://data.ontario.ca/dataset/f4f86e54-872d-43f8-8a86-3892fd3cb5e6/resource/ed270bb8-340b-41f9-a7c6-e8ef587e6d11/download/covidtesting.csv";

		private static string VACCINE_DATA_SOURCE = "https://data.ontario.ca/dataset/752ce2b7-c15a-4965-a3dc-397bf405e7cc/resource/8a89caa9-511c-4568-af89-7f2174b4378c/download/vaccine_doses.csv";


		private readonly List<CovidDayData> _caseDayData;
		private readonly List<CasesDayOverDay> _casesDayOverDay;

		private readonly List<VaccineDayData> _vaccineDayData;
		private readonly List<VaccineDayOverDay> _vaccineDayOverDayData;


		private readonly ILogger<ICovidRepository> _logger;
		
		public OntarioODataRepository(ILogger<ICovidRepository> logger)
		{
			_logger = logger;
			_caseDayData = new List<CovidDayData>();
			_casesDayOverDay = new List<CasesDayOverDay>();

			LoadCaseData();

			_vaccineDayData = new List<VaccineDayData>();
			_vaccineDayOverDayData = new List<VaccineDayOverDay>();

			LoadVaccineData();
		}

		#region Cases
		public IEnumerable<CovidDayData> GetCaseDayData()
		{
			return _caseDayData.AsEnumerable();
		}

		public IEnumerable<CasesDayOverDay> GetCaseDayOverDayData()
		{
			return _casesDayOverDay.AsEnumerable();
		}

		public IEnumerable<CasesRollingAverage> GetCasesRollingAverage(int avg = 7)
		{
			List<CasesRollingAverage> averages = new List<CasesRollingAverage>();

			//since average is inclusive, we want to go back avg-1 days
			int lookback = avg - 1;

			for (int i = lookback; i < _caseDayData.Count; i++)
			{
				Range r = new Range(i-lookback,i+1);
				var rolling = new CasesRollingAverage(_caseDayData.ToArray()[r].ToList(), _caseDayData[i]);
				averages.Add(rolling);
			}


			return averages;
		}


		public CovidDayData GetCaseDayDataByDate(DateTime date)
		{
			return _caseDayData.FirstOrDefault(c => c.Date.Date == date.Date);
		}

		public CasesDayOverDay GetCaseDayOverDayByDate(DateTime date)
		{
			return _casesDayOverDay.FirstOrDefault(d => d.Today.Date.Date == date.Date);
		}


		private void LoadCaseData()
		{
			string data = string.Empty;
			try
			{
				WebClient wc = new WebClient();
				data = wc.DownloadString(CASE_DATA_SOURCE);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error getting case data");
			}

			if (string.IsNullOrEmpty(data))
			{
				_logger.LogWarning("Unable to load case data from Government site");
			}

			List<CovidDayData> tempRead;

			using (var memory = new StringReader(data))
			using (var csv = new CsvReader(memory, CultureInfo.InvariantCulture))
			{
				csv.Configuration.RegisterClassMap<CovidDayDataCsvMap>();
				tempRead = csv.GetRecords<CovidDayData>().OrderBy(c => c.Date).ToList();
			}

			_logger.LogWarning($"Records Downloaded {tempRead.Count}");

			ProcessDayRecords(tempRead);
			ProcessDayOverDay(_caseDayData);

			_logger.LogWarning($"Records {_caseDayData.Count} Day over Day Count {_casesDayOverDay.Count}, ");
		}

		private void ProcessDayRecords(List<CovidDayData> dayData)
		{
			int start = dayData.FindIndex(c => c.Date.Date == FirstDate.Date);

			if (start == -1)
			{
				return;
			}

			for (int i = start; i < dayData.Count; i++)
			{
				if (dayData[i].TestsCompletedLastDay == 0)
				{
					dayData[i].TestsCompletedLastDay = dayData[i].TotalTestsPerformed - dayData[i-1].TotalTestsPerformed;
				}

				if (dayData[i].DeathsNewMethod > 0)
				{
					dayData[i].Deaths = dayData[i].DeathsNewMethod;
				}

				dayData[i].NewCases = dayData[i].TotalCases - dayData[i - 1].TotalCases;
				dayData[i].NewDeaths = dayData[i].Deaths - dayData[i - 1].Deaths;
				dayData[i].NewResolved = dayData[i].Resolved - dayData[i - 1].Resolved;					

				_caseDayData.Add(dayData[i]);
			}
		}

		private void ProcessDayOverDay(List<CovidDayData> dayData)
		{
			//Start at 1 to compare the 2nd date to the 1st.  Cannot compare the first date to nothing
			for (int i = 1; i < dayData.Count; i++)
			{
				_casesDayOverDay.Add(new CasesDayOverDay(dayData[i], dayData[i - 1]));

			}
		}

		#endregion

		#region Vaccine

		public IEnumerable<VaccineDayData> GetVaccineDayData()
		{
			return _vaccineDayData.AsEnumerable();
		}

		public IEnumerable<VaccineDayOverDay> GetVaccineDayOverDayData()
		{
			return _vaccineDayOverDayData.AsEnumerable();
		}

		//public IEnumerable<CasesRollingAverage> GetCasesRollingAverage(int avg = 7)
		//{
		//	List<CasesRollingAverage> averages = new List<CasesRollingAverage>();

		//	//since average is inclusive, we want to go back avg-1 days
		//	int lookback = avg - 1;

		//	for (int i = lookback; i < _caseDayData.Count; i++)
		//	{
		//		Range r = new Range(i - lookback, i + 1);
		//		var rolling = new CasesRollingAverage(_caseDayData.ToArray()[r].ToList(), _caseDayData[i]);
		//		averages.Add(rolling);
		//	}


		//	return averages;
		//}


		public VaccineDayData GetVaccineDayDataByDate(DateTime date)
		{
			return _vaccineDayData.FirstOrDefault(c => c.Date.Date == date.Date);
		}

		public VaccineDayOverDay GetVaccineDayOverDayByDate(DateTime date)
		{
			return _vaccineDayOverDayData.FirstOrDefault(d => d.Today.Date.Date == date.Date);
		}



		private void LoadVaccineData()
		{
			string data = string.Empty;
			try
			{
				WebClient wc = new WebClient();
				data = wc.DownloadString(VACCINE_DATA_SOURCE);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error getting vaccine data");
			}

			if (string.IsNullOrEmpty(data))
			{
				_logger.LogWarning("Unable to load vaccine data from Government site");
			}

			List<VaccineDayData> tempRead;

			using (var memory = new StringReader(data))
			using (var csv = new CsvReader(memory, CultureInfo.InvariantCulture))
			{
				csv.Configuration.RegisterClassMap<VaccineDayDataCsvMap>();
				tempRead = csv.GetRecords<VaccineDayData>().OrderBy(c => c.Date).ToList();
			}

			_logger.LogWarning($"Records Downloaded {tempRead.Count}");

			ProcessDayRecords(tempRead);
			ProcessDayOverDay(_vaccineDayData);

			_logger.LogWarning($"Records {_vaccineDayData.Count} Day over Day Count {_vaccineDayOverDayData.Count}, ");
		}

		private void ProcessDayRecords(List<VaccineDayData> dayData)
		{
			_vaccineDayData.AddRange( dayData.Where(c => c.Date.Date >= FirstDate.Date) );
		}

		private void ProcessDayOverDay(List<VaccineDayData> dayData)
		{
			//Start at 1 to compare the 2nd date to the 1st.  Cannot compare the first date to nothing
			for (int i = 1; i < dayData.Count; i++)
			{
				_vaccineDayOverDayData.Add(new VaccineDayOverDay(dayData[i], dayData[i - 1]));

			}
		}

		#endregion
		}

}
