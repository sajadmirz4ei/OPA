using System.Text.Json.Serialization;

namespace OpaAuth.Models
{
    public class OpaResponse
    {
        [JsonPropertyName("result")]
        public bool Result { get; set; }
    }

}
