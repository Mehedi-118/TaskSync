
using PTSL.DgFood.Procedure.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace PTSL.DgFood.Procedure.Implementation
{
    public interface StoredProcedureCollection2 : IStoredProcedureCollection2
    {
        public (bool success, DataTable entity, string Message) RunSP_ReturnDT(string procedureName, List<SqlParameter> parameters, string connectionString)
        {
            DataTable dtData = new DataTable();
            (bool success, DataTable entity,string Message) entityObject;
            entityObject.success = false;
            entityObject.entity = new DataTable();
            entityObject.Message = "Faild";
            using (SqlConnection sqlConn = new SqlConnection(connectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(procedureName, sqlConn))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    if (parameters != null)
                    {
                        sqlCommand.Parameters.AddRange(parameters.ToArray());
                    }
                    using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
                    {
                        sqlDataAdapter.Fill(dtData);
                    }
                }
                entityObject.success = true;
                entityObject.entity = dtData;
                entityObject.Message = "Data Loaded Successfullly";
            }
            return entityObject;
        }
    }
}
