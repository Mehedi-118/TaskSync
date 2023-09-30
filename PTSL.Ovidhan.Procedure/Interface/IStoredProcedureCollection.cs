
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace PTSL.DgFood.Procedure.Interface
{
    public interface IStoredProcedureCollection2
    {
        (bool success, DataTable entity, string Message) RunSP_ReturnDT(string procedureName, List<SqlParameter> parameters, string connectionString);
    } 
}
