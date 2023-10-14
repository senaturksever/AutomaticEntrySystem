using System.Data.SqlClient;
using System.Data;

namespace AutomaticEntrySystem.Library
{
    public class Database
    {
        private static string ConnString = "";

        public static string GetConnectionString()
        {
            if (ConnString == "")
            {
                ConnString = ConnectKeys();
            }
            return ConnString;
        }

        private static string ConnectKeys()
        {
            try
            {
                string dbConnectionString = "Data Source=DESKTOP-8E83SPI\\SQLEXPRESS;Initial Catalog=SenaTest;User ID=senaturksever;Password=Sena123.";
                return dbConnectionString;
            }
            catch (Exception ex)
            {

            }
            return "";
        }
        public static DataSet GetDataSetParameter(string sqlCommand, SqlParameter[] sqlParameters)
        {
            var ds = new DataSet();

            try
            {
                using (SqlConnection connection = new SqlConnection(GetConnectionString()))
                {
                    SqlCommand cmd = new SqlCommand(sqlCommand, connection);

                    if (sqlParameters != null)
                    {
                        foreach (var parameter in sqlParameters)
                        {
                            cmd.Parameters.Add(parameter);
                        }
                    }

                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }

                    var da = new SqlDataAdapter(cmd);
                    da.SelectCommand.CommandTimeout = 300;

                    da.Fill(ds);

                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                        connection.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return ds;
        }

        public static SqlParameter SetParameter(string parameterName, SqlDbType dbType, Int32 iSize, string direction, object oParamValue)
        {
            var parameter = new SqlParameter(parameterName, dbType, iSize);

            switch (direction)
            {
                case "Input":
                    parameter.Direction = ParameterDirection.Input;
                    break;
                case "Output":
                    parameter.Direction = ParameterDirection.Output;
                    break;
                case "ReturnValue":
                    parameter.Direction = ParameterDirection.ReturnValue;
                    break;
                case "InputOutput":
                    parameter.Direction = ParameterDirection.InputOutput;
                    break;
                default:
                    break;
            }

            parameter.Value = oParamValue;
            return parameter;
        }
        public static DataTable GetDataTableParameter(string sqlCommand, SqlParameter[] sqlParameters)
        {
            DataTable dt = null;

            try
            {
                dt = GetDataSetParameter(sqlCommand, sqlParameters).Tables[0];
            }
            catch (Exception)
            {

            }
            return dt;
        }

        public static int ExecuteNonQueryWithParameters(string sql, SqlParameter[] sqlParameters)
        {
            int result = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(GetConnectionString()))
                {
                    SqlCommand cmd = new SqlCommand(sql, connection);

                    if (sqlParameters != null)
                    {
                        foreach (var parameter in sqlParameters)
                        {
                            cmd.Parameters.Add(parameter);
                        }
                    }

                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }

                    cmd.CommandTimeout = 300;

                    result = cmd.ExecuteNonQuery();

                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                        connection.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return result;
        }
    }
}
