namespace SquirrelFramework.Utility.Windows
{
    using System;
    using System.Linq;
    using Microsoft.Win32;

    public class RegistryHelper
    {
        public static string CheckAndGetRegister(string path, string name, RegistryValueKind valueType = RegistryValueKind.DWord)
        {
            // To read the Register for X64, we need to use RegistryKey.OpenBaseKey and RegistryView.Registry64,
            // For x86 it should be RegistryView.Registry32
            // https://social.msdn.microsoft.com/Forums/vstudio/en-US/adc40513-e925-448e-94ea-4d03c5dc24aa/cant-change-regstry-key-please-help?forum=wpf
            var key = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
            key = key.CreateSubKey(path);
            if (key.GetValue(name) == null)
            {
                key.SetValue(name, 0, valueType);
            }
            return key.GetValue(name).ToString();
        }

        public static void CheckAndSetRegister(string path, string name, object value)
        {
            // To read the Register for X64, we need to use RegistryKey.OpenBaseKey and RegistryView.Registry64,
            // For x86 it should be RegistryView.Registry32
            // https://social.msdn.microsoft.com/Forums/vstudio/en-US/adc40513-e925-448e-94ea-4d03c5dc24aa/cant-change-regstry-key-please-help?forum=wpf
            var key = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
            key = key.CreateSubKey(path);
            key.SetValue(name, value);
        }
       
        
        public static string GetGAInstallPath()
        {
            var gaInstallPath = string.Empty;
            var hkml = Registry.LocalMachine;
            var software = hkml.OpenSubKey("SOFTWARE", false);
            if (software != null)
            {
                var avepoint = software.OpenSubKey("AvePoint", false);
                if (avepoint != null)
                {
                    var ga = avepoint.OpenSubKey("GovernanceAutomation", false);
                    if (ga != null)
                    {
                        var installPath = ga.GetValue("InstallPath");
                        if (installPath != null)
                        {
                            gaInstallPath = installPath.ToString();
                        }
                    }
                }
            }
            return gaInstallPath;
        }

        public static string ReadKey(string registryPath = @"SOFTWARE\AvePoint\GovernanceAutomation\InstallPath")
        {
            var keys = registryPath.Split('\\');
            var subKey =  Registry.LocalMachine;

            if (keys == null)
            {
                throw new ArgumentException("Please check the registry path, current value: " + registryPath, "registryPath");
            }

            for (var i = 0; i< keys.Length; i++)
            {
                if (i == keys.Length - 1)
                {
                    if (subKey != null)
                    {
                        return subKey.GetValue(keys.Last()).ToString();
                    }
                }
                if (subKey != null)
                {
                    subKey = subKey.OpenSubKey(keys[i], RegistryKeyPermissionCheck.ReadSubTree);
                }
            }

            return null;
        }        
    }
}