﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StartToBike.Models
{
    public class Training
    {
        /// <summary>
        /// Id of the training (PK)
        /// </summary>
        public int TrainingID { get; set; }
        /// <summary>
        /// The title of the training
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// The level of the training
        /// </summary>
        public int Level { get; set; }
        /// <summary>
        /// List of users that joined the training
        /// </summary>
        public ICollection<Account> Users { get; set; }

        internal bool AddUsersToTraining(List<Account> _users)
        {
            if (_users == null)
                throw new NotImplementedException("Users moeten worden opgegeven!");

            foreach (var user in _users)
            {
                if (!this.Users.Contains(user))
                {
                    this.Users.Add(user);
                }
            }
            return true;
        }
    }
}