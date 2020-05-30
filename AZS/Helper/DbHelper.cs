using AZS.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AZS.Helper
{
    public abstract class DbHelper
    {
        private static AZSEntities _context = new AZSEntities();

        public static AZSEntities GetContext()
        {
            return _context;
        }
    }
}
