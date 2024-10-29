using Microsoft.AspNetCore.SignalR;

namespace ChurchStore.WebAdmin.Hubs
{
    public class PedidoHub : Hub
    {
        public async Task EnviarNovaReserva(string reservaId)
        {
            await Clients.All.SendAsync("ReceberReserva", reservaId);
        }
    }
}
