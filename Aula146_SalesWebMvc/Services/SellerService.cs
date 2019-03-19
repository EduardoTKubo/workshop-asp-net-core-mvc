using Aula146_SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;                 // Include
using Aula146_SalesWebMvc.Services.Exceptions;       // NotFoundException

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

        // listar todos os vendedores - forma assincrona
        public async Task<List<Seller>> FindAllAsync()
        {
            return await _context.Seller.ToListAsync();
        }

        // metodo para inserir vendedor no bco de dados
        public async Task InsertAsync (Seller obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<Seller> FindByIDAsync ( int id)
        {
            return await _context.Seller.Include(obj => obj.Department).FirstOrDefaultAsync(obj => obj.Id == id);
            // Include funciona como um join
        }

        public async Task RemoveAsync(int id)
        {
            try { 
                var obj = await _context.Seller.FindAsync(id);
                _context.Seller.Remove(obj);
                await _context.SaveChangesAsync();
            }
            // catch para capturar uma possivel DbUpdateException
            catch (DbUpdateException e) 
            {
                throw new IntegrityException(e.Message);
            }
        }

        public async Task UpdateAsync (Seller obj)
        {
            bool hasAny = await _context.Seller.AnyAsync(x => x.Id == obj.Id);
            if (!hasAny)   
            {
                // se não existir id
                throw new NotFoundException("Id not found");
            }

            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch( DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }

        }

    }
}
