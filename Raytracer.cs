using OpenTK;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Constants;

namespace template
{
    public class Raytracer
    {
        Scene scene;
        Camera camera;
        static List<Primitive> primitives;
        static List<Light> lightSources;
        float scale = 1f / 256f;
        public bool debug2D = true;
        public Vector3[] rays2D;
        public bool debug;
        SkyDome skydome;

        public Raytracer()
        {
            rays2D = new Vector3[screenWidth/2];
            scene = new Scene();
            camera = new Camera();
            primitives = scene.primitives;
            lightSources = scene.lightSources;
            //skydome = new SkyDome("../../assets/stpeters_probe.hdr");
        }

        public void Render()
        {
            display.Clear(0x00000);
            camera.HandleInput();

            //trace the rays.
            for (int x = -scw4; x < scw4; x++)
            {
                for (int y = -sch2; y < sch2; y++)
                {
                    //the boolean debug determines wether the ray will be drawn in the debug window.
                    debug = (debug2D && y == 0 && x % nrDebugrays == 0);
                    Vector3 n = (x * 0.5f) * scale * (camera.screenCorners[1] - camera.screenCorners[0]) + (y * 0.5f) * scale * (camera.screenCorners[2] - camera.screenCorners[0]) + new Vector3(0, 0, camera.screenDistance);
                    n += camRotation;
                    n.Normalize();
                    Ray ray = new Ray(camera.position, n);

                    //trace the primary ray
                    if (antiAliasing)
                        display.Pixel(x + scw4, y + sch2, CalculateHex(AntiAliasing(ray)));
                    else
                        display.Pixel(x + scw4, y + sch2, CalculateHex(Trace(ray)));
                }
            }

            if (debug2D)
            {
                //de getransleerde locatie van de camera
                int camX = Tx(camera.position.X);
                int camY = Ty(camera.position.Z);
                //camera tekenen
                for (int i = -1; i < 2; i++)
                    display.Line(camX - 1, camY + i, camX + 1, camY + i, 0xff00ff);
                //breedte van het scherm/2
                int screenX = (int)(camera.screenSize / 2 * screenScale);
                DrawPrimitives();
                //scherm tekenen
                display.Line(camX - screenX, Ty(camera.screenDistance + camera.position.Z), camX + screenX, Ty(camera.screenDistance + camera.position.Z), 0x0000ff);
            }
        }

        Vector3 AntiAliasing(Ray ray)
        {
            Vector3 averageColor = new Vector3(0, 0, 0);
            Vector3 mainColor = Trace(ray);
            float sampleSize = 200f;
            Matrix4x2 plusMatrix = new Matrix4x2(
                -1f / sampleSize, 1f / sampleSize,
                1f / sampleSize, 1f / sampleSize,
                -1f / sampleSize, -1f / sampleSize,
                1f / sampleSize, -1f / sampleSize);

            Matrix4x2 multiplyMatrix = new Matrix4x2(
                0, 1f / sampleSize,
                1f / sampleSize, 0,
                -1f / sampleSize, 0,
                0, -1f / sampleSize);

            for (int sample = 0; sample < 0; sample++)
            {
                Ray plusRay = ray;
                plusRay.direction.X += plusMatrix[sample, 0];
                plusRay.direction.Y += plusMatrix[sample, 1];
                plusRay.direction.Normalize();
                averageColor += (1 / 16f) * Trace(plusRay);
                
                Ray multiplyRay = ray;
                multiplyRay.direction.X += multiplyMatrix[sample, 0];
                multiplyRay.direction.Y += multiplyMatrix[sample, 1];
                multiplyRay.direction.Normalize();
                averageColor += (1 / 16f) * Trace(multiplyRay);
            }
            return averageColor += 0.8f * mainColor;
        }

        void DrawPrimitives()
        {
            //draws the 2 primitives
            foreach(Primitive p in primitives)
            {
                p.DrawPrimitive();
            }
        }

