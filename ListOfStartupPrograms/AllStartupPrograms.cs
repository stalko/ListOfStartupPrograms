using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ListOfStartupPrograms.StartupPrograms
{
    class AllStartupPrograms
    {
        public List<ProgramDTO> GetAllPrograms()
        {
            var list = new List<ProgramDTO>();
            list.AddRange(new RegistryStartupPrograms().ListPrograms());
            //list.AddRange(new ScheduledStartupPrograms().ListPrograms());
            list.AddRange(new StartMenuStartupPrograms().ListPrograms());
            return list;
        }
    }
}
