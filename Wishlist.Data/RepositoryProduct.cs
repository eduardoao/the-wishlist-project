using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using Wishlist.Core.Interfaces.Repositorys;
using Wishlist.Core.Models;
using Dapper.Contrib.Extensions;
using Dapper;
using System.Data;
using Wishlist.Core.Models.ValueObject;

namespace Wishlist.Data
{
    public class RepositoryProduct : IRepositoryProduct
    {
        //Todo fazer Ioc camada
        private IConfiguration _configu;        
        private string connectionString; 

        public RepositoryProduct(IConfiguration config)
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
               

        public void Add(Product obj)
        {
            using IDbConnection dbConnection = Connection;
            dbConnection.Open();
            dbConnection.Execute("INSERT INTO Product (id, Title, Picture, Price) VALUES(@id, @Title, @Picture, @Price)", 
                new {
                    id = obj.Id,
                    title = obj.Title.Name.ToString(),
                    picture = obj.Picture.Url.ToString(),
                    price = obj.Price                    
                });           
        }

        public void Dispose()
        {
            Connection.Dispose();
        }

        public IEnumerable<Product> GetAll()
        {

            using IDbConnection dbConnection = Connection;
            dbConnection.Open();
            return dbConnection.Query<Product>("SELECT * FROM Product");

        }    

        public Product GetById(Guid id)
        {
            using IDbConnection dbConnection = Connection;
            dbConnection.Open();
            return dbConnection.QueryFirst<Product>("SELECT * FROM product WHERE ID=@id", new { Id = id });
        }

        public Product GetByProductTitle(string title)
        {
            using IDbConnection dbConnection = Connection;
            dbConnection.Open();
            return dbConnection.QueryFirstOrDefault<Product>("SELECT * FROM product WHERE Title=@title", new { Title = title });
        }

        public void Remove(Product obj)
        {
            using IDbConnection dbConnection = Connection;
            dbConnection.Open();
            dbConnection.Query("UPDATE PRODUCT SET ISENABLE = @IsEnable  WHERE id = @Id", obj);
        }
     
        public void Update(Product obj)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Query("UPDATE product SET Title = @Title, Picture= @Picture, Price= @Price WHERE id = @Id", 
                    new { Title = obj.Title.ToString(), Picture = obj.Picture.ToString(), Price = obj.Price, Id = obj.Id });
            }
        }
        
    }
}
