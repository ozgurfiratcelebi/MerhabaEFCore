using Data;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace ConsoleApp
{
    class Program
    {
        private static MyDbContext _context = new MyDbContext();
        static void Main(string[] args)
        {
            _context.Database.EnsureCreated();

            Console.WriteLine("İsminiz");
            string customerName = Console.ReadLine();


            var customer = new Customer { Name = customerName };
            _context.Customer.Add(customer);
            _context.SaveChanges();

            var products = _context.Product.ToList();
            Console.WriteLine($"Sistemde {products.Count()} adet ürün var. Satın Almak ister misiniz? E/H");
            string customerAnswer = Console.ReadLine();


            if (customerAnswer == "E")
            {
                Console.WriteLine("Hangi ürünü almak istersiniz? Almak istediğiniz ürün numarasını giriniz.");
                foreach (var product in products)
                {
                    Console.WriteLine($"{product.Id}. ürün {product.Name} ");
                }
                string productNumber = Console.ReadLine();
                int number;
                Int32.TryParse(productNumber, out number);
                Product selectionProduct = _context.Product.Where(p => p.Id == number).FirstOrDefault();


                _context.CustomerProduct.Add(new CustomerProduct { CustomerId = customer.Id, ProductId = selectionProduct.Id });

                _context.SaveChanges();
                Console.WriteLine();
            }
        }



    }
}
