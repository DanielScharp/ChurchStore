using ChurchStore.Web.Api;
using ChurchStore.Web.Models;
using Newtonsoft.Json;
using System.Net.Mime;
using System.Net;
using System.Text;

namespace ChurchStore.Web.Services
{
    public class ApiService : IApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public ApiService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }


        //Retornar uma requisição
        public async Task<ApiResponse> GetAsync(ApiRequest request)
        {
            try
            {
                //var endpoint = _configuration["SITE_Api_Uri"];
                var endpoint = _configuration.GetValue<string>("ApiUri"); //Lendo do arquivo de configuração

                var parameters = string.Join("&", request.Content.Select(x => $"{x.Key}={x.Value}"));
                var token = ClienteSessao.Logado.Token;

                var httpClient = _httpClientFactory.CreateClient();


                //httpClient.Timeout = TimeSpan.FromMilliseconds(5000);

                if (!string.IsNullOrEmpty(token))
                    httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                var response = await httpClient.GetAsync(endpoint + "/" + request.RouteValue + "?" + parameters).ConfigureAwait(false);

                var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                if (response.StatusCode != HttpStatusCode.Unauthorized && response.StatusCode != HttpStatusCode.InternalServerError)
                {
                    var content = JsonConvert.DeserializeObject<ApiResponse>(responseContent);
                    return content;
                }

                return new ApiResponse { Success = false, Message = "Erro ao tentar executar a operação: " + (responseContent != "" ? responseContent : "Erro interno") };


            }
            catch (Exception ex)
            {
                return new ApiResponse { Success = false, Message = "Erro ao tentar executar a operação: " + ex.Message };
            }

        }


        //Envia uma requisição
        public async Task<ApiResponse> PostAsync(ApiRequest request)
        {

            try
            {
                //var endpoint = _configuration["SITE_Api_Uri"];
                var endpoint = _configuration.GetValue<string>("ApiUri"); //Lendo do arquivo de configuração

                var model = new StringContent(JsonConvert.SerializeObject(request.Content), Encoding.UTF8, MediaTypeNames.Application.Json);
                var token = ClienteSessao.Logado.Token;

                var httpClient = _httpClientFactory.CreateClient();
                //httpClient.Timeout = TimeSpan.FromMilliseconds(5000);

                if (!string.IsNullOrEmpty(token))
                    httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                var response = await httpClient.PostAsync(endpoint + "/" + request.RouteValue, model).ConfigureAwait(false);

                var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                if (response.StatusCode != HttpStatusCode.Unauthorized && response.StatusCode != HttpStatusCode.InternalServerError)
                {
                    var content = JsonConvert.DeserializeObject<ApiResponse>(responseContent);
                    return content;
                }

                return new ApiResponse { Success = false, Message = "Erro ao tentar executar a operação: " + (responseContent != "" ? responseContent : "Erro interno") };


            }
            catch (Exception ex)
            {
                return new ApiResponse { Success = false, Message = "Erro ao tentar executar a operação: " + ex.Message };
            }


        }

        //Envia uma requisição
        public async Task<ApiResponse> PostFileAsync(ApiRequest request)
        {
            try
            {
                var endpoint = _configuration["SITE_Api_Uri"];
                var token = ClienteSessao.Logado.Token;

                var httpClient = _httpClientFactory.CreateClient();
                var multipartContent = new MultipartFormDataContent();

                if (!string.IsNullOrEmpty(token))
                    httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                // Adiciona os parâmetros do dicionário ao multipartContent
                foreach (var param in request.Content)
                {
                    multipartContent.Add(new StringContent(param.Value), param.Key);
                }

                // Adiciona os arquivos ao multipartContent
                foreach (var file in request.Files)
                {
                    var streamContent = new StreamContent(file.OpenReadStream());
                    multipartContent.Add(streamContent, "files", file.FileName);
                }


                var response = await httpClient.PostAsync(endpoint + "/" + request.RouteValue, multipartContent).ConfigureAwait(false);

                var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                if (response.StatusCode != HttpStatusCode.Unauthorized && response.StatusCode != HttpStatusCode.InternalServerError)
                {
                    var content = JsonConvert.DeserializeObject<ApiResponse>(responseContent);
                    return content;
                }

                return new ApiResponse { Success = false, Message = "Erro ao tentar executar a operação: " + (responseContent != "" ? responseContent : "Erro interno") };


            }
            catch (Exception ex)
            {
                return new ApiResponse { Success = false, Message = "Erro ao tentar executar a operação: " + ex.Message };
            }
        }
    }
}

