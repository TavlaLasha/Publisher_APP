using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DataViewModels
{
    public class DocDTO
    {
        [Required(ErrorMessage = "FileName is Required")]
        public string FileName { set; get; }
        public long FileSize { set; get; }
    }
}
