using ReservationAPI.Models;

namespace ReservationAPI.Repository.IRepository
{
    public interface IReservationRepository
    {
        List<Reservation> GetReservations();
        Task UpdateMailStatus(int id);
    }
}
