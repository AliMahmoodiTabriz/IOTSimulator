using Protocol.Nesneler.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protocol.Nesneler.Abstract
{
    public abstract class NEntity<T>
    {
        public NesnelerPacketStructure NesnelerPacketStructure;
        public NEntity()
        {
            NesnelerPacketStructure = new NesnelerPacketStructure();
            NesnelerPacketStructure.StratByte = Encoding.ASCII.GetBytes("AB");
            NesnelerPacketStructure.StratByte = Encoding.ASCII.GetBytes("CD");
        }
        public abstract byte[] Packet();
        public abstract T Object(byte[] data);
    }
}
