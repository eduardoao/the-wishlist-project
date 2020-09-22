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
    public class RepositoryClient: IRepositoryClient
    {
        //Todo fazer Ioc camada
        private IConfiguration _configu;        
        private string connectionString; 

        public RepositoryClient(IConfiguration config)
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

        public void Add(Client obj)
        {
            using IDbConnection dbConnection = Connection;
            dbConnection.Open();
            dbConnection.Execute("INSERT INTO client (id,name,email) VALUES (@id ,@Name ,@Email)", 
                new {
                    id = obj.Id 
                    ,name = obj.Name.ToString()
                    ,email = obj.Email.ToString() 
                });
        }

        public void Dispose()
        {
            Connection.Dispose();
        }

        public IEnumerable<Client> GetAll()
        {

            using IDbConnection dbConnection = Connection;
            dbConnection.Open();
            return dbConnection.Query<Client>("SELECT * FROM client");

        }

        public Client GetByEmail(string email)
        {
            using IDbConnection dbConnection = Connection;
            dbConnection.Open();
            return dbConnection.QueryFirstOrDefault<Client>("SELECT * FROM client WHERE Email=@email", new { Email = email });
        }

        public Client GetById(Guid id)
        {
            using IDbConnection dbConnection = Connection;
            dbConnection.Open();
            return dbConnection.QueryFirst<Client>("SELECT * FROM client WHERE ID=@id", new { Id = id });
        }       

        public void Remove(Client obj)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("DELETE FROM client WHERE Id=@Id", new { Id = obj.Id });
            }
        }

        public void Update(Client obj)
        {
            using IDbConnection dbConnection = Connection;
            dbConnection.Open();
            dbConnection.Query("UPDATE client SET name = @Name,  email= @Email WHERE id = @Id", obj);
        }
    }
}
