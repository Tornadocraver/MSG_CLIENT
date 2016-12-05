using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Security;

namespace MSG_CLIENT
{
    /// <summary>
    /// An TCP interface to connect to a chat server, or send messages using P2P
    /// </summary>
    public class MsgClient
    {
        #region Variables
        public bool Connected = false;
        public bool Connecting = false;
        private bool ConnectionAborted = false;

        private Boolean serverPing = false;
        private string serverPingResponse = string.Empty;

        private Client ThisClient;
        private TcpClient globalServer;
        private Thread messageWatcher;

        private StreamReader globalReader;
        private StreamWriter globalWriter;

        public int Port { get; private set; }
        public string ChatIP { get; private set; } //IPAddress of either peer or server
        public Boolean P2P { get; private set; } = false;
        public string ScreenName { get; private set; } //Don't list message just typed; have check when message is received for ScreenName property, and replace with, "you:"
        public bool EncryptTraffic { get; private set; }
        private SecureString Password;
        public List<Client> Clients { get; private set; }
        #endregion

        #region Events
        public delegate void ChatUpdatedEventHandler(String _message, Client _cli);
        public event ChatUpdatedEventHandler MessageReceived;

        public delegate void NameChangedEventHandler(String _oldName, Client _cli);
        public event NameChangedEventHandler NameChanged;

        public delegate void NewClientEventHandler(Client _cli);
        public event NewClientEventHandler NewClient;

        public delegate void ClientDisconnectedEventHandler(Client _cli);
        public event ClientDisconnectedEventHandler ClientDisconnected;

        public delegate void CustomCommandReceived(String _comm);
        public event CustomCommandReceived CustomCommand;
        #endregion

        #region Creation
        /// <summary>
        /// Creates a new interface to connect to an IRC server
        /// </summary>
        /// <param name="_serverIP">IPAddress of the server</param>
        /// <param name="_port">Port to connect to</param>
        /// <param name="_screenName">The name to be shown when you send messages</param>
        /// <param name="_pass">The secure password (if any) to encrypt and decrypt data</param>
        /// <param name="_encryptTraffic">Securely encrypt messages between you and the server (must match)</param>
        public MsgClient(string _serverIP, int _port, string _screenName, SecureString _pass)
        {
            if (_screenName.ToLower() == "server")
                throw new MsgException("The name '" + _screenName + "' is reserved for server use only.");
            else
            {
                ChatIP = IPAddress.Parse(_serverIP).ToString();
                Port = _port;
                ScreenName = _screenName;
                EncryptTraffic = true;
                Password = _pass;
                ThisClient = new Client();
            }
        }
        /// <summary>
        /// Creates a new interface to connect to a peer IRC
        /// </summary>
        /// <param name="_peerIP">IPAddress of the server</param>
        /// <param name="_port">Port to connect to</param>
        /// <param name="_screenName">The name to be shown when you send messages</param>
        /// <param name="_encryptTraffic">Securely encrypt messages between you and the server (must match)</param>
        /// <param name="_pass">The secure password (if any) to encrypt and decrypt data</param>
        /// <param name="_p2p">Boolean indicating whether to connect to a peer or server</param>
        public MsgClient(string _peerIP, int _port, string _screenName, SecureString _pass, bool _p2p = true)
        {
            ThisClient = new Client();
            ChatIP = IPAddress.Parse(_peerIP).ToString();
            Port = _port;
            ScreenName = _screenName;
            EncryptTraffic = true;
            Password = _pass;
            P2P = true;
        }
        #endregion

