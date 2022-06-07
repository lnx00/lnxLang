using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lnxLang.Parser.Instructions
{

    internal enum DebugTask
    {
        None, Dump
    }

    internal class Debug : IInstruction
    {

        public DebugTask Task { get; set; }

        public Debug(DebugTask task)
        {
            Task = task;
        }

        public static DebugTask GetDebugTask(string task)
        {
            return task switch
            {
                "DUMP" => DebugTask.Dump,
                _ => DebugTask.None
            };
        }

    }
}
