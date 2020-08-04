using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using Oracle.ManagedDataAccess.Client;
namespace WebApi.data
{
    public class OracleDynamicParameters : SqlMapper.IDynamicParameters
    {
        private readonly DynamicParameters m_dynamicParameters = new DynamicParameters();
        private readonly List<OracleParameter> m_oracleParameters = new List<OracleParameter>();
        //public OracleDynamicParameters(DynamicParameters dynamicParameters,List<OracleParameter> oracleParameters, IConfiguration configuration)
        //{
        //    m_dynamicParameters = dynamicParameters;
        //    m_oracleParameters = oracleParameters;
        //    m_configuration = configuration;
        //}

        public void Add(string name, OracleDbType oracleDbType, ParameterDirection direction, object value = null, int? size = null)
        {
            OracleParameter oracleParameter;
            if (size.HasValue)
            {
                oracleParameter = new OracleParameter(name, oracleDbType, size.Value, value, direction);
            }
            else
            {
                oracleParameter = new OracleParameter(name, oracleDbType, value, direction);
            }

            m_oracleParameters.Add(oracleParameter);
        }

        public void Add(string name, OracleDbType oracleDbType, ParameterDirection direction)
        {
            var oracleParameter = new OracleParameter(name, oracleDbType, direction);
            m_oracleParameters.Add(oracleParameter);
        }

        public void AddParameters(IDbCommand command, SqlMapper.Identity identity)
        {
            ((SqlMapper.IDynamicParameters)m_dynamicParameters).AddParameters(command, identity);

            var oracleCommand = command as OracleCommand;

            if (oracleCommand != null)
            {
                oracleCommand.Parameters.AddRange(m_oracleParameters.ToArray());
            }
        }
        
    }
}
