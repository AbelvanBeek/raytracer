﻿using OpenTK;
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
        Vector3[] pixels = new Vector3[screenHeight * (screenWidth / 2)];
        

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
            int j = 0;
            for (float x = -256; x < 256; x++)
            {
                for (float y = -256; y < 256; y++)
                {
                    pixels[j] = new Vector3(x / screenScale / axisLength + camera.position.X, y / screenScale / axisLength + camera.position.Y, camera.screenDistance + camera.position.Z);
                    j++;
                }
            }
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
            foreach (Vector3 v in pixels)
            {
                Vector3 n = v - camera.position;
                n.Normalize();
                display.Pixel((int)(((v.X - camera.position.X) * screenScale * axisLength)) + 256, (int)((v.Y - camera.position.Y)  * screenScale * axisLength) + 256, CalculateHex(CalculateRay(camera.position, n)));
            }

            //scherm tekenen
            display.Line(camX - screenX, Ty(camera.screenDistance + camera.position.Z), camX + screenX, Ty(camera.screenDistance + camera.position.Z), 0xffffff);

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
