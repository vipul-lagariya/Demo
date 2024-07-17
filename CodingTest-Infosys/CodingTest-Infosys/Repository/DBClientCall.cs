using CodingTest_Infosys.Models;
using CodingTest_Infosys.Translator;
using CodingTest_Infosys.Utility;
using Microsoft.Data.SqlClient;
using System.Data;


namespace CodingTest_Infosys.Repository
{
    public class DBClientCall
    {
        public List<ProductDetails> GetAllDetails(string connString)
        {
            return Sqlhelper.ExtecuteProcedureReturnData<List<ProductDetails>>(connString,
                "GetProductDetails", r => r.TranslateAsUsersList());
        }

        public string SaveDetails(ProductDetails model, string connString)
        {
            var outParam = new SqlParameter("@ReturnCode", SqlDbType.NVarChar, 20)
            {
                Direction = ParameterDirection.Output
            };
            SqlParameter[] param = {
                new SqlParameter("@Id",model.Id),
                new SqlParameter("@FirstName",model.FirstName),
                new SqlParameter("@LastName",model.LastName),
                new SqlParameter("@Description",model.Description),
                new SqlParameter("@Quantity",model.Quantity),
                outParam
            };
            Sqlhelper.ExecuteProcedureReturnString(connString, "SaveProductDetails", param);
            return (string)outParam.Value;
        }

        public string DeleteDetails(int id, string connString)
        {
            var outParam = new SqlParameter("@ReturnCode", SqlDbType.NVarChar, 20)
            {
                Direction = ParameterDirection.Output
            };
            SqlParameter[] param = {
                new SqlParameter("@Id",id),
                outParam
            };
            Sqlhelper.ExecuteProcedureReturnString(connString, "DeleteProductDetails", param);
            return (string)outParam.Value;
        }
    }
}
