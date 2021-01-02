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
			var today = _repository.GetDayOverDayByDate(DateTime.Today);
			
			while (today == null)
			{
				today = _repository.GetDayOverDayByDate(DateTime.Today.AddDays(-1));
			}

			var yesterday = _repository.GetDayOverDayByDate(today.Today.Date.AddDays(-1));

			return View( new SummaryModel { Today = today, Yesterday = yesterday});
		}

		public IActionResult RollingAverage()
		{
			return View(_repository.GetRollingAverage().OrderByDescending( d => d.CurrentDay.Date));
		}

		public IActionResult NoteOnFluMortality()
		{
			return View("NoteOnFluMortality");
		}
	}
}
