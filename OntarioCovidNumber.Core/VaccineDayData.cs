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

		public int TotalFullyVaccinated { get; set; }



		public int NumberOnlyFirstDose => TotalAdministered - (TotalFullyVaccinated * 2);

		public decimal FirstDoseOnlyPercentage => (NumberOnlyFirstDose / (decimal)StaticData.Ontario12PlusPopulation) * 100;

		public decimal AtleastOnePercentage => ( (NumberOnlyFirstDose + TotalFullyVaccinated) / (decimal)StaticData.Ontario12PlusPopulation) * 100;

		public decimal FullyVaccinatedPercentage => (TotalFullyVaccinated / (decimal)StaticData.Ontario12PlusPopulation) * 100;
	}
}
