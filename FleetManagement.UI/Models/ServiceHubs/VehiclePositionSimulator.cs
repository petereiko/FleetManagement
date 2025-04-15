using Microsoft.AspNetCore.SignalR;

namespace FleetManagement.UI.Models.ServiceHubs
{
    public class VehiclePositionSimulator : BackgroundService
    {
        private readonly IHubContext<VehicleTrackerHub> _hubContext;
        private Random _random = new Random();

        public VehiclePositionSimulator(IHubContext<VehicleTrackerHub> hubContext)
        {
            _hubContext = hubContext;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // For simulation, assume a specific vehicle with a starting position
            string vehicleId = "V1";
            double lat = 6.5244;
            double lng = 3.3792;

            while (!stoppingToken.IsCancellationRequested)
            {
                // Simulate movement: add a small random offset
                lat += (_random.NextDouble() - 0.5) * 0.001;
                lng += (_random.NextDouble() - 0.5) * 0.001;

                // Broadcast new position through the hub
                await _hubContext.Clients.All.SendAsync("ReceivePositionUpdate", vehicleId, lat, lng, cancellationToken: stoppingToken);

                // Wait for a period (e.g., 2 seconds) before the next update
                await Task.Delay(2000, stoppingToken);
            }
        }
    }
}
