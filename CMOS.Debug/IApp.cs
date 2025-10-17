using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMOS.Debug
{
    public interface IApp
    {
        public void Run();

        public void About();
        public void Exit();

        public void Help();
    }
}
