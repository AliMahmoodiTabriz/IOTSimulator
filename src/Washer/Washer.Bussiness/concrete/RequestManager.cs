using Protocol.Nesneler.Concrete;
using Protocol.Nesneler.Concrete.Washer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tcp.Client;
using static Protocol.Nesneler.Concrete.NesnelerProtocolEnums;

namespace Washer.Bussiness.concrete
{
    public class RequestManager
    {
        private Dictionary<WasherCommand, Func<TcpData, WasherCommand>> _requestProsseser;
        private SenderManager _senderManager;
        public RequestManager(SenderManager senderManager)
        {
            _requestProsseser = new Dictionary<WasherCommand, Func<TcpData, WasherCommand>>();
            _requestProsseser.Add(WasherCommand.Event, ProsessEvent);
            _requestProsseser.Add(WasherCommand.Error, ProsessError);
            _requestProsseser.Add(WasherCommand.Water, ProsessWater);
            _requestProsseser.Add(WasherCommand.StartWasher, ProsessStartWasher);
            _requestProsseser.Add(WasherCommand.StopWasher, ProsessStopWasher);
            _requestProsseser.Add(WasherCommand.StartWater, ProsessStartWater);
            _requestProsseser.Add(WasherCommand.StopWater, ProsessStopWater);
            _senderManager = senderManager;
        }
        public void Prosess(NesnelerPacketStructure packetStructure, TcpData tcpData)
        {
            _requestProsseser[(WasherCommand)packetStructure.Command](tcpData);
        }
        private WasherCommand ProsessEvent(TcpData payload)
        {
            NEvent nEvent = new NEvent();
            nEvent.Object(payload.Data);
            nEvent.Acknowledge = 15;

            TcpData tcpData = new TcpData
            {
                Buffer = nEvent.Packet(),
                SessionId = payload.SessionId
            };
            _senderManager.AddTcpQueue(tcpData);

            return WasherCommand.Event;
        }
        private WasherCommand ProsessError(TcpData payload)
        {
            return WasherCommand.Error;
        }
        private WasherCommand ProsessWater(TcpData payload)
        {
            return WasherCommand.Water;
        }
        private WasherCommand ProsessStartWasher(TcpData payload)
        {
            return WasherCommand.StartWasher;
        }
        private WasherCommand ProsessStopWasher(TcpData payload)
        {
            return WasherCommand.StopWasher;
        }
        private WasherCommand ProsessStartWater(TcpData payload)
        {
            return WasherCommand.StartWater;
        }
        private WasherCommand ProsessStopWater(TcpData payload)
        {
            return WasherCommand.StopWater;
        }
    }
}
