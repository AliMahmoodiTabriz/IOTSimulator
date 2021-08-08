using Protocol.Nesneler.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tcp.Client;
using static Protocol.Nesneler.Concrete.NesnelerProtocolEnums;

namespace Washer.Bussiness.concrete
{
    public class ObjectTypeManager
    {
        public Dictionary<ObjectType, Func<NesnelerPacketStructure, TcpData, ObjectType>> _objectTypeProsseser;
        private CommandTypeManager _commandTypeManager;
        public ObjectTypeManager(CommandTypeManager commandTypeManager)
        {
            _objectTypeProsseser = new Dictionary<ObjectType, Func<NesnelerPacketStructure, TcpData, ObjectType>>();
            _objectTypeProsseser.Add(ObjectType.Washer, ProsessWasherPacket);
            _objectTypeProsseser.Add(ObjectType.Refrigerator, ProsessRefrigeratorPacket);
            _commandTypeManager = commandTypeManager;
        }

        public virtual void ResolveData(TcpData tcpData)
        {
            var packet = new NesnelerPacketStructure().Resolve(tcpData.Buffer.ToList());
            tcpData.Data = packet.Payload.ToArray();
            _objectTypeProsseser[(ObjectType)packet.ObjectType](packet, tcpData);
        }

        private ObjectType ProsessWasherPacket(NesnelerPacketStructure nesnelerPacket, TcpData tcpData)
        {
            _commandTypeManager.Prosess(nesnelerPacket, tcpData);
            return ObjectType.Washer;
        }

        private ObjectType ProsessRefrigeratorPacket(NesnelerPacketStructure nesnelerPacket, TcpData tcpData)
        {
            throw new NotImplementedException();
        }
    }
}
