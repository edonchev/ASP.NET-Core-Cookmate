﻿namespace Cookmate.Controllers.Api
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Cookmate.Services.Statistics;

    [ApiController]
    [Route("api/statistics")]
    public class StatisticsApiController : ControllerBase
    {
        private readonly IStatisticsService statistics;

        public StatisticsApiController(IStatisticsService statistics)
            => this.statistics = statistics;

        [HttpGet]
        public StatisticsServiceModel GetStatistics()
            => this.statistics.Get();
    }
}
