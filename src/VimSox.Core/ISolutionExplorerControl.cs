using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VimSox.Core
{
    public interface ISolutionExplorerControl
    {
        void MoveUp();
        void MoveDown();
        void MoveLeft();
        void MoveRight();

        void MoveTop();

        void MoveBottom();
    }
}
