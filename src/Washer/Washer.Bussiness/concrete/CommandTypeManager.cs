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
    public class CommandTypeManager
    {
        private readonly RequestManager _requestManager;
        private readonly ResonceManager _resonceManager;
        private readonly InfoManager _infoManager;
        private readonly StreamManager _streamManager;
        public Dictionary<CommandType, Func<NesnelerPacketStructure, TcpData, CommandType>> _CommandTypeProsseser;
        public CommandTypeManager(RequestManager requestManager, ResonceManager resonceManager, InfoManager infoManager, StreamManager streamManager)
        {
            _CommandTypeProsseser = new Dictionary<CommandType, Func<NesnelerPacketStructure, TcpData, CommandType>>();
            _CommandTypeProsseser.Add(CommandType.Request, ProsessRequest);
            _CommandTypeProsseser.Add(CommandType.Resonce, ProsessResonce);
            _CommandTypeProsseser.Add(CommandType.Info, ProsessInfo);
            _CommandTypeProsseser.Add(CommandType.Stream, ProsessStream);
            _requestManager = requestManager;
            _resonceManager = resonceManager;
            _infoManager = infoManager;
            _streamManager = streamManager;
        }
        public void Prosess(NesnelerPacketStructure nesnelerPacket, TcpData tcpData)
        {
            _CommandTypeProsseser[(CommandType)nesnelerPacket.CommandType](nesnelerPacket, tcpData);
        }

        private CommandType ProsessRequest(NesnelerPacketStructure nesnelerPacket, TcpData tcpData)
        {
            _requestManager.Prosess(nesnelerPacket, tcpData);
            return CommandType.Request;
        }
        private CommandType ProsessResonce(NesnelerPacketStructure nesnelerPacket, TcpData tcpData)
        {
            _resonceManager.Prosess(nesnelerPacket, tcpData);
            return CommandType.Resonce;
        }
        private CommandType ProsessInfo(NesnelerPacketStructure nesnelerPacket, TcpData tcpData)
        {
            _infoManager.Prosess(nesnelerPacket, tcpData);
            return CommandType.Info;
        }
        private CommandType ProsessStream(NesnelerPacketStructure nesnelerPacket, TcpData tcpData)
        {
            _streamManager.Prosess(nesnelerPacket, tcpData);
            return CommandType.Stream;
        }

    }
}
