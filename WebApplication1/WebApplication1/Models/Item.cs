using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    [Table("DdbItem")]
    public class Item
    {
        [Key]
        [Column("ItemID")]
        public int ItemID { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        [DisplayName("Rendering Engine")]
        [Required(ErrorMessage = "This Field is required.")]
        public string RenderingEngine { get; set; }
        [DisplayName("Browser")]
        [Required(ErrorMessage = "This Field is required.")]
        public string Browser { get; set; }
        
        [DisplayName("Platform")]
        [Required(ErrorMessage = "This Field is required.")]
        public string Platform { get; set; }

        [DisplayName("Engine Version")]
        [Required(ErrorMessage = "This Field is required.")]
        public string EngineVersion { get; set; }
    
        [DisplayName("CSS Grade")]
        [Required(ErrorMessage = "This Field is required.")]
        public string CSSGrade { get; set; }
    }
}
    