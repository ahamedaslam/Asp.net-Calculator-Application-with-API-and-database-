using System.ComponentModel.DataAnnotations;

namespace Calc_ui.Models
{
    public class Calc
    {
        [Key]
        public int Value1 { get; set; }
   
        public int Value2 { get; set; }

        public string Operand { get; set; }
        public int Total { get; set; }

        public string Calculate { get; set; }
    }
}
