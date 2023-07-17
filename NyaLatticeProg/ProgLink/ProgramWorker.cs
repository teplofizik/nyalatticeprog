using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Extension.Array;

namespace ProgLink
{
    class ProgramWorker
    {
        public Programmer P = new Programmer();

        byte[] Bitmap;

        bool Running = false;
        int Steps = 0;
        int Step = 0;

        private const int BufferSize = 60;

        public bool Busy => Running;
        public bool Error = false;
        public string Status = "";
        public float Progress => ((Steps != 0) && (Step >= 0)) ? 100.0f * (Step + 1) / Steps : 0;

        public void Program(byte[] Bitmap)
        {
           // Bitmap = Bitmap.ReadArray(0x50, Bitmap.Length - 0x50);
            Running = true;
            Steps = ((Bitmap.Length + BufferSize - 1) / BufferSize) + 2;
            Step = -3;
            Error = false;
            Status = "";
            P.State.Reset();

            this.Bitmap = Bitmap;
        }

        private void UpdateStatus(string Text)
        {
            Status = Text;
        }

        private void Finished(bool Status)
        {
            Running = false;
        }

        public void Cancel()
        {
            Running = false;
            Status = "Cancelled";
            Step = 0;
        }

        public void Tick()
        {
            if (P.RespTimeout > 0) P.RespTimeout--;
            if (Running)
            {
                if (!P.State.Busy)
                {
                    if (Step < Steps)
                    {
                        if(Step < 0)
                        {
                            P.Info();
                        }
                        else if (Step == 0)
                        {
                            UpdateStatus("Start configuration...");
                            if (P.Start())
                            {
                                UpdateStatus("Started.");
                            }
                            else
                            {
                                UpdateStatus("Failed to start configuration.");
                                Running = false;
                            }
                        }
                        else if (Step == Steps - 1)
                        {
                            if (P.Finish())
                            {
                                UpdateStatus("Finished.");
                            }
                            else
                            {
                                UpdateStatus("Failed to finish configuration.");
                            }
                            Running = false;
                        }
                        else
                        {
                            int Offset = (Step - 1) * BufferSize;
                            int Size = BufferSize;
                            if (Offset + BufferSize > Bitmap.Length) Size = Bitmap.Length - Offset;
                            P.WriteBlock(Bitmap.ReadArray(Offset, Size));
                            UpdateStatus($"Write buffer block {Step-1}/{Steps-2}");
                        }
                        Step++;
                    }
                    else
                    {
                        Running = false;
                    }
                }
                else
                {
                    if (Step < 0) P.State.Response("Timeout...");
                }
            }
        }
    }
}
