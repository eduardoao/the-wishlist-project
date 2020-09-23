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
            dbConnection.Execute("INSERT INTO WISHCLIENT (id, id_client) VALUES(@id, @idClient)"
                , new { id = obj.Id, idClient = obj.Client.Id });
        }

        public void AddItem(WishClient obj)
        {
            using IDbConnection dbConnection = Connection;
            dbConnection.Open();
            dbConnection.Execute("INSERT INTO WISHCLIENTITEM (wishclient_id, product_id) VALUES(@wishclient_id, @product_id)"
                , new { wishclient_id = obj.Id, product_id= obj.Product.Id });
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
            return dbConnection.QueryFirstOrDefault<WishClient>("SELECT * FROM WISHCLIENT INNER JOIN WISHCLIENTITEM ON WISHCLIENT.ID = WISHCLIENTITEM.wishclient_id INNER JOIN CLIENT ON WISHCLIENT.ID_CLIENT = CLIENT.ID  WHERE CLIENT.EMAIL=@email", new { email = email });
       
        }

        public WishClient GetById(Guid id)
        {
            using IDbConnection dbConnection = Connection;
            dbConnection.Open();
            return dbConnection.QueryFirstOrDefault<WishClient>("SELECT * FROM WISHCLIENT INNER JOIN WISHCLIENTITEM ON WISHCLIENT.ID = WISHCLIENTITEM.wishclient_id WHERE WISHCLIENT.ID =@id", new { id = id });

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
