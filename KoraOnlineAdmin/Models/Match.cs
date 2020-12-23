using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace KoraOnlineAdmin.Models
{
    [Table("match")]
    public partial class Match
    {
        public Match()
        {
            Goals = new HashSet<Goal>();
            News = new HashSet<News>();
            Results = new HashSet<Result>();
        }

        [Key]
        [Column("match_id")]
        [Display(Name ="Match ID")]
        public int MatchId { get; set; }

        [Display(Name = "Match Date and Time")]
        [Column("match_date", TypeName = "date")]
        public DateTime? MatchDate { get; set; }

        [Column("match_time")]
        public TimeSpan? MatchTime { get; set; }

        [Display(Name = "Match Status")]
        [Column("match_status")]
        [StringLength(50)]
        public string MatchStatus { get; set; }

        [Display(Name = "Staduim Name")]
        [Column("staduim_id")]
        public int? StaduimId { get; set; }

        [Display(Name = "League")]
        [Column("league_id")]
        public int? LeagueId { get; set; }

        [Display(Name = "Team #1")]
        [Column("match_team_name1")]
        [StringLength(50)]
        public string MatchTeamName1 { get; set; }

        [Display(Name = "Team #2")]
        [Column("match_team_name2")]
        [StringLength(50)]
        public string MatchTeamName2 { get; set; }

        [Display(Name = "Week")]
        [Column("match_week")]
        [StringLength(50)]
        public string MatchWeek { get; set; }

        [Display(Name = "Team #1 Logo")]
        [Column("match_team_result1")]
        [StringLength(50)]
        public string MatchTeamResult1 { get; set; }

        [Display(Name = "Team #2 Logo")]
        [Column("match_team_result2")]
        [StringLength(50)]
        public string MatchTeamResult2 { get; set; }

        [ForeignKey(nameof(LeagueId))]
        [InverseProperty("Matches")]
        public virtual League League { get; set; }
        [ForeignKey(nameof(StaduimId))]
        [InverseProperty(nameof(Stadium.Matches))]
        public virtual Stadium Staduim { get; set; }
        [InverseProperty(nameof(Goal.Match))]
        public virtual ICollection<Goal> Goals { get; set; }
        [InverseProperty("Match")]
        public virtual ICollection<News> News { get; set; }
        [InverseProperty(nameof(Result.Match))]
        public virtual ICollection<Result> Results { get; set; }
    }
}
