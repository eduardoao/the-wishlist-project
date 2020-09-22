using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Wishlist.Api.DTO;
using Wishlist.Core.Interfaces.Repositorys;
using Wishlist.Core.Interfaces.Services;
using Wishlist.Core.Models;
using Wishlist.Core.Models.ValueObject;
using Wishlist.Core.Services;

namespace Wishlist.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController : Controller
    {      
        private readonly IClientService _clientService;
        private readonly IMapper _mapper;


        public ClientController( IClientService clientService, IMapper mapper)
        {
            
            _clientService = clientService;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] ClientDTO clientDTO)
        {
           
            var name = new Name(clientDTO.Nome, clientDTO.SobreNome );
            var email = new Email(clientDTO.Email);
            var client = Client.ClientBuilder(name, email);
            if (client.Errors.Count >0)
                return BadRequest(client.Errors.Select(c=>c.ErrorMessage));

            _clientService.Add(client);

            if (_clientService.GetErrors().Count > 0)
                return BadRequest(_clientService.GetErrors());

            return Ok();
        }

    }
}
