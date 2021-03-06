﻿using Aula146_SalesWebMvc.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Aula146_SalesWebMvc.Services
{
    public class DepartmentService
    {
        private readonly Aula146_SalesWebMvcContext _context;

        // construtor
        public DepartmentService(Aula146_SalesWebMvcContext context)
        {
            _context = context;
        }

        // listar todos os departamentos ordenados por departamento
        // implementacao sincrona
        //public List<Department> FindAll()
        //{
        //    return _context.Department.OrderBy(x => x.Name).ToList();
        //}

        // passamos para assincrona
        public async Task<List<Department>>FindAllAsync()
        {
            return await _context.Department.OrderBy(x => x.Name).ToListAsync();
        }

    }
}
