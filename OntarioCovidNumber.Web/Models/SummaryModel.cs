using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OntarioCovidNumber.Core;
using OntarioCovidNumber.OntarioOData;

namespace OntarioCovidNumber.Web.Models
{
	public class SummaryModel
	{
		public CasesDayOverDay TodayCases { get; set; }
		public CasesDayOverDay YesterdayCases { get; set; }

		public VaccineDayOverDay TodayVax { get; set; }

		public VaccineDayOverDay YesterdayVax { get; set; }

		public decimal Mortality2020 { get; set; }

		public decimal Mortality2021 { get; set; }

		public decimal Mortality2022 { get; set; }

		public string GetTodayVersusYesterdayCasesDisplayClass(string field)
		{
			ChangeType change = ChangeType.NoChange;
			
			switch (field.ToUpper())
			{
				case "NEWCASES":
					change = DayOverDayHelpers.GetChangeType(field.ToUpper(),
					                                         DayOverDayHelpers.GetChangeDirection(TodayCases.Today.NewCases, YesterdayCases.Today.NewCases),
					                                         CasesDayOverDay.IncreaseGoodFields);
					break;
				case "TESTSCOMPLETEDLASTDAY":
					change = DayOverDayHelpers.GetChangeType(field.ToUpper(),
					                                         DayOverDayHelpers.GetChangeDirection(TodayCases.Today.TestsCompletedLastDay, YesterdayCases.Today.TestsCompletedLastDay),
					                                         CasesDayOverDay.IncreaseGoodFields);
					break;
				case "NEWRESOLVED":
					change = DayOverDayHelpers.GetChangeType(field.ToUpper(),
					                                         DayOverDayHelpers.GetChangeDirection(TodayCases.Today.NewResolved, YesterdayCases.Today.NewResolved),
					                                         CasesDayOverDay.IncreaseGoodFields);
					break;
				case "NEWDEATHS":
					change = DayOverDayHelpers.GetChangeType(field.ToUpper(),
					                                         DayOverDayHelpers.GetChangeDirection(TodayCases.Today.NewDeaths, YesterdayCases.Today.NewDeaths),
					                                         CasesDayOverDay.IncreaseGoodFields);
					break;
				case "INHOSPITAL":
					change = DayOverDayHelpers.GetChangeType(field.ToUpper(),
					                                         DayOverDayHelpers.GetChangeDirection(TodayCases.Today.InHospital, YesterdayCases.Today.InHospital),
					                                         CasesDayOverDay.IncreaseGoodFields);
					break;
				case "INICU":
					change = DayOverDayHelpers.GetChangeType(field.ToUpper(),
					                                         DayOverDayHelpers.GetChangeDirection(TodayCases.Today.InIcu, YesterdayCases.Today.InIcu),
					                                         CasesDayOverDay.IncreaseGoodFields);
					break;
				case "ONVENTILATOR":
					change = DayOverDayHelpers.GetChangeType(field.ToUpper(),
					                                         DayOverDayHelpers.GetChangeDirection(TodayCases.Today.OnVentilator, YesterdayCases.Today.OnVentilator),
					                                         CasesDayOverDay.IncreaseGoodFields);
					break;
				case "TESTMORTALITYRATE":
					change = DayOverDayHelpers.GetChangeType(field.ToUpper(),
					                                         DayOverDayHelpers.GetChangeDirection(TodayCases.Today.TestMortalityRate, YesterdayCases.Today.TestMortalityRate),
					                                         CasesDayOverDay.IncreaseGoodFields);
					break;
				case "POSITIVEMORTALITYRATE":
					change = DayOverDayHelpers.GetChangeType(field.ToUpper(),
					                                         DayOverDayHelpers.GetChangeDirection(TodayCases.Today.PositiveMortalityRate, YesterdayCases.Today.PositiveMortalityRate),
					                                         CasesDayOverDay.IncreaseGoodFields);
					break;
				case "DEATHS":
					change = DayOverDayHelpers.GetChangeType(field.ToUpper(),
					                                         DayOverDayHelpers.GetChangeDirection(TodayCases.Today.Deaths, YesterdayCases.Today.Deaths),
					                                         CasesDayOverDay.IncreaseGoodFields);
					break;

				case "PROVINCIALMORTALITYRATE":
					change = DayOverDayHelpers.GetChangeType(field.ToUpper(),
					                                         DayOverDayHelpers.GetChangeDirection(TodayCases.Today.ProvincialMortalityRate, YesterdayCases.Today.ProvincialMortalityRate),
					                                         CasesDayOverDay.IncreaseGoodFields);
					break;
			}

			switch (change)
			{
				case ChangeType.Better:
					return "text-success";
				case ChangeType.Worse:
					return "text-danger";
			}

			return "text-info";
		}

