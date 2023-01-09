using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static NbaApi.ApiEntities.ImageEntities;

namespace NbaApi.Services
{
    public class ImageSearchService
    {
        public static async Task<string> GetImageUrl(string search)
        {
			var client = new HttpClient();
			var request = new HttpRequestMessage
			{
				Method = HttpMethod.Get,
				RequestUri = new Uri($"https://contextualwebsearch-websearch-v1.p.rapidapi.com/api/Search/ImageSearchAPI?q={search}&pageNumber=1&pageSize=10&autoCorrect=true"),
				Headers =
	{
		{ "X-RapidAPI-Key", "43a2ae82c1msh7a4d03beceb7bd1p120b52jsn71ffb4417d77" },
		{ "X-RapidAPI-Host", "contextualwebsearch-websearch-v1.p.rapidapi.com" },
	},
			};
			using (var response = await client.SendAsync(request))
			{
				response.EnsureSuccessStatusCode();
				var body = await response.Content.ReadAsStringAsync();

				var data = JsonConvert.DeserializeObject<Rootobject>(body);
				string link = data.value[0].url;

				return link;
			}
		}
    }
}
