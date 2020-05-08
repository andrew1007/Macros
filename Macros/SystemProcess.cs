using System;
using System.Collections.Generic;
using System.Text;

namespace Macros
{
    class SystemProcess
    {
        char processName;
        SystemProcess(char exe)
        {
            processName = exe;
        }

        private SystemProcess init
        {
            System.Diagnostics.Process.GetProcessesByName("chrome")
        }
    }
}
