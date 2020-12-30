using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OntarioCovidNumber.Core
{
	public class RollingAverage
	{
		private readonly List<CovidDayData> _days;
		public CovidDayData CurrentDay { get; }

		public decimal ActiveCases => (decimal)_days.Sum(d => d.ActiveCases) / _days.Count;
		public decimal Resolved => (decimal)_days.Sum(d => d.Resolved) / _days.Count;
		public decimal Deaths => (decimal)_days.Sum(d => d.Deaths) / _days.Count;
		public decimal TestsCompletedLastDay => (decimal)_days.Sum(d => d.TestsCompletedLastDay) / _days.Count;
		public decimal PendingTests => (decimal)_days.Sum(d => d.PendingTests) / _days.Count;
		public decimal InHospital => (decimal)_days.Sum(d => d.InHospital) / _days.Count;
		public decimal InIcu => (decimal)_days.Sum(d => d.InIcu) / _days.Count;
		public decimal OnVentilator => (decimal)_days.Sum(d => d.OnVentilator) / _days.Count;
		public decimal NewCases => (decimal)_days.Sum(d => d.NewCases) / _days.Count;
		public decimal NewDeaths => (decimal)_days.Sum(d => d.NewDeaths) / _days.Count;
		public decimal NewResolved => (decimal)_days.Sum(d => d.NewResolved) / _days.Count;
		public decimal PercentPositive => _days.Sum(d => d.PercentPositive) / _days.Count;


		public RollingAverage(List<CovidDayData> days, CovidDayData currentDay)
		{
			_days = days;
			CurrentDay = currentDay;
		}
	}
}
