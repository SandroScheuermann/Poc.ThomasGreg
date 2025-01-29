using FluentAssertions;
using Moq;
using Poc.ThomasGreg.Application.DTOs;
using Poc.ThomasGreg.Application.Services;
using Poc.ThomasGreg.Application.Services.Interfaces;
using Poc.ThomasGreg.Domain.Entities;
using Poc.ThomasGreg.Domain.Interfaces;

namespace Poc.ThomasGreg.Test
{
	public class ClienteTests
	{
		private Mock<IClienteRepository> _clienteRepository;
		private IClienteService _clienteService;

		[SetUp]
		public void Setup()
		{
			_clienteRepository = new();

			_clienteRepository
				.Setup(x => x.CriarClienteAsync(It.IsAny<Cliente>()))
				.ReturnsAsync(1);

			_clienteService = new ClienteService(_clienteRepository.Object);
		}

		[Test]
		public async Task Deve_Criar_Cliente_Com_Sucesso()
		{
			var clienteDTO = new CadastrarClienteDTO
			{
				Nome = "Teste",
				Email = "teste@teste.com",
				Logotipo = [0x01, 0x02]
			};

			var resultado = await _clienteService.CriarClienteAsync(clienteDTO);

			resultado.Should().Be(1);

			_clienteRepository.Verify(x => x.CriarClienteAsync(It.Is<Cliente>(
				c => c.Nome == clienteDTO.Nome &&
					 c.Email == clienteDTO.Email &&
					 c.Logotipo == clienteDTO.Logotipo
			)), Times.Once);
		} 
	}
}