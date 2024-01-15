using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReservationAPI.Models;
using ReservationAPI.Repository.IRepository;

namespace ReservationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationRepository _reservationRepository;
        public ReservationController(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }
        [HttpGet]
        public IEnumerable<Reservation> Get()
        {
            return  _reservationRepository.GetReservations();
        }
        [HttpPut("{id}")]
        public async Task Put(int id)
        {
            await _reservationRepository.UpdateMailStatus(id);
        }
    }
}
