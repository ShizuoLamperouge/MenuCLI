using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuCLI;
internal class ConsoleWrapper : IConsoleWrapper
{
    public void WaitForUserKeyPress()
    {
        Console.ReadKey();
    }
}
