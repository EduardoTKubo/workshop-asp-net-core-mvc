using System;

namespace Aula146_SalesWebMvc.Services.Exceptions
{
    // exceção personalizada de serviço para erros de integridade referencial : IntegrityException  ,que herda de ApplicationException
    public class IntegrityException : ApplicationException
    {
        // construtor basico para a exceção personalizada
        // repassar a string message para a super-classe : base
        public IntegrityException (string message) : base(message)
        {
        }
    }
}
