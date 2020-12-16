using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace KoraOnlineAdmin.Models
{
    [Table("punishment")]
    public partial class Punishment
    {
        [Key]
        [Column("punish_id")]
        [Display(Name ="Punishment Id")]
        public int PunishId { get; set; }

        [Column("punish_type")]
        [StringLength(50)]
        [Display(Name = "Punishment Type")]
        public string PunishType { get; set; }

        [Column("player_id")]
        [Display(Name = "Player Name")]
        public int? PlayerId { get; set; }

        [ForeignKey(nameof(PlayerId))]
        [InverseProperty("Punishments")]
        public virtual Player Player { get; set; }
    }
}
