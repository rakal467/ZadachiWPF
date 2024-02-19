using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using ZadachiWPF.Api.DTO;

namespace ZadachiWPF
{
   public class Client
    {
        HttpClient httpClient = new HttpClient();

        public Client()
        {
            httpClient.BaseAddress = new Uri(@"https://localhost:7174/api/");

        }
        //static Client Instance = new();
        //public static Client Instsans { get => Instance; }

        public async Task<List<ZadachiDTO>> GetZadachis()
        {
            try
            {
                var responce = await httpClient.GetAsync("Zadachi");
                if (responce.IsSuccessStatusCode)
                {
                    var content = await responce.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<ZadachiDTO>>(content);

                }
                else
                {
                    throw new Exception($"Error: {responce.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<List<StatusDTO>> GetStatus()
        {
            try
            {
                var responce = await httpClient.GetAsync("Status");
                if (responce.IsSuccessStatusCode)
                {
                    var content = await responce.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<StatusDTO>>(content);

                }
                else
                {
                    throw new Exception($"Error: {responce.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
            public async Task<ZadachiDTO> EditZadachi(ZadachiDTO zadachi, int idIdzadachi)
        {
                using StringContent jsonContent = new(
                       System.Text.Json.JsonSerializer.Serialize(zadachi),
                       Encoding.UTF8,
                       "application/json");
                using HttpResponseMessage response = await httpClient.PutAsync("Zadachi/" + zadachi.Idzadachi, jsonContent);
                response.EnsureSuccessStatusCode();
                // MessageBox.Show(response.StatusCode.ToString());
                return zadachi;
         }

        public async Task DeleteZadachi(int id)
        {
            using HttpResponseMessage response = await httpClient.DeleteAsync("Zadachi/" + id);
            response.EnsureSuccessStatusCode();

        }

        public async Task AddZadachi1(ZadachiDTO zadachi)
        {
            using (var client = new HttpClient())
            {
                var jsonContent = JsonConvert.SerializeObject(zadachi);
                var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.PostAsync("Zadachi", httpContent);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Не удалось добавить задачу.");
                }
            }
        }
        static Client instance = new();
        public static Client Instance
        {
            get
            {
                if (instance == null)
                    instance = new Client();
                return instance;
            }
        }
    }
}