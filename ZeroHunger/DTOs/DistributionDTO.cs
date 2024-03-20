using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ZeroHunger.DTOs
{
    public class DistributionDTO
    {
        public int Id { get; set; }

        [Required]
        public int RequestId { get; set; }

        [Required]
        public int EmployeeId { get; set; }
        public Nullable<System.DateTime> DistributedAt { get; set; }

        public int PeopleFed { get; set; }
        public string Status { get; set; }
    }
}