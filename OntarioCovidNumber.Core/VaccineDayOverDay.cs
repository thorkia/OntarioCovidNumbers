using System;
using System.Collections.Generic;
using System.Text;

namespace OntarioCovidNumber.Core
{
	public class VaccineDayOverDay
	{
		private readonly VaccineDayData _today;
		private readonly VaccineDayData _previousDay;

		public VaccineDayData Today => _today;

		#region AbsoluteChanges

		public int DosesAdministeredTodayChangeAmount => _today.DosesAdministeredToday - _previousDay.DosesAdministeredToday;

		public int FirstDoseAdministeredChangeAmount => _today.FirstDoseAdministered - _previousDay.FirstDoseAdministered;

		public int SecondDoseAdministeredChangeAmount => _today.SecondDoseAdministered - _previousDay.SecondDoseAdministered;

		public int TotalAdministeredChangeAmount => _today.TotalAdministered - _previousDay.TotalAdministered;

		public int TotalFullyVaccinatedChangeAmount => _today.TotalFullyVaccinated - _previousDay.TotalFullyVaccinated;

		public int FirstDoseOnlyChangeAmount => _today.NumberOnlyFirstDose - _previousDay.NumberOnlyFirstDose;

		public decimal FirstDoseOnlyPercentChangeAmount => _today.FirstDoseOnlyPercentage - _previousDay.FirstDoseOnlyPercentage;

		public decimal AtleastOnePercentChangeAmount => _today.AtleastOnePercentage - _previousDay.AtleastOnePercentage;

		public decimal FullyVaccinatedPercentChangeAmount => _today.FullyVaccinatedPercentage - _previousDay.FullyVaccinatedPercentage;

		#endregion

		#region Percent Change Amounts

		public decimal DosesAdministeredTodayChangePercent => DayOverDayHelpers.PercentChange(_today.DosesAdministeredToday, _previousDay.DosesAdministeredToday);

		public decimal FirstDoseAdministeredChangePercent => DayOverDayHelpers.PercentChange(_today.FirstDoseAdministered, _previousDay.FirstDoseAdministered);

		public decimal SecondDoseAdministeredChangePercent => DayOverDayHelpers.PercentChange(_today.SecondDoseAdministered, _previousDay.SecondDoseAdministered);
		
		public decimal TotalAdministeredChangPercent => DayOverDayHelpers.PercentChange(_today.TotalAdministered, _previousDay.TotalAdministered);

		public decimal TotalFullyVaccinatedChangePercent => DayOverDayHelpers.PercentChange(_today.TotalFullyVaccinated, _previousDay.TotalFullyVaccinated);

		public decimal FirstDoseOnlyChangePercent => DayOverDayHelpers.PercentChange(_today.NumberOnlyFirstDose, _previousDay.NumberOnlyFirstDose);

		public decimal FirstDoseOnlyPercentChangePercent => DayOverDayHelpers.PercentChange(_today.FirstDoseOnlyPercentage, _previousDay.FirstDoseOnlyPercentage);

		public decimal AtleastOnePercentChangePercent => DayOverDayHelpers.PercentChange(_today.AtleastOnePercentage, _previousDay.AtleastOnePercentage);

		public decimal FullyVaccinatedPercentChangePercent => DayOverDayHelpers.PercentChange(_today.FullyVaccinatedPercentage, _previousDay.FullyVaccinatedPercentage);


		#endregion region


		public VaccineDayOverDay(VaccineDayData today, VaccineDayData previousDay)
		{
			_today = today;
			_previousDay = previousDay;
		}
	}
}
