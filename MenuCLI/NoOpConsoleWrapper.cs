using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuCLI;
internal class NoOpConsoleWrapper : IConsoleWrapper
{
    public void WaitForUserKeyPress()
    {
        Console.WriteLine("*User press a key to continue*");
    }
}
