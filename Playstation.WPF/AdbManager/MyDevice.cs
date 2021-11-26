using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playstation.WPF.AdbManager
{
   public class MyDevice
    {
        private string _adbFileNme = "adb.exe";
        public string Serial { get; }
        public string Model { get; }
        public string Product { get; }

        public MyDevice(string serial)
        {

            Serial = serial;
            Model = GetSpecificProperty("ro.product.model");
            Product = GetSpecificProperty("ro.build.product");
        }

        public override string ToString()
        {
            return $"Model: {Model}, Serial: {Serial}, Product: {Product}";
        }
        public string ExecuteShellCommand(string command)
        {
            Process proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = _adbFileNme,
                    Arguments = command,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                }
            };

            proc.Start();
            return proc.StandardOutput.ReadToEnd().Trim();
        }

        public string GetAllPropertirs()
        {
            return ExecuteShellCommand($"-s {Serial} shell getprop");
        }

        public string GetSpecificProperty(string propertyName)
        {
            return ExecuteShellCommand($"-s {Serial} shell getprop {propertyName}");
        }

        //public void EnableData()
        //{
        //    ExecuteShellCommand($"-s {Serial} shell svc data enable");
        //}

        //public void DisableData()
        //{
        //    ExecuteShellCommand($"-s {Serial} shell svc data disable");
        //}

        //public void RestartData()
        //{
        //    DisableData();
        //    EnableData();
        //}
    }
}
