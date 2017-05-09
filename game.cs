using System;
using System.IO;

namespace Template
{
    class Game
    {

	    //Member Variables_ Place member variables here
	    public Surface screen;

	    //Init_ Initializes the program
	    public void Init()
	    {        
	    }

	    //Tick_ Renders one frame
	    public void Tick()
	    {

		    screen.Clear( 0 );
		    screen.Print( "Hello World!", 2, 2, 0xffffff );
            screen.Line(2, 20, 160, 20, 0xff0000);
	    }
    }

}