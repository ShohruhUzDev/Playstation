using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playstation.WPF.AdbManager
{
   public class MyAdbManager
    {
        private string _adbFileName = "adb.exe";

        public MyAdbManager()
        {
        }

        public string ExecuteShellCommand(string command)
        {
            Process proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = _adbFileName,
                    Arguments = command,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                }
            };

            proc.Start();
            return proc.StandardOutput.ReadToEnd().Trim();
        }

        //this method skips unauthorized devices, becuase commands do not execute
        //on unauthorized devices and we need to run adb kill-server, which
        //doesnt solve the problem all the time.
        public List<MyDevice> GetDevices()
        {
            string output = ExecuteShellCommand("devices");
            List<string> serials = output.Split('\n').ToList();
            serials = serials.GetRange(1, serials.Count - 1); //skip the first line of output
            List<MyDevice> myDevices = new List<MyDevice>();
            foreach (var item in serials)
            {
                if (item.Contains("device"))
                {
                    myDevices.Add(new MyDevice(item.Split('\t')[0]));
                }
            }

            return myDevices;
        }
    }
}
