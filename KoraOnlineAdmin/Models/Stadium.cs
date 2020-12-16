using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace KoraOnlineAdmin.Models
{
    [Table("stadium")]
    public partial class Stadium
    {
        public Stadium()
        {
            Matches = new HashSet<Match>();
            News = new HashSet<News>();
        }

        [Key]
        [Column("stadium_id")]
        [Display (Name ="Stadium Id")]
        public int StadiumId { get; set; }

        [Column("stadium_name")]
        [StringLength(50)]
        [Display(Name = "Stadium Name")]
        public string StadiumName { get; set; }

        [Column("satdium_capacity")]
        [Display(Name = "Stadium Capacity")]
        public int? SatdiumCapacity { get; set; }

        [Column("city_id")]
        [Display(Name = "city Name")]
        public int? CityId { get; set; }

        [ForeignKey(nameof(CityId))]
        [InverseProperty("Stadia")]
        public virtual City City { get; set; }
        [InverseProperty(nameof(Match.Staduim))]
        public virtual ICollection<Match> Matches { get; set; }
        [InverseProperty("Stad")]
        public virtual ICollection<News> News { get; set; }
    }
}
