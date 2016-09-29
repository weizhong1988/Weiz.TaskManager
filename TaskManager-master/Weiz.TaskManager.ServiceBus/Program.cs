using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Topshelf;

namespace Weiz.TaskManager.ServiceBus
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(x =>
            {
                x.Service<TaskManagerServiceBus>(s =>
                {
                    s.ConstructUsing(name => new TaskManagerServiceBus());
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });
                x.RunAsPrompt();

                x.SetDescription("TaskManagerServiceBus Host");
                x.SetDisplayName("TaskManagerServiceBus");
                x.SetServiceName("TaskManagerServiceBus");
            });
        }
    }
}
