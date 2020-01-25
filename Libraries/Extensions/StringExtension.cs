using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Extensions
{
    public static class StringExtension
    {

        public static bool CompareStringIgnoreCase(this string a, string b)
        {
            return string.Equals(a, b, StringComparison.CurrentCultureIgnoreCase);

        }

    }
}
