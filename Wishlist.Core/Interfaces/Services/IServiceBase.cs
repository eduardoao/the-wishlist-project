﻿using System;
using System.Collections.Generic;
using System.Text;
using Wishlist.Core.Models.ValueObject;

namespace Wishlist.Core.Interfaces.Services
{
    public interface IServiceBase<T> where T : class
    {
        ICollection<Error> GetErrors();

        void Add(T obj);

        T GetById(Guid id);

        IEnumerable<T> GetAll();

        void Update(T obj);

        void Remove(T obj);

        void Dispose();
    }
}
