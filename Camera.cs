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
        public float screenDistance;
        public float screenSize;

        public Camera()
        {
            position = new Vector3(0, 0f, 0);
            direction = new Vector3(0, 0, 1);
            screenSize = 2;
            screenDistance = 1;
            screenCorners = new Vector3[4];

            UpdateScreen();

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
            if (k.IsKeyDown(Key.C))
                screenDistance += 0.05f; UpdateScreen();
            if (k.IsKeyDown(Key.V))
                screenDistance -= 0.05f; UpdateScreen();
            if (k.IsKeyDown(Key.W))
                camRotation.Y += 0.2f; UpdateScreen();
            if (k.IsKeyDown(Key.A))
                camRotation.X -= 0.2f; UpdateScreen();
            if (k.IsKeyDown(Key.S))
                camRotation.Y -= 0.2f; UpdateScreen();
            if (k.IsKeyDown(Key.D))
                camRotation.X += 0.2f; UpdateScreen();
            if (k.IsKeyDown(Key.P))
                antiAliasing = !antiAliasing; UpdateScreen();
        }

        public void UpdateScreen()
        {
            //screen
            screenCorners[0] = new Vector3(-1, 1, screenDistance); // top left
            screenCorners[1] = new Vector3(1, 1, screenDistance); // top right
            screenCorners[2] = new Vector3(-1, -1, screenDistance); // bottom right
            screenCorners[3] = new Vector3(1, -1, screenDistance); // bottom left
        }
    }
}
