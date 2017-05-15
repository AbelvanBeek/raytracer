using OpenTK;
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
        Vector3[] pixels = new Vector3[screenHeight * (screenWidth / 2)];
        

        public Raytracer()
        {
            scene = new Scene();
            camera = new Camera();
            primitives = scene.primitives;
            int i = 0;
            for(int x = -256; x < 256; x++)
            {
                for (int y = -256; y < 256; y++)
                {
                    pixels[i] = new Vector3(x, camera.screenDistance * screenScale, y);
                    i++;
                }
            }
        }

        public void Render()
        {
            //de getransleerde locatie van de camera
            int camX = Tx((int)camera.position.X);
            int camY = Ty((int)camera.position.Z);
            
            //camera tekenen
            for(int i =-1; i < 2; i++)
                display.Line(camX -1, camY + i, camX + 1, camY + i, 0xff00ff);
            //breedte van het scherm/2
            int screenX = (int) (screenScale * camera.screenDistance * Math.Tan(camera.fov/2));
            //scherm tekenen
            display.Line(camX - screenX, Ty((int)camera.screenDistance), camX + screenX, Ty((int)camera.screenDistance), 0xff0000);

            DrawPrimitives();
            foreach (Vector3 v in pixels)
            {
                Vector3 n = v - camera.position;
                n.Normalize();
                display.Pixel((int)v.X + 256, (int)v.Z + 256, CalculateHex(CalculateRay(camera.position, n)));
            }
        }

        void DrawPrimitives()
        {
            foreach(Primitive p in primitives)
            {
                p.DrawPrimitive();
            }
        }
        static Vector3 CalculateRay(Vector3 origin, Vector3 normal)
        {
            Ray ray = new Ray(origin, normal);
            return Trace(ray);

        }

        static Vector3 Trace(Ray ray)
        {
            Intersection i = IntersectScene(ray);
            if (i != null)
                return i.nearestPrimitive.color;
            else
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
