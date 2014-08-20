using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CurseTeamBrowserBL.Services;
using CurseTeamBrowserUI.Models;
using CurseTeamBrowserBL.Models;

namespace CurseTeamBrowserUI.Controllers
{
    public class TeamController : Controller
    {

        public ActionResult Index()
        {

            var model = new TeamListModel();
            try {
                foreach (var team in TeamService.list()) {
                    model.Teams.Add(new TeamModel() {
                        Id = team.id,
                        Name = team.name,
                        Avatar = team.avatar,
                    });
                }
            }catch (Exception ex) {
                ModelState.AddModelError("", ex);
            }
            
           

            return View(model);
        }

        public ActionResult Details(int id) {

            var model = new TeamModel();
            try {
                var data = TeamService.find(id);

                if (data == null)
                   return RedirectToAction("Index");

                model.Id = data.id;
                model.Name = data.name;
                model.Avatar = data.avatar;

                foreach(var player in PlayerService.list(data.id))
                    model.Roster.Add(new PlayerModel() { 
                            Id = player.id,
                            Name = player.name,
                            Avatar = player.avatar,
                            GamesPlayed = player.games_played,
                            GamesWon = player.games_won,
                            Kills = player.kills,
                            Deaths = player.deaths,
                            Assists = player.assists,
                            IdTeam = player.id_team
                        });
            }catch (Exception ex) {
               throw ex;
            }

            return View(model);
        }
    }
}
