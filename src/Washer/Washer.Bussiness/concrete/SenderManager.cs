using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tcp.Client;

namespace Washer.Bussiness.concrete
{
    public class SenderManager
    {
        private ConcurrentQueue<TcpData> _tcpDataQueue;
        public SenderManager()
        {
            _tcpDataQueue = new ConcurrentQueue<TcpData>();
        }
        public async Task AddTcpQueue(TcpData transferData)
        {
            await Task.Run(() =>
            {
                _tcpDataQueue.Enqueue(transferData);
            });
        }
        public async Task<TcpData> GetTcpueue()
        {
            return await Task.Run(() =>
            {
                if (_tcpDataQueue.TryDequeue(out TcpData result))
                {
                    return result;
                }
                return result;
            });
        }
    }
}
