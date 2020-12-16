using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace KoraOnlineAdmin.Models
{
    [Table("result")]
    public partial class Result
    {
        [Key]
        [Column("match_id")]
        [Display(Name ="Match Id")]
        public int MatchId { get; set; }

        [Key]
        [Column("team_id")]
        [Display(Name ="Team Name")]
        public int TeamId { get; set; }

        [Column("result")]
        [StringLength(50)]
        [Display(Name = "Result")]
        public string Result1 { get; set; }


        [Column("M_red_card")]
        [Display(Name = "#Red Card")]
        public int? MRedCard { get; set; }

        [Column("M_yel_card")]
        [Display(Name = "#Yellow Card")]
        public int? MYelCard { get; set; }

        [Column("M_goals")]
        [Display(Name = "#Goals")]
        public int? MGoals { get; set; }

        [ForeignKey(nameof(MatchId))]
        [InverseProperty("Results")]
        public virtual Match Match { get; set; }
        [ForeignKey(nameof(TeamId))]
        [InverseProperty("Results")]
        public virtual Team Team { get; set; }
    }
}
