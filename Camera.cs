using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace template
{
    class Camera
    {
        public Vector3 position;
        Vector3 direction;
        Vector3[] screenCorners;
        public float fov;
        public float screenDistance;

        public Camera()
        {
            position = new Vector3(0,0,0);
            direction = new Vector3(0, 0, 1);
            fov = 90;
            screenCorners = new Vector3[4];
            screenDistance = 50;

            //screen
            screenCorners[0] = new Vector3(0, 0, 400);
            screenCorners[0] = new Vector3(512, 0, 400);
            screenCorners[0] = new Vector3(512, 512, 400);
            screenCorners[0] = new Vector3(0, 512, 400);

        }
    }
}
