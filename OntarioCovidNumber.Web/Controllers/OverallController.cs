using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using OntarioCovidNumber.Core;
using OntarioCovidNumber.Web.Models;

namespace OntarioCovidNumber.Web.Controllers
{
	public class OverallController : Controller
	{
		private ICovidRepository _repository;
		private ILogger<OverallController> _logger;
		public OverallController(ILogger<OverallController> logger, ICovidRepository repository)
		{
			_repository = repository;
			_logger = logger;

		}
		public IActionResult Index()
		{
			return View("Summary");
		}

		public IActionResult Summary()
		{
			var todayCases = _repository.GetCaseDayOverDayByDate(DateTime.Today);
			var todayVax = _repository.GetVaccineDayOverDayByDate(DateTime.Today);

			int errorCount = 0;
			while (todayCases == null)
			{
				errorCount++;
				todayCases = _repository.GetCaseDayOverDayByDate(DateTime.Today.AddDays(-errorCount));
				todayVax = _repository.GetVaccineDayOverDayByDate(DateTime.Today.AddDays(-errorCount));
			}

			var yesterdayCases = _repository.GetCaseDayOverDayByDate(todayCases.Today.Date.AddDays(-1));

			var deaths2020 = _repository.GetCaseDayDataByDate(new DateTime(2020, 12, 31)).Deaths;
			var deaths2021 = todayCases.Today.Deaths - deaths2020;

			decimal mort2020 = (deaths2020 / (decimal)StaticData.OntarioPopulation) * 100;
			decimal mort2021 = (deaths2021 / (decimal)StaticData.OntarioPopulation) * 100;

			return View( new SummaryModel { TodayCases = todayCases, YesterdayCases = yesterdayCases, Mortality2020 = mort2020, Mortality2021 = mort2021, TodayVax = todayVax});
		}

		public IActionResult RollingAverage()
		{
			return View(_repository.GetCasesRollingAverage().OrderByDescending( d => d.CurrentDay.Date));
		}

		public IActionResult NoteOnFluMortality()
		{
			return View("NoteOnFluMortality");
		}
	}
}
