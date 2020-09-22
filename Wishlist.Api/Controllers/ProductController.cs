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
    public class ProductController : Controller
    {      
        private readonly IProductService _productService;
        private readonly IMapper _mapper;


        public ProductController(IProductService productService, IMapper mapper)
        {

            _productService = productService;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult CreateProduct([FromBody] ProductDTO productDTO)
        {
            var title = new Title(productDTO.Title);
            var picture = new Picture(productDTO.Picture);

            var product = Product.ProductBuilder(title, picture, productDTO.Price, productDTO.Brand);
            if (product.Errors.Count >0)
                return BadRequest(product.Errors.Select(c=>c.ErrorMessage));

            _productService.Add(product);

            if (_productService.GetErrors().Count > 0)
                return BadRequest(_productService.GetErrors());

            return Ok();
        }

    }
}
