using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TestShopv1.Models
{
    [Index(nameof(NextPlayerId), Name = "IX_Games_NextPlayerId")]
    [Index(nameof(UserAid), Name = "IX_Games_UserAId")]
    [Index(nameof(UserBid), Name = "IX_Games_UserBId")]
    public partial class Game
    {
        public Game()
        {
            Stones = new HashSet<Stone>();
        }

        [Key]
        public int Id { get; set; }
        [Column("UserAId")]
        public int UserAid { get; set; }
        [Column("UserBId")]
        public int UserBid { get; set; }
        public string Status { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public TimeSpan AutoLost { get; set; }
        public DateTime LastActiveTime { get; set; }
        public int NextPlayerId { get; set; }

        [ForeignKey(nameof(NextPlayerId))]
        [InverseProperty(nameof(User.GameNextPlayers))]
        public virtual User NextPlayer { get; set; }
        [ForeignKey(nameof(UserAid))]
        [InverseProperty(nameof(User.GameUserAs))]
        public virtual User UserA { get; set; }
        [ForeignKey(nameof(UserBid))]
        [InverseProperty(nameof(User.GameUserBs))]
        public virtual User UserB { get; set; }
        [InverseProperty(nameof(Stone.Game))]
        public virtual ICollection<Stone> Stones { get; set; }
    }
}
