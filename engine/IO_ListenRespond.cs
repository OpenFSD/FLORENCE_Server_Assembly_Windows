using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FLORENCE.Frame.Serv.Algo
{
    public class IO_ListenRespond
    {
        static private Int16 threadId = 1;
        static private FLORENCE.Frame.Serv.Algo.In.IO_ListenRespond_Control io_Control;

        public IO_ListenRespond()
        {
            io_Control = null;
        }
        public void InitialiseControl()
        {
            io_Control = new FLORENCE.Frame.Serv.Algo.In.IO_ListenRespond_Control();
            while (io_Control == null) { /* Wait while is created */ }
        }

        public void Thread_io_ListenRespond()
        {
            //Framework.GetClient().GetExecute().GetExecute_Control().SetConditionCodeOfThisThreadedCore(threadId);
            //while (Framework.GetClient().GetExecute().GetExecute_Control().GetFlag_SystemInitialised(Framework.GetClient().GetGlobal().Get_NumCores()) != false)
            //{
            // wait untill ALL threads initalised in preperation of system init.
            //}
            while (true)
            {
                switch (Framework.GetClient().GetAlgorithms().GetIO_ListenRespond().GetIO_Control().GetFlag_IO_ThreadState())
                {
                    case true:
                    {
        
                        break;
                    }
                    case false:
                    {
                            byte[] buffer = new byte[1024];
                            //netMessage.CopyTo(buffer);

                            Framework.GetClient().GetExecute().GetWriteEnable().Write_Start(
                                Framework.GetClient().GetExecute().GetWriteEnable().GetWriteEnable_Contorl(),
                                0,
                                Framework.GetClient().GetGlobal().Get_NumCores(),
                                Framework.GetClient().GetGlobal()
                            );
                            using (var stream = File.Open("..\\resources\\Binary_PacketData.bin", FileMode.Open))
                            {
                                using (BinaryReader reader = new BinaryReader(stream))
                                {
                                    if (File.Exists("..\\resources\\Binary_PacketData.bin"))
                                    {
                                        var switch_praiseEventId = reader.ReadUInt16();
                                        //Console.WriteLine("Error Code : " + reader.ReadString());
                                        // Console.WriteLine("Message : " + reader.ReadString());
                                        // Console.WriteLine("Restart Explorer : " + reader.ReadBoolean());
                                        switch (switch_praiseEventId)
                                        {
                                            case 0:
                                                //data = new byte[64];
                                                break;

                                            case 1:
                                                //ToDo
                                                break;

                                            default:
                                                break;
                                        }
                                    }
                                }
                            }
                            Networking.CopyPayloadFromMessage();

                        break;
                    }
                }
            }
        }
        public FLORENCE.Frame.Serv.Algo.In.IO_ListenRespond_Control GetIO_Control()
        {
            return io_Control;
        }
    }
}
