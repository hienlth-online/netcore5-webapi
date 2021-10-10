using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBooks.Data.Services;
using MyBooks.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBooks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private AuthorsService _authorsService;
        public AuthorsController(AuthorsService authorsService)
        {
            _authorsService = authorsService;
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] AuthorVM author)
        {
            _authorsService.AddAuthor(author);
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetAuthorWithBooks(int id)
        {
            var response = _authorsService.GetAuthorWithBooks(id);
            return Ok(response);
        }
    }
}
