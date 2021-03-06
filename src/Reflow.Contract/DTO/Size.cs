﻿using System;
using Reflow.Contract.Enum;
using System.Diagnostics;

namespace Reflow.Contract.DTO
{
    [DebuggerDisplay("{Amount} {Magnitude} ({SizeInBytes})")]
    public class Size : IComparable<Size>
    {

        public Size(double amount, Magnitude magnitude)
        {
            this.Amount = amount;
            this.Magnitude = magnitude;
        }

        public Size(long byteSize)
        {
            this.SizeInBytes = byteSize;
        }

        public double Amount { get; }

        public Magnitude Magnitude { get; }

        public long SizeInBytes { get; }

        public static Size Parse(string size)
        {
            var props = size.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            Magnitude magnitudeValue;
            if(System.Enum.TryParse(props[1], true, out magnitudeValue))
            {
                if(System.Enum.IsDefined(typeof(Magnitude), magnitudeValue))
                {
                    return new Size(double.Parse(props[0]), magnitudeValue);
                }
            }

            return new Size(double.Parse(props[0]), Magnitude.Unknown);

        }

        public int CompareTo(Size other)
        {
            return this.SizeInBytes.CompareTo(other.SizeInBytes);
        }
    }
}
