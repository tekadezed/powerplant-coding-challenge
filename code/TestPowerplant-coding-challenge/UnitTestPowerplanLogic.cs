using Newtonsoft.Json;
using Powerplant_coding_challenge.Controllers;
using Powerplant_coding_challenge.Model;
using Powerplant_coding_challenge.Service;
using System.Threading.Channels;

namespace TestPowerplant_coding_challenge
{
    public class Constances
    {
        static public string payloadSample = @"
{
  ""load"": 910,
  ""fuels"":
  {
    ""gas(euro/MWh)"": 13.4,
    ""kerosine(euro/MWh)"": 50.8,
    ""co2(euro/ton)"": 20,
    ""wind(%)"": 60
  },
  ""powerplants"": [
    {
      ""name"": ""gasfiredbig1"",
      ""type"": ""gasfired"",
      ""efficiency"": 0.53,
      ""pmin"": 100,
      ""pmax"": 460
    },
    {
      ""name"": ""gasfiredbig2"",
      ""type"": ""gasfired"",
      ""efficiency"": 0.53,
      ""pmin"": 100,
      ""pmax"": 460
    },
    {
      ""name"": ""gasfiredsomewhatsmaller"",
      ""type"": ""gasfired"",
      ""efficiency"": 0.37,
      ""pmin"": 40,
      ""pmax"": 210
    },
    {
      ""name"": ""tj1"",
      ""type"": ""turbojet"",
      ""efficiency"": 0.3,
      ""pmin"": 0,
      ""pmax"": 16
    },
    {
      ""name"": ""windpark1"",
      ""type"": ""windturbine"",
      ""efficiency"": 1,
      ""pmin"": 0,
      ""pmax"": 150
    },
    {
      ""name"": ""windpark2"",
      ""type"": ""windturbine"",
      ""efficiency"": 1,
      ""pmin"": 0,
      ""pmax"": 36
    }
  ]
}
";
    }

    public class UnitTest1
    {
        //For debuging
        [Fact]
        public void Test1()
        {
            PowerplantsInput ppInput = JsonConvert.DeserializeObject<PowerplantsInput>(Constances.payloadSample);
            List<PowerplantsResult> results = PowerplantLogic.Calculate(ppInput);

            results.ForEach(p => Console.WriteLine(p));
        }

        [Fact]
        public static void TestPowerplantCalculationBehaviorIsCorrect() 
        {
            int load = 500;
            Powerplant powerplant = new()
            {
                Efficiency = 1,
                Name = "Test",
                Type = "gasfired",
                Pmax = 200,
                Pmin = 100
            };

            var resultGreaterLoad = PowerplantLogic.CalculatePowerplan(500, powerplant, new List<Powerplant>() { powerplant });
            var resultSmallerLoad = PowerplantLogic.CalculatePowerplan(90, powerplant, new List<Powerplant>() { powerplant });
            var resultBetweenLoad = PowerplantLogic.CalculatePowerplan(150, powerplant, new List<Powerplant>() { powerplant });

            Assert.Equal(300, resultGreaterLoad.loadLeft);
            Assert.Equal(0  , resultBetweenLoad.loadLeft);
            Assert.Equal(0  , resultSmallerLoad.loadLeft);


        }
    }
}