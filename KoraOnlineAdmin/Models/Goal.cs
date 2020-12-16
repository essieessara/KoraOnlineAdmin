using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace KoraOnlineAdmin.Models
{
    [Table("goals")]
    public partial class Goal
    {
        [Key]
        [Column("goal_id")]
        [Display(Name ="Goal Id")]
        public int GoalId { get; set; }

        [Display(Name = "Match Name")]
        [Column("match_id")]
        public int? MatchId { get; set; }

        [Display(Name = "Player Name")]
        [Column("player_id")]
        public int? PlayerId { get; set; }

        [Display(Name = "Team")]
        [Column("team_id")]
        public int? TeamId { get; set; }

        [ForeignKey(nameof(MatchId))]
        [InverseProperty("Goals")]
        public virtual Match Match { get; set; }
        [ForeignKey(nameof(PlayerId))]
        [InverseProperty("Goals")]
        public virtual Player Player { get; set; }
        [ForeignKey(nameof(TeamId))]
        [InverseProperty("Goals")]
        public virtual Team Team { get; set; }
    }
}
