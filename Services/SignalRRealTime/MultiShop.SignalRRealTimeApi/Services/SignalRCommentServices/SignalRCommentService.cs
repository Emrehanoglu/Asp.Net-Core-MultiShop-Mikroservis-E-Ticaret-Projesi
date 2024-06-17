namespace MultiShop.SignalRRealTimeApi.Services.SignalRCommentServices
{
    public class SignalRCommentService
    {
        private readonly HttpClient _httpClient;
        public SignalRCommentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<int> GetTotalCommentCount()
        {
            var responseMessage = await _httpClient.GetAsync("comments/GetTotalCommentCount");
            var value = await responseMessage.Content.ReadFromJsonAsync<int>();
            return value;
        }
    }
}
