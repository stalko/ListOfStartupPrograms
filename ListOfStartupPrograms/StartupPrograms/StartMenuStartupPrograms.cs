using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace ListOfStartupPrograms.StartupPrograms
{
    class StartMenuStartupPrograms : StartupPrograms, IStartupPrograms
    {
        public List<ProgramDTO> ListPrograms()
        {
            var list = new List<ProgramDTO>();
            var files = Directory.GetFiles(Environment.GetEnvironmentVariable("AppData") + @"\Microsoft\Windows\Start Menu\Programs\Startup");
            foreach (string file in files)
            {
                if (Path.GetExtension(file).ToLower() != ".lnk")
                    continue;
                var originPath = GetShortcutTarget(file);
                var program = new ProgramDTO()
                {
                    Image = GetImage(originPath),
                    Name = new FileInfo(originPath).Name,
                    Command = "",
                    FilePath = originPath,
                    TypeAutorun = TypeAutorun.StartMenu
                };
                CheckDigitalSignature(program);
                list.Add(program);
            }
            return list;
        }

        private string GetShortcutTarget(string file)
        {
            try
            {
                FileStream fileStream = File.Open(file, FileMode.Open, FileAccess.Read);
                using (var fileReader = new BinaryReader(fileStream))
                {
                    fileStream.Seek(0x14, SeekOrigin.Begin);
                    uint flags = fileReader.ReadUInt32();
                    if ((flags & 1) == 1)
                    {
                        fileStream.Seek(0x4c, SeekOrigin.Begin);
                        uint offset = fileReader.ReadUInt16();
                        fileStream.Seek(offset, SeekOrigin.Current);
                    }

                    long fileInfoStartsAt = fileStream.Position;
                    uint totalStructLength = fileReader.ReadUInt32();
                    fileStream.Seek(0xc, SeekOrigin.Current);
                    uint fileOffset = fileReader.ReadUInt32();
                    fileStream.Seek((fileInfoStartsAt + fileOffset), SeekOrigin.Begin);
                    long pathLength = (totalStructLength + fileInfoStartsAt) - fileStream.Position - 2;
                    char[] linkTarget = fileReader.ReadChars((int)pathLength);
                    var link = new string(linkTarget);

                    int begin = link.IndexOf("\0\0");
                    if (begin > -1)
                    {
                        int end = link.IndexOf("\\\\", begin + 2) + 2;
                        end = link.IndexOf('\0', end) + 1;

                        string firstPart = link.Substring(0, begin);
                        string secondPart = link.Substring(end);

                        return firstPart + secondPart;
                    }
                    else
                    {
                        return link;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return "";
            }
        }
    }
}
