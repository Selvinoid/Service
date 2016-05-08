using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminUI.Models
{
    public class CustomPrincipalSerializeModel
    {
        public int Id { get; set; }
        
        public string[] Roles { get; set; }
    }
}