using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tcp.Client;

namespace Washer.Bussiness.concrete
{
    public class TransferManager
    {
        private SenderManager _senderManager;
        private ReciverManager _reciverManager;
        private Client _client;
        public TransferManager(SenderManager senderManager, ReciverManager reciverManager, Client client)
        {
            _senderManager = senderManager;
            _reciverManager = reciverManager;
            _client = client;
            _client.dataReady += _client_dataReady;
            _client.Connect += _client_Connect;
            _client.Disconnect += _client_Disconnect;
            Task.Run(() => { StartSend(); });
        }

        private void _client_Disconnect()
        {
            Thread.Sleep(1000);
            Task.Run(() => { _client.Reconnect(); });
        }

        private void _client_Connect()
        {
           
        }

        private async void _client_dataReady()
        {
            var data = await _client.GetTcpData();
            if (data != null)
                await _reciverManager.ProsessPacketTcp(data);
        }
        private async void StartSend()
        {
            try
            {
                while (true)
                {
                    var data = await _senderManager.GetTcpueue();
                    if (data != null)
                        await _client.Send(data);
                    else
                        Thread.Sleep(100);
                }
            }
            catch
            {
                Thread.Sleep(1000);
                _client.Reconnect();
                StartSend();
            }

        }
    }
}
