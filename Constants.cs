using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template;

internal static class Constants
{
    //initialization
    internal static int screenWidth = 1025;
    internal static int screenHeight = 512;
    internal static int axisLength = 5;
    internal static float screenScale = (float)screenHeight / (axisLength*2) / ((float)screenHeight/(float)(screenWidth/2));
    internal static float rayLength = 100f;
    internal static float E = 0.001f;
    internal static int scw4 = screenWidth / 4; //these two reduce the amount of divisions necesary
    internal static int sch2 = screenHeight / 2;
    internal static float glassRef = 1.52f;
    internal static float nrDebugrays = 50; //every xth ray will be visible in the debug window (for y = 0)
    internal static int threads = 1; //threads don't really work

    //Photograph variables
    internal static bool antiAliasing = true;
    internal static int recursionCount = 8;

    internal static Vector3 camRotation = new Vector3(0, 0, 0);

    internal static Vector3 centerDebug = new Vector3(0, 0, 0);

    internal static Template.Surface display;
    internal static Random random = new Random();


    //formulas
    internal static int CalculateHex(Vector3 color)
    {
        //convert vector3 color to hexadecimal
        int hexcolor = 0;
        hexcolor += (int) MathHelper.Clamp(color.X * 255, 0, 255) << 16;
        hexcolor += (int) MathHelper.Clamp(color.Y * 255, 0, 255) << 8;
        hexcolor += (int) MathHelper.Clamp(color.Z * 255, 0, 255);

        return hexcolor;
    }
    internal static Vector3 CalculateColor(int hex)
    {
        //convert hex color to vector3 color
        return new Vector3((float)((hex>>16)&0xFF) /255, (float)((hex>>8) & 0xFF) /255, (float) ((hex) & 0xFF) / 255);
    }

    //debug screen translations
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
        ray.direction = ( ray.direction - 2 * normal * Dot(ray.direction, normal ));
        ray.origin = intersection + ray.direction * E;
        return ray;
    }
    internal static Vector3 Cross(Vector3 a, Vector3 b)
    {
        return new Vector3(a.Y * b.Z - a.Z * b.Y, a.Z * b.X - a.X * b.Z, a.X * b.Y - a.Y * b.X);
    }

    //calculate whether the ray started in the sphere or not. used to flip around normals.
    internal static float insideSphere(Vector3 origin, Vector3 spherePos, float radius)
    {
        if (Length(origin - spherePos) < radius + E)
            return -1; // inside sphere
        else
            return 1; // outside sphere
    }

    internal static Ray refractRay(Ray ray, Vector3 Normal, Vector3 intersection)
    {
        float n = 1 / glassRef;
        Ray refray = new Ray(ray.origin, ray.direction);
        refray.recursionDepth = ray.recursionDepth - 1;

        Vector3 N = Normal;
        float cosI = Dot(N, ray.direction);
        float cosT2 = 1f - n * n * (1.0f - cosI * cosI);
        if (cosT2 > 0.0f)
        {
            Vector3 T = (n * ray.direction) + (float)(n * cosI - Math.Sqrt(cosT2)) * N;
            T.Normalize();
            refray.direction = T;
            refray.origin = intersection;
            refray.origin += E * ray.direction;
        }
        return refray;
    }

    internal static float Fresnel(Vector3 rayDir, Vector3 normal)
    {
        float ViewProjecion = Dot(rayDir, normal);
        float CosTheta = Math.Abs(ViewProjecion);
        float sinTheta = (float) Math.Sqrt(1 - CosTheta * CosTheta);
        float sinThetaT = (1f / glassRef) * sinTheta;
        if(sinThetaT * sinThetaT > 0.9999f)
        {
            return 1.0f;
        }
        else
        {
            float cosThetaT = (float)Math.Sqrt(1f - sinThetaT * sinThetaT);
            float reflectanceOrtho = (1/glassRef * cosThetaT - 1f * CosTheta) / (1/glassRef * cosThetaT + 1f * CosTheta);
            reflectanceOrtho = reflectanceOrtho * reflectanceOrtho;
            float reflectancePara = (1f * cosThetaT - 1/glassRef * CosTheta) / (1f * cosThetaT + 1/glassRef * CosTheta);
            reflectancePara = reflectancePara * reflectancePara;
            return (0.5f * (reflectanceOrtho + reflectancePara));
        }
        return 0f;
    }
}
