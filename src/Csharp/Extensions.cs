using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csharp
{
    public static class Extensions
    {
        private static int? CharToDigit(char c)
        {
            int x = c - '0';
            if (x >= 0 && x <= 9)
                return x;
            else return null;
        }

        public static int? ToInt32(string s)
        {
            int result = 0;
            int sign = 1;
            if (s.Length > 1 && s[0] == '-')
            {
                sign = -1;
                s = s.Substring(1);
            }

            for (int i = 0; i < s.Length; i++)
            {
                var digit = CharToDigit(s[i]);
                if (!digit.HasValue)
                    return null;
                else
                    result = 10 * result + digit.Value;
            }
            return result * sign;
        }


    }
}
