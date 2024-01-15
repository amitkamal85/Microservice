using VehicleAPI.Models;

namespace VehicleAPI.Repository.IRepository
{
    public interface IVehicleRepository
    {
        Task<List<Vehicle>> GetVehicles();
        Task<Vehicle> GetVehicleById(int id);
        Task AddVehicle(Vehicle vehicle);
        Task UpdateVehicle(int id,Vehicle vehicle);
        Task DeleteVehicle(int id);
    }
}
