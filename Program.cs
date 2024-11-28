using FLORENCE;

namespace FLORENCE
{
    public class Program
    {
        private static FLORENCE.framework framework;

        public static void Main(String[] args)
        {
            System.Console.WriteLine("FLORENCE START");
            framework = new FLORENCE.framework();
            while (framework == null) { /* wait untill created */ }
            framework.Get_Client().Get_Execute().Create_And_Run_Graphics();
        }

        public static FLORENCE.framework Get_Framework()
        {
            return framework;
        }
    }
}