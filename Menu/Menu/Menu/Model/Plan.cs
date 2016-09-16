using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SQLite;

namespace Menu.Model
{
    [Table("Plan")]
    public class Plan : IPlan
    {
        [PrimaryKey]
        public Guid id { get; set; }
        public string description { get; set; }

        public Plan()
        { }

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
