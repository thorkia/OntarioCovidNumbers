using System;
using System.Collections.Generic;
using System.Text;

namespace OntarioCovidNumber.Core
{
	public class VaccineDayData
	{
		public DateTime Date { get; set; }

		
		public int DosesAdministeredToday { get; set; }

		public int FirstDoseAdministered { get; set; }

		public int SecondDoseAdministered { get; set; }


		public int TotalAdministered { get; set; }

		public int TotalAtleastOne { get; set; }

		public int TotalFullyVaccinated { get; set; }

		public int TotalThirdDose { get; set; }



		public int NumberOnlyFirstDose => TotalAtleastOne - TotalFullyVaccinated;

		public decimal FirstDoseOnlyPercentage => (NumberOnlyFirstDose / (decimal)StaticData.OntarioVaccineEligible) * 100;

		public decimal AtleastOnePercentage => ( (TotalAtleastOne) / (decimal)StaticData.OntarioVaccineEligible) * 100;

		public decimal FullyVaccinatedPercentage => (TotalFullyVaccinated / (decimal)StaticData.OntarioVaccineEligible) * 100;

		public decimal ThirdDosePercentage => (TotalThirdDose / (decimal)StaticData.OntarioVaccineEligible) * 100;
	}
}
