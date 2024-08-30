using ChurchStore.Web.Api;

namespace ChurchStore.Web.Services
{
    public interface IApiService
    {
        Task<ApiResponse> GetAsync(ApiRequest request);

        Task<ApiResponse> PostAsync(ApiRequest request);

        Task<ApiResponse> PostFileAsync(ApiRequest request);
    }
}