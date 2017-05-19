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
    class Camera
    {
        public Vector3 position;
        public Vector3 direction;
        public Vector3[] screenCorners;
        public double radiansfov;
        public float screenDistance;
        public float screenSize;

        public Camera()
        {
            position = new Vector3(0, 0, 0);
            direction = new Vector3(0, 0, 1);
            screenSize = (screenWidth  / screenScale / (axisLength*2));
            screenDistance = 1;
            screenCorners = new Vector3[4];

            //screen
            screenCorners[0] = new Vector3(-1, 1, 0);
            screenCorners[1] = new Vector3(1, 1, 0);
            screenCorners[2] = new Vector3(-1, -1, 0);
            screenCorners[3] = new Vector3(1, -1, 0);

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
