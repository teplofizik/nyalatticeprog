using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgLink
{
    class ProgState
    {
        /// <summary>
        /// Занятость
        /// </summary>
        public bool Busy = false;

        /// <summary>
        /// Результат
        /// </summary>
        public bool Result = false;

        public void Reset()
        {
            Busy = false;
            Result = false;
        }

        public void SendAction(string Comment)
        {
            Busy = true;
        }

        public void Response(string Comment)
        {
            Busy = false;
        }
    }
}
