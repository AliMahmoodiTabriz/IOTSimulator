using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protocol.Nesneler.Concrete
{
    public class NesnelerProtocolEnums
    {
        public enum ProtocolStep
        {
            StartByte = 0,
            ObjectType = 1,
            CommandType = 2,
            Command = 3,
            PayloadLen = 4,
            Payload = 5,
            Crs16 = 6,
            StopByte = 7,
        }
        public enum ObjectType
        {
            Washer = 1,
            Refrigerator = 2,
        }
        public enum CommandType
        {
            Request = 0,
            Resonce = 1,
            Info = 2,
            Stream = 3,
        }
        public enum WasherCommand
        {
            Event = 0,
            Error = 1,
            Water = 2,
            StartWasher = 3,
            StopWasher = 4,
            StartWater = 5,
            StopWater = 6
        }
        public enum WasherEventType
        {
            OpenDoor = 0,
            CloseDoor = 1,
            Start = 2,
            End = 3
        }
        public enum ErrorEvent
        {
            Foaming = 1,
        }
    }
}
