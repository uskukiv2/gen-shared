using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using alms.application.installer.library.Helpers;
using alms.application.installer.library.Managers;
using Serilog;

namespace alms.application.installer.library
{
    public class ChoosePresentation
    {
        private readonly DatabaseGenerator _databaseGenerator;
        private readonly ConsoleConfigurationSetup _configurationSetup;

        private IDictionary<int, string> _chooses;

        public ChoosePresentation()
        {
            _databaseGenerator = new DatabaseGenerator();
            _configurationSetup = new ConsoleConfigurationSetup();
            _chooses = new ConcurrentDictionary<int, string>();
        }

        public void Init()
        {
            _chooses = LoadChooses();
        }

        public void ShowSteps()
        {
            Console.WriteLine("Select any choose");
            foreach (var choose in _chooses)
            {
                Console.WriteLine($"\t{choose.Key} - {choose.Value}");
            }

            WaitInput();
        }

        private void WaitInput()
        {
            while (true)
            {
                Console.Write("Chose: ");
                var input = InputHelper.IntroduceHelper(Console.ReadKey());
                Console.WriteLine();
                if (CheckStepExisting(input))
                {
                    ContinueWithSelectedStep(input);
                    break;
                }
            }
        }

        private bool CheckStepExisting(int step)
        {
            if (_chooses.TryGetValue(step, out var stepName))
            {
                Console.WriteLine();
                Console.WriteLine($"Start executing {stepName}");
                return true;
            }

            Console.WriteLine("step is doesn't exists\n");
            return false;
        }

        private void ContinueWithSelectedStep(int step)
        {
            switch (step)
            {
                case 1:
                    DatabaseCreator();
                    break;
                case 2:
                    ConfigurationSetup();
                    break;
                case 3:
                    InstallServerCore();
                    break;
                case 4:
                    InstallServerGui();
                    break;
                case 5:
                    InstallServerWebGui();
                    break;
                case 6:
                    UpdateDatabaseIfNeeded();
                    break;
                case 0:
                    ExitFromInstaller();
                    return;
            }

            ReturnToStepSelection();
        }

        private void ExitFromInstaller()
        {
            Console.WriteLine("Exit from installer");
            Environment.Exit(0);
        }

        private void DatabaseCreator()
        {
            Log.Logger.Information("try create database");
            _databaseGenerator.CreateDatabaseAndUser(InputHelper.ProcessDatabaseCredentialsInput());
            Console.WriteLine("Creation Complete");
        }

        private void UpdateDatabaseIfNeeded()
        {
            Log.Logger.Information("try update database");
            _databaseGenerator.UpdateDatabaseIfNeeded(InputHelper.ProcessDatabaseCredentialsInput());
            Console.WriteLine("Update completed");
            Thread.Sleep(1000);
        }

        private void ConfigurationSetup()
        {
            _configurationSetup.Init();
            _configurationSetup.CreateConfiguration(InputHelper.ProcessDatabaseCredentialsInput());
        }

        private void InstallServerCore()
        {
            throw new NotImplementedException();
        }

        private void InstallServerGui()
        {
            throw new NotImplementedException();
        }

        private void InstallServerWebGui()
        {
            throw new NotImplementedException();
        }

        private void ReturnToStepSelection()
        {
            Thread.Sleep(1000);
            Console.Clear();
            ShowSteps();
        }

        private IDictionary<int, string> LoadChooses()
        {
            var dictionary = new Dictionary<int, string>();
            dictionary.Add(1, "Generate new server database");
            dictionary.Add(2, "Create configuration");
            dictionary.Add(3, "Install server core");
            dictionary.Add(4, "Install server desktop ui");
            dictionary.Add(5, "Install server web ui");
            dictionary.Add(6, "Update database");
            dictionary.Add(0, "Exit from installer");

            return dictionary;
        }
    }
}
