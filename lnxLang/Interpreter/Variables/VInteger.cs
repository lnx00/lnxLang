﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lnxLang.Interpreter.Variables
{
    internal class VInteger : IVariable
    {

        public string Name { get; set; }
        public int Value { get; set; } = 0;

        public VInteger() { }

        public VInteger(int value)
        {
            Value = value;
        }

        public void SetValue(string value)
        {
            Value = int.Parse(value);
        }

        public void Add(int b)
        {
            Value += b;
        }

        public void Substract(int b)
        {
            Value -= b;
        }
    }
}
