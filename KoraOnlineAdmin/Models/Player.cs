using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace KoraOnlineAdmin.Models
{
    [Table("player")]
    public partial class Player
    {
        public Player()
        {
            Goals = new HashSet<Goal>();
            News = new HashSet<News>();
            Punishments = new HashSet<Punishment>();
        }

        [Key]
        [Column("player_id")]
        [Display (Name ="Player Id")]
        public int PlayerId { get; set; }

        [Column("player_name")]
        [StringLength(50)]
        [Display(Name = "Player Name")]
        public string PlayerName { get; set; }

        [Column("player_position")]
        [StringLength(50)]
        [Display(Name = "Player Position")]
        public string PlayerPosition { get; set; }

        [Column("team_id")]
        [Display(Name = "Team Name")]
        public int? TeamId { get; set; }

        [Column("player_goals")]
        [Display(Name = "#Goals")]
        public int? PlayerGoals { get; set; }

        [Column("player_image")]
        [Display(Name = "Player Image")]
        public string PlayerImage { get; set; }

        [ForeignKey(nameof(TeamId))]
        [InverseProperty("Players")]
        public virtual Team Team { get; set; }
        [InverseProperty(nameof(Goal.Player))]
        public virtual ICollection<Goal> Goals { get; set; }
        [InverseProperty("Player")]
        public virtual ICollection<News> News { get; set; }
        [InverseProperty(nameof(Punishment.Player))]
        public virtual ICollection<Punishment> Punishments { get; set; }
    }
}
