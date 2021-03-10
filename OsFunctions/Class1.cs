using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

namespace ClassLibrary3
{
    public class OsUtil
    {
        private string GetComputerName()
        {
            return Environment.MachineName;
        }

        private string GetHardDriveInfo()
        {
            DriveInfo[] drives = DriveInfo.GetDrives();
            string result = "";
            foreach (DriveInfo drive in drives)
            {
                //There are more attributes you can use.
                //Check the MSDN link for a complete example.
                result += "Drive name: " + drive.Name + "\n";
                Console.WriteLine(drive.Name);
                if (drive.IsReady)
                {
                    result += "Drive size: " + drive.TotalSize + "\n";
                }
            }

            return result;
        }

        private void GetCPUusage()
        {
            var cpuUsage = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            Thread.Sleep(1000);
            var firstCall = cpuUsage.NextValue();

            for (int i = 0; i < 5; i++)
            {
                Thread.Sleep(1000);
                Console.WriteLine(cpuUsage.NextValue() + "%");
            }
        }

        private double GetMemoryAllInfo()
        {
            var gcMemoryInfo = GC.GetGCMemoryInfo();
            var installedMemory = gcMemoryInfo.TotalAvailableMemoryBytes;
            // it will give the size of memory in MB
            return installedMemory / 1048576.0;
        }
        
        private double GetMemoryFreeInfo()
        {
            var gcMemoryInfo = GC.GetGCMemoryInfo();

            var freeMemory = gcMemoryInfo.HighMemoryLoadThresholdBytes;
            // it will give the size of memory in MB
            return freeMemory / 1048576.0;
        }


        public void GetInfoAboutSystem()
        {
            Console.WriteLine(GetComputerName());
            
            //doesn't work now with arm chips
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                GetCPUusage();
            }
            
            Console.WriteLine(GetHardDriveInfo());
            Console.WriteLine("Total Memory (MB): " + GetMemoryAllInfo());
            Console.WriteLine("Free Memory (MB): " + GetMemoryFreeInfo());
            
        }
    }

    public class EventLogInfo
    {
        public void GetInfo(string name)
        {
            Console.WriteLine(RuntimeInformation.OSDescription);
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                EventLog eventLog = new EventLog(name);
                int counter = 0;
                foreach (EventLogEntry eventLogEntry in eventLog.Entries)
                {
                    Console.WriteLine("ID: " + eventLogEntry.InstanceId + " Message: " + eventLogEntry.Message);
                    if (++counter > 10)
                    {
                        break;
                    }
                }
                Console.WriteLine(eventLog.Log);
            }
            
        }
    }
}