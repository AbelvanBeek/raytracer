using System;
using System.IO;
using template;
using static Constants;

namespace Template
{
    class Game
    {

	    //Member Variables_ Place member variables here
	    public Surface screen;
        public Raytracer rayTracer;

	    //Init_ Initializes the program
	    public void Init()
	    {
            Constants.display = screen;
            rayTracer = new Raytracer();
	    }

	    //Tick_ Renders one frame
	    public void Tick()
	    {
            screen.Line(screenWidth/2, 0, screenWidth/2, screenHeight, 0xffffff);
            rayTracer.Render();
        }
    }

}