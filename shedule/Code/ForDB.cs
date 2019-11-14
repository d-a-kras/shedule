using Npgsql;
using shedule.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shedule.Code
{
    class ForDB
    {
        public static List<hourSale> getHourFromDB(string connectionString, string sql, int idShop)
        {
            List<hourSale> hss = new List<hourSale>();
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {

                try
                {
                    connection.Open();
                    
                    using (var command = new NpgsqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = sql;
                        command.CommandTimeout = 3000;
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
                catch (Exception ex)
                {
                    Logger.Error(ex.ToString());

                }
            }

            return hss;
        }

        public static string getSQL_statisticbyshopsdayhour(int id, string date1, string date2,string sheme)
        {
            return "SELECT * FROM "+ sheme + "get_statisticbyshopsdayhour('" + id + "', to_timestamp('"+date1+"', 'YYYY/MM/DD')::timestamp without time zone, to_timestamp('"+date2+" 23:59', 'YYYY/MM/DD hh24:mi')::timestamp without time zone)";
        }


        public static void getShopsFromDB()
        {
            mShop h;
            Connection connect = Connection.getActiveConnection(Program.currentShop.getIdShop());
            var connectionString = Connection.getConnectionString(connect);
            string sql = "select * from " + Connection.getSheme(connect) + "get_shops() order by КодМагазина";
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
                                // MessageBox.Show(reader.GetInt16(0)+" "+ reader.GetString(1));
                                h = new mShop(reader.GetInt16(0), reader.GetString(1));
                                Program.listShops.Add(h);
                               

                            }
                        }
                    }

                    /* string writePath = Environment.CurrentDirectory + @"\Shops.txt";
                     using (StreamWriter sw = new StreamWriter(writePath, false, Encoding.Default))
                     {

                         foreach (mShop shop in Program.listShops)
                         {
                             string pyth = Environment.CurrentDirectory + "/Shops/" + shop.getIdShop().ToString();
                             if (!Directory.Exists(pyth))
                             {
                                 Directory.CreateDirectory(pyth);
                             }

                             sw.WriteLine(shop.getIdShop() + "_" + shop.getAddress());
                         }
                     }*/
                    DBShop.setShops(Program.listShops);
                

                }
                catch (Exception ex)
                {
                    Logger.Error("Ошибка соединения с базой данных " + ex.Message);
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

        }
    }
}
