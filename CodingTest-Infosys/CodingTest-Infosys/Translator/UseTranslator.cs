using CodingTest_Infosys.Utility;
using Microsoft.Data.SqlClient;
using CodingTest_Infosys.Models;

namespace CodingTest_Infosys.Translator
{
    public static class UserTranslator
    {
        public static ProductDetails TranslateAsUser(this SqlDataReader reader, bool isList = false)
        {
            if (!isList)
            {
                if (!reader.HasRows)
                    return null;
                reader.Read();
            }
            var item = new ProductDetails();
            if (reader.IsColumnExists("Id"))
                item.Id = Sqlhelper.GetNullableInt32(reader, "Id");

            if (reader.IsColumnExists("FirstName"))
                item.FirstName = Sqlhelper.GetNullableString(reader, "FirstName");

            if (reader.IsColumnExists("LastName"))
                item.LastName = Sqlhelper.GetNullableString(reader, "LastName");

            if (reader.IsColumnExists("Description"))
                item.Description = Sqlhelper.GetNullableString(reader, "Description");

            if (reader.IsColumnExists("Quantity"))
                item.Quantity = Sqlhelper.GetNullableInt32(reader, "Quantity");


            return item;
        }

        public static List<ProductDetails> TranslateAsUsersList(this SqlDataReader reader)
        {
            var list = new List<ProductDetails>();
            while (reader.Read())
            {
                list.Add(TranslateAsUser(reader, true));
            }
            return list;
        }
    }
}