        /// <summary>
        /// Connects to either the server OR the remote client
        /// </summary>
        public void Connect()
        {
            Connecting = true;
            if (P2P)
            {
                do
                {
                    try
                    {
                        TcpClient peer = new TcpClient();
                        peer.Connect(ChatIP, Port);
                        Connecting = false;
                        ThisClient.Created(ScreenName, IPAddress.Parse(Misc.RemoteIP()), peer);
                        StreamWriter writeLInfo = new StreamWriter(new NDNSW(peer.GetStream()));
                        string LInfo = "CLIENT_CONNECT_" + ThisClient.IP + "_" + ThisClient.Name + "_" + ThisClient.ConnectedAt.ToString(); //Make sure it matches with receiving end
                        writeLInfo.WriteLine(LInfo);
                        writeLInfo.Flush();
                        messageWatcher = new Thread(() => handleMessages(peer));
                        messageWatcher.Start();
                        globalWriter = writeLInfo;
                        Connected = true;
                    }
                    catch (SocketException) { throw new ConnectionFailed(MachineType.Client); }
                } while (Connecting == true);
            }
            else
            {
                do
                {
                    try
                    {
                        //if (!Connecting)
                        //{ ConnectionAborted = true; break; }
                        TcpClient server = new TcpClient();
                        server.Connect(ChatIP, Port);
                        ThisClient.Created(ScreenName, IPAddress.Parse(Misc.RemoteIP()), server);
                        StreamWriter writeLInfo = new StreamWriter(new NDNSW(server.GetStream()));
                        StreamReader readSInfo = new StreamReader(new NDNSW(server.GetStream()));
                        string LInfo = "CLIENT_NEW_" + ThisClient.IP + "_" + ThisClient.Name + "_" + ThisClient.ConnectedAt.ToString(); //Make sure it matches with receiving end
                        writeLInfo.WriteLine(LInfo);
                        writeLInfo.Flush();
                        string sInfo = readSInfo.ReadLine();
                        string serverIP = sInfo.Replace("SERVER_STARTED_", "").Substring(0, sInfo.Replace("SERVER_STARTED_", "").IndexOf("_"));
                        string serverTimeConn = sInfo.Replace("SERVER_STARTED_" + serverIP + "_", "");
                        messageWatcher = new Thread(() => handleMessages(server));
                        messageWatcher.Start();
                        globalWriter = writeLInfo;
                        globalReader = readSInfo;
                        globalServer = server;
                        Client serv = new Client(serverIP, "Server", DateTime.Now, globalServer);
                        Connected = true;
                        Connecting = false;
                        Clients = new List<Client>();
                        Clients.Add(ThisClient);
                        Clients.Add(serv);
                        NewClient(ThisClient);
                        NewClient(serv);
                        MessageReceived("Connected to server successfully at: " + DateTime.Now.ToString("HH:mm:ss"), null);
                    }
                    catch (SocketException ex) { Connected = false; if (!Connecting) { ConnectionAborted = true; break; } else throw new ConnectionFailed(MachineType.Client); }
                } while (Connecting == true);
            }
        }

