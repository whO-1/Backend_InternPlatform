using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internPlatform.Domain.Models.ViewModels
{
    public class JTSelectListItem
    {
        public string Value { get; set; }
        public string DisplayText { get; set; }

        public bool Selected { get; set; }

        public JTSelectListItem() { }

        
    }
}
