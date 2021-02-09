using CorporateQnA.Services.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateQnA.Services
{
    class DbConnectionService:IDbConnectionService
    {
        private readonly IDbConnection Db;
        public DbConnectionService(IConfiguration config)
        {
            Db = new SqlConnection(config.GetConnectionString("CorporateQnAContext"));
            Db.Open();
        }
        public IDbConnection GetDbConnection()
        { 
            return Db;
        }
    }
}
