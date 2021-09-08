using System;
using System.Collections.Generic;

namespace Domain
{
    public class Product
    {
        public Product()
        {
            CustomerProducts = new HashSet<CustomerProduct>();
        }
        public int  Id { get; set; }
        public string Name { get; set; } 
        public ICollection<CustomerProduct> CustomerProducts { get; set; }
    }
}
