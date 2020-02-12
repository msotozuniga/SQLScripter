using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLScripter.Structures
{
    public class Node
    {
        public string dbName { get; private set; }
        public string type { get; private set; }
        public string fileName { get; private set; }

        public Node(string dbName, string type, string fileName)
        {
            this.dbName = dbName;
            this.type = type;
            this.fileName = fileName;
        }
    }
}
