using System;
using System.Collections;
using System.Collections.Generic;
using Web.Models;

namespace Web.StaticHelpers
{
    public class Types
    {
        public static Type IEnumerable = typeof(IEnumerable);
        public static Type String { get; } = typeof(string);
        public static Type Int { get;} = typeof(int);
        public static Type Double { get; } = typeof(double);
        public static Type Byte { get; set; } = typeof(byte);
        public static Type Single { get; set; } = typeof(Single);
        public static Type Long { get; set; } = typeof(long);
        public static Type DataTime { get; set; } = typeof(DateTime);
        public static Type Bool { get; set; } = typeof(bool);
        public static Type Image { get; set; } = typeof(Image);
        public static Type ListImage { get; set; } = typeof(List<Image>);
        public static Type File { get; set; } = typeof(FileModel);

    }
}
