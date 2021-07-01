using System;
using System.Net;

namespace OntarioCovidNumber.Core
{
	public class CovidDayData
	{
		#region OdataFields
		public DateTime Date { get; set; }

		/// <summary>
		/// Confirmed Positive : This is the number of active cases
		/// </summary>
		public int ActiveCases { get; set; }
		/// <summary>
		/// Resolved : This is the number of recoveries
		/// </summary>
		public int Resolved { get; set;}
		/// <summary>
		/// Deaths : This is the number of deaths
		/// </summary>
		public int Deaths { get; set; }
		/// <summary>
		/// Total Cases : This is the number of cases up to this date
		/// </summary>
		public int TotalCases { get; set; } 
		/// <summary>
		/// Total patients approved for testing as of Reporting Date : Total number of patients tests
		/// </summary>
		public int TotalTestsPerformed { get; set; }
		/// <summary>
		/// Total tests completed in the last day : 
		/// </summary>
		public int TestsCompletedLastDay { get; set; } 
		/// <summary>
		/// Under Investigation : Total backlog of tests
		/// </summary>
		public int PendingTests { get; set; }
		/// <summary>
		/// Number of patients hospitalized with COVID-19 : Number of patients in hospital on that date
		/// </summary>
		public int InHospital { get; set; }
		/// <summary>
		/// Number of patients in ICU with COVID-19 : Number of patients in hospital on that date
		/// </summary>
		public int InIcu { get; set; }
		/// <summary>
		/// Number of patients in ICU on a ventilator with COVID-19 : Number of ICU patients on ventilation on that date
		/// </summary>
		public int OnVentilator { get; set; }
		
		#endregion

		#region Calculated Fields
		/// <summary>
		/// Number of new cases since last reported date
		/// </summary>
		public int NewCases { get; set; }

		/// <summary>
		/// Number of new deaths since last reported date
		/// </summary>
		public int NewDeaths { get; set; }

		/// <summary>
		/// Number of new resolved since last reported date
		/// </summary>
		public int NewResolved { get; set; }

		/// <summary>
		/// Number of Positive Cases
		/// </summary>
		public decimal PercentPositive => (NewCases / (decimal)TestsCompletedLastDay) * 100;

		/// <summary>
		/// Mortality Rate for those who received a test for covid
		/// </summary>
		public decimal TestMortalityRate => (Deaths / (decimal)TotalTestsPerformed) * 100;

		/// <summary>
		/// Mortality Rate for those who tested positive for covid
		/// </summary>
		public decimal PositiveMortalityRate => (Deaths / (decimal)TotalCases) * 100;

		/// <summary>
		/// Mortality Rate for the entire population of Ontario
		/// </summary>
		public decimal ProvincialMortalityRate => (Deaths / (decimal) StaticData.OntarioPopulation) * 100;

		#endregion
	}
}

