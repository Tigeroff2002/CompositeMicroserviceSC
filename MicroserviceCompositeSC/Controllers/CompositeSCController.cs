using Microsoft.AspNetCore.Mvc;
using MicroserviceCompositeSC.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
namespace MicroserviceCompositeSC.Controllers
{
    [ApiController] 
	[Route("api/[controller]")] 
    public class CompositeSCController : ControllerBase
    {
        [HttpGet]
        public string Start()
        {
            return "Composite Microservice is running!";
        }
        private readonly string? _sportsmanServiceAdress = "https://localhost:7082/api/sportsmen";
        private readonly string? _organizationServiceAdress = "https://localhost:7163/api/organizations";
        private JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };
        [HttpGet("organizations/{championshipName}")]
        public async Task<List<Organization>> GetOrganizationsByChampionshipAsync(string championshipName)
        {
            var clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert,
                chain, sslPolicyErrors) =>
                    { return true; };
            using (var client = new HttpClient(clientHandler))
            {
                HttpResponseMessage response = await client.GetAsync($"{_organizationServiceAdress}");
                if (response.IsSuccessStatusCode)
                {
                    List<Organization>? organizations = await JsonSerializer.DeserializeAsync<List<Organization>>(
                        await response.Content.ReadAsStreamAsync(), options);
                    return organizations!.Where(org => org.Championships!.Split(',').Contains(championshipName))
                                         .ToList();
                }
            }
            return null!;
        }

        [HttpGet("sportsmen/{clubName}")]
        public async Task<List<SportsMan>> GetSportsMenByClubAsync(string clubName)
        {
            var clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert,
                chain, sslPolicyErrors) =>
            { return true; };
            using (var client = new HttpClient(clientHandler))
            {
                HttpResponseMessage response = await client.GetAsync($"{_sportsmanServiceAdress}");
                if (response.IsSuccessStatusCode)
                {
                    List<SportsMan>? sportsmen = await JsonSerializer.DeserializeAsync<List<SportsMan>>(
                        await response.Content.ReadAsStreamAsync(), options);
                    return sportsmen!.Where(man => man.ClubName! == clubName)
                                        .ToList();
                }
            }
            return null!;
        }

        [HttpGet("rating/{clubName}")]
        public async Task<RatingOfClub> GetAverageClubRating(string clubName)
        {
            var clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert,
                chain, sslPolicyErrors) =>
            { return true; };
            using (var client = new HttpClient(clientHandler))
            {
                HttpResponseMessage response = await client.GetAsync($"{_sportsmanServiceAdress}");
                if (response.IsSuccessStatusCode)
                {
                    List<SportsMan>? sportsmen = await JsonSerializer.DeserializeAsync<List<SportsMan>>(
                        await response.Content.ReadAsStreamAsync(), options);
                    var collection = sportsmen!.Where(man => man.ClubName! == clubName)
                                               .ToList();
                    int ratingSum = 0;
                    foreach(var i in collection)
                        ratingSum += i.Rating;
                    return new RatingOfClub()
                    {
                        ClubName = clubName,
                        AverageRating = (double)ratingSum / collection.Count
                    };
                }
            }
            return null!;
        }
    }
}
