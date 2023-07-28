using MySqlConnector;
using Model;
using System.Collections.Generic;

namespace DAL
{
  public class OrderDAL
  {
    private MySqlConnection connection = DbConfig.GetConnection();
    public bool CreateOrder(Order order)
    {
      if (order == null || order.TabaccosList == null || order.TabaccosList.Count == 0)
      {
        return false;
      }
      bool result = false;
      try
      {
        connection.Open();
        MySqlCommand cmd = connection.CreateCommand();
        cmd.Connection = connection;
        cmd.CommandText = "lock tables Customers write, Orders write, Items write, OrderDetails write;";
        cmd.ExecuteNonQuery();
        MySqlTransaction trans = connection.BeginTransaction();
        cmd.Transaction = trans;
        MySqlDataReader reader = null;
        if (order.OrderCustomer == null || order.OrderCustomer.CustomerName == null || order.OrderCustomer.CustomerName == "")
        {
          order.OrderCustomer = new Customer() { CustmerId = 1 };
        }
        try
        {
          if (order.OrderCustomer.CustmerId == null)
          {
            cmd.CommandText = @"insert into Customers(customer_name, customer_address)
                            values ('" + order.OrderCustomer.CustomerName + "','" + (order.OrderCustomer.CustomerAddress ?? "") + "');";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "select customer_id from Customers order by customer_id desc limit 1;";
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
              order.OrderCustomer.CustmerId = reader.GetInt32("customer_id");
            }
            reader.Close();
          }
          else
          {
            cmd.CommandText = "select * from Customers where customer_id=@customerId;";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@customerId", order.OrderCustomer.CustmerId);
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
              order.OrderCustomer = new CustomerDAL().GetCustomer(reader);
            }
            reader.Close();
          }
          if (order.OrderCustomer == null || order.OrderCustomer.CustmerId == null)
          {
            throw new Exception("Can't find Customer!");
          }
          cmd.CommandText = "insert into Orders(customer_id, order_status) values (@customerId, @orderStatus);";
          cmd.Parameters.Clear();
          cmd.Parameters.AddWithValue("@customerId", order.OrderCustomer.CustmerId);
          cmd.Parameters.AddWithValue("@orderStatus", OrderStatus.CREATE_NEW_ORDER);
          cmd.ExecuteNonQuery();
          cmd.CommandText = "select LAST_INSERT_ID() as Order_id";
          reader = cmd.ExecuteReader();
          if (reader.Read())
          {
            order.OrderId = reader.GetInt32("Order_id");
          }
          reader.Close();

          foreach (var tabacco in order.TabaccosList)
          {
            if (tabacco.TabaccoId == null || tabacco.Amount <= 0)
            {
              throw new Exception("Not Exists Item");
            }
            cmd.CommandText = "select Tabacco_Price from Items where Tabacco_id=@TabaccoId";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@TabaccoId", tabacco.TabaccoId);
            reader = cmd.ExecuteReader();
            if (!reader.Read())
            {
              throw new Exception("Not Exists Item");
            }
            tabacco.TabaccoPrice = reader.GetDecimal("Tabacco_Price");
            reader.Close();

            cmd.CommandText = @"insert into OrderDetails(Order_id, Tabacco_id, Tabacco_Price, Quantity) values 
                            (" + order.OrderId + ", " + tabacco.TabaccoId + ", " + tabacco.TabaccoPrice + ", " + tabacco.Amount + ");";
            cmd.ExecuteNonQuery();

            cmd.CommandText = "update Items set amount=amount-@Quantity where Tabacco_id=" + tabacco.TabaccoId + ";";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@Quantity", tabacco.Amount);
            cmd.ExecuteNonQuery();
          }
          trans.Commit();
          result = true;
        }
        catch
        {
          try
          {
            trans.Rollback();
          }
          catch { }
        }
        finally
        {
          cmd.CommandText = "unlock tables;";
          cmd.ExecuteNonQuery();
        }
      }
      catch { }
      finally
      {
        connection.Close();
      }
      return result;
    }
  }
}