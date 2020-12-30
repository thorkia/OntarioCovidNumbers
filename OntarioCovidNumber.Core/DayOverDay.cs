using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace OntarioCovidNumber.Core
{
	public class DayOverDay
	{
		private static readonly HashSet<string> IncreaseGoodFields = new HashSet<string> { "RESOLVED", "TOTALTESTSPERFORMED", "TESTSCOMPLETEDLASTDAY", "NEWRESOLVED" };

		private readonly CovidDayData _today;
		private readonly CovidDayData _previousDay;

		public CovidDayData Today => _today;

		#region Absolute Change Amounts
		
		public int ActiveCaseChangeAmount => _today.ActiveCases - _previousDay.ActiveCases;
		public int ResolvedCaseChangeAmount => _today.Resolved - _previousDay.Resolved;
		public int DeathsChangeAmount => _today.Deaths - _previousDay.Deaths;
		public int TotalCaseChangeAmount => _today.TotalCases - _previousDay.TotalCases;
		public int TotalTestsPerformedChangeAmount => _today.TotalTestsPerformed - _previousDay.TotalTestsPerformed;
		public int TestCompletedChangeAmount => _today.TestsCompletedLastDay - _previousDay.TestsCompletedLastDay;
		public int PendingTestChangeAmount => _today.PendingTests - _previousDay.PendingTests;
		public int InHospitalChangeAmount => _today.InHospital - _previousDay.InHospital;
		public int InIcuChangeAmount => _today.InIcu - _previousDay.InIcu;
		public int OnVentilatorChangeAmount => _today.OnVentilator - _previousDay.OnVentilator;
		public int NewCasesChangeAmount => _today.NewCases - _previousDay.NewCases;
		public int NewDeathsChangeAmount => _today.NewDeaths - _previousDay.NewDeaths;
		public int NewResolvedChangeAmount => _today.NewResolved - _previousDay.NewResolved;
		public decimal PositiveRateChangeAmount => _today.PercentPositive - _previousDay.PercentPositive;
		public decimal TestMortalityRateChangeAmount => _today.TestMortalityRate - _previousDay.TestMortalityRate;
		public decimal PositiveMortalityRateChangeAmount => _today.PositiveMortalityRate - _previousDay.PositiveMortalityRate;
		public decimal ProvincialMortalityRateChangeAmount => _today.ProvincialMortalityRate - _previousDay.ProvincialMortalityRate;


		#endregion

		#region Percent Change Amounts

		public decimal ActiveCaseChangePercent => PercentChange(_today.ActiveCases, _previousDay.ActiveCases);
		public decimal ResolvedCaseChangePercent => PercentChange(_today.Resolved, _previousDay.Resolved);
		public decimal DeathsChangePercent => PercentChange(_today.Deaths, _previousDay.Deaths);
		public decimal TotalCaseChangePercent => PercentChange(_today.TotalCases, _previousDay.TotalCases);
		public decimal TotalTestsPerformedChangePercent => PercentChange(_today.TotalTestsPerformed, _previousDay.TotalTestsPerformed);
		public decimal TestCompletedChangePercent => PercentChange(_today.TestsCompletedLastDay, _previousDay.TestsCompletedLastDay);
		public decimal PendingTestChangePercent => PercentChange(_today.PendingTests, _previousDay.PendingTests);
		public decimal InHospitalChangePercent => PercentChange(_today.InHospital, _previousDay.InHospital);
		public decimal InIcuChangePercent => PercentChange(_today.InIcu, _previousDay.InIcu);
		public decimal OnVentilatorChangePercent => PercentChange(_today.OnVentilator, _previousDay.OnVentilator);
		public decimal NewCasesChangePercent => PercentChange(_today.NewCases, _previousDay.NewCases);
		public decimal NewDeathsChangePercent => PercentChange(_today.NewDeaths, _previousDay.NewDeaths);
		public decimal NewResolvedChangePercent => PercentChange(_today.NewResolved, _previousDay.NewResolved);
		public decimal PositiveRateChangePercent => PercentChange(_today.PercentPositive, _previousDay.PercentPositive);

		#endregion

		public DayOverDay(CovidDayData today, CovidDayData previousDay)
		{
			_today = today;
			_previousDay = previousDay;
		}

		private decimal PercentChange(int today, int previousDay)
		{
			return Math.Abs((decimal)today / previousDay - 1.0m);
		}

		private decimal PercentChange(decimal today, decimal previousDay)
		{
			return Math.Abs(today / previousDay - 1.0m);
		}

		public static ChangeDirection GetChangeDirection(int today, int previousDay)
		{
			return GetChangeDirection((decimal) today, (decimal) previousDay);
		}

		public static ChangeDirection GetChangeDirection(decimal today, decimal previousDay)
		{
			if (today < previousDay)
			{
				return ChangeDirection.Decrease;
			}
			else if (today > previousDay)
			{
				return ChangeDirection.Increase;
			}


			return ChangeDirection.NoChange;
		}

		public static ChangeType GetChangeType(string field, ChangeDirection direction)
		{
			switch (direction)
			{
				case ChangeDirection.Increase:
					if (IncreaseGoodFields.Contains(field.ToUpper()))
					{
						return ChangeType.Better;
					}

					return ChangeType.Worse;
				case ChangeDirection.Decrease:
					if (IncreaseGoodFields.Contains(field.ToUpper()))
					{
						return ChangeType.Worse;
					}

					return ChangeType.Better;
				case ChangeDirection.NoChange:
				default:
					return ChangeType.NoChange;

			}
		}
		
			                                
	}
}