        /// <summary>
        /// Sends the specified message to the server/peer
        /// </summary>
        /// <param name="_messageToSend"></param>
        public void Write(String _messageToSend)
        {
            if (Connected == true)
            {
                if (EncryptTraffic == true)
                {
                    globalWriter.WriteLine((ThisClient.Name + ": " + _messageToSend).TripleDES_Encrypt(Password, false));
                    globalWriter.Flush();
                }
                else
                {
                    globalWriter.WriteLine(ThisClient.Name + ": " + _messageToSend);
                    globalWriter.Flush();
                }
            }
            else { throw new MsgException("Cannot write a message to a non-connected server."); }
        }
        /// <summary>
        /// Disconnects you from the chat session
        /// </summary>
        public void Disconnect()
        {
            if (Connected == true)
            {
                if (P2P)
                {
                    if (EncryptTraffic == true)
                    {
                        globalWriter.WriteLine(("CLIENT_DISCONNECT_" + DateTime.Now.ToString("HH:mm:ss")).TripleDES_Encrypt(Password, false));
                        globalWriter.Flush();
                    }
                    else
                    {
                        globalWriter.WriteLine("CLIENT_DISCONNECT_" + DateTime.Now.ToString("HH:mm:ss"));
                        globalWriter.Flush();
                    }
                }
                else
                {
                    if (EncryptTraffic == true)
                    {
                        globalWriter.WriteLine(("CLIENT_DISCONNECT_" + ThisClient.IP.ToString() + "_" + DateTime.Now.ToString("HH:mm:ss")).TripleDES_Encrypt(Password, false));
                        globalWriter.Flush();
                    }
                    else
                    {
                        globalWriter.WriteLine("CLIENT_DISCONNECT_" + ThisClient.IP.ToString() + "_" + DateTime.Now.ToString("HH:mm:ss"));
                        globalWriter.Flush();
                    }
                }
            }
            else if (Connecting)
            {
                Connecting = false;
                while (ConnectionAborted == false)
                    Thread.Sleep(50);
                ConnectionAborted = false;
            }
            else { throw new MsgException("Cannot disconnect from a non-connected server."); }
        }
        /// <summary>
        /// Edits the name displayed on your messages
        /// </summary>
        /// <param name="_newName"></param>
        public void Rename(String _newName)
        {
            if (_newName.ToLower() == "server")
                throw new MsgException("The name '" + _newName + "' is reserved for server use only."); 
            else
            {
                ScreenName = _newName;
                ThisClient = new Client(ThisClient.IP.ToString(), _newName, ThisClient.ConnectedAt.ToString(), ClientState.Connected);
                if (Connected == true)
                {
                    if (EncryptTraffic == true)
                    {
                        globalWriter.WriteLine(("CLIENT_RENAME_" + ThisClient.IP.ToString() + "_" + _newName).TripleDES_Encrypt(Password, false));
                        globalWriter.Flush();
                    }
                    else
                    {
                        globalWriter.WriteLine("CLIENT_RENAME_" + ThisClient.IP.ToString() + "_" + _newName);
                        globalWriter.Flush();
                    }
                }
            }
        }
        /// <summary>
        /// Sends a personal message to the specified client
        /// </summary>
        /// <param name="_message">Message to send</param>
        /// <param name="_target">Client to send personal message to</param>
        public void PM(String _message, Client _target)
        {
            globalWriter = new StreamWriter(new NDNSW(_target.Connection.GetStream()));
            if (EncryptTraffic)
            {
                globalWriter.WriteLine(("CLIENT_PM_" + ThisClient.IP.ToString() + "_" + _target.IP + "_" + _message).TripleDES_Encrypt(Password, false));
                globalWriter.Flush();
            }
            else
            {
                globalWriter.WriteLine("CLIENT_PM_" + ThisClient.IP.ToString() + "_" + _target.IP + "_" + _message);
                globalWriter.Flush();
            }
        }
        /// <summary>
        /// Sends a ping message to the specified client to ensure that you're connected.
        /// </summary>
        /// <param name="_cli">Client to ping -- leave null to ping server</param>
        /// <returns></returns>
        public Boolean Ping(Client _cli, out string _latency)
        {
            if (_cli != null)
            {
                NDNSW strm = null;
                if (_cli.Name != "Server")
                {
                    strm = new NDNSW(_cli.Connection.GetStream());
                    globalReader = new StreamReader(strm);
                    globalWriter = new StreamWriter(strm);
                }
                else
                    serverPing = true;
                System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();
                timer.Start();
                if (EncryptTraffic)
                {
                    globalWriter.WriteLine(("CLIENT_PING").TripleDES_Encrypt(Password, false));
                    globalWriter.Flush();
                }
                else
                {
                    globalWriter.WriteLine("CLIENT_PING");
                    globalWriter.Flush();
                }
                string pong = string.Empty;
                if (_cli.Name != "Server")
                    pong = globalReader.ReadLine();
                else
                {
                    while (serverPing)
                        Thread.Sleep(10);
                    pong = serverPingResponse;
                    serverPingResponse = string.Empty;
                }
                if (P2P)
                    if (pong == "CLIENT_PONG")
                    {
                        timer.Stop();
                        _latency = timer.Elapsed.ToString("ffffff");
                        return true;
                    }
                    else
                    {
                        timer.Stop();
                        _latency = "n/a";
                        return false;
                    }
                else if (pong == "SERVER_PONG")
                {
                    timer.Stop();
                    _latency = timer.Elapsed.ToString("ffffff");
                    return true;
                }
                else
                {
                    timer.Stop();
                    _latency = "n/a";
                    return false;
                }
            }
            else
                throw new MsgException("The client to ping cannot be null.");
        }
        /// <summary>
        /// Writes a custom command to the MSG stream
        /// </summary>
        /// <param name="_comm">The custom command and attributes</param>
        public void Custom(string _comm)
        {
            globalWriter = new StreamWriter(new NDNSW(globalServer.GetStream()));
            if (EncryptTraffic)
            {
                globalWriter.WriteLine(("CLIENT_CUSTOM_" + _comm).TripleDES_Encrypt(Password, false));
                globalWriter.Flush();
            }
            else
            {
                globalWriter.WriteLine("CLIENT_CUSTOM_" + _comm);
                globalWriter.Flush();
            }
        }

