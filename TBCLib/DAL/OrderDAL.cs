using System;
using System.Collections.Generic;
using MySqlConnector;
using Model;

namespace OrderManagementApp.DAL
{
    public class DataAccess
    {
        private readonly string connectionString;

        public DataAccess(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Customer> GetAllCustomers()
        {
            List<Customer> customers = new List<Customer>();
            using MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            string query = "SELECT customer_id, customer_name, customer_address, customer_phone FROM Customers";
            using MySqlCommand command = new MySqlCommand(query, connection);
            using MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                customers.Add(new Customer
                {
                    CustomerId = reader.GetInt32("customer_id"),
                    CustomerName = reader.GetString("customer_name"),
                    CustomerAddress = reader.GetString("customer_address"),
                    CustomerPhone = reader.GetString("customer_phone")
                });
            }

            return customers;
        }

        public List<Order> GetAllOrders()
        {
            List<Order> orders = new List<Order>();
            using MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            string query = "SELECT order_id, Customer_id, Seller_id, Order_data, Order_status FROM Orders";
            using MySqlCommand command = new MySqlCommand(query, connection);
            using MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                orders.Add(new Order
                {
                    OrderId = reader.GetInt32("order_id"),
                    CustomerId = reader.GetInt32("Customer_id"),
                    SellerId = reader.GetInt32("Seller_id"),
                    OrderDate = reader.GetDateTime("Order_data"),
                    OrderStatus = reader.GetString("Order_status")
                    
                });
            }

            return orders;
        }

        public List<OrderDetail> GetAllOrderDetails()
        {
            List<OrderDetail> orderDetails = new List<OrderDetail>();
            using MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            string query = "SELECT Order_id, Tabacco_id, Quantity FROM OrderDetails";
            using MySqlCommand command = new MySqlCommand(query, connection);
            using MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                orderDetails.Add(new OrderDetail
                {
                    OrderId = reader.GetInt32("Order_id"),
                    TabaccoId = reader.GetInt32("Tabacco_id"),
                    Quantity = reader.GetInt32("Quantity")
                });
            }

            return orderDetails;
        }
        public Customer GetCustomerById(int customerId)
        {
            using MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            string query = "SELECT customer_id, customer_name, customer_address, customer_phone FROM Customers WHERE customer_id = @CustomerId";
            using MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@CustomerId", customerId);

            using MySqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new Customer
                {
                    CustomerId = reader.GetInt32("customer_id"),
                    CustomerName = reader.GetString("customer_name"),
                    CustomerAddress = reader.GetString("customer_address"),
                    CustomerPhone = reader.GetString("customer_phone")
                };
            }

            return null; 
        }
        public Staff GetStaffById(int staffId)
        {
            using MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            string query = "SELECT Staff_id, Staff_name, Password1 FROM Staffs WHERE Staff_id = @StaffId";
            using MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@StaffId", staffId);

            using MySqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new Staff
                {
                    StaffId = reader.GetInt32("Staff_id"),
                    StaffName = reader.GetString("Staff_name")
                };
            }

            return null; 
        }
        public List<Tabacco> GetAllTabaccos()
        {
            List<Tabacco> tabaccos = new List<Tabacco>();
            using MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            string query = "SELECT Tabacco_id, Tabacco_name, Manufactory, Tabacco_Price, amount, Tabacco_pack, Tabacco_date FROM Tabaccos";
            using MySqlCommand command = new MySqlCommand(query, connection);
            
            using MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                tabaccos.Add(new Tabacco
                {
                    TabaccoId = reader.GetInt32("Tabacco_id"),
                    TabaccoName = reader.GetString("Tabacco_name"),
                    Manufactory = reader.GetString("Manufactory"),
                    TabaccoPrice = reader.GetDecimal("Tabacco_Price"),
                    Amount = reader.GetDecimal("amount"),
                    TabaccoPack = reader.GetInt32("Tabacco_pack"),
                    TabaccoDate = reader.GetDateTime("Tabacco_date")
                });
            }

            return tabaccos;
        }
        public void AddOrder(Order order)
        {
            using MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();


            using var transaction = connection.BeginTransaction();

            try
            {

                string insertOrderQuery = "INSERT INTO Orders (Customer_id, Seller_id, Order_data, Order_status) VALUES (@CustomerId, @SellerId, @OrderDate, @OrderStatus)";
                using var insertOrderCommand = new MySqlCommand(insertOrderQuery, connection, transaction);
                insertOrderCommand.Parameters.AddWithValue("@CustomerId", order.CustomerId);
                insertOrderCommand.Parameters.AddWithValue("@SellerId", order.SellerId);
                insertOrderCommand.Parameters.AddWithValue("@OrderDate", order.OrderDate);
                insertOrderCommand.Parameters.AddWithValue("@OrderStatus", order.OrderStatus);
                insertOrderCommand.ExecuteNonQuery();


                int orderId = (int)insertOrderCommand.LastInsertedId;

                foreach (var orderDetail in order.OrderDetails)
                {
                    string insertOrderDetailQuery = "INSERT INTO OrderDetails (Order_id, Tabacco_id, Quantity) VALUES (@OrderId, @TabaccoId, @Quantity)";
                    using var insertOrderDetailCommand = new MySqlCommand(insertOrderDetailQuery, connection, transaction);
                    insertOrderDetailCommand.Parameters.AddWithValue("@OrderId", orderId);
                    insertOrderDetailCommand.Parameters.AddWithValue("@TabaccoId", orderDetail.TabaccoId);
                    insertOrderDetailCommand.Parameters.AddWithValue("@Quantity", orderDetail.Quantity);
                    insertOrderDetailCommand.ExecuteNonQuery();
                }
                transaction.Commit();
            }
            catch (Exception)
            {

                transaction.Rollback();
                throw;
            }
        }
    }
}