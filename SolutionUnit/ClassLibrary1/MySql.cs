using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class MySQL
    {
        private MySqlConnection conn;
        bool status;
        
        public MySQL()
        {
            status = GetConnection();
        }

        private bool GetConnection()
        {
            try
            {
                string server = "192.168.3.155";
                string user = "root";
                string passwd = "1234";
                string database = "gdc";

                string strConn = string.Format("server={0};user={1};password={2};database={3}", server, user, passwd, database);

                conn = new MySqlConnection(strConn);
                conn.Open();
                Console.WriteLine("DB 연결 성공");
                return true;
            }
            catch
            {
                conn.Close();
                Console.WriteLine("DB 연결 실패");
                return false;
            }
        }


        public MySqlDataReader Reader(string sql)
        {
            if (status)
            {
                try
                {
                    MySqlCommand comm = new MySqlCommand();
                    comm.Connection = conn;
                    comm.CommandText = sql;

                    return comm.ExecuteReader();

                }
                catch
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public int NonQuery(string sql)
        {
            Console.WriteLine(status);
            if (status)
            {
                try
                {
                    MySqlCommand comm = new MySqlCommand();
                    comm.Connection = conn;
                    comm.CommandText = sql;

                    return comm.ExecuteNonQuery();
                }
                catch
                {
                    Console.WriteLine("논쿼리문 실패...");
                    return -1;
                }
            }
            else
            {
                return -1;
            }
        }

        public bool ReaderClose(MySqlDataReader sdr)
        {
            try
            {
                sdr.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Close()
        {
            if (status)
            {
                try
                {
                    conn.Close();
                }
                catch
                {
                    Console.WriteLine("접속 해제 실패...");
                }
            }
            else
            {
                Console.WriteLine("컨넥션 없음...");
            }
        }
    }
}
