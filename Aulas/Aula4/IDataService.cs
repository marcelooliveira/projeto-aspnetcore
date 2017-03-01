using Aula.Models;
using System.Collections.Generic;

namespace Aula
{
    public interface IDataService
    {
        Contexto InicializaDB();
        List<Produto> GetProdutos();
    }
}