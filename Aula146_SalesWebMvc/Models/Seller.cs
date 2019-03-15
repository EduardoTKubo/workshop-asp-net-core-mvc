using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq; 

namespace Aula146_SalesWebMvc.Models
{
    public class Seller
    {
        public int Id { get; set; }


        // Requered = torna o campo obrigatorio
        [Required(ErrorMessage ="{0} requerid")]
        // definindo tamanho max e min para o campo ,mensagem personalizada de erro
        [StringLength(60 ,MinimumLength = 3, ErrorMessage = "{0} size should be between {2} and {1}" )]
        public string Name { get; set; }                    // {0} chama o valor de Name / {1} 1º parametro de StringLength {2} 2º param


        [Required(ErrorMessage = "{0} requerid")]
        [EmailAddress(ErrorMessage = "Enter a valid email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} requerid")]
        [Display(Name ="Birth Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "{0} requerid")]
        [Range(100.0, 50000.0, ErrorMessage = "{0} must be from {1} to {2}")]
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
