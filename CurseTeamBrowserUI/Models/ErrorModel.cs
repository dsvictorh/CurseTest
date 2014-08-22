using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CurseTeamBrowserUI.Models
{
    public class ErrorModel
    {
        public ErrorModel(String errorMessage) {
            this.ErrorMessage = errorMessage;
        }

        public String ErrorMessage { get; set; }
    }
}