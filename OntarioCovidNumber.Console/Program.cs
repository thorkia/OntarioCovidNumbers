using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OntarioCovidNumber.Core;
using OntarioCovidNumber.OntarioOData;

namespace OntarioCovidNumber.Console
{
	class Program
	{
		static void Main(string[] args)
		{
			var services = new ServiceCollection();
			ConfigureServices(services);


			var logger = services.BuildServiceProvider().GetService<ILogger<ICovidRepository>>();

			ICovidRepository repo = new OntarioODataRepository(logger);

			var items = repo.GetDayData();
			var dayover = repo.GetDayOverDayData();
			var roll = repo.GetRollingAverage(7);

			var lastavg = roll.Last();

			System.Console.WriteLine($"Count {items.Count()}");
			System.Console.ReadLine();
		}

		private static void ConfigureServices(ServiceCollection services)
		{
			services.AddLogging(configure => configure.AddConsole());
		}
	}
}
