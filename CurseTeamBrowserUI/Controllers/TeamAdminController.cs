using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CurseTeamBrowserBL.Services;
using CurseTeamBrowserUI.Models;
using System.Web.Script.Serialization;

namespace CurseTeamBrowserUI.Controllers
{
    public class TeamAdminController : Controller
    {
        //
        // GET: /TeamAdmin/

        public ActionResult Index()
        {

            return View();
        }

        public ActionResult ListTeams() {
            try
            {
                var model = new TeamListModel();

                foreach (var team in TeamService.list()){
                    model.Teams.Add(new TeamModel()
                    {
                        Id = team.id,
                        Name = team.name,
                        Avatar = team.avatar,
                    });

                    foreach (var player in PlayerService.list(model.Teams.Last().Id))
                        model.Teams.Last().Roster.Add(new PlayerModel()
                        {
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
                }

                return Content(new JavaScriptSerializer().Serialize(model), "application/json");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult DeleteTeam(int id) { 

            try{
                TeamService.delete(id);

                return Content("Team Deleted");
            }catch(Exception ex){
                throw ex;
            }
        }

    }
}
