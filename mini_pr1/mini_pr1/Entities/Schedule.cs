
using System.ComponentModel.DataAnnotations;

namespace mini_pr1.Entities
{
    public class Schedule
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Start { get; set; }

        [Required]
        [MaxLength(20)]
        public string Destination { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Only letters and numbers are allowed.")]
        [MaxLength(5)]
        public string RouteNumber { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public DateTime StartTime { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public DateTime EndTime { get; set; }

    }
}
