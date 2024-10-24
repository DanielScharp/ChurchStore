namespace ChurchStore.WebAdmin.Api
{
    public class ApiRequest
    {
        public string RouteValue { get; set; }
        public Dictionary<string, string> Content { get; set; }

        public List<IFormFile> Files { get; set; }
    }
}
