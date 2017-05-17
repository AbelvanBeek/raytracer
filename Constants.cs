﻿using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template;

internal static class Constants
{
    internal static int screenWidth = 1025;
    internal static int screenHeight = 512;
    internal static int axisLength = 5;
    internal static float screenScale = (float)screenHeight / (axisLength*2) / ((float)screenHeight/(float)(screenWidth/2));
    internal static float rayLength = 100f;

    internal static Vector3 centerDebug = new Vector3(0, 0, 0);

    internal static Template.Surface display;

    internal static int CalculateHex(Vector3 color)
    {
        int hexcolor;
        hexcolor = (int)(color.X * 255 * 256 * 256);
        hexcolor += (int)(color.Y * 255 * 256);
        hexcolor += (int)(color.Z * 255);

        return hexcolor;
    }

    internal static int Tx(float x)
    {
        return (int)(2 + screenWidth / 2 + screenScale * (x + centerDebug.X + 5));
    }

    internal static int Ty(float y)
    {
        return (int)(screenHeight - screenScale * (y + centerDebug.Y + 1));
    }
    internal static float Dot(Vector3 a, Vector3 b)
    {
        return (a.X * b.X + a.Y * b.Y + a.Z * b.Z);
    }
}
