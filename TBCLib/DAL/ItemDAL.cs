using System;
using System.Collections.Generic;
using MySqlConnector;
using Model;

namespace DAL
{
  public static class TabaccoFilter
  {
    public const int GET_ALL = 0;
    public const int FILTER_BY_Tabacco_NAME = 1;
  }
  public class TabaccoDAL
  {
    private string query;
    private MySqlConnection connection = DbConfig.GetConnection();

    public Tabacco GetTabaccoById(int tabaccoId)
    {
      Tabacco tabacco = null;
      try
      {
        connection.Open();
        query = @"select Tabacco_id, Tabacco_name, Tabacco_Price, amount, Tabacco_date, Tabacco_pack
                        
                        from Tabaccos where Tabacco_id=@TabaccoId;";
        MySqlCommand command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("@TabaccoId", tabaccoId);
        MySqlDataReader reader = command.ExecuteReader();
        if (reader.Read())
        {
          tabacco = GetTabacco(reader);
        }
        reader.Close();
      }
      catch { }
      finally { connection.Close(); }
      return tabacco;
    }
    internal Tabacco GetTabacco(MySqlDataReader reader)
    {
      Tabacco tabacco = new Tabacco();
      tabacco.TabaccoId = reader.GetInt32("Tabacco_id");
      tabacco.TabaccoName = reader.GetString("Tabacco_name");
      tabacco.TabaccoPrice = reader.GetDecimal("Tabacco_Price");
      tabacco.Amount = reader.GetInt32("amount");
      tabacco.Pack = reader.GetInt32("Tabacco_pack");
      tabacco.TabaccoDate = reader.GetDateTime("Tabacco_date");
      
      return tabacco;
    }
    public List<Tabacco> GetTabaccos(int tabaccoFilter, Tabacco tabacco)
    {
      List<Tabacco> lst = null;
      try
      {
        connection.Open();
        MySqlCommand command = new MySqlCommand("", connection);
        switch (tabaccoFilter)
        {
          case TabaccoFilter.GET_ALL:
            query = @"select Tabacco_id, Tabacco_name, Tabacco_Price, amount, Tabacco_date, Tabacco_pack '') from Tabaccos";
            break;
          case TabaccoFilter.FILTER_BY_Tabacco_NAME:
            query = @"select Tabacco_id, Tabacco_name, Tabacco_Price, amount, Tabacco_date,Tabacco_pack '') from Tabaccos
                                where Tabacco_name like concat('%',@TabaccoName,'%');";
            command.Parameters.AddWithValue("@TabaccoName", tabacco.TabaccoName);
            break;
        }
        command.CommandText = query;
        MySqlDataReader reader = command.ExecuteReader();
        lst = new List<Tabacco>();
        while (reader.Read())
        {
          lst.Add(GetTabacco(reader));
        }
        reader.Close();
      }
      catch { }
      finally
      {
        connection.Close();
      }
      return lst;
    }
  }
}