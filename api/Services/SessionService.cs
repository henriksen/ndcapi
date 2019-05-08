using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace api.Services
{
    public interface ISessionService
    {
        int Create();
        Session Get(int id);
        void Set(int id, int value);
    }

    public class SessionService : ISessionService
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public SessionService(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public Session Get(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var sql = @"SELECT * FROM Session WHERE Id = @id";

                var session = conn.QueryFirstOrDefault<Session>(sql, new
                {
                    id,
                });
                return session;
            }

        }

        public int Create()
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                return conn.QuerySingle("INSERT INTO Session OUTPUT Inserted.Id VALUES(0)").Id;
            }
        }

        public void Set(int id, int value)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Execute("UPDATE Session SET Value=@value WHERE Id=@id ",
                    new { id, value });
            }
        }
    }
}
