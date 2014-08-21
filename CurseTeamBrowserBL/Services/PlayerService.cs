using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CurseTeamBrowserBL.Models;
using CurseTeamBrowserBL.DataAccess;

namespace CurseTeamBrowserBL.Services
{
    public class PlayerService
    {
        public static Player insert(string name, int gamesPlayed, int gamesWon, int kills, int deaths, int assists, int idTeam) {
            try{
                var context = new CurseDBDataContext();
                var player = new Player() {
                    name = name,
                    games_played = gamesPlayed,
                    games_won = gamesWon,
                    kills = kills,
                    deaths = deaths,
                    assists = assists,
                    id_team = idTeam
                };

                context.Players.InsertOnSubmit(player);
                context.SubmitChanges();

                return player;
            }catch(Exception ex){
                throw ex;
            }
        }

        public static Player update(int id, string name, int gamesPlayed, int gamesWon, int kills, int deaths, int assists)
        {
            try
            {
                var context = new CurseDBDataContext();
                var player = context.Players.Single(p => p.id == id);

                if (player != null) {
                    player.name = name;
                    player.games_played = gamesPlayed;
                    player.games_won = gamesWon;
                    player.kills = kills;
                    player.deaths = deaths;
                    player.assists = assists;
                    context.SubmitChanges();
                }

                return player;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void delete(int id)
        {
            try
            {
                var context = new CurseDBDataContext();
                var player = context.Players.Single(t => t.id == id);
                if (player != null) {
                    context.Players.DeleteOnSubmit(player);
                    context.SubmitChanges();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Player find(int playerId) {
            try
            {
                var context = new CurseDBDataContext();
                return context.Players.SingleOrDefault(p => p.id == playerId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<Player> list(int teamId)
        {
            try
            {
                var context = new CurseDBDataContext();
                var result = (from p in context.Players where p.id_team == teamId select p);
                return result.ToList<Player>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
