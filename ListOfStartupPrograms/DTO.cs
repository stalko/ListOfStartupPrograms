using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ListOfStartupPrograms
{
    public class ProgramDTO
    {
        public Icon Image { get; set; }
        public string Name { get; set; }
        public string Command { get; set; }
        public string FilePath { get; set; }
        public TypeAutorun TypeAutorun { get; set; }
        public bool IsDigitalSignatureExists { get; set; }
        public bool IsDigitalSignatureCorrect { get; set; }
        public string CompanyName { get; set; }
    }

    public enum TypeAutorun
    {
        Registry = 0,
        StartMenu = 1,
        Scheduler = 2
    }
}
