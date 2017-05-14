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

        public Raytracer()
        {
            scene = new Scene();
            camera = new Camera();
            primitives = scene.primitives;
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
            int screenX = (int) (screenScale * camera.screenDistance * Math.Tanh(camera.fov/2));
            //scherm tekenen
            display.Line(camX - screenX, Ty((int)camera.screenDistance), camX + screenX, Ty((int)camera.screenDistance), 0xff0000);

            DrawPrimitives();
        }

        void DrawPrimitives()
        {
            foreach(Primitive p in primitives)
            {
                p.DrawPrimitive();
            }
        }
    }
}
