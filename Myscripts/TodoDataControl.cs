using HandyControl.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkToDo.Myscripts
{
    internal class TodoDataControl
    {
        private MysqlBase mysqlBase;
        public TodoDataControl()
        {
            mysqlBase = new MysqlBase();
        }
        public void insertTodoInfo(TodoInfo todoInfo)
        {
            string sql = "INSERT INTO todoinfo (uuid,content,date,priority,isdone,teammate) VALUES ('" + todoInfo.UUID + "','" + todoInfo.Content + "','" + todoInfo.Date + "','" + todoInfo.Priority + "','" + todoInfo.IsDone +"','"+todoInfo.Teammate+ "')";
            int res = mysqlBase.commonExecute(sql);
            if (res > 0)
            {
                Growl.Success("云端数据添加成功！");
            }
            else
            {
                Growl.Warning("云端数据添加失败！");
            }
        }
        public void updateTodoInfo(TodoInfo todoInfo)
        {
            string sql = "UPDATE todoinfo SET content='" + todoInfo.Content + "',date='" + todoInfo.Date.ToString("yyyy-MM-dd HH:mm:ss") + "',priority='" + todoInfo.Priority + "',isdone='" + todoInfo.IsDone +"',teammate='"+ todoInfo.Teammate + "' where uuid='" + todoInfo.UUID + "'";
            int res = mysqlBase.commonExecute(sql);
            if (res > 0)
            {
                Growl.Success("云端数据更新成功！");
            }
            else
            {
                Growl.Warning("云端数据更新失败！");
            }
        }
        public void deleteTodoInfo(TodoInfo todoInfo)
        {
            string sql = "DELETE FROM todoinfo WHERE uuid='" + todoInfo.UUID + "'";
            int res = mysqlBase.commonExecute(sql);
            if (res > 0)
            {
                Growl.Success("云端数据删除成功！");
            }
            else
            {
                Growl.Warning("云端数据删除失败！");
            }
        }
        public DataTable queryTodoInfo()
        {
            string sql = "SELECT uuid,content,date,priority,isdone,teammate FROM todoinfo ORDER BY priority DESC,date";
            DataSet ds = mysqlBase.GetDataSet(sql, "todoinfo");
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count != 0)
            {
                Growl.Success("云端数据拉取成功");
                return dt;
            }
            else
            {
                Growl.Info("暂无数据");
            }
            return dt;
        }
    }
}
