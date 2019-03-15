using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq; 

namespace Aula146_SalesWebMvc.Models
{
    public class Seller
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name ="Birth Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Base Salary")]
        [DisplayFormat(DataFormatString ="{0:F2}")]
        public double BaseSalary { get; set; }

        // associando Seller com Department   ( 1 vendedor esta associado a um departamento )
        public Department Department { get; set; }
        public int DepartmentID { get; set; }

        // associando Seller com SalesRecord  ( 1 vendedor pode ter mais de uma venda )
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();


        // construtores
        public Seller() { }

        public Seller(int id, string name, string email, DateTime birthDate, double baseSalary, Department department)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            BaseSalary = baseSalary;
            Department = department;
        }


        // metodos
            // AddSales  recebe  um SalesRecord (sr) 
        public void AddSales ( SalesRecord sr)
        {
            Sales.Add(sr);
        }

        public void RemoveSales(SalesRecord sr)
        {
            Sales.Remove(sr);
        }

        public double TotalSales(DateTime initial ,DateTime final)
        {
            // retornar de Sales ( SalesRecord ) para o periodo a soma do campo Amount
            return Sales.Where(sr => sr.Date >= initial && sr.Date <= final).Sum(sr => sr.Amount);
        }
    }
}
