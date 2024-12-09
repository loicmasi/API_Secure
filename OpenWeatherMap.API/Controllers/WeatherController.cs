using Microsoft.AspNetCore.Mvc;
using OpenWeatherMap.Tools;
using OpenWeatherMap.API.Models;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using static System.Net.WebRequestMethods;

namespace OpenWeatherMap.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        Call call;
        public WeatherController() 
        {
            call = new Call();
        }

        /// <summary>
        /// Méthode qui appel l'API OpenWeatherMap pour demander les infos 
        /// météorologique sur la ville de Dijon
        /// </summary>
        /// <returns>L'ensemble des infos météorologiques</returns>
        // GET: api/<ValuesController>
        [HttpGet]
        public async Task<ActionResult<FootCompetition>> GetData()
        {
            // Il s'agit de l'élément qui sera retourné : soit Ok() ou BadRequest()
            ActionResult result;
            try
            {
                // GetData est la méthode générique qui permet de
                // désérialiser le flux json reçu, on doit donc passer la
                // class correspondant au JSON reçu
                var datas = await call.GetData<WeatherInfo>("https://api.openweathermap.org/data/2.5/weather?q=Dijon,FR&appid=83ed9732a24abaf84be956930973eeea");
                // On retourne donc les données reçus si
                result = Ok(datas);
            }
            catch (Exception ex)
            {
                // Sinon on retourne l'erreur produite
                result = BadRequest(new { message = "Error: " + ex.Message });
            }
            return result;
        }
                                                 
        /// <summary>
        /// Cette méthode sert à récupérer la météo d'une ville entré par
        /// l'utilisateur
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Données météorologiques -> WeatherInfo</returns>
        // GET api/<ValuesController>/stringValue
        [HttpGet("{city}")]
        public async Task<ActionResult<Weather>> GetWeatherOfCity(string city)
        {
            // Il s'agit de l'élément qui sera retourné : soit Ok() ou BadRequest()
            ActionResult result;
            // Url ou l'on récupère les infos en insérant la ville souhaité
            string url = "https://api.openweathermap.org/data/2.5/weather?q=" + city + "&appid=83ed9732a24abaf84be956930973eeea";
            try
            {
                // GetData est la méthode générique qui permet de
                // désérialiser le flux json reçu, on doit donc passer la
                // class correspondant au JSON reçu
                var datas =  await call.GetData<WeatherInfo>(url);
                // On retourne donc les données reçus
                result = Ok(datas);
            }
            catch (Exception ex)
            {
                // Sinon on retourne l'erreur produite
                result = BadRequest(new { message = "Error: " + ex.Message });
            }
            return result;
        }
    }
}
