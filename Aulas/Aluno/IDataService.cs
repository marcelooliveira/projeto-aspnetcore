﻿using Aluno.Models;
using System.Collections.Generic;

namespace Aluno
{
    public interface IDataService
    {
        void InicializaDB();
        List<Produto> GetProdutos();
    }
}