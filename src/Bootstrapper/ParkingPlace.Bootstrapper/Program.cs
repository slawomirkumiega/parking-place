using ParkingPlace.Modules.Clients.Api;
using ParkingPlace.Modules.ParkingSpaces.Api;
using ParkingPlace.Shared;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddClientsModule()
    .AddParkingSpacesModule()
    .AddSharedLayer(builder.Configuration);

var app = builder.Build();

app.UseSharedLayer();
app.UseClientsModule();
app.UseParkingSpacesModule();

//app.UseHttpsRedirection();
//app.UseAuthorization();
app.MapControllers();

app.Run();
