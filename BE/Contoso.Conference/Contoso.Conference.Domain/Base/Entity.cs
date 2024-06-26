﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contoso.Conference.Domain.Base
{
    public abstract class Entity<TId>
    {
        public TId Id { get; set; }
        protected abstract void EnsureValidState();
    }   
}
