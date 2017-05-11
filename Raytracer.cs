using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace template
{
    public class Raytracer
    {
        Scene scene;
        Camera camera;
        Template.Surface display;
        Vector3 centerDebug;

        public Raytracer(Template.Surface screen)
        {
            display = screen;
            centerDebug = new Vector3(256, 50, 0);
            camera = new Camera();
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
            int screenX = (int) (camera.screenDistance * Math.Tanh(camera.fov/2));
            //scherm tekenen
            display.Line(camX - screenX, Ty((int)camera.screenDistance), camX + screenX, Ty((int)camera.screenDistance), 0xff0000);
        }
        int Tx(int x)
        {
            return display.width/2 +1 + x + (int)centerDebug.X;
        }
        int Ty(int y)
        {
            return  display.height - (y + (int)centerDebug.Y);
        }
    }
}
