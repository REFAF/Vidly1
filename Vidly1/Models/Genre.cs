using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Vidly1.Models
{
    public class Genre
    {
        //ex3.3
        public byte Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

    }
}