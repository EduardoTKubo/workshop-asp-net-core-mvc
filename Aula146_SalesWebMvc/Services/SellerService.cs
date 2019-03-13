using Aula146_SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aula146_SalesWebMvc.Services
{
    public class SellerService
    {
        private readonly Aula146_SalesWebMvcContext _context;

        // construtor
        public SellerService (Aula146_SalesWebMvcContext context)
        {
            _context = context;
        }

        // listar todos os vendedores
        public List<Seller> FindAll()
        {
            return _context.Seller.ToList();
        }

        // metodo para inserir vendedor no bco de dados
        public void Insert ( Seller obj)
        {
            _context.Add(obj);
            _context.SaveChanges();
        }

        public Seller FindByID ( int id)
        {
            return _context.Seller.FirstOrDefault(obj => obj.Id == id);
        }

        public void Remove(int id)
        {
            var obj = _context.Seller.Find(id);
            _context.Seller.Remove(obj);
            _context.SaveChanges();
        }

    }
}
