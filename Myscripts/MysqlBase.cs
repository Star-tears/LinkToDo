using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkToDo.Myscripts
{
    internal class MysqlBase
    {
        private MySqlConnection conn = null;
        private MySqlCommand command = null;
        private MySqlDataReader reader = null;
        
        /// <summary>
        /// 构造方法里建议连接
        /// </summary>
        /// <param name="connstr"></param>
        public MysqlBase()
        {
            string connstr = "Database=" + Settings.DatbaseName + ";Data Source="+Settings.DatabaseHost+";User Id="+Settings.DatabaseUsername+";Password="+Settings.DatabasePassword+";pooling=false;CharSet=utf8;port="+Settings.DatabasePort+";";
            conn = new MySqlConnection(connstr);
        }
        /// <summary>
        /// 检测数据库连接状态
        /// </summary>
        /// <returns></returns>
        public bool CheckConnectStatus()
        {
            bool result = false;
            try
            {
                conn.Open();
                if (conn.State == ConnectionState.Open)
                {
                    result = true;
                }

            }
            catch
            {
                result = false;
            }
            finally
            {
                conn.Close();
            }
            return result;
        }
        /// <summary>
        /// 增、删、改公共方法
        /// </summary>
        /// <returns></returns>
        public int commonExecute(string sql)
        {
            int res = -1;
            try
            {
                //当连接处于打开状态时关闭,然后再打开,避免有时候数据不能及时更新
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                conn.Open();
                command = new MySqlCommand(sql, conn);
                res = command.ExecuteNonQuery();
            }
            catch (MySqlException)
            {
            }
            conn.Close();
            return res;
        }
        /// <summary>
        /// 查询方法
        /// 注意：尽量不要用select * from table表（返回的数据过长时，DataTable可能会出错），最好指定要查询的字段。
        /// </summary>
        /// <returns></returns>
        public DataTable query(string sql)
        {
            //当连接处于打开状态时关闭,然后再打开,避免有时候数据不能及时更新
            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.Open();
            command = new MySqlCommand(sql, conn);
            DataTable dt = new DataTable();
            using (reader = command.ExecuteReader(CommandBehavior.CloseConnection))
            {
                dt.Load(reader);
            }
            return dt;
        }
        /// <summary>
        /// 获取DataSet数据集
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="tablename"></param>
        /// <returns></returns>
        public DataSet GetDataSet(string sql, string tablename)
        {
            //当连接处于打开状态时关闭,然后再打开,避免有时候数据不能及时更新
            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.Open();
            command = new MySqlCommand(sql, conn);
            DataSet dataset = new DataSet();
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            adapter.Fill(dataset, tablename);
            conn.Close();
            return dataset;
        }
        /// <summary>
        /// 实现多SQL语句执行的数据库事务
        /// </summary>
        /// <param name="SQLStringList">SQL语句集合（多条语句）</param>
        public bool ExecuteSqlTran(List<string> SQLStringList)
        {
            bool flag = false;
            //当连接处于打开状态时关闭,然后再打开,避免有时候数据不能及时更新
            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            //开启事务
            MySqlTransaction tran = this.conn.BeginTransaction();
            cmd.Transaction = tran;//将事务应用于CMD
            try
            {
                foreach (string strsql in SQLStringList)
                {
                    if (strsql.Trim() != "")
                    {
                        cmd.CommandText = strsql;
                        cmd.ExecuteNonQuery();
                    }
                }
                tran.Commit();//提交事务（不提交不会回滚错误）
                flag = true;
            }
            catch (Exception)
            {
                tran.Rollback();
                flag = false;
            }
            finally
            {
                conn.Close();
            }
            return flag;
        }
    }
}