        Vector3 Trace(Ray ray)
        {
            //make sure the recursion doesnt go on endlessly
            if (ray.recursionDepth <= 0)
                return new Vector3(0, 0, 0);
            ray.recursionDepth--;

            Intersection intersect = IntersectScene(ray);
            if (intersect == null)
            {
                //draw the debug line without intersectionpoint
                if (debug)
                    DrawDebug(ray.origin, ray.origin + ray.direction * ray.distance, new Vector3(1, 1, 1));
                return new Vector3(0, 0, 0);
            }
            //debug for when there is an intersection
            if (debug) { DrawDebug(ray.origin, intersect.intersection, new Vector3(1, 1, 1)); }

            //if the primitive is a mirror
            if (intersect.nearestPrimitive.reflectiveness == 1f)
            {
                Ray refray = reflectRay(ray, intersect.nearestPrimitive.Normal(intersect.intersection, ray.direction), intersect.intersection);
                return Trace(refray);
            }
            else if (intersect.nearestPrimitive.reflectiveness != 0f)
            {
                return intersect.nearestPrimitive.reflectiveness * Trace(reflectRay(ray, intersect.nearestPrimitive.Normal(intersect.intersection, ray.direction), intersect.intersection)) + (1 - intersect.nearestPrimitive.reflectiveness) * DirectIllumination(intersect.intersection, intersect.nearestPrimitive.Normal(intersect.intersection, ray.direction)) * intersect.nearestPrimitive.color;
            }
            else if (intersect.nearestPrimitive.glass)
            {
                float n = 1 / glassRef;
                Sphere sphere = (Sphere) intersect.nearestPrimitive;
                Vector3 N = intersect.nearestPrimitive.Normal(intersect.intersection, ray.direction);
                float cosI = Dot(N, ray.direction);
                float cosT2 = 1f - n * n * (1.0f - cosI * cosI);
                if (cosT2 > 0.0f)
                {
                    Vector3 T = (n * ray.direction) + (float)(n * cosI - Math.Sqrt(cosT2)) * N;
                    T.Normalize();
                    ray.direction = T;
                    ray.origin = intersect.intersection;
                    ray.origin += E * ray.direction;
                    return Trace(ray);
                }
                
                //directIllumination casts a shadow ray.
                //als je directIllumination hier weeghaalt lijkt het net alsof het bijna klopt.
                return DirectIllumination(intersect.intersection, intersect.nearestPrimitive.Normal(intersect.intersection, ray.direction)) * intersect.nearestPrimitive.color;
            }else if (intersect.nearestPrimitive.gloss != 0)
            {
                return DirectIllumination(intersect.intersection, intersect.nearestPrimitive.Normal(intersect.intersection, ray.direction)) * intersect.nearestPrimitive.color * GlossIllumination(ray, intersect.nearestPrimitive, intersect.intersection, intersect.nearestPrimitive.Normal(intersect.intersection, ray.direction), intersect.nearestPrimitive.gloss);
            }
            //if the primitive is not reflective
            return DirectIllumination(intersect.intersection, intersect.nearestPrimitive.Normal(intersect.intersection, ray.direction)) * intersect.nearestPrimitive.color;
        }


        Vector3 DirectIllumination(Vector3 intersection, Vector3 normal)
        {
            Vector3 lighting = new Vector3(0,0,0);
            foreach (Light light in lightSources)
            {
                Vector3 L = (light.origin - intersection);
                float dist = Length(L);
                normal.Normalize();
                L *= (1.0f / dist);

                //test whether there is an object between the lightsource and the intersection point
                if (!IsVisible(light.origin, L, dist))
                    return new Vector3(0, 0, 0);

                float attenuation = 1 / (dist * dist);
                lighting += (light.intensity * Dot(normal, L) * attenuation);
            }
            return lighting;
        }

        Vector3 GlossIllumination(Ray ray, Primitive nearest, Vector3 intersection, Vector3 normal, float gloss)
        {
            ray.direction.Normalize();
            Vector3 lighting = new Vector3(0, 0, 0);
            foreach(Light light in lightSources)
            {
                Vector3 L = (light.origin - intersection);
                float dist = Length(L);
                normal.Normalize();
                L *= (1.0f / dist);

                //test whether there is an object between the lightsource and the intersection point
                if (!IsVisible(light.origin, L, dist))
                    return new Vector3(0, 0, 0);

                Vector3 refDir = L - 2 * normal * Dot(L, normal);
                refDir.Normalize();
                Vector3 ranDir;
                float a = Dot(nearest.Normal(intersection, ray.direction), refDir);

                for (int i = 0; i < 30; i++)
                {

                    /*
                    float x = (float)(random.NextDouble() * (1 - Math.Cos(a)) + Math.Cos(a));
                    float r = Math.Max((float)Math.E-30, x * x - 1);
                    float random_angle = (float)(2 * Math.PI * random.NextDouble());
                    float y = r * (float)Math.Sin(random_angle);
                    float z = r * (float)Math.Cos(random_angle);
                    ranDir = new Vector3(x, y, z);
                    */

                    ranDir = new Vector3(refDir.X * (float)random.NextDouble(), refDir.Y * (float)random.NextDouble(), refDir.Z * (float)random.NextDouble());
                    ranDir.Normalize();
                    lighting += light.intensity * (float)Math.Pow(Dot(refDir, ranDir), gloss);
                }
            }

            return lighting/30;
        }

        public void DrawDebug(Vector3 rayOrigin, Vector3 x, Vector3 color)
        {
            display.Line(Tx(rayOrigin.X), Ty(rayOrigin.Z), Tx(x.X), Ty(x.Z), CalculateHex(color));
        }

        public Intersection IntersectScene(Ray ray)
        {
            float x = ray.distance;
            Intersection k = null;
            foreach (Primitive p in primitives)
            {
                Intersection i = p.CalculateIntersection(ray);
                if (i != null && i.distance < x)
                {
                    x = i.distance;
                    k = i;
                }
            }
            return k;
        }

        public bool IsVisible(Vector3 lightPos, Vector3 LightDir, float dist)
        {
            Ray ray = new Ray(lightPos, -LightDir);
            Intersection i = IntersectScene(ray);
            if (i == null || i.nearestPrimitive.glass)
            {
                if (debug) { DrawDebug(ray.origin, ray.origin + ray.direction * ray.distance, new Vector3(1, 0, 1)); }
                return true;
            }
            if (debug) { DrawDebug(ray.origin, i.intersection, new Vector3(0.5f, 0, 1)); }
            //check the distance of the other intersection we found
            float length = Length(lightPos - i.intersection);
            //return false if it is closer than the one we shoot the shadowray from
            return (length > dist - 2 * E);
            
        }

    }
}
