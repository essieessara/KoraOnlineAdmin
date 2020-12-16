using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace KoraOnlineAdmin.Models
{
    [Table("team")]
    public partial class Team
    {
        public Team()
        {
            Goals = new HashSet<Goal>();
            News = new HashSet<News>();
            Players = new HashSet<Player>();
            Results = new HashSet<Result>();
        }

        [Key]
        [Column("team_id")]
        [Display(Name ="Team Id")]
        public int TeamId { get; set; }

        [Column("team_name")]
        [StringLength(50)]
        [Display(Name = "Team Name")]
        public string TeamName { get; set; }

        [Column("team_match_played")]
        [Display(Name = "#Matches")]
        public int? TeamMatchPlayed { get; set; }

        [Column("team_won")]
        [Display(Name = "#Wins")]
        public int? TeamWon { get; set; }

        [Column("team_draw")]
        [Display(Name = "#Draws")]
        public int? TeamDraw { get; set; }

        [Column("team_lost")]
        [Display(Name = "#Loses")]
        public int? TeamLost { get; set; }

        [Column("team_goal_for")]
        [Display(Name = "#Goal For")]
        public int? TeamGoalFor { get; set; }

        [Column("team_goal_ag")]
        [Display(Name = "#Goal Against")]
        public int? TeamGoalAg { get; set; }

        [Column("team_goal_on")]
        [Display(Name = "#Goal on")]
        public int? TeamGoalOn { get; set; }

        [Column("league_id")]
        [Display(Name = "League Name")]
        public int? LeagueId { get; set; }

        [Column("coach_id")]
        [Display(Name = "Couch Name")]
        public int? CoachId { get; set; }

        [Column("team_image")]
        [Display(Name = "Team Image")]
        [StringLength(500)]
        public string TeamImage { get; set; }

        [ForeignKey(nameof(CoachId))]
        [InverseProperty("Teams")]
        public virtual Coach Coach { get; set; }
        [ForeignKey(nameof(LeagueId))]
        [InverseProperty("Teams")]
        public virtual League League { get; set; }
        [InverseProperty(nameof(Goal.Team))]
        public virtual ICollection<Goal> Goals { get; set; }
        [InverseProperty("Team")]
        public virtual ICollection<News> News { get; set; }
        [InverseProperty(nameof(Player.Team))]
        public virtual ICollection<Player> Players { get; set; }
        [InverseProperty(nameof(Result.Team))]
        public virtual ICollection<Result> Results { get; set; }
    }
}
