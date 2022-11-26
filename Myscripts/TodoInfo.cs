using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkToDo.Myscripts
{
    internal class TodoInfo
    {
        public string UUID { get; set; }    
        public string Content { get; set; }
        public string Date { get; set; }
        public int Priority { get; set; }
        public int IsDone { get; set; }
        public string Teammate { get; set; }
        public TodoInfo(string uUID,string content,string date,int priority,int isDone,string teammate)
        {
            UUID = uUID;
            Content= content;
            Date = date;
            Priority = priority;
            IsDone = isDone;
            Teammate = teammate;
        }

    }
}
