using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using Wishlist.Core.Interfaces.Repositorys;
using Wishlist.Core.Models;
using Dapper;
using System.Data;

namespace Wishlist.Data
{    
    public class RepositoryWishClient : IRepositoryWishClient
    {
        //Todo fazer Ioc camada
        private readonly IConfiguration _configu;        
        private readonly string connectionString; 

        public RepositoryWishClient(IConfiguration config)
        {
            _configu = config;
            connectionString = _configu.GetConnectionString("DBConnection");
        }

        internal IDbConnection Connection
        {
            get
            {
                return new NpgsqlConnection(connectionString);
            }
        }

        public void Add(WishClient obj)
        {
            using IDbConnection dbConnection = Connection;
            dbConnection.Open();
            dbConnection.Execute("INSERT INTO WISHCLIENT (id, DateCreate, idClient) VALUES(@id, @DateCreate, @idClient)"
                , new { id = obj.Id.ToString(), obj.DateCreate, idClient = obj.Client.Id.ToString() });
        }

        public void AddItem(WishClient obj)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            Connection.Dispose();
        }

        public IEnumerable<WishClient> GetAll()
        {
            throw new NotImplementedException();
        }

        public WishClient GetByClientEmail(string id)
        {
            throw new NotImplementedException();
        }

        public WishClient GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Remove(WishClient obj)
        {
            throw new NotImplementedException();
        }

        public void Update(WishClient obj)
        {
            throw new NotImplementedException();
        }
    }
}
