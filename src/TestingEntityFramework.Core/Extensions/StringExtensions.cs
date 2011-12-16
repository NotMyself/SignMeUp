using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestingEntityFramework.Core.Extensions
{
    public static class StringExtensions
    {
        public static string FormatWith(this string @format, params string[] args)
        {
            return string.Format(@format, args);
        }

    }
}
