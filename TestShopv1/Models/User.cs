using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TestShopv1.Models
{
    public partial class User
    {
        public User()
        {
            GameNextPlayers = new HashSet<Game>();
            GameUserAs = new HashSet<Game>();
            GameUserBs = new HashSet<Game>();
            Stones = new HashSet<Stone>();
        }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public Guid Guid { get; set; }

        [InverseProperty(nameof(Game.NextPlayer))]
        public virtual ICollection<Game> GameNextPlayers { get; set; }
        [InverseProperty(nameof(Game.UserA))]
        public virtual ICollection<Game> GameUserAs { get; set; }
        [InverseProperty(nameof(Game.UserB))]
        public virtual ICollection<Game> GameUserBs { get; set; }
        [InverseProperty(nameof(Stone.User))]
        public virtual ICollection<Stone> Stones { get; set; }
    }
}
