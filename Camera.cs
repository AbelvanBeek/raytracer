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
        Vector3 position;
        Vector3 direction;
        Vector3[] screenCorners;
        Vector3[] planeCorners;
        float screenDistance;

        public Camera()
        {
            position = new Vector3(0,0,0);
            direction = new Vector3(0, 0, 1);
            screenDistance = 10;

            //screen
            screenCorners[0] = new Vector3(0, 0, 400);
            screenCorners[0] = new Vector3(512, 0, 400);
            screenCorners[0] = new Vector3(512, 512, 400);
            screenCorners[0] = new Vector3(0, 512, 400);

            //plane
            planeCorners[0] = new Vector3(0, 0, -1);
            planeCorners[0] = new Vector3(100, 0, -1);
            planeCorners[0] = new Vector3(100, 100, -1);
            planeCorners[0] = new Vector3(0, 100, -1);


        }
    }
}
