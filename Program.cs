using System.Runtime.InteropServices;

namespace FLORENCE
{
    public class Program
    {
        //private static FLORENCE.framework framework;
        private static FLORENCE.Server networkingServer;

        public static void Main(String[] args)
        {
            System.Console.WriteLine("FLORENCE START");

            //framework = new FLORENCE.framework();
            //while (framework == null) { /* wait untill created */ }

            Valve.Sockets.Library.Initialize();
            networkingServer = new FLORENCE.Server();
        }

        //public static FLORENCE.framework Get_Framework()
        //{
        //    return framework;
        //}

        //[DllImport("\\bin\\networking\\GameNetworkingSockets.dll")]
        
    }
}