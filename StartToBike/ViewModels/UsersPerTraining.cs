using StartToBike.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StartToBike.ViewModels
{
    public class UsersPerTraining
    {
        public Training Training { get; set; }

        public ICollection<Account> Users { get; set; }

        public static Training TrainingLoaded;
    }
}