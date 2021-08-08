using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tcp.Client;

namespace Washer.Bussiness.concrete
{
    public class ReciverManager
    {
        private ProsessPacketManager _prosessPacketManager;
        public ReciverManager(ProsessPacketManager prosessPacketManager)
        {
            _prosessPacketManager = prosessPacketManager;
        }

        public async Task ProsessPacketTcp(TcpData transferData)
        {
            await _prosessPacketManager.ProsessPacket(transferData);
        }
    }
}
