﻿using Aluno.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aluno.WebApi
{
    [Route("api/[controller]")]
    public class CarrinhoController : Controller
    {
        public void Post([FromBody]ItemPedido value)
        {

        }
    }
}
