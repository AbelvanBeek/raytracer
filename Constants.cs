using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template;

internal static class Constants
{
    internal static int screenWidth = 1024;
    internal static int screenHeight = 512;
    internal static int axisLength = 5;
    internal static float screenScale = (float)screenHeight / (axisLength*2) / ((float)screenHeight/(float)(screenWidth/2));
    internal static float rayLength = 1000f;
    internal static float E = 0.001f;
    internal static float OneOver256 = 1/256;


    internal static Vector3 camRotation = new Vector3(0, 0, 0);

    internal static Vector3 centerDebug = new Vector3(0, 0, 0);

    internal static Template.Surface display;

    internal static int CalculateHex(Vector3 color)
    {
        int hexcolor = 0;
        hexcolor += (int) MathHelper.Clamp(color.X * 255, 0, 255) << 16;
        hexcolor += (int) MathHelper.Clamp(color.Y * 255, 0, 255) << 8;
        hexcolor += (int) MathHelper.Clamp(color.Z * 255, 0, 255);

        return hexcolor;
    }
    internal static Vector3 CalculateColor(int hex)
    {

        return new Vector3((float)((hex>>16)&0xFF) /255, (float)((hex>>8) & 0xFF) /255, (float) ((hex) & 0xFF) / 255);
        //((hex << 8) >> 32) / 255
        // ((hex << 16) >> 32) /256
        // ((hex << 24) >> 32) /256)
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
    internal static float Length(Vector3 v)
    {
        return (float) Math.Sqrt(v.X * v.X + v.Y * v.Y + v.Z * v.Z);
    }
    internal static Ray reflectRay(Ray ray, Vector3 normal, Vector3 intersection)
    {
        ray.direction = ( ray.direction - 2 * normal * Dot(ray.direction, normal));
        ray.origin = intersection + ray.direction * E;
        return ray;
    }
    internal static Vector3 Cross(Vector3 a, Vector3 b)
    {
        return new Vector3(a.Y * b.Z - a.Z * b.Y, a.Z * b.X - a.X * b.Z, a.X * b.Y - a.Y * b.X);
    }
}
