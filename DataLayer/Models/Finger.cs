using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class Finger
    {
        [Key]
        public int Id { get; set; }

        public int FingerId { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }
    }
}
