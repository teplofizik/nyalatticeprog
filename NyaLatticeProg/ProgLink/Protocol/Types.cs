using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgLink.Protocol
{
    enum TProgrammerCommand
    {
        CMD_INFO = 0x00,
        CMD_START = 0x01,
        CMD_DATA = 0x02,
        CMD_FINISH = 0x03,
        CMD_TEST1  = 0x04
    }

    enum TProgrammerStatus
    {
        STA_OK = 0,
        STA_ERROR = 1,
        STA_INVALID_COMMAND = 2
    }


}
