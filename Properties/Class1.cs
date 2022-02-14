using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewGame1.Properties
{
    class Class1
    {

        public string LoadASCII(string filename)
        {
            var lines = System.IO.File.ReadAllLines(filename).ToList();
            return String.Join("\n", lines);

        }
    }
}
