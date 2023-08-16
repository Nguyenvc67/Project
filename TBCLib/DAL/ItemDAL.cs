using System;
using System.Collections.Generic;
using MySqlConnector;
using Model;
using System.Data;



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
                query = @"select * from Tabaccos where Tabacco_id=@TabaccoId;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@TabaccoId", tabaccoId);
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    tabacco = GetTabacco(reader);
                }
                reader.Close();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally { }

            return tabacco;
        }
        internal Tabacco GetTabacco(MySqlDataReader reader)
        {
            Tabacco tabacco = new Tabacco();
            tabacco.TabaccoId = reader.GetInt32("Tabacco_id");
            tabacco.TabaccoName = reader.GetString("Tabacco_name");
            tabacco.Manufactory = reader.GetString("Manufactory");
            tabacco.TabaccoPrice = reader.GetDecimal("Tabacco_Price");
            tabacco.Amount = reader.GetDecimal("amount");
            tabacco.TabaccoPack = reader.GetInt32("Tabacco_pack");
            tabacco.TabaccoDate = reader.GetDateTime("Tabacco_date");
            return tabacco;
        }
        public List<Tabacco> GetTabaccos(int tabaccoFilter, Tabacco tabacco)
        {
            List<Tabacco> lst = null;
            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
                MySqlCommand command = new MySqlCommand("", connection);
                switch (tabaccoFilter)
                {
                    case TabaccoFilter.GET_ALL:
                        query = @"select * from Tabaccos";
                        break;
                    case TabaccoFilter.FILTER_BY_Tabacco_NAME:
                        query = @"select *  from Tabaccos
                                where Tabacco_name like Concat('%',@TabaccoName,'%');";
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
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return lst;
        }
    }
}