		public string GetCasesChangeDirection(string field)
		{
			ChangeDirection dir = ChangeDirection.NoChange;

			switch (field.ToUpper())
			{
				case "TOTALCASES":
					dir = DayOverDayHelpers.GetChangeDirection(TodayCases.Today.TotalCases, YesterdayCases.Today.TotalCases);
					break;
				case "NEWCASES":
					dir = DayOverDayHelpers.GetChangeDirection(TodayCases.Today.NewCases, YesterdayCases.Today.NewCases);
					break;
				case "TESTSCOMPLETEDLASTDAY":
					dir = DayOverDayHelpers.GetChangeDirection(TodayCases.Today.TestsCompletedLastDay, YesterdayCases.Today.TestsCompletedLastDay);
					break;

				case "NEWRESOLVED":
					dir = DayOverDayHelpers.GetChangeDirection(TodayCases.Today.NewResolved, YesterdayCases.Today.NewResolved);
					break;

				case "NEWDEATHS":
					dir = DayOverDayHelpers.GetChangeDirection(TodayCases.Today.NewDeaths, YesterdayCases.Today.NewDeaths);
					break;

				case "INHOSPITAL":
					dir = DayOverDayHelpers.GetChangeDirection(TodayCases.Today.InHospital, YesterdayCases.Today.InHospital);
					break;
				case "INICU":
					dir = DayOverDayHelpers.GetChangeDirection(TodayCases.Today.InIcu, YesterdayCases.Today.InIcu);
					break;
				case "ONVENTILATOR":
					dir = DayOverDayHelpers.GetChangeDirection(TodayCases.Today.OnVentilator, YesterdayCases.Today.OnVentilator);
					break;

				case "TESTMORTALITYRATE":
					dir = DayOverDayHelpers.GetChangeDirection(TodayCases.Today.TestMortalityRate, YesterdayCases.Today.TestMortalityRate);
					break;
				case "POSITIVEMORTALITYRATE":
					dir = DayOverDayHelpers.GetChangeDirection(TodayCases.Today.PositiveMortalityRate, YesterdayCases.Today.PositiveMortalityRate);
					break;
				case "DEATHS":
					dir = DayOverDayHelpers.GetChangeDirection(TodayCases.Today.Deaths, YesterdayCases.Today.Deaths);
					break;
				case "PROVINCIALMORTALITYRATE":
					dir = DayOverDayHelpers.GetChangeDirection(TodayCases.Today.ProvincialMortalityRate, YesterdayCases.Today.ProvincialMortalityRate);
					break;
			}

			switch (dir)
			{
				case ChangeDirection.NoChange:
					return "";
				case ChangeDirection.Decrease:
					return "Decrease of ";
				case ChangeDirection.Increase:
					return "Increase of ";
			}

			return "";
		}

		public string GetVaxChangeDirection(int value)
		{
			if (value == 0)
			{
				return "";
			}

			if (value < 0)
			{
				return "Decrease of ";
			}

			return "Increase of ";
		}

		public string GetVaxChangeDirection(decimal value)
		{
			if (value == 0)
			{
				return "";
			}

			if (value < 0)
			{
				return "Decrease of ";
			}

			return "Increase of ";
		}

	}
}
