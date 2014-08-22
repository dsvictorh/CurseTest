using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CurseTeamBrowserBL.Services;
using CurseTeamBrowserUI.Models;
using CurseTeamBrowserUI.Helpers;

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
                        Avatar = FileHelper.getTeamImage(team.id),
                    });
                }
            }catch (Exception ex) {
                return Redirect("/Team/Error?error=" + ex.Message);
            }
            
           

            return View(model);
        }

        public ActionResult Details(int id) {

            var model = new TeamModel();
            try {
                var team = TeamService.find(id);

                if (team == null)
                   return RedirectToAction("Index");

                model.Id = team.id;
                model.Name = team.name;
                model.Avatar = FileHelper.getTeamImage(team.id);

                foreach(var player in PlayerService.list(team.id))
                    model.Roster.Add(new PlayerModel() { 
                            Id = player.id,
                            Name = player.name,
                            Avatar = FileHelper.getPlayerImage(player.id_team, player.id),
                            GamesPlayed = player.games_played,
                            GamesWon = player.games_won,
                            Kills = player.kills,
                            Deaths = player.deaths,
                            Assists = player.assists,
                            IdTeam = player.id_team
                        });
            }catch (Exception ex) {
                return Redirect("/Team/Error?error=" + ex.Message);
            }

            return View(model);
        }

        public ActionResult Error(String error) {
            var model = new ErrorModel(error);
            return View(model);
        }
    }
}
