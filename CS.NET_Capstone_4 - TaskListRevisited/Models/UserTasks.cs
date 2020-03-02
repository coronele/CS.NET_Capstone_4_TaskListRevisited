using System;
using System.Collections.Generic;

namespace CS.NET_Capstone_4___TaskListRevisited.Models
{
    public partial class UserTasks
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime? DueDate { get; set; }
        public bool? Complete { get; set; }
        public string OwnerId { get; set; }

        public virtual AspNetUsers Owner { get; set; }

        public string StatusString(bool? complete)
        {
            if (complete==true)
            {
                return "Completed";
            }
            else
            {
                return "Incomplete";
            }
        }

        public string DateToString(DateTime? taskDueDate)
        {
            return String.Format("{0: dddd, MMMM dd, yyyy}",(DateTime)taskDueDate);
        }

    }
}
