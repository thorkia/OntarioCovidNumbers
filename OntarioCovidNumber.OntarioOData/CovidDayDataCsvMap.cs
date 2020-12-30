using System;
using System.Collections.Generic;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using OntarioCovidNumber.Core;

namespace OntarioCovidNumber.OntarioOData
{
	public class CovidDayDataCsvMap : ClassMap<CovidDayData>
	{
		public CovidDayDataCsvMap()
		{
			Map(m => m.Date).Name("Reported Date");
			Map(m => m.ActiveCases).TypeConverter<ConvertToInt>().Name("Confirmed Positive").Default(0);
			Map(m => m.Deaths).TypeConverter<ConvertToInt>().Name("Deaths").Default(0);
			Map(m => m.Resolved).TypeConverter<ConvertToInt>().Name("Resolved").Default(0);
			Map(m => m.TotalCases).TypeConverter<ConvertToInt>().Name("Total Cases").Default(0);
			Map(m => m.TotalTestsPerformed).TypeConverter<ConvertToInt>().Name("Total patients approved for testing as of Reporting Date").Default(0);
			Map(m => m.TestsCompletedLastDay).TypeConverter<ConvertToInt>().Name("Total tests completed in the last day").Default(0);
			Map(m => m.PendingTests).TypeConverter<ConvertToInt>().Name("Under Investigation").Default(0);

			Map(m => m.InHospital).TypeConverter<ConvertToInt>().Name("Number of patients hospitalized with COVID-19").Default(0);
			Map(m => m.InIcu).TypeConverter<ConvertToInt>().Name("Number of patients in ICU with COVID-19").Default(0);
			Map(m => m.OnVentilator).TypeConverter<ConvertToInt>().Name("Number of patients in ICU on a ventilator with COVID-19").Default(0);

		}
	}

	public class ConvertToInt : ITypeConverter
	{
		public object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
		{
			return (int)decimal.Parse(text);
		}

		public string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
		{
			return value.ToString();
		}
	}

}
