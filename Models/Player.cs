using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace IplPlayersClubDetailsApp.Models
{
    public partial class Player
    {
        public int PlayerId { get; set; }

        [RegularExpression("^([a-zA-Z ]+)$", ErrorMessage = "Please Enter Characters only")]
        [Required(ErrorMessage = "First Name is required")]
        [StringLength(20, MinimumLength = 3)]
        public string FirstName { get; set; }

        [RegularExpression("^([a-zA-Z ]+)$", ErrorMessage = "Please Enter Characters only")]
        [Required(ErrorMessage = "Last Name is required")]
        [StringLength(20, MinimumLength = 3)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "ContactNo is required")]
        public long ContactNo { get; set; }

        [Required(ErrorMessage = "Date of Joining is required")]
        public DateTime DateOfJoining { get; set; }
        public int? ClubId { get; set; }

        public virtual Club Club { get; set; }
    }
}
