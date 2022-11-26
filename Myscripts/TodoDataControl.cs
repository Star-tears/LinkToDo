using System;
using System.Collections.Generic;
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
    }
}
