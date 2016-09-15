using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu.Model
{
    public class Plan : IPlan
    {
        Guid id { get; set; }
        string description { get; set; }

        public Plan(string description)
        {
            this.id = Guid.NewGuid();
            this.description = description;
        }

        public string GetDescription()
        {
            return this.description;
        }
    }
}
