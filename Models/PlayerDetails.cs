using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IplPlayersClubDetailsApp.Models
{
    public class PlayerDetails
    {
        public virtual required Club Club { get; set; }
        public virtual required Player Player { get; set; }
    }
}
