using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.Windows.Forms;

namespace Bang_.NetEngine
{
    class BangUdpWrapper
    {
        #region Local variables
        private int udpPort = 6127;
        private UdpClient udpClient = null;
        private Thread udpListenerThread = null;
        #endregion

        #region Delegate & Event definition
        public delegate void OnDataReceivedHandler(OnDataReceivedArgs eventArgs);
        public event OnDataReceivedHandler OnDataReceived;

        #region EventArguments definition
        public class OnDataReceivedArgs : EventArgs
        {
            private string senderIP;
            private string dataReceived;

            public string SenderIP
            {
                get { return senderIP; }
                set { senderIP = value; }
            }
            public string DataReceived
            {
                get { return dataReceived; }
                set { dataReceived = value; }
            }
        }
        #endregion
        #endregion

        #region C-tors
        // Default constructor
        public BangUdpWrapper()
        {
            Init();
        }

        // Port-custom c-tor
        public BangUdpWrapper(int udpPort)
        {
            this.udpPort = udpPort;
            Init();
        }
        #endregion

        #region Helper functionality
        // Initialize socket & listening thread
        private void Init()
        {
            udpClient = new UdpClient(udpPort);
            udpListenerThread = new Thread(new ThreadStart(ListenerThreadProc));
            udpListenerThread.Start();
        }

        // Listener thread functionality
        private void ListenerThreadProc()
        {
            while (true)
            {
                IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);                
                if (udpClient.Available > 0)
                {
                    // Blocks until a message returns on this socket from a remote host.
                    try
                    {
                        Byte[] receiveBytes = udpClient.Receive(ref RemoteIpEndPoint);
                        string returnData = Encoding.ASCII.GetString(receiveBytes);
                        if (OnDataReceived != null)
                        {
                            OnDataReceivedArgs eventArgs = new OnDataReceivedArgs();
                            eventArgs.SenderIP = RemoteIpEndPoint.Address.ToString();
                            eventArgs.DataReceived = returnData;
                            OnDataReceived(eventArgs);
                        }
                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show(ex.Message, ex.Source);
                    }
                }
                // Yield the rest of the time slice.
                Thread.Sleep(0);
            }
        }
        #endregion

        #region Public functionality
        // Send broadcast message
        public void Send(string dataToSend)
        {
            Send(IPAddress.Broadcast.ToString(), dataToSend);
        }

        // Send message to IP specified
        public void Send(string ipAddress, string dataToSend)
        {
            byte[] buffer;
            buffer = Encoding.ASCII.GetBytes(dataToSend);
            udpClient.Send(buffer, buffer.Length, ipAddress, udpPort);
        }

        // Close connection and stop listener thread
        public void Close()
        {
            udpListenerThread.Abort();
        }
        #endregion
    }
}
