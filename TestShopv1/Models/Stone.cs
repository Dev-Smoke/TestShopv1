using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TestShopv1.Models
{
    [Index(nameof(GameId), Name = "IX_Stones_GameId")]
    [Index(nameof(UserId), Name = "IX_Stones_UserId")]
    public partial class Stone
    {
        [Key]
        public int Id { get; set; }
        public int GameId { get; set; }
        public int UserId { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }

        [ForeignKey(nameof(GameId))]
        [InverseProperty("Stones")]
        public virtual Game Game { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty("Stones")]
        public virtual User User { get; set; }
    }
}
