using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ListOfStartupPrograms.StartupPrograms
{
    class StartMenuStartupPrograms : IStartupPrograms
    {
        public List<ProgramDTO> ListPrograms()
        {
            var list = new List<ProgramDTO>();
            var files = System.IO.Directory.GetFiles(Environment.GetEnvironmentVariable("AppData")+ @"\Microsoft\Windows\Start Menu\Programs\Startup");
            foreach (string Programs in files)
            {
                var program = new ProgramDTO()
                {
                    Command = "",
                    TypeAutorun = TypeAutorun.StartMenu
                };
                list.Add(program);
            }
            return list;
        }
    }
}
