using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ListOfStartupPrograms.StartupPrograms
{
    interface IStartupPrograms
    {
        List<ProgramDTO> ListPrograms();
    }
}
