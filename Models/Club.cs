
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace   IplPlayersClubDetailsApp.Models
{
    public partial class Club
    {
        public Club()
        {
            Players = new HashSet<Player>();
        }

        public int ClubId { get; set; }

        //[RegularExpression("^([a-zA-Z]+)$", ErrorMessage = " Please Enter Characters only")]
        [Required(ErrorMessage = "Club Name is required")]
        [StringLength(20, MinimumLength = 3)]
        public string Name { get; set; }

        public virtual ICollection<Player> Players { get; set; }
    }
}


