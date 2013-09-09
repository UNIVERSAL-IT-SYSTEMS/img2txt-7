﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace img2txt
{
    public static class Utils
    {
        public static float Remap(this float value, float from1, float to1, float from2, float to2)
        {
            return (float)Math.Floor((value - from1) / (to1 - from1) * (to2 - from2) + from2);
        }
    }
}
