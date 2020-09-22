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
            using IDbConnection dbConnection = Connection;
            dbConnection.Open();
            dbConnection.Execute("INSERT INTO WISHCLIENTITEM (wishclient_id, product_id, datecreate, dateupdate) VALUES(@wishclient_id, @product_id, @datecreate, @dateupdate)"
                , new { wishclient_id = obj.Id.ToString(), product_id= obj.Product.Id, datecreate= obj.DateCreate, dateupdate=obj.DateUpdate });
        }

        public void Dispose()
        {
            Connection.Dispose();
        }

        public IEnumerable<WishClient> GetAll()
        {
            using IDbConnection dbConnection = Connection;
            dbConnection.Open();
            return dbConnection.Query<WishClient>("SELECT * FROM WISHCLIENT INNER JOIN WISHCLIENTITEM ON WISHCLIENT.ID = WISHCLIENTITEM.wishclient_id");
        }

        public WishClient GetByClientEmail(string email)
        {
            using IDbConnection dbConnection = Connection;
            dbConnection.Open();
            return dbConnection.QueryFirst<WishClient>("SELECT * FROM WISHCLIENT INNER JOIN WISHCLIENTITEM ON WISHCLIENT.ID = WISHCLIENTITEM.wishclient_id INNER JOIN CLIENT ON WISHCLIENT.ID_CLIENT = CLIENTE.ID  WHERE CLIENTE.EMAIL=@email", new { email = email });
       
        }

        public WishClient GetById(Guid id)
        {
            using IDbConnection dbConnection = Connection;
            dbConnection.Open();
            return dbConnection.QueryFirst<WishClient>("SELECT * FROM WISHCLIENT INNER JOIN WISHCLIENTITEM ON WISHCLIENT.ID = WISHCLIENTITEM.wishclient_id WHERE WISHCLIENT.ID =@id", new { id = id });

        }

        public bool ProductExistInWish(Guid id,  string title)
        {
            using IDbConnection dbConnection = Connection;
            dbConnection.Open();
            var result =  dbConnection.QueryFirst<WishClient>("SELECT * FROM WISHCLIENT INNER JOIN WISHCLIENTITEM ON WISHCLIENT.ID = WISHCLIENTITEM.wishclient_id INNER JOIN PRODUCT WISHCLIENTITEM.PRODUCT_ID = PRODUCT.ID ON  WHERE WISHCLIENT.ID =@id AND PRODUCT.TITLE = @title ", new { id = id , title = title});
            if (result != null)
                return true;
            return false;
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
