using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NSE.Clientes.API.Models;
using NSE.Core.Data;
using NSE.Customers.API.Data;
using NSE.Customers.API.Models;

namespace NSE.Clientes.API.Data.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomersContext _context;

        public CustomerRepository(CustomersContext context)
        {
            _context = context;
        }
        
        public IUnitOfWork UnitOfWork => _context;

        public async Task<IEnumerable<Customer>> GetAll()
        {
            return await _context.Customers.AsNoTracking().ToListAsync();
        }
        
        public Task<Customer> GetByCpf(string cpf)
        {
            return _context.Customers.FirstOrDefaultAsync(c => c.Cpf.Numero == cpf);
        }
        
        public void Adicionar(Customer customer)
        {
            _context.Customers.Add(customer);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}