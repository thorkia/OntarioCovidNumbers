using System;
using System.Collections.Generic;
using System.Text;

namespace OntarioCovidNumber.Core
{
	public interface ICovidRepository
	{

		IEnumerable<CovidDayData> GetCaseDayData();

		IEnumerable<CasesDayOverDay> GetCaseDayOverDayData();

		IEnumerable<CasesRollingAverage> GetCasesRollingAverage(int avg = 7);

		CovidDayData GetCaseDayDataByDate(DateTime date);

		CasesDayOverDay GetCaseDayOverDayByDate(DateTime date);


		IEnumerable<VaccineDayData> GetVaccineDayData();

		IEnumerable<VaccineDayOverDay> GetVaccineDayOverDayData();

		//IEnumerable<CasesRollingAverage> GetVaccinesRollingAverage(int avg = 7);

		VaccineDayData GetVaccineDayDataByDate(DateTime date);

		VaccineDayOverDay GetVaccineDayOverDayByDate(DateTime date);



	}
}
