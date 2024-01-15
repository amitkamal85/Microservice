using Azure.Messaging.ServiceBus;
using Confluent.Kafka;
using CustomerAPI.Models;
using CustomerAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CustomerAPI.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _context;
        private ProducerConfig _producerConfig;
        public CustomerRepository(ApplicationDbContext context, ProducerConfig producerConfig)
        {
            _context = context;
            _producerConfig = producerConfig;
        }
        public async Task AddCustomer(Customer customer)
        {
            var vehicleInDb = await _context.Vehicles.FirstOrDefaultAsync(v => v.Id == customer.VehicleId);
            if(vehicleInDb == null)
            {
                await _context.AddAsync(customer.Vehicle);
                await _context.SaveChangesAsync();
            }
            customer.Vehicle = null;
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
           

            // Kafka
            var jsonString = JsonConvert.SerializeObject(customer);
            using (var producer = new ProducerBuilder<Null,string>(_producerConfig).Build())
            {
                await producer.ProduceAsync("mySixthTopic", new Message<Null, string> { Value=jsonString });
                producer.Flush(TimeSpan.FromSeconds(10));
            }
        }
    }
}
