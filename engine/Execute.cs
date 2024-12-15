using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FLORENCE_Server_Assembly_Windows.engine;

namespace FLORENCE.Frame.Serv
{
    public class Execute
    {
        static private Execute_Control execute_Control = null;
        static private FLORENCE.Frame.Serv.Exe.WriteEnable writeEnable = null;
        private Thread listenRespond = null;
        private Thread[] threads = null;

        Execute()
        {

        }
        public void Initialise()
        {
            Framework.GetClient().GetAlgorithms().Initialise(Framework.GetClient().GetGlobal().Get_NumCores());

            writeEnable = new FLORENCE.Frame.Serv.Exe.WriteEnable();
            while (writeEnable == null) { /* Wait while is created */ }
            writeEnable.Initialise_Control(
                 Framework.GetClient().GetGlobal(),
                 Framework.GetClient().GetGlobal().Get_NumCores()
             );
        }

        public void Initialise_Control(
            int numberOfCores,
            Global global
        )
        {
            execute_Control = new FLORENCE.Frame.Serv.Exe.Execute_Control(numberOfCores);
            while (execute_Control == null) { /* Wait while is created */ }
        }

        public void Initialise_Threads(
            int numberOfCores
        )
        {
            threads = new Thread[numberOfCores];
            threads[0] = System.Threading.Thread.CurrentThread;
            //Framework.GetClient().GetExecute().GetExecute_Control().SetConditionCodeOfThisThreadedCore(0);

            threads[1] = new Thread(Framework.GetClient().GetAlgorithms().GetIO_ListenRespond().Thread_io_ListenRespond);
            threads[1].Start();

            /*while (Framework.GetClient().GetExecute().GetControlOfExecute().GetFlag_SystemInitialised(Framework.GetClient().GetGlobal().Get_NumCores()) != false)
            {
                /* wait while initialising */
            ///}

        }

        public FLORENCE.Frame.Serv.Exe.WriteEnable GetWriteEnable()
        {
            return writeEnable;
        }
    }
}
