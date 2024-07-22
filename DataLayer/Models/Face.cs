using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class Face
    {
        [Key]
        public int Id { get; set; }

        public string Img { get; set; }

        public int Height { get; set; }

        public int Width { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }

    }
}
