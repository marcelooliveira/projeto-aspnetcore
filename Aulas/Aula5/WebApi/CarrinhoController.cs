using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Aula.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Aula.WebApi
{
    [Route("api/[controller]")]
    public class CarrinhoController : Controller
    {
        [HttpPost]
        [ResponseCache(NoStore = true)]
        public void Post([FromBody]ItemPedido value)
        {

        }
    }
}
