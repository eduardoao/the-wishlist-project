using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using Wishlist.Core.Interfaces.Repositorys;
using Wishlist.Core.Models;
using Dapper;
using System.Data;
using System.Linq;
using Wishlist.Core.Models.ValueObject;

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
            dbConnection.Execute("INSERT INTO client (id ,firstname ,lastname ,email,) VALUES (@id ,@firstname, @lastname ,@Email)", 
                new {
                    id = obj.Id 
                    ,firstname = obj.Name.FirstName.ToString()                    
                    ,lastname = obj.Name.LastName.ToString()                    
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
            var clientresult = dbConnection.Query("SELECT * FROM client", commandType: CommandType.Text)
               .Select(x =>
               {
                   var result = new Client(new Name(x.Firstname, x.Lastname), new Email(x.email));
                   return result;
               });

            return clientresult;



        }

        public Client GetByEmail(string email)
        {
            using IDbConnection dbConnection = Connection;
            dbConnection.Open();

            var clientresult = dbConnection.Query("SELECT * FROM client WHERE Email = @email", new { Email = email }, commandType: CommandType.Text)
                .Select(x =>
                {
                   var result = new Client(new Name(x.Firstname, x.Lastname), new Email(x.email));                  
                   return result;
                }).FirstOrDefault();

            return clientresult;         
        }

        public Client GetById(Guid id)
        {
            using IDbConnection dbConnection = Connection;
            dbConnection.Open();
          
            var clientresult = dbConnection.Query("SELECT * FROM client WHERE ID=@id", new { Id = id }, commandType: CommandType.Text)
                .Select(x =>
                {
                    var result = new Client(new Name(x.Firstname, x.Lastname), new Email(x.email));
                    return result;
                }).FirstOrDefault();

            return clientresult;
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
            dbConnection.Query("UPDATE client SET name = @Name WHERE id = @Id", new { Name = obj.Name.ToString(), id = obj.Id });
        }
    }
}
