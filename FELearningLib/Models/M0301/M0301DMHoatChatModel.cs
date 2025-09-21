using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FELearningLib.Models.M0301
{
    [Table("HH_DM_HoatChat")]
    public class M0301DMHoatChatModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long? ID { get; set; }

        [Required(ErrorMessage = "Tên hoạt chất không được để trống.")]
        [StringLength(255, ErrorMessage = "Tên hoạt chất không được vượt quá 255 ký tự.")]
        public string TenHoatChat { get; set; }

        [Required]
        public bool Active { get; set; }
    }
}
