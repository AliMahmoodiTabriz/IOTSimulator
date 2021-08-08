using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tcp.Client;

namespace Washer.Bussiness.concrete
{
    public class ProsessPacketManager
    {
        private ObjectTypeManager _objectTypeManager;
        public ProsessPacketManager(ObjectTypeManager systemTypeManager)
        {
            _objectTypeManager = systemTypeManager;
        }

        public async Task ProsessPacket(TcpData transferData)
        {
            await Task.Run(() =>
            {
                _objectTypeManager.ResolveData(transferData);
            });
        }
    }
}
