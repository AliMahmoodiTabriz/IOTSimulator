using Protocol.Nesneler.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Protocol.Nesneler.Concrete.NesnelerProtocolEnums;

namespace Protocol.Nesneler.Concrete.Washer
{
    public class NEvent : NEntity<NEvent>
    {
        public uint EventDateTime;
        public byte Acknowledge;
        public override NEvent Object(byte[] data)
        {
            int index = 0;
            //EventDateTime = System.BitConverter.ToUInt32(data, index);
            //index += sizeof(uint);
            Acknowledge = data[index++];
            return this;
        }

        public override byte[] Packet()
        {
            NesnelerPacketStructure.ObjectType = (ushort)ObjectType.Washer;
            NesnelerPacketStructure.CommandType = (ushort)CommandType.Info;
            NesnelerPacketStructure.Command = (ushort)WasherCommand.Event;
            NesnelerPacketStructure.Payload.AddRange(BitConverter.GetBytes(EventDateTime));
            return NesnelerPacketStructure.GetPacket();
        }
    }
}
