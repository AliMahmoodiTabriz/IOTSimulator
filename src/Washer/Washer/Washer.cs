using Protocol.Nesneler.Concrete.Washer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tcp.Client;
using Washer.Bussiness.concrete;

namespace Washer
{
    public partial class Washer : Form
    {
        private SenderManager _senderManager;
        private TransferManager _transferManager;
        public Washer(SenderManager senderManager, TransferManager transferManager)
        {
            InitializeComponent();
            _senderManager = senderManager;
            _transferManager = transferManager;
        }

        private async void BtnCreateEvent_Click(object sender, EventArgs e)
        {
            NEvent nEvent = new NEvent();
            nEvent.EventDateTime = (uint)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
           await _senderManager.AddTcpQueue(new TcpData { Buffer= nEvent.Packet() });
        }
    }
}
