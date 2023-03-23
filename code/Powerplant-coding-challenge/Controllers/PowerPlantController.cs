﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Powerplant_coding_challenge.Model;
using Powerplant_coding_challenge.Service;
using System.Linq;

namespace Powerplant_coding_challenge.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PowerPlantController : Controller
    {

        [HttpPost(Name = "productionplan")]
        public List<PowerplantsResult> ProductionPlan(PowerplantsInput productionPlan)
        {
            return PowerplantLogic.Calculate(productionPlan);
        }


    }
}
