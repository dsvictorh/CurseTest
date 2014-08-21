using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CurseTeamBrowserBL.Services;
using CurseTeamBrowserUI.Models;
using CurseTeamBrowserUI.Helpers;
using System.Web.Script.Serialization;

namespace CurseTeamBrowserUI.Controllers
{
    public class PlayerAdminController : Controller
    {

        public ContentResult ListPlayers(int idTeam)
        {
            try
            {
                var model = new List<PlayerModel>();

                foreach (var player in PlayerService.list(idTeam))
                {
                    model.Add(new PlayerModel()
                    {
                        Id = player.id,
                        Name = player.name,
                        GamesPlayed = player.games_played,
                        GamesWon = player.games_won,
                        Kills = player.kills,
                        Deaths = player.deaths,
                        Assists = player.assists,
                        Avatar = FileHelper.getPlayerImage(player.id_team, player.id),
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

        public ContentResult DeletePlayer(int id, int idTeam)
        {
            try
            {
                PlayerService.delete(id);
                FileHelper.deletePlayerImage(id, idTeam);

                return Content("Team Deleted");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ContentResult SavePlayer(PlayerModel player)
        {
            try
            {
                var error = "";

                if (!ModelState.IsValid)
                {
                    foreach (var property in ModelState.Values)
                    {
                        if (property.Errors.Count() > 0)
                        {
                            error = property.Errors[0].ErrorMessage;
                            break;
                        }
                    }
                }
                else
                {
                    if (player.Id != null)
                    {
                        PlayerService.update(player.Id.Value, player.Name, player.GamesPlayed, player.GamesWon, player.Kills, player.Deaths, player.Assists);
                        if (player.ImageUpload != null)
                            error = FileHelper.savePlayerImage(player.Id.Value, player.IdTeam, player.ImageUpload);
                    }
                    else
                    {
                        if (player.ImageUpload == null)
                            error = "No Avatar Image Selected";
                        else
                        {
                            var result = PlayerService.insert(player.Name, player.GamesPlayed, player.GamesWon, player.Kills, player.Deaths, player.Assists, player.IdTeam);
                            error = FileHelper.savePlayerImage(result.id, result.id_team, player.ImageUpload);
                        }
                    }
                }

                return Content(new JavaScriptSerializer().Serialize(error), "application/json");

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
