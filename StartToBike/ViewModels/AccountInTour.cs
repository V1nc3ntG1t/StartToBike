using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StartToBike.Models;

namespace StartToBike.ViewModels
{
    public class AccountInTour
    {

        public Tour Tour { get; set; }

        public IEnumerable<Account> Players { get; set; }

        public static Tour GameLoaded;
    }
}