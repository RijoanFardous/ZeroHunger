using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ZeroHunger.DTOs
{
    public class CollectionRequestDTO
    {
        public int Id { get; set; }
        public int RequestedBy { get; set; }
        public System.DateTime RequestedAt { get; set; }

        [Required]
        public System.DateTime MaxPreserveTime { get; set; }

        [Required]
        public string Description { get; set; }
        public string Status { get; set; }
    }
}