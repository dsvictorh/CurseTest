using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DataAnnotationsExtensions;
using System.Linq;
using System.Web;

namespace CurseTeamBrowserUI.Models
{
    public class PlayerModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessage="Player's name is required")]
        [DataType(DataType.Text, ErrorMessage = "Player name is a text field")]
        [StringLength(25, ErrorMessage = "Player's name cannot contain more than 25 characters")]
        public String Name { get; set; }
        
        public String Avatar { get; set; }

        [Required(ErrorMessage="Player's games played is required")]
        [Integer(ErrorMessage="Player's games played is an number field")]
        [Range(0, System.Int32.MaxValue, ErrorMessage="Player's games played cannot be less than 0")]
        public int GamesPlayed { get; set; }

        [Required(ErrorMessage = "Player's games won is required")]
        [Integer(ErrorMessage = "Player's games won is an number field")]
        [Range(0, System.Int32.MaxValue, ErrorMessage = "Player's games won cannot be less than 0")]
        public int GamesWon { get; set; }

        [Required(ErrorMessage = "Player's kills is required")]
        [Integer(ErrorMessage = "Player's kills is an number field")]
        [Range(0, System.Int32.MaxValue, ErrorMessage = "Player's kills cannot be less than 0")]
        public int Kills { get; set; }

        [Required(ErrorMessage = "Player's deaths is required")]
        [Integer(ErrorMessage = "Player's deaths is an number field")]
        [Range(0, System.Int32.MaxValue, ErrorMessage = "Player's deaths cannot be less than 0")]
        public int Deaths { get; set; }

        [Required(ErrorMessage = "Player's assists is required")]
        [Integer(ErrorMessage = "Player's assists is an number field")]
        [Range(0, System.Int32.MaxValue, ErrorMessage = "Player's assists cannot be less than 0")]
        public int Assists { get; set; }

        [Required(ErrorMessage = "Team's Id is required. Corrupted Data")]
        [Integer(ErrorMessage = "Team's Id must be a number. Corrupted Data")]
        public int IdTeam { get; set; }

        public HttpPostedFileBase ImageUpload { get; set; }
    }
}