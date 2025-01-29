﻿namespace Poc.ThomasGreg.Domain.Entities
{
    public class Usuario
    {
        public Guid Id { get; set; } 
        public required string Nome { get; set; }
        public required string Email { get; set; } 
        public required string SenhaHash { get; set; }  
    } 
}
