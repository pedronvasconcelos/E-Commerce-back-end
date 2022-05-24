﻿using NSE.Core.DomainObjects;

namespace NSE.Catalog.API.Models
{
    public class Product : Entity, IAggregateRoot
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public bool Available { get; set; }

        public decimal Price { get; set; }

        public DateTime RegistrationDate { get; set; }

        public string Image { get; set; }

        public int QuantityStock { get; set; }

    }
}
