﻿using System;

namespace NSE.WebApp.MVC.Models
{
    public class ProductViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Available { get; set; }
        public decimal Price { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string Image { get; set; }
        public int QuantityStock { get; set; }
    }
}