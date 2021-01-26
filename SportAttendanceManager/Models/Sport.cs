using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SportAttendanceSystem.Models
{
    public class Sport
    {
        [Key, Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdSport { get; set; }

        [Required]
        public string SportName { get; set; }

        [Required]
        [Range(0, 23, ErrorMessage  = "Hour is not valid")]
        public int Hour { get; set; }

        [Required]
        [Range(0, 59, ErrorMessage = "Minute is not valid")]
        public int Minute { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public String DayOfWeek { get; set; }

        // add the teacher who has this sport
        public int IdUser { get; set; }
        public virtual User User { get; set; }

        // add one to many Students that the sport has
        public List<Student> Students { get; set; }
    }
}