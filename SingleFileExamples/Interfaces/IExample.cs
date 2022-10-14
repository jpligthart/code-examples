using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingleFileExamples.Interfaces;

internal interface IExample
{
    string Name => GetType().Name;

    void Execute();
}

