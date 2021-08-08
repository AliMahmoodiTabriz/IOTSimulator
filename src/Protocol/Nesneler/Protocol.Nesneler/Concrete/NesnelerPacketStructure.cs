using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protocol.Nesneler.Concrete
{
    public class NesnelerPacketStructure
    {
        private List<byte> _buffer;
        public byte[] StratByte { get; set; }
        public ushort ObjectType { get; set; }
        public ushort CommandType { get; set; }
        public ushort Command { get; set; }
        public ushort PayloadLen { get; set; }
        public List<byte> Payload { get; set; }
        public ushort Crs16 { get; set; }
        public byte[] StopByte { get; set; }
        public List<byte> CrcData { get; set; }

        public NesnelerPacketStructure()
        {
            Payload = new List<byte>();
            CrcData = new List<byte>();
            _buffer = new List<byte>();
        }
        public NesnelerPacketStructure Resolve(List<byte> buffer)
        {
            _buffer.AddRange(buffer);
            try
            {
                return GetStartByte().GetObjectType()
                     .GetCommandType().GetCommand()
                     .GetPayloadLen().GetCrs16()
                     .GetStopByte();
            }
            catch
            {
                throw new Exception("Packet extract error");
            }
        }
        public byte[] GetPacket()
        {
            CrcData.AddRange(StratByte);
            CrcData.AddRange(BitConverter.GetBytes(ObjectType));
            CrcData.AddRange(BitConverter.GetBytes(CommandType));
            CrcData.AddRange(BitConverter.GetBytes(Command));
            CrcData.AddRange(Payload);
            CrcData.AddRange(BitConverter.GetBytes((ushort)Payload.Count));
            CrcData.AddRange(BitConverter.GetBytes(CalculatSrc16(CrcData.ToArray())));
            return CrcData.ToArray();
        }
        private ushort CalculatSrc16(byte[] Datas)
        {
            byte x;
            ushort crc = 0xFFFF;
            int length = Datas.Length;
            int index = 0;
            while (length-- > 0)
            {
                x = (byte)((crc >> 8) ^ (Datas[index++]));
                x = (byte)(x ^ (x >> 4));
                crc = (ushort)((crc << 8) ^ (x << 12) ^ (x << 5) ^ (x));
            }
            return crc;
        }
        private NesnelerPacketStructure GetStartByte()
        {
            var buf = _buffer.GetRange(0, 2).ToArray();
            CrcData.AddRange(buf);
            StratByte = buf;
            _buffer.RemoveRange(0, 2);
            return this;
        }
        private NesnelerPacketStructure GetObjectType()
        {
            var buf = _buffer.GetRange(0, 2).ToArray();
            var objectType = BitConverter.ToUInt16(buf);
            CrcData.AddRange(buf);
            ObjectType = objectType;
            _buffer.RemoveRange(0, 2);
            return this;
        }
        private NesnelerPacketStructure GetCommandType()
        {
            var buf = _buffer.GetRange(0, 2).ToArray();
            var commandType = BitConverter.ToUInt16(buf);
            CrcData.AddRange(buf);
            CommandType = commandType;
            _buffer.RemoveRange(0, 2);
            return this;
        }
        private NesnelerPacketStructure GetCommand()
        {
            var buf = _buffer.GetRange(0, 2).ToArray();
            var command = BitConverter.ToUInt16(buf);
            CrcData.AddRange(buf);
            Command = command;
            _buffer.RemoveRange(0, 2);
            return this;
        }
        private NesnelerPacketStructure GetPayloadLen()
        {
            var buf = _buffer.GetRange(0, 2).ToArray();
            var len = BitConverter.ToUInt16(buf);
            CrcData.AddRange(buf);
            PayloadLen = len;
            _buffer.RemoveRange(0, 2);
            if (len != 0)
                GetPayload();
            return this;
        }
        private void GetPayload()
        {
            var buf = _buffer.GetRange(0, PayloadLen).ToArray();
            CrcData.AddRange(buf);
            Payload.AddRange(buf);
            _buffer.RemoveRange(0, PayloadLen);
        }
        private NesnelerPacketStructure GetCrs16()
        {
            var buf = _buffer.GetRange(CrcData.Count, 2).ToArray();
            var crc = BitConverter.ToUInt16(buf);
            Crs16 = crc;
            _buffer.RemoveRange(0, 2);
            return this;
        }
        private NesnelerPacketStructure GetStopByte()
        {
            var buf = _buffer.GetRange(0, 2).ToArray();
            StopByte = buf;

            if (CalculatSrc16(CrcData.ToArray()) != Crs16)
                throw new Exception();

            _buffer.RemoveRange(0, 2);
            return this;
        }
    }
}