        private void Close()
        {
            if (messageWatcher.ThreadState == ThreadState.Running)
            {
                messageWatcher.Abort();
            }
        }

        private void handleMessages(TcpClient _server)
        {
            try
            {
                NDNSW stream = new NDNSW(_server.GetStream());
                StreamReader reader = new StreamReader(stream);
                StreamWriter writer = new StreamWriter(stream);
                globalReader = reader;
                globalWriter = writer;
                do
                {
                    if (stream.DataAvailable())
                    {
                        Thread.Sleep(500);
                        continue;
                    }
                    string message = reader.ReadLine();
                    if (message != null)
                    {
                        if (EncryptTraffic == true)
                        {
                            message = message.TripleDES_Decrypt(Password, false);
                        }
                        #region P2P
                        if (P2P)
                        {
                            //if (message.StartsWith("CLIENT_"))
                            //{
                            //    string cliComm = message.Replace("CLIENT_", "");
                            //    if (cliComm.StartsWith("CONNECT_"))
                            //    {
                            //        string cliIP = cliComm.Replace("CONNECT_", "").Substring(0, cliComm.Replace("CONNECT_", "").IndexOf("_"));
                            //        string cliName = cliComm.Replace("CONNECT_" + cliIP + "_", "").Substring(0, cliComm.Replace("CONNECT_" + cliIP + "_", "").IndexOf("_"));
                            //        string cliTime = cliComm.Replace("CONNECT_" + cliIP + "_" + cliName + "_", "");
                            //        Clients.Add(new Client(cliIP, cliName, cliTime, ClientState.Connected));
                            //        NewClient("=> " + cliTime + " - Connected to " + cliName + " ( " + cliIP + ") successfully.", cliName, cliIP, cliTime);
                            //    }
                            //    else if (cliComm.StartsWith("DISCONNECT_"))
                            //    {
                            //        string cliTime = cliComm.Replace("DISCONNECT_", "");
                            //        Client cli = Clients[0];
                            //        Clients.Remove(cli);
                            //        ClientDisconnected("=> " + cliTime + " - " + cli.Name + " (" + cli.IP.ToString() + ") has disconnected from the session.", cli.Name, cli.IP.ToString(), cliTime);
                            //    }
                            //    else if (cliComm.StartsWith("RENAME_"))
                            //    {
                            //        string name = cliComm.Replace("RENAME_", "");
                            //        Client sender = Clients[0];
                            //        MessageReceived("=> " + sender.Name + " (" + sender.IP.ToString() + ") has changed their name to: " + name + ".");
                            //        Clients.Remove(sender);
                            //        Clients.Add(new Client(sender.IP.ToString(), name, sender.ConnectedAt.ToString(), sender.State));
                            //    }
                            //    else if (cliComm.StartsWith("PM_"))
                            //    {
                            //        string sender = cliComm.Replace("PM_", "").Substring(0, cliComm.Replace("PM_", "").IndexOf("_"));
                            //        string pm = cliComm.Replace("PM_" + sender + "_", "");
                            //        Client send = Clients.Find(c => c.IP.ToString() == sender);
                            //        MessageReceived("PM from " + send.Name + ": " + pm);
                            //    }
                            //    else if (cliComm.StartsWith("PING"))
                            //    {
                            //        if (EncryptTraffic)
                            //        {
                            //            writer.WriteLine(("CLIENT_PONG").TripleDES_Encrypt(Password, false));
                            //            writer.Flush();
                            //        }
                            //        else
                            //        {
                            //            writer.WriteLine("CLIENT_PONG");
                            //            writer.Flush();
                            //        }
                            //    }
                            //    else if (cliComm.StartsWith("CUSTOM_"))
                            //    {
                            //        CustomCommand(cliComm.Replace("CUSTOM_", ""));
                            //    }
                            //}
                            //else if (message.StartsWith(ThisClient.Name))
                            //{
                            //    MessageReceived(message.Replace(ThisClient.Name, "You"));
                            //}
                            //else
                            //{
                            //    MessageReceived(message);
                            //}
                        }
                        #endregion
                        #region Client-Server
                        else
                        {
                            if (message.StartsWith("SERVER_"))
                            {
                                string svrComm = message.Replace("SERVER_", "");
                                if (svrComm.StartsWith("CLIENT_"))
                                {
                                    string svrComm2 = svrComm.Replace("CLIENT_", "");
                                    if (svrComm2.StartsWith("NEW_"))
                                    {
                                        string cliIP = svrComm2.Replace("NEW_", "").Substring(0, svrComm2.Replace("NEW_", "").IndexOf("_"));
                                        string cliName = svrComm2.Replace("NEW_" + cliIP + "_", "").Substring(0, svrComm2.Replace("NEW_" + cliIP + "_", "").IndexOf("_"));
                                        string cliTime = svrComm2.Replace("NEW_" + cliIP + "_" + cliName + "_", "");
                                        Client tmp = new Client(cliIP, cliName, cliTime, ClientState.Connected);
                                        if (ThisClient.IP.ToString() != cliIP)
                                        {
                                            NewClient(tmp);
                                            Clients.Add(tmp);
                                            MessageReceived("=> " + tmp.ConnectedAt.ToShortTimeString() + " - " + tmp.Name + " ( " + tmp.IP.ToString() + ") has joined the session.", tmp);
                                        }
                                    }
                                    else if (svrComm2.StartsWith("DISCONNECT_"))
                                    {
                                        string cliIP = svrComm2.Replace("DISCONNECT_", "").Substring(0, svrComm2.Replace("DISCONNECT_", "").IndexOf("_"));
                                        string cliTime = svrComm2.Replace("DISCONNECT_" + cliIP + "_", "");
                                        Client cli = Clients.Find(c => c.IP.ToString() == cliIP);
                                        Clients.Remove(cli);
                                        MessageReceived("=> " + cliTime + " - " + cli.Name + " (" + cliIP + ") has disconnected from the session.", cli);
                                        ClientDisconnected(cli);
                                    }
                                    else if (svrComm2.StartsWith("BAN_"))
                                    {
                                        string cliIP = svrComm2.Replace("BAN_", "").Substring(0, svrComm2.Replace("BAN_", "").IndexOf("_"));
                                        string cliTime = svrComm2.Replace("BAN_" + cliIP + "_", "");
                                        Client cli = Clients.Find(c => c.IP.ToString() == cliIP);
                                        Clients.Remove(cli);
                                        ClientDisconnected(cli);
                                        if (cli == ThisClient)
                                        {
                                            MessageReceived("=> " + DateTime.Now.ToString() + " - You have been banned from this server.", null);
                                            Connected = false;
                                        }
                                        else
                                            MessageReceived("=> " + cliTime + " - " + cli.Name + " (" + cliIP + ") has been banned from this server.", cli);
                                    }
                                }
                                else if (svrComm.StartsWith("CLOSING"))
                                {
                                    MessageReceived?.Invoke("=> " + DateTime.Now.ToString() + " - The server has closed and all clients have been disconnected.", null);
                                    Connected = false;
                                }
                                else if (svrComm.StartsWith("KICK"))
                                {
                                    MessageReceived("=> " + DateTime.Now.ToString() + " - You have been kicked from the server.", null);
                                    Disconnect();
                                    Connected = false;
                                }
                                else if (svrComm.StartsWith("BAN"))
                                {
                                    //MessageReceived("=> " + DateTime.Now.ToString() + " - You have been banned from this server.", null);
                                }
                                else if (svrComm.StartsWith("PING"))
                                {
                                    if (EncryptTraffic)
                                    {
                                        writer.WriteLine(("CLIENT_PONG").TripleDES_Encrypt(Password, false));
                                        writer.Flush();
                                    }
                                    else
                                    {
                                        writer.WriteLine("CLIENT_PONG");
                                        writer.Flush();
                                    }
                                }
                                else if (svrComm.StartsWith("PONG"))
                                {
                                    if (serverPing)
                                        serverPing = false;
                                    serverPingResponse = "SERVER_PONG";
                                }
                                else if (svrComm.StartsWith("CUSTOM_"))
                                {
                                    CustomCommand(svrComm.Replace("CUSTOM_", ""));
                                }
                            }
                            else if (message.StartsWith("CLIENT_"))
                            {
                                string cliComm = message.Replace("CLIENT_", "");
                                if (cliComm.StartsWith("RENAME_"))
                                {
                                    string cliIP = cliComm.Replace("RENAME_", "").Substring(0, cliComm.Replace("RENAME_", "").IndexOf("_"));
                                    string name = cliComm.Replace("RENAME_" + cliIP + "_", "").Substring(0, cliComm.Replace("RENAME_" + cliIP + "_", "").IndexOf("_"));
                                    Client sender = Clients.Find(c => c.IP.ToString() == cliIP);
                                    string old = sender.Name;
                                    sender.Rename(name);
                                    NameChanged(old, sender);
                                    MessageReceived("=> " + old + " (" + cliIP + ") has changed their name to: " + name + ".", sender);
                                    Clients[Clients.IndexOf(Clients.Find(c => c.IP == sender.IP))] = sender;
                                    Clients.Add(new Client(sender.IP.ToString(), name, sender.ConnectedAt.ToString(), sender.State));
                                }
                                else if (cliComm.StartsWith("PM_"))
                                {
                                    string sender = cliComm.Replace("PM_", "").Substring(0, cliComm.Replace("PM_", "").IndexOf("_"));
                                    string pm = cliComm.Replace("PM_" + sender + "_", "");
                                    Client send = Clients.Find(c => c.IP.ToString() == sender); //Clients is empty!!!
                                    MessageReceived("PM from " + Clients.Find(c => c.IP.ToString() == sender).Name + ": " + pm, send);
                                }
                                else if (cliComm.StartsWith("PING"))
                                {
                                    if (EncryptTraffic)
                                    {
                                        writer.WriteLine(("CLIENT_PONG").TripleDES_Encrypt(Password, false));
                                        writer.Flush();
                                    }
                                    else
                                    {
                                        writer.WriteLine("CLIENT_PONG");
                                        writer.Flush();
                                    }
                                }
                                else if (cliComm.StartsWith("CUSTOM_"))
                                {
                                    CustomCommand(cliComm.Replace("CUSTOM_", ""));
                                }
                            }
                            else if (message.StartsWith(ThisClient.Name))
                            {
                                MessageReceived(message.Replace(ThisClient.Name, "You"), null);
                            }
                            else
                            {
                                MessageReceived(message, null);
                            }
                        }
                        #endregion
                    }
                } while (Connected == true);
                if (writer != null)
                    writer.Dispose();
                if (reader != null)
                    reader.Dispose();
                if (globalReader != null)
                    globalReader.Dispose();
                if (globalWriter != null)
                    globalWriter.Dispose();
                if (stream != null)
                    stream.Dispose();
                if (globalServer.Connected == true)
                    globalServer.Close();
            }
            catch (Exception ex) { }
        }
    }

