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
        public static void insert(Player player) {
            try{
                var context = new CurseDBDataContext();
                context.Players.InsertOnSubmit(player);
                context.SubmitChanges();
            }catch(Exception ex){
                throw ex;
            }
        }

        public static void update(Player player)
        {
            try
            {
                var context = new CurseDBDataContext();
                var update = context.Players.Single(p => p.id == player.id);
                update.name = player.name;
                update.avatar = player.avatar;
                update.games_played = player.games_played;
                update.games_won = player.games_won;
                update.kills = player.kills;
                update.deaths = player.deaths;
                update.assists = player.assists;
                context.SubmitChanges();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void delete(Player player)
        {
            try
            {
                var context = new CurseDBDataContext();
                context.Players.DeleteOnSubmit(player);
                context.SubmitChanges();

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
