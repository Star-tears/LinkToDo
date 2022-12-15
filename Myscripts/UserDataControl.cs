using HandyControl.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LinkToDo.Myscripts
{
    internal class UserDataControl
    {
        private MysqlBase mysqlBase;
        public UserDataControl()
        {
            mysqlBase = new MysqlBase();
        }

        public void insertUserInfo(UserInfo userInfo)
        {
            string sql = "INSERT INTO userinfo (uuid,name,phone,email,imgpath) VALUES ('" + userInfo.UUID + "','" + userInfo.Name + "','" + userInfo.PhoneNum + "','" + userInfo.Email + "','" + userInfo.ImgPath + "')";
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
        public void updateUserInfo(UserInfo userInfo)
        {
            string sql = "UPDATE userinfo SET name='" + userInfo.Name + "',phone='" + userInfo.PhoneNum + "',email='" + userInfo.Email + "',imgpath='" + userInfo.ImgPath + "' where uuid='" + userInfo.UUID+"'";
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
        public void deleteUserInfo(UserInfo userInfo)
        {
            string sql = "DELETE FROM userinfo WHERE uuid='" + userInfo.UUID+"'";
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
        public DataTable queryUserInfo()
        {
            string sql = "SELECT uuid,name,phone,email,imgpath FROM userinfo";
            DataSet ds = mysqlBase.GetDataSet(sql, "userinfo");
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
