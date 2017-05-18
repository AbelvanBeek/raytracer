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
    class Camera
    {
        public Vector3 position;
        Vector3 direction;
        Vector3[] screenCorners;
        public double radiansfov;
        public float screenDistance;
        public float screenSize;

        public Camera()
        {
            position = new Vector3(0, 0, 0);
            direction = new Vector3(0, 0, 0);
            radiansfov =  90 * (Math.PI / 180);
            screenSize = (screenWidth  / screenScale / (axisLength*2));
            screenDistance = (screenSize/2) / (float) Math.Tan(radiansfov/2);
            screenCorners = new Vector3[4];

            //screen
            screenCorners[0] = new Vector3(0, 0, 1);
            screenCorners[0] = new Vector3(512, 0, 400);
            screenCorners[0] = new Vector3(512, 512, 400);
            screenCorners[0] = new Vector3(0, 512, 400);

        }
        public void HandleInput()
        {
            KeyboardState k = Keyboard.GetState();
            if (k.IsKeyDown(Key.Up))
                position.Y += 0.05f;
            if (k.IsKeyDown(Key.Down))
                position.Y -= 0.05f;
            if (k.IsKeyDown(Key.Left))
                position.X += 0.05f;
            if (k.IsKeyDown(Key.Right))
                position.X -= 0.05f;
            if (k.IsKeyDown(Key.Z))
                position.Z += 0.05f;
            if (k.IsKeyDown(Key.X))
                position.Z -= 0.05f;
        }
    }
}
