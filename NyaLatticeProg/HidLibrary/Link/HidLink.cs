using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

using Extension.Array;

namespace HidLibrary.Link
{
    class HidLink : BaseLink
    {
        private string DeviceName = "unknown";

        private int vid;
        private int pid;

        private HidEnumerator enumerator;
        private IHidDevice device;

        public HidLink(int vid, int pid)
        {
            enumerator = new HidEnumerator();

            this.vid = vid;
            this.pid = pid;
        }

        public override string Name => DeviceName;

        private string GetName(IHidDevice Dev)
        {
            byte[] Data;
            if (Dev.ReadProduct(out Data))
            {
                var ResChars = Encoding.Unicode.GetChars(Data);
                string Res = "";
                for (int i = 0; i < ResChars.Length; i++)
                    if (ResChars[i] == '\0')
                        break;
                    else
                        Res += ResChars[i];

                return Res;
            }
            else
                return "unknown";
        }

        private void OnReport(HidReport report)
        {
            if (report.ReadStatus == HidDeviceData.ReadStatus.Success)
            {
               // if (report.ReportId != 0)
                {
                    Debug.WriteLine("Received");
                    FireReceived(report.Data);
                }
               // else
               //     Debug.WriteLine("Dummy");
            }
            else
                Debug.WriteLine($"{report.ReadStatus}");

            if (report.ReadStatus != HidDeviceData.ReadStatus.NotConnected)
                device.ReadReport(OnReport, 8000);
        }


        public override void Tick()
        {
            // Ждём подключения
            if (device == null)
            {
                IHidDevice Dev = enumerator.Enumerate(vid, pid).FirstOrDefault();
                var Dev2 = enumerator.Enumerate();

                if (Dev != null)
                {
                    Dev.OpenDevice(DeviceMode.Overlapped, DeviceMode.Overlapped, ShareMode.ShareRead | ShareMode.ShareWrite);//, ShareMode.Exclusive);

                    Dev.Inserted += DeviceAttachedHandler;
                    Dev.Removed += DeviceRemovedHandler;

                    Dev.MonitorDeviceEvents = true;
                    device = Dev;
                    DeviceName = GetName(Dev);
                    Dev.ReadReport(OnReport, 8000);
                }
            }
        }

        private void DeviceRemovedHandler()
        {
            FireDisconnected();
        }

        private void DeviceAttachedHandler()
        {
            device.OpenDevice(DeviceMode.Overlapped, DeviceMode.Overlapped, ShareMode.Exclusive);
            FireConnected();
        }

        public override void Start()
        {

        }

        public override void Stop()
        {

        }

        public override void WritePacket(int Report, byte[] Data)
        {
            if (device != null)
            {
                int Len = (Data.Length > 63) ? 63 : Data.Length;

                byte[] Raw = new byte[Len + 1];
                Raw[0] = Convert.ToByte(Report);
                Raw.WriteArray(1, Data, Len);
                device.WriteReport(new HidReport(Len + 1, new HidDeviceData(Raw, HidDeviceData.ReadStatus.Success)), 200);
            }
        }
    }
}
