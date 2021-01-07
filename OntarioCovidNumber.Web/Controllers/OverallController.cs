﻿using Microsoft.AspNetCore.Mvc;
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

			int errorCount = 0;
			while (today == null)
			{
				errorCount++;
				today = _repository.GetDayOverDayByDate(DateTime.Today.AddDays(-errorCount));
			}

			var yesterday = _repository.GetDayOverDayByDate(today.Today.Date.AddDays(-1));

			var deaths2020 = _repository.GetDayDataByDate(new DateTime(2020, 12, 31)).Deaths;
			var deaths2021 = today.Today.Deaths - deaths2020;

			decimal mort2020 = (deaths2020 / (decimal) CovidDayData.OntarioPopulation) * 100;
			decimal mort2021 = (deaths2021 / (decimal)CovidDayData.OntarioPopulation) * 100;

			return View( new SummaryModel { Today = today, Yesterday = yesterday, Mortality2020 = mort2020, Mortality2021 = mort2021});
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
