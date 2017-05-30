using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Constants;

static class Photograph
{
    public static void PhotographMode(bool status)
    {
        if (status)
        {
            antiAliasing = true;
            recursionCount = 8;
        }

        else //slightly optimize performance
        {
            antiAliasing = false;
            recursionCount = 6;
        }
    }
}
