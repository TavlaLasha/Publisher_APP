using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DataViewModels.WordManagement
{
    public class MorphologyDTO
    {
        public int Id { set; get; }
        [Required]
        public string Correct_Word { set; get; }
        [Required]
        public string Wrong_Word { set; get; }
    }
}
