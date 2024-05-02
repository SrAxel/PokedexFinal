using AppPeliculas.Modelos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPeliculas.Repositories
{
    public class RepositoryPokemon
    {
        string urlApi = "https://pokemons-bcbb.restdb.io/rest/pokemons";
        HttpClient client = new HttpClient();

        public RepositoryPokemon()
        {
            //configuramos que trabajará con respuestas JSON
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("apikey", "663050a98088113db9761e04");
        }

        public async Task<ObservableCollection<Pokemon>> GetAllAsync()
        {
            try
            {
                var response = await client.GetStringAsync(urlApi);
                return JsonConvert.DeserializeObject<ObservableCollection<Pokemon>>(response);
            }
            catch (Exception error)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Hubo un error:" + error.Message, "Ok");
                return null;
            }
        }
        public async Task<bool> RemoveAsync(string id)
        {
            var response = await client.DeleteAsync($"{urlApi}/{id}");
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> AddAsync(Pokemon pokemon)
        {
            var response = await client.PostAsync(urlApi,
                new StringContent(JsonConvert.SerializeObject(pokemon), Encoding.UTF8, "application/json"));
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(Pokemon pokemon)
        {
            var response = await client.PutAsync($"{urlApi}/{pokemon._id}",
                new StringContent(JsonConvert.SerializeObject(pokemon), Encoding.UTF8, "application/json"));
            return response.IsSuccessStatusCode;
        }
    }
}
