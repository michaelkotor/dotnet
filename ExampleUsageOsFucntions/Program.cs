using ClassLibrary3;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            new OsUtil().GetInfoAboutSystem();
            new EventLogInfo().GetInfo("");
        }
    }
}