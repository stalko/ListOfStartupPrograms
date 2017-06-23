using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace ListOfStartupPrograms.StartupPrograms
{
    class ScheduledStartupPrograms : IStartupPrograms
    {
        public List<ProgramDTO> ListPrograms()
        {
            var list = new List<ProgramDTO>();
            return list;
        }
    }
}
