using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace KoraOnlineAdmin.Models
{
    [Table("league")]
    public partial class League
    {
        public League()
        {
            Matches = new HashSet<Match>();
            News = new HashSet<News>();
            Teams = new HashSet<Team>();
        }

        [Key]
        [Column("league_id")]
        [Display(Name ="League Id")]
        public int LeagueId { get; set; }

        [Column("league_name")]
        [StringLength(50)]
        [Display(Name = "League Name")]
        public string LeagueName { get; set; }

        [Column("league_image")]
        [Display(Name = "League Image")]

        public string LeagueImage { get; set; }

        [InverseProperty(nameof(Match.League))]
        public virtual ICollection<Match> Matches { get; set; }
        [InverseProperty("League")]
        public virtual ICollection<News> News { get; set; }
        [InverseProperty(nameof(Team.League))]
        public virtual ICollection<Team> Teams { get; set; }
    }
}
