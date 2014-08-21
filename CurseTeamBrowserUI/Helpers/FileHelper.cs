using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using CurseTeamBrowserUI.Models;
using System.Drawing;
using System.Drawing.Imaging;

namespace CurseTeamBrowserUI.Helpers
{
	public class FileHelper
	{
        public static bool isImage(HttpPostedFileBase file) {
            if (file.ContentType.ToLower() != "image/jpg" 
                && file.ContentType.ToLower() != "image/jpeg" 
                && file.ContentType.ToLower() != "image/pjpeg"
                && file.ContentType.ToLower() != "image/gif" 
                && file.ContentType.ToLower() != "image/x-png" 
                && file.ContentType.ToLower() != "image/png")
                return false;

            if (Path.GetExtension(file.FileName).ToLower() != ".jpg"
                && Path.GetExtension(file.FileName).ToLower() != ".png"
                && Path.GetExtension(file.FileName).ToLower() != ".gif"
                && Path.GetExtension(file.FileName).ToLower() != ".jpeg")
                return false;

            return true;
        }

        public static String getTeamImage(int teamId) {
           return "/Content/images/" + teamId.ToString() + "/logo.png";
        }

        public static String getPlayerImage(int teamId, int playerId)
        {
            return "/Content/images/" + teamId + "/" + playerId + ".png";
        }

        public static string saveTeamImage(int teamId, HttpPostedFileBase file) {
            if (isImage(file)){
                var path = HttpContext.Current.Server.MapPath("/Content/images/" + teamId);
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                var image = new Bitmap(file.InputStream);
                image.Save(Path.Combine(path, "logo.png"), ImageFormat.Png);
            }else {
                return "The selected file is not an image";
            }

            return ""; 
        }

        public static void deleteTeamImages(int teamId) { 
            var path = HttpContext.Current.Server.MapPath("/Content/images/" + teamId);
                if (Directory.Exists(path))
                    Directory.Delete(path, true);
        }

        public static string savePlayerImage(int playerId, int teamId, HttpPostedFileBase file)
        {
            if (isImage(file))
            {
                var path = HttpContext.Current.Server.MapPath("/Content/images/" + teamId);
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                var image = new Bitmap(file.InputStream);
                image.Save(Path.Combine(path + "/", playerId + ".png"), ImageFormat.Png);
            }
            else
            {
                return "The selected file is not an image";
            }

            return "";
        }

        public static void deletePlayerImage(int playerId, int teamId)
        {
            var path = HttpContext.Current.Server.MapPath("/Content/images/" + teamId);
            var file = Path.Combine(path + "/", playerId + ".png");
            if (File.Exists(file))
                File.Delete(file);
        }
	}
}