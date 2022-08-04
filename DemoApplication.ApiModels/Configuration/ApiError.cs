using Newtonsoft.Json;

namespace DemoApplication.ApiModels.Configuration
{
    /// <summary>
    /// Contains details of an error in Web Api operation
    /// </summary>
    public class ApiError
    {
        [JsonProperty(PropertyName ="statusCode")]
        public int StatusCode { get; set; }

        [JsonProperty(PropertyName = "errorType")]
        public string ErrorType { get; set; }

        [JsonProperty(PropertyName = "errorMessage")]
        public string ErrorMessage { get; set; }
    }
}
