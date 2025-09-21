using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FELearningLib.Models.M0301
{
    [Table("HH_DM_HoatChatCuaHangHoa")]
    public class M0301HoatChatChoHangHoaModel
    {
        [Key]
        public long? ID { get; set; }
        public long? IDHangHoa { get; set; }
        public long? IDHoatChat { get; set; }

        public M0301DMHangHoaModel HangHoa { get; set; }
        public M0301DMHoatChatModel HoatChat { get; set; }
    }
}
