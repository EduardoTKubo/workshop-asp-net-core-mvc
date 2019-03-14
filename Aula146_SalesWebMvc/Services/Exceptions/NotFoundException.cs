using System;

namespace Aula146_SalesWebMvc.Services.Exceptions
{
    public class NotFoundException : ApplicationException 
    {
        // construtor basico
        public NotFoundException(string message) : base(message)
        {
        }
    }
}
