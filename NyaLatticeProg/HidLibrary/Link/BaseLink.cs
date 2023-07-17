using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace HidLibrary.Link
{
    abstract class BaseLink
    {
        public event EventHandler onConnected;
        public event EventHandler onDisconnected;
        public event PacketEventHandler onPacketReceived;

        /// <summary>
        /// Наличие подключения
        /// </summary>
        public virtual bool Connected { get { return false; } }

        /// <summary>
        /// Название порта
        /// </summary>
        public virtual string Name
        {
            get { return "HID"; }
            set { }
        }


        public virtual void Tick()
        {

        }

        /// <summary>
        /// Подключено устройство
        /// </summary>
        protected void FireConnected()
        {
            Debug.WriteLine("Device attached.");
            onConnected?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Отключено устройство
        /// </summary>
        protected void FireDisconnected()
        {
            Debug.WriteLine("Device removed.");
            onDisconnected?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Получен пакет от устройства
        /// </summary>
        /// <param name="Data"></param>
        protected void FireReceived(byte[] Data)
        {
            onPacketReceived?.Invoke(this, new PacketEventArgs(Data));
        }

        /// <summary>
        /// Начать ожидание данных
        /// </summary>
        public abstract void Start();

        /// <summary>
        /// Завершить ожидание данных
        /// </summary>
        public abstract void Stop();

        /// <summary>
        /// Запись пакета
        /// </summary>
        /// <param name="Data"></param>
        public abstract void WritePacket(int Report, byte[] Data);
    }

    public class PacketEventArgs : EventArgs
    {
        /// <summary>
        /// Пакет с данными
        /// </summary>
        public byte[] Packet;

        public PacketEventArgs(byte[] P)
        {
            Packet = P;
        }
    }

    public delegate void PacketEventHandler(object sender, PacketEventArgs e);
}
