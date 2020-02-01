using Npgsql;
using schedule.Models;
using shedule.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace schedule.Code
{
    class ForDB
    {
        public static List<hourSale> getHourFromDB(string connectionString, string sql, int idShop, string connectionType)
        {
            List<hourSale> hss = new List<hourSale>();
            object connection = new object();
            switch (connectionType)
            {
                case "PostgreSQL": connection = new NpgsqlConnection(connectionString);  break;
                case "MS SQL": connection = new SqlConnection(connectionString);  break;   
            }

            try
            {
                if (connection as NpgsqlConnection != null)
                {
                    using ((NpgsqlConnection)connection)
                    {
                        var command = new NpgsqlCommand();
                        ((NpgsqlConnection)connection).Open();
                        (command).Connection = (NpgsqlConnection)connection;
                        (command).CommandText = sql;
                        (command).CommandTimeout = 5000;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                hourSale h = new hourSale(idShop, reader.GetDateTime(1), reader.GetString(2), reader.GetString(3), reader.GetInt32(4), reader.GetInt32(5), reader.GetDouble(6));
                                hss.Add(h);

                            }
                        }
                    }
                }
                else if (connection as SqlConnection != null)
                {
                    using ((SqlConnection)connection)
                    {
                        var command = new SqlCommand();
                        ((SqlConnection)connection).Open();
                        (command).Connection = (SqlConnection)connection;
                        (command).CommandText = sql;
                        (command).CommandTimeout = 5000;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                hourSale h = new hourSale(idShop, reader.GetDateTime(1), reader.GetString(2), reader.GetString(3), reader.GetInt32(4), reader.GetInt32(5), reader.GetDouble(6));
                                hss.Add(h);

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());

            }
            

            return hss;
        }

        public static string getSQL_statisticbyshopsdayhour(int id, DateTime start, DateTime end,string sheme, string connectionType)
        {
            string sql = "";
            
            if (connectionType=="PostgreSQL") {
                string date1 = start.Year + "/" + Helper.NumberToString(start.Month) + "/" + Helper.NumberToString(start.Day);
                string date2 = end.Year + "/" + Helper.NumberToString(end.Month) + "/" + Helper.NumberToString(end.Day);
                sql = "SELECT * FROM " + sheme + "get_statisticbyshopsdayhour('" + id + "', to_timestamp('" + date1 + "', 'YYYY/MM/DD')::timestamp without time zone, to_timestamp('" + date2 + " 23:59', 'YYYY/MM/DD hh24:mi')::timestamp without time zone)";
            }else if (connectionType == "MS SQL")
            {
                string date1 = start.Year + "/" + Helper.NumberToString(start.Day) + "/" +  Helper.NumberToString(start.Month);
                string date2 = end.Year + "/" + Helper.NumberToString(end.Day) + "/" + Helper.NumberToString(end.Month);
                sql = "select * from dbo.get_StatisticByShopsDayHour('" + id + "', '" + date1 + "', '" + date2 + " 23:59:00')"; 

            }

            return sql;


        }


        public static void getShopsFromDB()
        {
            mShop h;
            Connection connect = Connection.getActiveConnection();
            var connectionString = Connection.getConnectionString(connect);
            string sql = "select * from " + Connection.getSheme(connect) + "get_shops() order by КодМагазина";
            if (connect.typeDB=="PostgreSQL") {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    try
                    {
                        Program.listShops.Clear();
                        connection.Open();
                        using (var command = new NpgsqlCommand())
                        {
                            command.Connection = connection;
                            command.CommandText = sql;
                            command.CommandTimeout = 500;

                            using (var reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                   
                                    h = new mShop(reader.GetInt16(0), reader.GetString(1));
                                    Program.listShops.Add(h);


                                }
                            }
                        }
                        DBShop.setShops(Program.listShops);

                    }
                    catch (Exception ex)
                    {
                        Logger.Error("Ошибка соединения с базой данных " + ex.Message);
                    }
                }
            } else if (connect.typeDB == "MS SQL") {
                using (var connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        Program.listShops.Clear();
                        connection.Open();
                        using (var command = new SqlCommand())
                        {
                            command.Connection = connection;
                            command.CommandText = sql;
                            command.CommandTimeout = 500;

                            using (var reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {

                                    h = new mShop(reader.GetInt16(0), reader.GetString(1));
                                    Program.listShops.Add(h);


                                }
                            }
                        }
                        DBShop.setShops(Program.listShops);

                    }
                    catch (Exception ex)
                    {
                        Logger.Error("Ошибка соединения с базой данных " + ex.Message);
                    }
                }
            }
        }

        public static bool isConnected()
        {
            bool connect = false;
            // var connectionString = "Data Source=CENTRUMSRV;Persist Security Info=True;User ID=" + l + ";Password=" + p;
            Connection activeconnect = Connection.getActiveConnection(Program.currentShop.getIdShop());
            var connectionString = Connection.getConnectionString(activeconnect);

            //  connectionString = "Data Source=CENTRUMSRV;Persist Security Info=True;User ID=VShleyev;Password=gjkrjdybr@93";
            if (activeconnect.typeDB == "PostgreSQL")
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {

                try
                {
                    connection.Open();

                    if (connection.State == System.Data.ConnectionState.Open) { connect = true; return connect; }
                    else { connect = false; return connect; }
                }
                catch (Exception ex)
                {
                    Logger.Error(ex.ToString());
                    connect = false;
                    return connect;
                }

            }
            
        } else if (activeconnect.typeDB == "MS SQL") {
             using (SqlConnection connection = new SqlConnection(connectionString))
            {

                try
                {
                    connection.Open();

                    if (connection.State == System.Data.ConnectionState.Open) { connect = true; return connect; }
                    else { connect = false; return connect; }
                }
                catch (Exception ex)
                {
                    Logger.Error(ex.ToString());
                    connect = false;
                    return connect;
                }

            }
            }
            return connect;
        }

        
    }
}
