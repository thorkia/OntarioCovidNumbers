using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OntarioCovidNumber.Core;

namespace OntarioCovidNumber.Web.Models
{
	public class SummaryModel
	{
		public DayOverDay Today { get; set; }
		public DayOverDay Yesterday { get; set; }

		public decimal Mortality2020 { get; set; }

		public decimal Mortality2021 { get; set; }
		
		public string GetTodayVersusYesterdayDisplayClass(string field)
		{
			ChangeType change = ChangeType.NoChange;
			
			switch (field.ToUpper())
			{
				case "NEWCASES":
					change = DayOverDay.GetChangeType(field.ToUpper(),
					                                DayOverDay.GetChangeDirection(Today.Today.NewCases, Yesterday.Today.NewCases));
					break;
				case "TESTSCOMPLETEDLASTDAY":
					change = DayOverDay.GetChangeType(field.ToUpper(),
					                                  DayOverDay.GetChangeDirection(Today.Today.TestsCompletedLastDay, Yesterday.Today.TestsCompletedLastDay));
					break;
				case "NEWRESOLVED":
					change = DayOverDay.GetChangeType(field.ToUpper(),
					                                  DayOverDay.GetChangeDirection(Today.Today.NewResolved, Yesterday.Today.NewResolved));
					break;
				case "NEWDEATHS":
					change = DayOverDay.GetChangeType(field.ToUpper(),
					                                  DayOverDay.GetChangeDirection(Today.Today.NewDeaths, Yesterday.Today.NewDeaths));
					break;
				case "INHOSPITAL":
					change = DayOverDay.GetChangeType(field.ToUpper(),
					                                  DayOverDay.GetChangeDirection(Today.Today.InHospital, Yesterday.Today.InHospital));
					break;
				case "INICU":
					change = DayOverDay.GetChangeType(field.ToUpper(),
					                                  DayOverDay.GetChangeDirection(Today.Today.InIcu, Yesterday.Today.InIcu));
					break;
				case "ONVENTILATOR":
					change = DayOverDay.GetChangeType(field.ToUpper(),
					                                  DayOverDay.GetChangeDirection(Today.Today.OnVentilator, Yesterday.Today.OnVentilator));
					break;
				case "TESTMORTALITYRATE":
					change = DayOverDay.GetChangeType(field.ToUpper(),
					                                  DayOverDay.GetChangeDirection(Today.Today.TestMortalityRate, Yesterday.Today.TestMortalityRate));
					break;
				case "POSITIVEMORTALITYRATE":
					change = DayOverDay.GetChangeType(field.ToUpper(),
					                                  DayOverDay.GetChangeDirection(Today.Today.PositiveMortalityRate, Yesterday.Today.PositiveMortalityRate));
					break;
				case "DEATHS":
					change = DayOverDay.GetChangeType(field.ToUpper(),
					                                  DayOverDay.GetChangeDirection(Today.Today.Deaths, Yesterday.Today.Deaths));
					break;

				case "PROVINCIALMORTALITYRATE":
					change = DayOverDay.GetChangeType(field.ToUpper(),
					                                  DayOverDay.GetChangeDirection(Today.Today.ProvincialMortalityRate, Yesterday.Today.ProvincialMortalityRate));
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

		public string GetChangeDirection(string field)
		{
			ChangeDirection dir = ChangeDirection.NoChange;

			switch (field.ToUpper())
			{
				case "TOTALCASES":
					dir = DayOverDay.GetChangeDirection(Today.Today.TotalCases, Yesterday.Today.TotalCases);
					break;
				case "NEWCASES":
					dir = DayOverDay.GetChangeDirection(Today.Today.NewCases, Yesterday.Today.NewCases);
					break;
				case "TESTSCOMPLETEDLASTDAY":
					dir = DayOverDay.GetChangeDirection(Today.Today.TestsCompletedLastDay, Yesterday.Today.TestsCompletedLastDay);
					break;

				case "NEWRESOLVED":
					dir = DayOverDay.GetChangeDirection(Today.Today.NewResolved, Yesterday.Today.NewResolved);
					break;

				case "NEWDEATHS":
					dir = DayOverDay.GetChangeDirection(Today.Today.NewDeaths, Yesterday.Today.NewDeaths);
					break;

				case "INHOSPITAL":
					dir = DayOverDay.GetChangeDirection(Today.Today.InHospital, Yesterday.Today.InHospital);
					break;
				case "INICU":
					dir = DayOverDay.GetChangeDirection(Today.Today.InIcu, Yesterday.Today.InIcu);
					break;
				case "ONVENTILATOR":
					dir = DayOverDay.GetChangeDirection(Today.Today.OnVentilator, Yesterday.Today.OnVentilator);
					break;

				case "TESTMORTALITYRATE":
					dir = DayOverDay.GetChangeDirection(Today.Today.TestMortalityRate, Yesterday.Today.TestMortalityRate);
					break;
				case "POSITIVEMORTALITYRATE":
					dir = DayOverDay.GetChangeDirection(Today.Today.PositiveMortalityRate, Yesterday.Today.PositiveMortalityRate);
					break;
				case "DEATHS":
					dir = DayOverDay.GetChangeDirection(Today.Today.Deaths, Yesterday.Today.Deaths);
					break;
				case "PROVINCIALMORTALITYRATE":
					dir = DayOverDay.GetChangeDirection(Today.Today.ProvincialMortalityRate, Yesterday.Today.ProvincialMortalityRate);
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
	}
}
