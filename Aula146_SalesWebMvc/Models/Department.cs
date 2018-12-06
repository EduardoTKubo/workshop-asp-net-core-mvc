using System;
using System.Collections.Generic;
using System.Linq;

namespace Aula146_SalesWebMvc.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // associando Department com Seller  , um departamento pode ter varios vendedores
        public ICollection<Seller> Sellers { get; set; } = new List<Seller>(); 


        // construtores
        public Department() { }

        public Department(int id, string name)
        {
            Id = id;
            Name = name;
        }

        // metodos
        public void AddSeller (Seller seller)
        {
            Sellers.Add(seller);
        }

        public double TotalSales( DateTime initial ,DateTime final)
        {
            //              soma  ( para cada vendedor : seller vou aplicar ... TotalSales ( inicial,final) )
            return Sellers.Sum(seller => seller.TotalSales(initial, final)); 
        }
    }
}
