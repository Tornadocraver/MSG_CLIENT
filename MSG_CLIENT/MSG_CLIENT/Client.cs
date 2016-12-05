using System;
using System.Net;
using System.Net.Sockets;

namespace MSG_CLIENT
{
    public enum ClientState
    {
        /// <summary>
        /// The client object is null
        /// </summary>
        Empty = 0,
        /// <summary>
        /// The client is currently connected and can send/receive messages
        /// </summary>
        Connected = 1,
        /// <summary>
        /// The client is no longer connected to the chat session
        /// </summary>
        Disconnected = 2,
        /// <summary>
        /// The client has been banned from their current chat session
        /// </summary>
        Banned = 3,
        ///<summary>
        /// The client can only receive messages from the chat session
        ///</summary>
        Muted = 5,
    }
    enum MachineType
    {
        Client = 0,
        Server = 1
    }

    /// <summary>
    /// 
    /// </summary>
    public class Client
    {
        #region Variables
        public ClientState State { get; set; }
        public String Name { get; private set; }
        public IPAddress IP { get; private set; } //public IPAddress IP { get { return IPAddress.Parse(MicahzStrings.RemoteIP()); } private set { this.IP = value; } }
        public DateTime ConnectedAt { get; private set; }
        public TcpClient Connection { get; private set; }
        #endregion

        /// <summary>
        /// Initializes a new, null Client instance
        /// </summary>
        public Client()
        {
            this.State = ClientState.Empty;
            this.Name = null;
            this.Connection = null;
        }
        /// <summary>
        /// For use in client applications: creates a new Client instance
        /// </summary>
        /// <param name="_name">The screen name of the client</param>
        /// <param name="_timeConnected">The time the client connected at</param>
        /// <param name="_connectionToChat">The TCPClient that the Client is connected on</param>
        public Client(string _name, DateTime _timeConnected, TcpClient _connectionToChat)
        {
            this.Name = _name;
            this.ConnectedAt = _timeConnected;
            this.Connection = _connectionToChat;
            this.State = ClientState.Connected;
        }
        /// <summary>
        /// For use of making lists of clients in client applications
        /// </summary>
        /// <param name="_ip">The IP Address of the client</param>
        /// <param name="_name">The screen name of the client</param>
        /// <param name="_timeConnected">The time the client connected at</param>
        /// <param name="_state">The current state of the client</param>
        public Client(string _ip, string _name, string _timeConnected, ClientState _state)
        {
            this.IP = IPAddress.Parse(_ip);
            this.Name = _name;
            this.ConnectedAt = DateTime.Parse(_timeConnected);
            this.State = _state;
        }
        /// <summary>
        /// For use in server applications: creates a new Client instance
        /// </summary>
        /// <param name="_ip">IPAddress of the remote client</param>
        /// <param name="_name">The screen name of the remote client</param>
        /// <param name="_timeConnected">The time the client connected at</param>
        /// <param name="_connectionToChat">The TCPClient that the Client is connected on</param>
        public Client(string _ip, string _name, DateTime _timeConnected, TcpClient _connectionToChat)
        {
            this.Name = _name;
            this.IP = IPAddress.Parse(_ip);
            this.ConnectedAt = _timeConnected;
            this.Connection = _connectionToChat;
            this.State = ClientState.Connected;
        }

        public void Created(string _name, IPAddress _thisIP, TcpClient _connectionToChat)
        {
            if (this.State == ClientState.Empty)
            {
                this.Name = _name;
                this.ConnectedAt = DateTime.Now;
                this.Connection = _connectionToChat;
                this.State = ClientState.Connected;
                this.IP = _thisIP;
            }
        }
        public void Rename(string _newName)
        {
            this.Name = _newName;
        }
    }
}