using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Data.Models
{
    public class LibroCursor
    {
        public int Current { get; set; }
        public int Total { get; set; }

        public LibroCursor()
        {
            Current = 0;
            Total = 0;
        }
    }
}
