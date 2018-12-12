using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ПАК_ПС
{
    interface ICommands
    {
        void Execute();
        void Undo();
    }
}
