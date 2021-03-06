﻿using System;
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
           
            var name = new Name(clientDTO.FirstNome, clientDTO.LastNome );
            var email = new Email(clientDTO.Email);
            var client = Client.ClientBuilder(name, email);
            if (client.Errors.Count >0)
                return BadRequest(client.Errors.Select(c=>c.ErrorMessage));

            _clientService.Add(client);

            if (_clientService.GetErrors().Count > 0)
                return BadRequest(_clientService.GetErrors());

            return Ok();
        }

        [HttpPut]
        public IActionResult UpdateUser([FromBody] ClientDTO clientDTO)
        {

            var name = new Name(clientDTO.FirstNome, clientDTO.LastNome);
            var email = new Email(clientDTO.Email);
            var client = Client.ClientBuilder(name, email);

            if (client.Errors.Count > 0)
                return BadRequest(client.Errors.Select(c => c.ErrorMessage));

            _clientService.Update(client);

            if (_clientService.GetErrors().Count > 0)
                return BadRequest(_clientService.GetErrors());

            return Ok();
        }

        [HttpGet]
        public  IActionResult GetAllClient()
        {
            var allClients = _clientService.GetAll();
            return (ActionResult)(allClients.Count() != 0 ? Ok(allClients) : (IActionResult)BadRequest(allClients));
        }

        [HttpDelete]
        //[Route("Delete")]
        public IActionResult DeleteUser([FromBody] Guid idClient)
        {                      
            var client = Client.ClientBuilder(idClient);

            if (client.Errors.Count > 0)
                return BadRequest(client.Errors.Select(c => c.ErrorMessage));

            _clientService.Remove(client);

            if (_clientService.GetErrors().Count > 0)
                return BadRequest(_clientService.GetErrors());

            return Ok();
        }

    }
}
