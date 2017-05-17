using OpenTK;
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
            screenCorners = new Vector3[4];
            screenSize = (screenWidth  / screenScale / (axisLength*2));
            screenDistance = (screenSize/2) / (float) Math.Tan(radiansfov/2);

            //screen
            screenCorners[0] = new Vector3(0, 0, 400);
            screenCorners[0] = new Vector3(512, 0, 400);
            screenCorners[0] = new Vector3(512, 512, 400);
            screenCorners[0] = new Vector3(0, 512, 400);

        }
    }
}
