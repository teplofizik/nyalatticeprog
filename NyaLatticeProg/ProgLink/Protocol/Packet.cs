using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Extension.Packet;

namespace ProgLink.Protocol
{
    class Packet : RawPacket
    {
        public Packet(byte[] Raw) : base(Raw) { }

        public Packet(TProgrammerCommand Cmd, byte[] Data) : base(63)
        {
            Command = Cmd;
            this.Data = Data;
        }

        /// <summary>
        /// Команда программатору
        /// </summary>
        public TProgrammerCommand Command
        {
            get { return (TProgrammerCommand)ReadByte(0); }
            set { WriteByte(0, Convert.ToInt32(value)); }
        }

        /// <summary>
        /// Ответ
        /// </summary>
        public TProgrammerStatus Status
        {
            get { return (TProgrammerStatus)ReadByte(1); }
            set { WriteByte(1, Convert.ToInt32(value)); }
        }

        /// <summary>
        /// Количество байт в поле данных актуальны
        /// </summary>
        public int Length
        {
            get { return ReadByte(2); }
            set { WriteByte(2, value); }
        }

        public byte[] Data
        {
            get { return ReadArray(3, Math.Min(60, Length)); }
            set
            {
                if (value == null)
                    Length = 0;
                else
                {
                    int L = value.Length;
                    if (L > 60) L = 60;
                    Length = L;
                    WriteArray(3, value, L);
                }
            }
        }
    }
}
