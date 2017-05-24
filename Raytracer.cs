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
        double counter;
        float scale = 1f / 256f;
        public bool debug2D = true;
        public Vector3[] rays2D;


        public Raytracer()
        {
            rays2D = new Vector3[screenWidth/2];
            scene = new Scene();
            camera = new Camera();
            primitives = scene.primitives;
            lightSources = scene.lightSources;
        }

        public void Render()
        {
            display.Clear(0x000000);
            camera.HandleInput();

            //de getransleerde locatie van de camera
            int camX = Tx(camera.position.X);
            int camY = Ty(camera.position.Z);

            //trace the rays.
            for (int x = -256; x < 256; x++)
            {
                for (int y = -256; y < 256; y++)
                {
                    display.Pixel(x + 256, y + 256, CalculateHex(Trace(x, y)));
                }
            }

            if (debug2D)
            {
                //camera tekenen
                for (int i = -1; i < 2; i++)
                    display.Line(camX - 1, camY + i, camX + 1, camY + i, 0xff00ff);
                //breedte van het scherm/2
                counter += 0.1d;
                int screenX = (int)(camera.screenSize / 2 * screenScale);
                DrawPrimitives();

                //scherm tekenen
                display.Line(camX - screenX, Ty(camera.screenDistance + camera.position.Z), camX + screenX, Ty(camera.screenDistance + camera.position.Z), 0x0000ff);
            }
        }

        void DrawPrimitives()
        {
            foreach(Primitive p in primitives)
            {
                p.DrawPrimitive();
            }
        }

        public Vector3 Trace( int x, int y)
        {
            Vector3 n = (x * 0.5f) * scale * (camera.screenCorners[1] - camera.screenCorners[0]) + (y * 0.5f) * scale * (camera.screenCorners[2] - camera.screenCorners[0]) + new Vector3(0, 0, camera.screenDistance);
            n.Normalize();
            Ray ray = new Ray(camera.position, n);

            Intersection i = IntersectScene(ray);
            if (debug2D)
            {
                if (y == 0 && x % 10 == 0)
                {
                    DrawDebug(ray, i);
                }
            }
            else
            {
                display.Pixel(512 + 256 + x, 256 + y, CalculateHex(new Vector3(1,1,1) * ( 10- ray.distance/10)));
            }

            if (i != null)
            {
                //Shadow rays
                foreach (Light light in lightSources)
                {
                    Vector3 iets = (i.intersection - light.origin);
                    Vector3 dir = iets;
                    dir.Normalize();
                    Ray temp = new Ray(light.origin + E*dir, dir);
                    temp.distance = Length(iets) - 2 * E;
                    //ray.origin = new Vector3(-2, 0, 0);
                    if (IntersectScene(temp) == null)
                    {
                        if (ray.direction.Y == 0)
                        DrawDebug(temp, i);
                        Vector3 test = DirectIllumination(i.intersection, i.intersection - i.nearestPrimitive.position, light) * i.nearestPrimitive.color;
                        return test;
                    }
                    else
                        return new Vector3(0, 0, 0);

                }
            }
            return new Vector3(0, 0, 0);
        }

        Vector3 DirectIllumination(Vector3 intersection, Vector3 normal, Light light)
        {
            Vector3 L = light.origin - intersection;
            float dist = Length(L);
            normal.Normalize();
            L *= (1.0f / dist);
            float attenuation = 1 / (dist * dist);
            return (light.intensity * Dot(normal, L) * attenuation);
        }

        public void DrawDebug(Ray ray, Intersection x)
        {
                if (x != null)
                    display.Line(Tx(ray.origin.X), Ty(ray.origin.Z), Tx(x.intersection.X), Ty(x.intersection.Z), 0xffff00);
                else
                    display.Line(Tx(ray.origin.X), Ty(ray.origin.Z), Tx(ray.origin.X + ray.direction.X * ray.distance), Ty(ray.origin.Z + ray.direction.Z * ray.distance), 0xffff00);
        }

        static Intersection IntersectScene(Ray ray)
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

    }
}
