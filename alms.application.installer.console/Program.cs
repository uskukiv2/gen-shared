using System;
using System.IO;
using System.Reflection;
using System.Threading;
using alms.application.installer.library;
using alms.application.installer.library.Managers;
using Serilog;
using Serilog.Enrichers;
using Serilog.Events;
using Serilog.Sinks.RollingFileAlternative;

namespace alms.application.installer.console
{
    class Program
    {
        static void Main(string[] args)
        {
            LoadLogger();
            LoadGreeting();

            LoadPresentations();

            Console.WriteLine("Press any key for exit");
            Console.ReadKey();
        }

        private static void LoadPresentations()
        {
            var choose = new ChoosePresentation();
            choose.Init();
            choose.ShowSteps();
        }

        private static void LoadLogger()
        {
            var appInfo = Assembly.GetExecutingAssembly().GetName().Name;
            var sysPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), $"cherry/installer/logs/{appInfo}.cherrylog");
            var path = Path.ChangeExtension(sysPath, ".cherrylog");

            Log.Logger = new LoggerConfiguration()
                .Enrich.With(new ThreadIdEnricher())
                .WriteTo.RollingFile(path, restrictedToMinimumLevel: LogEventLevel.Information)
#if DEBUG
                .WriteTo.Debug(LogEventLevel.Information)
#endif
                .CreateLogger();
        }

        private static void LoadGreeting()
        {
            Console.Title = "ALMS VOLA installer";
            Console.WriteLine(" VOLA            Welcome to VOLA installer. Select any choose to continue\n");
            Thread.Sleep(1000);
            Console.Clear();
        }
    }
}
