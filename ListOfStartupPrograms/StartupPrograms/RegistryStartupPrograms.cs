using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ListOfStartupPrograms.StartupPrograms
{
    class RegistryStartupPrograms : StartupPrograms, IStartupPrograms
    {
        public List<ProgramDTO> ListPrograms()
        {
            var list = new List<ProgramDTO>();
            //var key = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\Explorer\\Run");
            var currentUserReg = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run");
            list.AddRange(GetPrograms(currentUserReg));
            currentUserReg.Close();


            var localMachineReg = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run");
            list.AddRange(GetPrograms(localMachineReg));
            localMachineReg.Close();

            return list;
        }

        private List<ProgramDTO> GetPrograms(RegistryKey reg)
        {
            var list = new List<ProgramDTO>();
            foreach (string Programs in reg.GetValueNames())
            {
                string GetValue = reg.GetValue(Programs).ToString();
                string filePath = GetPathToProgram(GetValue);
                var program = new ProgramDTO()
                {
                    Image = GetImage(filePath),
                    Name = Programs,
                    FilePath = filePath,
                    Command = GetValue,
                    TypeAutorun = TypeAutorun.Registry
                };
                CheckDigitalSignature(program);
                list.Add(program);
            }
            return list;
        }

        private string GetPathToProgram(string str)
        {
            //TODO: Переделать под Regex
            if (str.IndexOf('"') > -1)
                return str.Substring(str.IndexOf('"') + 1, str.IndexOf('"', 2) - 1);
            else
                return str;
        }

    }
}
