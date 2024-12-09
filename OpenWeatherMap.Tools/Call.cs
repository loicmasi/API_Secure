using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace OpenWeatherMap.Tools
{
    public class Call
    {
        private string _url;
        public Call()
        {
            _url = "";
        }

        /// <summary>
        /// Cette méthode sert à récupérer la réponse HTTP de l'API
        /// appelé en chaine de caractère
        /// </summary>
        /// <param name="url">API appelé (OpenWeatherMap)</param>
        /// <returns>Chaine de caractère de l'API appelé</returns>
        public string GetDataFromApi(string url)
        {
            // initialisation de response
            var response = "";
            try
            {
                // On tente de récupérer les éléments de l'API
                // sous forme de string
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    response = client.GetStringAsync(url).Result;
                }
            }
            catch (Exception ex)
            {
                // Sinon on renvoie l'erreur produite
                response = "Error : " + ex.Message;
            }
            return response;
        }

        /// <summary>
        /// Cette méthode permet de retourner un flux json désérialiser.
        /// Etant donné qu'il s'agit d'une méthode générique, on doit lui
        /// donner la class correspondante pour désérialiser le flux.
        /// </summary>
        /// <typeparam name="T">Class qui permet de désérialiser</typeparam>
        /// <param name="url">Url du flux json reçu (ici OpenWeatherMap)</param>
        /// <returns>élément désérialisé</returns>
        /// <exception cref="Exception">Erreur de désérialisation</exception>
        public async Task<T> GetData<T>(string url) {
            // Récupération du flux à l'aide de la méthode GetDataFromApi
            string resultCall = GetDataFromApi(url);

            try
            {
                // Désérialisation du flux json
                var dataFromApi = JsonConvert.DeserializeObject<T>(resultCall);
                // Retourne le résultat
                return dataFromApi;
            }
            catch (JsonException ex)
            {
                // Sinon on lève une nouvelle exception
                throw new Exception($"Erreur de désérialisation : {ex.Message}");
            }
        }
    }
}
