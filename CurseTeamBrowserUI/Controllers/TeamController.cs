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
                model.Teams = TeamService.list();
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
                model.Roster = PlayerService.list(data.id);
            }
            catch (Exception ex) {
               throw ex;
            }

            return View(model);
        }
    }
}
