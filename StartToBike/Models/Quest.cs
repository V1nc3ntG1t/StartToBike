namespace StartToBike.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Quest")]
    public partial class Quest
    {
        public Quest()
        {
            Account = new HashSet<Account>();
            Tour = new HashSet<Tour>();
        }

        public int QuestId { get; set; }

        public int QuestLevel { get; set; }

        public string QuestTask { get; set; }

        public string Status { get; set; }

        public virtual ICollection<Account> Account { get; set; }

        public virtual ICollection<Tour> Tour { get; set; }


        public Boolean CreateQuest()
        {
            Status = "open";

            return true;
        }

        public Boolean QuestCompleted()
        {
            if (Status == "Quest Completed")
            {
                return false;
            }

            Status = "Quest Completed";
            return true;
        }
    }
}
