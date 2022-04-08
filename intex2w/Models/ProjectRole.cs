using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace intex2w.Models
{
    //Role Model ;)
    public class ProjectRole
    {
        [Key]
        public int RoleID { get; set; }
        public string RoleName { get; set; }
    }
}
