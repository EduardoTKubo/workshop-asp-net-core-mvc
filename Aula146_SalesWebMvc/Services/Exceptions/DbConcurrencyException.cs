using System;

namespace Aula146_SalesWebMvc.Services.Exceptions
{
    public class DbConcurrencyException : ApplicationException 
    {
        // construtor basico
        public DbConcurrencyException(string message) : base(message)
        {
        }
    }
}
