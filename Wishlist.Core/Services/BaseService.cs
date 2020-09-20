using System;
using System.Collections.Generic;
using System.Text;
using Wishlist.Core.Interfaces.Repositorys;
using Wishlist.Core.Models.ValueObject;

namespace Wishlist.Core.Services
{
    public abstract class BaseService<T> where T: class
    {
        private readonly IRepositoryBase<T> _repository;

        public BaseService(IRepositoryBase<T> Repository)
        {
            _repository = Repository;
            Errors = new List<Error>();
        }
        public virtual void Add(T obj)
        {
            _repository.Add(obj);
        }
        public virtual T GetById(Guid id)
        {
            return _repository.GetById(id);
        }
        public virtual IEnumerable<T> GetAll()
        {
            return _repository.GetAll();
        }
        public virtual void Update(T obj)
        {
            _repository.Update(obj);
        }
        public virtual void Remove(T obj)
        {
            _repository.Remove(obj);
        }

        public virtual void Dispose()
        {
            _repository.Dispose();
        }

        protected ICollection<Error> Errors { get; set; }
        public BaseService()
        {
            Errors = new List<Error>();
        }

        public virtual ICollection<Error> GetErrors()
        {
            return Errors;
        }

    }
}
