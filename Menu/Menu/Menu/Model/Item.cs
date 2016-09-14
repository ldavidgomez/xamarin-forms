using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu.Model
{
    public class Item
    {
        string descrtiption;

        public Item(string text)
        {
            this.descrtiption = text;
        }

        public string GetText()
        {
            return this.descrtiption;
        }
    }
}
