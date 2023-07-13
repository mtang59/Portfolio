using System;

namespace FenceWebApp.Models
{
    public class Customer
    {
        // each property corresponds to a column name in FenceAppDB Customers table
        public int Id { get; set; }
        public string firstName { get; set; } // question marks for getting rid of annoying error
        public string lastName { get; set; }
        public string phoneNumber { get; set; }
        public string emailAddress { get; set; }
        public string address { get; set; }
    }
}
