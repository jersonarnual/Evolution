using System;
using System.ComponentModel.DataAnnotations;

namespace Evolution.Data.models.Base
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string CreateBy { get; set; }
        public string UpdateBy { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
