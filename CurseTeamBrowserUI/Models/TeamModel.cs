using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CurseTeamBrowserBL.Models;

namespace CurseTeamBrowserUI.Models
{
    public class TeamModel
    {

        public TeamModel() {
            Roster = new List<PlayerModel>();
        }

        public int Id { get; set; }
        public String Name { get; set; }
        public String Avatar { get; set; }
        public List<PlayerModel> Roster { get; set; }
    }
}