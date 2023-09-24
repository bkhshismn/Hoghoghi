using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hoghoghi
{
    class clsGetDataSource
    {
        public string GetDataSource()
        {
            string path = "";
            path = File.ReadAllText(@"C:\Program Files\DataSource\HDataSource.txt", Encoding.UTF8);
            return path;
        }
    }
}
