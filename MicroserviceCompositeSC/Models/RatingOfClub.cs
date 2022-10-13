using System.Text.Json.Serialization;
namespace MicroserviceCompositeSC.Models
{
    public class RatingOfClub
    {
        [JsonPropertyName("clubName")]
        public string? ClubName { get; set; }
        [JsonPropertyName("averageRating")]
        public double? AverageRating { get; set; }
    }
}
