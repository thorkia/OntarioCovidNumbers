using System;
using System.Collections.Generic;
using System.Text;

namespace OntarioCovidNumber.Core
{
	public interface ICovidRepository
	{

		IEnumerable<CovidDayData> GetDayData();

		IEnumerable<DayOverDay> GetDayOverDayData();

		IEnumerable<RollingAverage> GetRollingAverage(int avg = 7);

		CovidDayData GetDayDataByDate(DateTime date);

		DayOverDay GetDayOverDayByDate(DateTime date);

	}
}
