using System;
using System.Linq;
using OntarioCovidNumber.Core;
using OntarioCovidNumber.OntarioOData;

namespace OntarioCovidNumber.Console
{
	class Program
	{
		static void Main(string[] args)
		{
			ICovidRepository repo = new OntarioODataRepository();

			var items = repo.GetDayData();
			var dayover = repo.GetDayOverDayData();
			var roll = repo.GetRollingAverage(7);

			var lastavg = roll.Last();

			System.Console.WriteLine($"Count {items.Count()}");
			System.Console.ReadLine();
		}
	}
}
