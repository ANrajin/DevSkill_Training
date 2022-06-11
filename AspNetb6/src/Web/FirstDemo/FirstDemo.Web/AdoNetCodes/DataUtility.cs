using Microsoft.Data.SqlClient;
using System.Data;

namespace FirstDemo.Web.AdoNetCodes
{
    public class DataUtility
    {
        private readonly string _connectionString;
        public DataUtility(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void ExecuteCommand(string sqlCommand, IDictionary<string, object> parameters)
        {
            using SqlCommand command = CreateCommand(sqlCommand, parameters);
            
            if(command.Connection.State != System.Data.ConnectionState.Open)
                command.Connection.Open();

            command.ExecuteNonQuery();
        }

        public IList<IDictionary<string, object>> GetData(string query, IDictionary<string, object> parameters)
        {
            using SqlCommand command = CreateCommand(query, parameters);

            if (command.Connection.State != System.Data.ConnectionState.Open)
                command.Connection.Open();

            using SqlDataReader reader = command.ExecuteReader();

            List<IDictionary<string, object>> data = new List<IDictionary<string, object>>();

            while(reader.Read())
            {
                Dictionary<string, object> row = new Dictionary<string, object>();
                foreach(var col in reader.GetColumnSchema())
                {
                    row.Add(col.ColumnName, reader[col.ColumnName]);
                }
                data.Add(row);
            }

            return data;
        }

        public DataSet UseDataSet()
        {
            DataSet dataSet = new DataSet();
            DataTable table = new DataTable("Courses");
            table.Columns.Add(new DataColumn("Title"));
            table.Columns.Add(new DataColumn("Fees"));
            dataSet.Tables.Add(table);

            var _sqlConnection = new SqlConnection(_connectionString);
            if (_sqlConnection.State != System.Data.ConnectionState.Open)
            {
                _sqlConnection.Open();
            }

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select * from courses where isActive = 1",
                _sqlConnection);
            sqlDataAdapter.TableMappings.Add("Table", "Courses");
            sqlDataAdapter.Fill(dataSet);

            return dataSet;
        }

        private SqlCommand CreateCommand(string sql, IDictionary<string, object> parameters)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand command = connection.CreateCommand();
            command.CommandText = sql;
            if (parameters != null)
            {
                foreach (var param in parameters)
                {
                    command.Parameters.Add(new SqlParameter(param.Key, param.Value));
                }
            }

            return command;
        }
    }
}
