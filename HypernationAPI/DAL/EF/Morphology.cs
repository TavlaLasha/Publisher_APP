namespace DAL.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Morphology
    {
        public int id { get; set; }

        [Required]
        [StringLength(60)]
        public string wrong_word { get; set; }

        [Required]
        [StringLength(60)]
        public string correct_word { get; set; }
    }
}
