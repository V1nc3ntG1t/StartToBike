namespace StartToBike.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Friend")]
    public partial class Friend
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Friend1Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Friend2Id { get; set; }

        [Key]
        [Column(Order = 2)]
        public string StartDate { get; set; }

        public virtual Account Account { get; set; }

        public virtual Account Account1 { get; set; }

        public Boolean CreateFriendship()
        {
            ///<summary>
            ///sets localDate from this moment
            /// </summary>
            DateTime localDate = DateTime.Now;
            StartDate = Convert.ToString(localDate);

            ///<summary>
            ///gets the AccountId from the user who logged in (friend1)
            /// </summary>
            /// 
            Account logInAccount = Account.LogInAccount;
            Friend1Id = logInAccount.AccountId;

            if (Friend2Id == Friend1Id)
            {
                return false;
            }

            return true;
        }
    }
}
