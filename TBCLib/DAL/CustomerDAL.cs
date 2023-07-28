using MySqlConnector;
using Model;

namespace DAL
{
    public class CustomerDAL
    {
        private string? query;
        private MySqlConnection? connection;

        public CustomerDAL()
        {
            connection = DbConfig.GetConnection();
            query = "";
        }
        public Customer? GetById(int customerId)
        {
            Customer? c = null;
            try
            {
                query = @"select customer_id, customer_name, customer_phone,
                        ifnull(customer_address, '') as customer_address
                        from Customers where customer_id=" + customerId + ";";
                MySqlDataReader reader = (new MySqlCommand(query, connection)).ExecuteReader();
                if (reader.Read())
                {
                    c = GetCustomer(reader);
                }
                reader.Close();
            }
            catch { }
            return c;
        }
        internal Customer GetCustomer(MySqlDataReader reader)
        {
            Customer c = new Customer();
            c.CustmerId = reader.GetInt32("customer_id");
            c.CustomerName = reader.GetString("customer_name");
            c.CustomerAddress = reader.GetString("customer_address");
            c.CustomerPhone = reader.GetString("customer_phone");
            return c;
        }
        public int AddCustomer(Customer c)
        {
            int result = 0;
            if (connection != null)
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }
                MySqlCommand cmd = new MySqlCommand("sp_createCustomer", connection);
                try
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@customerName", c.CustomerName);
                    cmd.Parameters["@customerName"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters.AddWithValue("@customerAddress", c.CustomerAddress);
                    cmd.Parameters["@customerAddress"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters.AddWithValue("@customerPhone", MySqlDbType.Int32);
                    cmd.Parameters["@customerId"].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters.AddWithValue("@customerId", MySqlDbType.Int32);
                    cmd.Parameters["@customerId"].Direction = System.Data.ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    result = (int?)cmd.Parameters["@customerId"].Value ?? 0;
                }
                catch { }
                finally
                {
                    connection.Close();
                }
            }
            return result;
        }
    }
}