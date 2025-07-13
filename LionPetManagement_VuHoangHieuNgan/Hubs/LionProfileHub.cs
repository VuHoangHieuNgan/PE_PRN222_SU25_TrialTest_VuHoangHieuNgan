using Microsoft.AspNetCore.SignalR;
using Services;

namespace LionPetManagement_VuHoangHieuNgan.Hubs
{
    public class LionProfileHub : Hub
    {
        private readonly LionProfileService _lionProfileService;

        public LionProfileHub(LionProfileService lionProfileService)
        {
            _lionProfileService = lionProfileService;
        }

        public async Task HubDelete_LionProfile(string lionProfileId)
        {
            await Clients.All.SendAsync("ReceiveDelete_LionProfile", lionProfileId);
            await _lionProfileService.DeleteAsync(int.Parse(lionProfileId));
        }
    }
}
