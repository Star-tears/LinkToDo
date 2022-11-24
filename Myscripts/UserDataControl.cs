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
                MessageBox.Show("更新成功");
            }
            else
            {
                MessageBox.Show("更新失败");
            }
        }
        public void updateUserInfo(UserInfo userInfo)
        {
            string sql = "UPDATE userinfo SET name='" + userInfo.Name + "',phone='" + userInfo.PhoneNum + "',email='" + userInfo.Email + "',imgpath='" + userInfo.ImgPath + "' where uuid='" + userInfo.UUID+"'";
            int res = mysqlBase.commonExecute(sql);
            if (res > 0)
            {
                MessageBox.Show("更新成功");
            }
            else
            {
                MessageBox.Show("更新失败");
            }
        }
        public void deleteUserInfo(UserInfo userInfo)
        {
            string sql = "DELETE FROM userinfo WHERE uuid='" + userInfo.UUID+"'";
            int res = mysqlBase.commonExecute(sql);
            if (res > 0)
            {
                MessageBox.Show("更新成功");
            }
            else
            {
                MessageBox.Show("更新失败");
            }
        }
        public DataTable queryUserInfo()
        {
            string sql = "SELECT * FROM userinfo";
            DataSet ds = mysqlBase.GetDataSet(sql, "userinfo");
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count != 0)
            {
                string uuid = (string)dt.Rows[0][0];
                string name = (string)dt.Rows[0][1];
                return dt;
            }
            else
            {
                MessageBox.Show("无数据");
            }
            return dt;
        }
    }
}
