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
        private readonly IDataService _dataService;
        public CarrinhoController(IDataService dataService)
        {
            this._dataService = dataService;
        }

        [HttpPost]
        [ResponseCache(NoStore = true)]
        public ItemPedido Post([FromBody]ItemPedido itemPedido)
        {
            if (itemPedido.Quantidade > 0)
            {
                this._dataService.UpdateItemPedido(itemPedido);
                return this._dataService.GetItemPedido(itemPedido.Id);
            }
            else
            {
                this._dataService.DeleteItemPedido(itemPedido.Id);
                return itemPedido;
            }
        }
    }
}
