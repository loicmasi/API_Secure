using OpenWeatherMap.API.Models;
using OpenWeatherMap.Tools;
namespace OpenWeatherMap.MsTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GetDataFromOWM()
        {
            // Assert
            string urlGetData = "https://api.openweathermap.org/data/2.5/weather?q=Dijon,FR&appid=83ed9732a24abaf84be956930973eeea";
            // Actual
            Call call = new Call();
            call.GetData<WeatherInfo>(urlGetData);
            // Arrange
            Assert.IsNotNull(call);
        }

        // 

        [TestMethod]
        public void GetDataFromFoot()
        {
            string url = "https://api.football-data.org/v2/competitions";
            Call result = new Call();
            result.GetData<FootCompetition>(url);
            Assert.IsNotNull(result);
        }
    }
}