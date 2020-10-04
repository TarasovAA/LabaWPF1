using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WpfLaba1.Models
{
    public class Hero
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [Range(1, 100)]
        public int Hp { get; set; }

        [Required]
        [Range(1, 1000)]
        public int Energy { get; set; }


        public string Skills { get; set; }

    }
}
