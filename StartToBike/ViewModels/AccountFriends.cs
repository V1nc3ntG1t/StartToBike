using StartToBike.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StartToBike.ViewModels
{
    public class AccountFriends
    {
        /// <summary>
        /// The Account
        /// </summary>
        public Account Account { get; set; }
        
        /// <summary>
        /// The friends from the account
        /// </summary>
        public IEnumerable<Account> Friends { get; set; }
    }
}