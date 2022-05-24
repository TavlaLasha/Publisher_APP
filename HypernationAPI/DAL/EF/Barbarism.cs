namespace DAL.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Barbarisms")]
    public partial class Barbarism
    {
        public int id { get; set; }

        [Required]
        public string wrong_word { get; set; }

        public string correct_word { get; set; }

        public string description { get; set; }
    }
}
