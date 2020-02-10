using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public static class StringUtil
{

    public static string Color(this string input, string color)
    {
        return "<color=#" + color + ">" + input + "</color>";
    }
}
