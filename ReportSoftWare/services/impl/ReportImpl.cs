
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Oracle.ManagedDataAccess.Client;
using Dapper;
using WebApi.data;

namespace ReportSoftWare.services.impl
{
    public class ReportImpl : IReport
    {
        private IConfiguration m_configuration;
        public ReportImpl(IConfiguration configuration)
        {
            m_configuration = configuration;
        }

        public dynamic execureQuery()
        {
            string query = "select * from test1";
            object result = null;
            try
            {
                var dyParam = new OracleDynamicParameters();
                var conn = GetConnection();
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                if (conn.State == ConnectionState.Open)
                {
                    //var query = "select * from QLVT_HCM.CONGTRINH";
                    result = SqlMapper.Query(conn, query, param: dyParam, commandType: CommandType.Text);
                }
                return result;
            }
            catch (Exception e)
            {
                return e;
            }
        }

        public IDbConnection GetConnection()
        {
            var connectionString = m_configuration.GetSection("ConnectionStrings").GetSection("defaultconnection").Value;
            var conn = new OracleConnection(connectionString);
            return conn;
        }
    }
}
