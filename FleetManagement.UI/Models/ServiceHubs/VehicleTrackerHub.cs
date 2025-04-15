using Microsoft.AspNetCore.SignalR;

namespace FleetManagement.UI.Models.ServiceHubs
{
    public class VehicleTrackerHub : Hub
    {
        // Broadcast updated position to all connected clients.
        public async Task UpdateVehiclePosition(string vehicleId, double latitude, double longitude)
        {
            await Clients.All.SendAsync("ReceivePositionUpdate", vehicleId, latitude, longitude);
        }
    }
}

