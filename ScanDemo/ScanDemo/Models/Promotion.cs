using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScanDemo.Models
{
    [Table("Promotion")]
    public class Promotion
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Nom { get; set; }
        public int Description { get; set; }
    }
}
