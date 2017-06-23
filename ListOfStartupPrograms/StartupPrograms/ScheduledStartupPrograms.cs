using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskScheduler;

namespace ListOfStartupPrograms.StartupPrograms
{
    class ScheduledStartupPrograms : StartupPrograms, IStartupPrograms
    {
        public List<ProgramDTO> ListPrograms()
        {
            TaskScheduler.TaskScheduler scheduler = new TaskScheduler.TaskScheduler();
            scheduler.Connect(null, null, null, null); //run as current user.
            ITaskFolder root = scheduler.GetFolder("\\");
            var list = GetActions(root);
            return list;
        }

        private List<ProgramDTO> GetActions(ITaskFolder folder)
        {
            var list = new List<ProgramDTO>();
            var mainFolderName = folder.Name;
            foreach (ITaskFolder subFolder in folder.GetFolders(1))
            {
                var folderName = subFolder.Name;
                list.AddRange(GetActions(subFolder));
            }
            if (folder.GetFolders(1).Count == 0)
                foreach (IRegisteredTask task in folder.GetTasks(1))
                {
                    var context = task.Definition.Actions.Context;
                    foreach (var item in task.Definition.Actions)
                    {
                        if (!(item is IExecAction)) continue;
                        var action = item as IExecAction;
                        var asdasd = task.Name + "!!!!" + task.Path;
                        var path = Environment.ExpandEnvironmentVariables(action.Path);
                        var id = action.Id;
                        var at = action.Arguments;
                        var asds = action.WorkingDirectory;
                        var program = new ProgramDTO()
                        {
                            Image = GetImage(path),
                            Name = task.Name,
                            FilePath = path,
                            Command = action.Arguments,
                            TypeAutorun = TypeAutorun.Scheduler
                        };
                        CheckDigitalSignature(program);
                        list.Add(program);
                    }
                }
            return list;
        }

    }
}
