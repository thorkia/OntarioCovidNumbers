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
		public static DateTime FirstDate = new DateTime(2020, 02, 05);
		public static string DateFormat = "yyyy-MM-dd";

		private static string DATA_SOURCE = "https://data.ontario.ca/dataset/f4f86e54-872d-43f8-8a86-3892fd3cb5e6/resource/ed270bb8-340b-41f9-a7c6-e8ef587e6d11/download/covidtesting.csv";
		
		private readonly List<CovidDayData> _covidDayData;
		private readonly List<DayOverDay> _dayOverDay;

		private readonly ILogger<ICovidRepository> _logger;
		
		public OntarioODataRepository(ILogger<ICovidRepository> logger)
		{
			_logger = logger;
			_covidDayData = new List<CovidDayData>();
			_dayOverDay = new List<DayOverDay>();

			string data = string.Empty;
			try
			{
				WebClient wc = new WebClient();
				data = wc.DownloadString(DATA_SOURCE);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error getting data");
			}

			if (string.IsNullOrEmpty(data))
			{
				_logger.LogWarning("Unable to load data from Government site");
			}
			
			List<CovidDayData> tempRead;

			using (var memory = new StringReader(data))
			using (var csv = new CsvReader(memory, CultureInfo.InvariantCulture))
			{
				csv.Configuration.RegisterClassMap<CovidDayDataCsvMap>();
				tempRead = csv.GetRecords<CovidDayData>().OrderBy(c => c.Date).ToList();
			}

			_logger.LogWarning($"Records Downloaded {tempRead.Count}");

			ProcessCovidDayRecords(tempRead);
			ProcessDayOverDay(_covidDayData);

			_logger.LogWarning($"Records {_covidDayData.Count} Day over Day Count {_dayOverDay.Count}, ");
		}

		
		public IEnumerable<CovidDayData> GetDayData()
		{
			return _covidDayData.AsEnumerable();
		}

		public IEnumerable<DayOverDay> GetDayOverDayData()
		{
			return _dayOverDay.AsEnumerable();
		}

		public IEnumerable<RollingAverage> GetRollingAverage(int avg = 7)
		{
			List<RollingAverage> averages = new List<RollingAverage>();

			//since average is inclusive, we want to go back avg-1 days
			int lookback = avg - 1;

			for (int i = lookback; i < _covidDayData.Count; i++)
			{
				Range r = new Range(i-lookback,i+1);
				var rolling = new RollingAverage(_covidDayData.ToArray()[r].ToList(), _covidDayData[i]);
				averages.Add(rolling);
			}


			return averages;
		}


		public CovidDayData GetDayDataByDate(DateTime date)
		{
			return _covidDayData.FirstOrDefault(c => c.Date.Date == date.Date);
		}

		public DayOverDay GetDayOverDayByDate(DateTime date)
		{
			return _dayOverDay.FirstOrDefault(d => d.Today.Date.Date == date.Date);
		}


		private void ProcessCovidDayRecords(List<CovidDayData> dayData)
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

				dayData[i].NewCases = dayData[i].TotalCases - dayData[i - 1].TotalCases;
				dayData[i].NewDeaths = dayData[i].Deaths - dayData[i - 1].Deaths;
				dayData[i].NewResolved = dayData[i].Resolved - dayData[i - 1].Resolved;

				_covidDayData.Add(dayData[i]);
			}
		}

		private void ProcessDayOverDay(List<CovidDayData> dayData)
		{
			//Start at 1 to compare the 2nd date to the 1st.  Cannot compare the first date to nothing
			for (int i = 1; i < dayData.Count; i++)
			{
				_dayOverDay.Add(new DayOverDay(dayData[i], dayData[i - 1]));

			}
		}
	}
}
