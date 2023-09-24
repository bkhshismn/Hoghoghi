using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hoghoghi
{
    class clsMethods
    {
        public string DataSource()
        {
            string Path = "";
            Path = File.ReadAllText(@"C:\Program Files\DataSource\DataSource.txt", Encoding.UTF8);
            return Path;
        }
    }
}
