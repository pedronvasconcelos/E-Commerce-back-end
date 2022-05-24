using NSE.Core.DomainObjects;

namespace NSE.Customers.API.Models
{
    public class Address : Entity
    {
        public string Street { get; private set; }

        public string Number { get; private set; }

        public string Complement { get; private set; }

        public string District { get; private set; }

        public string City { get; private set; }

        public string State { get; private set; }

        public string ZipCode { get; private set; }

        public Guid CustomerId { get; private set; }
        public Customer Customer { get; private set; }
        public Address(string street, string number, string complement, string district, string city, string state,  string zipCode)
        {
            Street = street;
            Number = number;
            Complement = complement;
            District = district;
            City = city;
            State = state;
        
            ZipCode = zipCode;
        }

    }
}
