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
        double counter;
        float scale = 1f / 256f;


        public Raytracer()
        {
            scene = new Scene();
            camera = new Camera();
            primitives = scene.primitives;
        }

        public void Render()
        {
            display.Clear(0x000000);
            camera.HandleInput();

            //de getransleerde locatie van de camera
            int camX = Tx(camera.position.X);
            int camY = Ty(camera.position.Z);
            
            //camera tekenen
            for(int i =-1; i < 2; i++)
                display.Line(camX -1, camY + i, camX + 1, camY + i, 0xff00ff);
            //breedte van het scherm/2
            counter += 0.1d;
            int screenX = (int) (camera.screenSize/2 * screenScale);
            DrawPrimitives();

            //trace the rays.
            for (int x = -256; x < 256; x++)
            {
                for (int y = -256; y < 256; y++)
                {
                    Vector3 n = (x*0.5f) * scale * (camera.screenCorners[1] - camera.screenCorners[0]) + (y*0.5f) * scale * (camera.screenCorners[2] - camera.screenCorners[0]) + camera.direction;
                    n.Normalize();
                    display.Pixel(x + 256, y + 256, CalculateHex(CalculateRay(camera.position, n)));
                }
            }

            //scherm tekenen
            display.Line(camX - screenX, Ty(camera.screenDistance + camera.position.Z), camX + screenX, Ty(camera.screenDistance + camera.position.Z), 0x0000ff);

        }

        void DrawPrimitives()
        {
            foreach(Primitive p in primitives)
            {
                p.DrawPrimitive();
            }
        }
        public Vector3 CalculateRay(Vector3 origin, Vector3 normal)
        {
            Ray ray = new Ray(origin, normal);
            return Trace(ray);

        }

        public Vector3 Trace(Ray ray)
        {
            Intersection i = IntersectScene(ray);

            if (i != null)
            {
                //Draw every single ray with an intersection
                if (ray.direction.Y == 0)
                    display.Line(Tx(camera.position.X), Ty(camera.position.Z), Tx(i.intersection.X), Ty(i.intersection.Z), CalculateHex(i.nearestPrimitive.color));
            
                return i.nearestPrimitive.color;
            }
            else
                if(ray.direction.Y == 0)
                    display.Line(Tx(ray.origin.X), Ty(ray.origin.Z), Tx(ray.origin.X + ray.direction.X * 100), Ty(ray.origin.Z + ray.direction.Z * 100f), 0xffffff);
                return new Vector3(0, 0, 0);
        }
        static Intersection IntersectScene(Ray ray)
        {
            float x = rayLength;
            Intersection k = null;
            foreach (Primitive p in primitives)
            {
                Intersection i = p.CalculateIntersection(ray);
                if (i != null &&i.distance < x)
                {
                    x = i.distance;
                    k = i;
                }
            }
            return k;
        }

    }
}
