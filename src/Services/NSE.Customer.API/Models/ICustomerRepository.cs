using System.Collections.Generic;
using System.Threading.Tasks;
using NSE.Core.Data;
using NSE.Customers.API.Models;

namespace NSE.Clientes.API.Models
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        void Add(Customer cliente);

        Task<IEnumerable<Customer>> GetAll();
        Task<Customer> GetByCpf(string cpf);
    }
}