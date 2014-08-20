using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CurseTeamBrowserUI.Models
{
    public class PlayerModel
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String Avatar { get; set; }
        public int GamesPlayed { get; set; }
        public int GamesWon { get; set; }
        public int Kills { get; set; }
        public int Deaths { get; set; }
        public int Assists { get; set; }
        public int IdTeam { get; set; }
    }
}