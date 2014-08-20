using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CurseTeamBrowserBL.Models;
using CurseTeamBrowserBL.DataAccess;

namespace CurseTeamBrowserBL.Services
{
    public class TeamService
    {
        public static void insert(String name, String avatar)
        {
            try{
                var context = new CurseDBDataContext();
                var team = new Team() { 
                    name = name,
                    avatar = avatar
                };
                context.Teams.InsertOnSubmit(team);
                context.SubmitChanges();
            }catch(Exception ex){
                throw ex;
            }
        }

        public static void update(int id, String name, String avatar)
        {
            try
            {
                var context = new CurseDBDataContext();
                var team = context.Teams.Single(t => t.id == id);
                if (team != null){
                    team.name = name;
                    team.avatar = avatar;
                    context.SubmitChanges();
                }

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
                var team = context.Teams.Single(t => t.id == id);
                if(team != null){
                    context.Teams.DeleteOnSubmit(team);
                    context.SubmitChanges();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Team find(int teamId) {
            try
            {
                var context = new CurseDBDataContext();
                return context.Teams.SingleOrDefault(t => t.id == teamId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<Team> list()
        {
            try
            {
                var context = new CurseDBDataContext();
                var result = (from t in context.Teams select t);
                return result.ToList<Team>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
