using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace KoraOnlineAdmin.Models
{
    [Table("news")]
    public partial class News
    {
        [Key]
        [Column("news_id")]
        [Display(Name ="News ID")]
        public int NewsId { get; set; }

        [Column("news_title")]
        [Display(Name = "News Title")]
        public string NewsTitle { get; set; }

        [Column("news_date", TypeName = "date")]
        [Display(Name = "News Data and Time")]
        public DateTime? NewsDate { get; set; }

        //this line here not working
        [Column("news_time")]
        public TimeSpan? NewsTime { get; set; }

        [Column("news_category_id")]
        [Display(Name = "Category Name")]
        public int? NewsCategoryId { get; set; }

        [Column("news_image")]
        [Display(Name = "News Image")]
        public string NewsImage { get; set; }

        [Column("news_writer")]
        [StringLength(50)]
        [Display(Name = "News Writer")]
        public string NewsWriter { get; set; }

        [Column("news_description")]
        [Display(Name = "News Description")]
        public string NewsDescription { get; set; }

        [Column("coach_id")]
        [Display(Name = "Coach Name")]
        public int? CoachId { get; set; }

        [Column("league_id")]
        [Display(Name = "League Name")]
        public int? LeagueId { get; set; }

        [Column("team_id")]
        [Display(Name = "Team Name")]
        public int? TeamId { get; set; }

        [Column("stad_id")]
        [Display(Name = "Stadium Name")]
        public int? StadId { get; set; }

        [Column("match_id")]
        public int? MatchId { get; set; }

        [Column("player_id")]
        [Display(Name = "Player Name")]
        public int? PlayerId { get; set; }

        [ForeignKey(nameof(LeagueId))]
        [InverseProperty("News")]
        public virtual League League { get; set; }

        [ForeignKey(nameof(MatchId))]
        [InverseProperty("News")]
        public virtual Match Match { get; set; }

        [ForeignKey(nameof(NewsCategoryId))]
        [InverseProperty(nameof(Category.News))]
        public virtual Category NewsCategory { get; set; }

        [ForeignKey(nameof(PlayerId))]
        [InverseProperty("News")]
        public virtual Player Player { get; set; }

        [ForeignKey(nameof(StadId))]
        [InverseProperty(nameof(Stadium.News))]
        public virtual Stadium Stad { get; set; }

        [ForeignKey(nameof(TeamId))]
        [InverseProperty("News")]
        public virtual Team Team { get; set; }
    }
}
