using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPNET_Core_PostgreSQL_RLS.Models
{
    [Table("testmodel")]
    public class TestModel
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("columna")]
        public string ColumnA { get; set; }
        [Column("columnb")]
        public DateTime ColumnB { get; set; }
    }
}
