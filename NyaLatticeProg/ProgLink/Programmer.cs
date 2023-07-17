using HidLibrary.Link;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ProgLink
{
    class Programmer
    {
        public ProgState State = new ProgState();
        public HidLink Link = new HidLink(0x0483, 0x5710);
        public int RespTimeout = 0;

        public bool Responsed => (RespTimeout > 0);

        public Programmer()
        {
            Link.onDisconnected += Link_onDisconnected;
            Link.onPacketReceived += Link_onPacketReceived;
        }

        private void Link_onDisconnected(object sender, EventArgs e)
        {
            if(State.Busy)
                State.Response("Disconnected...");
        }

        private void Link_onPacketReceived(object sender, PacketEventArgs e)
        {
            RespTimeout = 10;
            State.Response("Resp");
            if (e.Packet != null)
            {
                switch(e.Packet[4])
                {
                    case 66: Debug.WriteLine($"Timer Resp: {e.Packet[6]}"); break;
                    case 85: Debug.WriteLine("Cmd Resp"); break;
                }
            }
            
        }

        public bool Control(byte[] Raw)
        {
            //State.SendAction("Control...");
            Link.WritePacket(1, new Protocol.Packet(Protocol.TProgrammerCommand.CMD_TEST1, Raw).getPacket());
            return true;
        }

        public bool Info()
        {
            State.SendAction("Info...");
            Link.WritePacket(1, new Protocol.Packet(Protocol.TProgrammerCommand.CMD_INFO, null).getPacket());
            return true;
        }

        public bool Start()
        {
            State.SendAction("Start of configuration...");
            Link.WritePacket(1, new Protocol.Packet(Protocol.TProgrammerCommand.CMD_START, null).getPacket());
            return true;
        }

        public bool WriteBlock(byte[] Data)
        {
            State.SendAction("Programming...");
            Link.WritePacket(1, new Protocol.Packet(Protocol.TProgrammerCommand.CMD_DATA, Data).getPacket());
            return true;
        }

        public bool Finish()
        {
            State.SendAction("Finish configuration...");
            Link.WritePacket(1, new Protocol.Packet(Protocol.TProgrammerCommand.CMD_FINISH, null).getPacket());
            return true;
        }
    }
}
