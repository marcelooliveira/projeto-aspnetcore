﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aluno.Models
{
    public abstract class BaseModel
    {
        public int Id { get; protected set; }
    }
}
