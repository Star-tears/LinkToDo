using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkToDo.Myscripts
{
    public class TodoInfo : IComparable<TodoInfo>
    {
        public string UUID { get; set; }    
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public int Priority { get; set; }
        public int IsDone { get; set; }
        public string Teammate { get; set; }
        public TodoInfo(string uUID,string content, DateTime date,int priority,int isDone,string teammate)
        {
            UUID = uUID;
            Content= content;
            Date = date;
            Priority = priority;
            IsDone = isDone;
            Teammate = teammate;
        }

        public int CompareTo(TodoInfo other)
        {
            if (Priority!=other.Priority)
            {
                return Priority.CompareTo(other.Priority);
            }
            return other.Date.CompareTo(Date);
        }
    }
}
