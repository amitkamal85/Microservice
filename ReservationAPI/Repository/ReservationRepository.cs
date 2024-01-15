using Azure.Messaging.ServiceBus;
using Confluent.Kafka;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ReservationAPI.Models;
using ReservationAPI.Repository.IRepository;
using System.Net;
using System.Net.Mail;
using System.Reflection.Metadata.Ecma335;

namespace ReservationAPI.Repository
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly ApplicationDbContext _context;
        public ReservationRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<Reservation> GetReservations()
        {
           
            // Kafka
            try
            {
                var config = new ConsumerConfig
                {
                    GroupId = "gid-consumers",
                    BootstrapServers = "localhost:9092",
                    AutoOffsetReset = AutoOffsetReset.Earliest // Add this line to start consuming from the beginning of the topic
                };
                using (var consumer = new ConsumerBuilder<Null, string>(config).Build())
                {
                    consumer.Subscribe("mySixthTopic");

                    while (true) // Keep consuming messages
                    {
                    var cr = consumer.Consume(TimeSpan.FromSeconds(10));
                    if (cr == null)
                    {
                        // If there are no more messages, break the loop
                        break;
                    }
                    else
                    {
                        var receivedMessage = cr.Message.Value;
                        var messageCreated = JsonConvert.DeserializeObject<Reservation>(receivedMessage);

                        _context.Reservations.Add(messageCreated);
                        _context.SaveChanges();
                    }

                    }

                }

                return _context.Reservations.ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
            

        }

        public async Task UpdateMailStatus(int id)
        {
            var reservationInDb = await _context.Reservations.FindAsync(id);
            if (reservationInDb != null && reservationInDb.IsMailSent == false)
            {
                var smtpClient = new SmtpClient("smtp.mail.outlook.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("", ""),
                    EnableSsl = true
                };
                smtpClient.Send("", reservationInDb.Email, "", "");
                await _context.SaveChangesAsync();
            }
        }
    }
}
