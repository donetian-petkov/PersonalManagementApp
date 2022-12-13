﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseManagementApp.Core.Models.Administration
{
    public class CreateRoleViewModel
    {
        [Required]
        [StringLength(30)]
        public string RoleName { get; set; }
    }
}
