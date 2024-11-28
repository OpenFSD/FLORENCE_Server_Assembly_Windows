using FLORENCE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

using Valve.Sockets;

namespace FLORENCE
{
    public class Server
    {
        public Server()
        {
            NetworkingSockets server = new NetworkingSockets();

            uint pollGroup = server.CreatePollGroup();

            StatusCallback status = (ref StatusInfo info) =>
            {
                switch (info.connectionInfo.state)
                {
                    case ConnectionState.None:
                        break;

                    case ConnectionState.Connecting:
                        server.AcceptConnection(info.connection);
                        server.SetConnectionPollGroup(pollGroup, info.connection);
                        break;

                    case ConnectionState.Connected:
                        Console.WriteLine("Client connected - ID: " + info.connection + ", IP: " + info.connectionInfo.address.GetIP());
                        break;

                    case ConnectionState.ClosedByPeer:
                    case ConnectionState.ProblemDetectedLocally:
                        server.CloseConnection(info.connection);
                        Console.WriteLine("Client disconnected - ID: " + info.connection + ", IP: " + info.connectionInfo.address.GetIP());
                        break;
                }
            };

            NetworkingUtils utils = new NetworkingUtils();
            utils.SetStatusCallback(status);

            Address address = new Address();

            address.SetAddress("::0", port);

            uint listenSocket = server.CreateListenSocket(ref address);

#if VALVESOCKETS_SPAN
	        MessageCallback message = (in NetworkingMessage netMessage) => {
		        Console.WriteLine("Message received from - ID: " + netMessage.connection + ", Channel ID: " + netMessage.channel + ", Data length: " + netMessage.length);
	        };
#else
            const int maxMessages = 20;

            NetworkingMessage[] netMessages = new NetworkingMessage[maxMessages];
#endif

            while (!Console.KeyAvailable)
            {
                server.RunCallbacks();

#if VALVESOCKETS_SPAN
		        server.ReceiveMessagesOnPollGroup(pollGroup, message, 20);
#else
                int netMessagesCount = server.ReceiveMessagesOnPollGroup(pollGroup, netMessages, maxMessages);

                if (netMessagesCount > 0)
                {
                    for (int i = 0; i < netMessagesCount; i++)
                    {
                        ref NetworkingMessage netMessage = ref netMessages[i];

                        Console.WriteLine("Message received from - ID: " + netMessage.connection + ", Channel ID: " + netMessage.channel + ", Data length: " + netMessage.length);

                        netMessage.Destroy();
                    }
                }
#endif

                Thread.Sleep(15);
            }
            server.DestroyPollGroup(pollGroup);
        }

        public static void CreateAndSendNewMessage()
        {
            byte[] data = new byte[64];
            sockets.SendMessageToConnection(connection, data);
        }

        public static void CopyPayloadFromMessage()
        {
            byte[] buffer = new byte[1024];
            netMessage.CopyTo(buffer);
        }

        public static void SetA_HookForDebugInformation()
        {
            DebugCallback debug = (type, message) => {
                Console.WriteLine("Debug - Type: " + type + ", Message: " + message);
            };

            NetworkingUtils utils = new NetworkingUtils();

            utils.SetDebugCallback(DebugType.Everything, debug);
        }
    }
}