    public class NDNSW : Stream
    {
        private NetworkStream wrappedStream;
        public NDNSW(NetworkStream wrappedStream) { this.wrappedStream = wrappedStream; }
        public override void Flush() { wrappedStream.Flush(); }
        public override long Seek(long offset, SeekOrigin origin) { return wrappedStream.Seek(offset, origin); }
        public override void SetLength(long value) { wrappedStream.SetLength(value); }
        public override int Read(byte[] buffer, int offset, int count) { return wrappedStream.Read(buffer, offset, count); } 
        public override void Write(byte[] buffer, int offset, int count) { wrappedStream.Write(buffer, offset, count); }
        public override bool CanRead { get { return wrappedStream.CanRead; } }
        public override bool CanSeek { get { return wrappedStream.CanSeek; } }
        public override bool CanWrite { get { return wrappedStream.CanWrite; } }
        public override long Length { get { return wrappedStream.Length; } }
        public override long Position { get { return wrappedStream.Position; } set { wrappedStream.Position = value; } }
        public Boolean DataAvailable() { return wrappedStream.DataAvailable; }
    }

    #region Exceptions
    class NoInternet : Exception
    {
        public String ErrorMessage { get; private set; }
        public NoInternet(MachineType _type)
        {
            switch (_type)
            {
                case MachineType.Client:
                    {
                        ErrorMessage = "WARNING: You are not connected to the internet. Please connect and try again."; break;
                    }
                case MachineType.Server:
                    {
                        ErrorMessage = "WARNING: You are not connected to the internet. Please connect and try again."; break;
                    }
            }
        }
    }
    class SendFailed : Exception
    {
        public String ErrorMessage { get; private set; }
        public SendFailed(MachineType _type, ClientState _state)
        {
            if (_type == MachineType.Client)
            {
                if (_state == ClientState.Banned)
                    ;
                else if (_state == ClientState.Muted)
                    ;
            }
            switch (_type)
            {
                case MachineType.Client:
                    {

                        ErrorMessage = ""; break;
                    }
                case MachineType.Server:
                    {
                        ErrorMessage = ""; break;
                    }
            }
        }
    }
    class NotConnected : Exception
    {
        public String ErrorMessage { get; private set; }
        public NotConnected(MachineType _type)
        {
            switch (_type)
            {
                case MachineType.Client:
                    {
                        ErrorMessage = ""; break;
                    }
                case MachineType.Server:
                    {
                        ErrorMessage = ""; break;
                    }
            }
        }
    }
    class ConnectionFailed : Exception
    {
        public String ErrorMessage { get; private set; }
        public ConnectionFailed(MachineType _type)
        {
            switch (_type)
            {
                case MachineType.Client:
                    {
                        ErrorMessage = "Failed to connect to server/peer."; break;
                    }
                case MachineType.Server:
                    {
                        ErrorMessage = "Failed to connect for an unknown reason."; break;
                    }
            }
        }
    }
    class MsgException : Exception
    {
        public string Reason { get; private set; }

        public MsgException(string _reason)
        {
            Reason = _reason;
        }
    }
    #endregion
}