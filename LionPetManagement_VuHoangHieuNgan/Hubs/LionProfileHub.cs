using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Repositories.Models;
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

        public async Task HubUpdate_LionProfile(string jsonString)
        {
            var item = JsonConvert.DeserializeObject<LionProfile>(jsonString);
            if (item == null)
            {
                return;
            }
            await _lionProfileService.UpdateAsync(item);

            var obj = await _lionProfileService.GetByIdAsync(item.LionProfileId);
            if (obj != null)
            {
                var result = new
                {
                    obj.LionProfileId,
                    obj.LionName,
                    obj.Weight,
                    obj.Warning,
                    obj.LionTypeId,
                    obj.Characteristics,
                    obj.ModifiedDate,
                    obj.LionType.LionTypeName
                };

                await Clients.All.SendAsync("ReceiveUpdate_LionProfile", result);
            }
        }

        public async Task HubCreate_LionProfile(string jsonString)
        {
            var item = JsonConvert.DeserializeObject<LionProfile>(jsonString);
            if (item == null)
            {
                return;
            }
            await _lionProfileService.CreateAsync(item);

            var obj = await _lionProfileService.GetByIdAsync(item.LionProfileId);
            if(obj != null)
            {
                var result = new
                {
                    obj.LionProfileId,
                    obj.LionName,
                    obj.Weight,
                    obj.Warning,
                    obj.LionTypeId,
                    obj.Characteristics,
                    obj.ModifiedDate,
                    obj.LionType.LionTypeName
                };

                await Clients.All.SendAsync("ReceiveCreate_LionProfile", result);
            }
        }
    }
}
