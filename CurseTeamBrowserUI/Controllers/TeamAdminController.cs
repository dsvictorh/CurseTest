using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CurseTeamBrowserBL.Services;
using CurseTeamBrowserUI.Models;
using System.Web.Script.Serialization;
using CurseTeamBrowserUI.Helpers;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

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

        public ContentResult ListTeams() {
            try
            {
                var model = new TeamListModel();

                foreach (var team in TeamService.list()){
                    model.Teams.Add(new TeamModel()
                    {
                        Id = team.id,
                        Name = team.name,
                        Avatar = FileHelper.getTeamImage(team.id),
                    });

                    foreach (var player in PlayerService.list(model.Teams.Last().Id.Value))
                        model.Teams.Last().Roster.Add(new PlayerModel()
                        {
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
                }

                return Content(new JavaScriptSerializer().Serialize(model), "application/json");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ContentResult DeleteTeam(int id) { 
            try{
                TeamService.delete(id);
                FileHelper.deleteTeamImages(id);

                return Content("Team Deleted");
            }catch(Exception ex){
                throw ex;
            }
        }

        public ContentResult SaveTeam(TeamModel team) {
            try
            {
                var error = "";

                if (!ModelState.IsValid){
                    foreach (var property in ModelState.Values) {
                        if (property.Errors.Count() > 0){
                            error = property.Errors[0].ErrorMessage;
                            break;
                        }
                    }
                }else{
                    if (team.Id != null){
                        TeamService.update(team.Id.Value, team.Name);
                        if(team.ImageUpload != null)
                            error = FileHelper.saveTeamImage(team.Id.Value, team.ImageUpload);
                    }
                    else {
                        if (team.ImageUpload == null)
                            error = "No Avatar Image Selected";
                        else {
                            var result = TeamService.insert(team.Name);
                            error = FileHelper.saveTeamImage(result.id, team.ImageUpload);
                        }
                    }
                }

                return Content(new JavaScriptSerializer().Serialize(error), "application/json");

            } catch (Exception ex) {
                throw ex;
            }
        }
    }
}
