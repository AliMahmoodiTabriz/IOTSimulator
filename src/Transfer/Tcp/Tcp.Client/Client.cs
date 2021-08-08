using NetCoreServer;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;

namespace Tcp.Client
{
    public class Client : TcpClient
    {
        private ConcurrentQueue<TcpData> _tcpDataQueue;
        public delegate void DataReady();
        public event DataReady dataReady;
        public event DataReady Connect;
        public event DataReady Disconnect;
        public Client(string address, int port) : base(address, port)
        {
            _tcpDataQueue = new ConcurrentQueue<TcpData>();
            ConnectAsync();
        }
        public async Task Reconnect()
        {
            ConnectAsync();
        }
        protected override void OnConnected()
        {
            Connect();
            base.OnConnected();
        }
        protected override void OnDisconnected()
        {
            Disconnect();
            base.OnDisconnected();
        }
        public override bool DisconnectAsync()
        {
            _tcpDataQueue.Clear();
            return base.DisconnectAsync();
        }
        public async Task<TcpData> GetTcpData()
        {
            if (!(_tcpDataQueue.Count > 0))
                return null;

            return await Task.Run(() =>
            {
                if (_tcpDataQueue.TryDequeue(out TcpData result))
                {
                    return result;
                }
                return result;
            });
        }
        public async Task<bool> Send(TcpData tcpData)
        {
            return await Task.Run(() =>
            {
                if (IsConnected)
                    return SendAsync(tcpData.Buffer);
                else
                {
                    return false;
                }
            });
        }
        protected override void OnReceived(byte[] buffer, long offset, long size)
        {
            TcpData tcpData = new TcpData
            {
                Buffer = buffer.Take((int)size).ToArray(),
                Ip = Endpoint.ToString(),
                SessionId = Id
            };
            _tcpDataQueue.Enqueue(tcpData);
            dataReady();
        }
    }
}
