TEAM MEMBERS
__________________________________
Abel van Beek	| Wouter Castelein
5978831		| 5936608
__________________________________

BONUS ASSINGMENTS
__________________________________

Glossy reflections 
| For every intersection with a glossy object, ten random rays are casted for every lightsource.
| Using the spherical coordinate system and two random values between 0 and 1, the direction vectors
| of the random vectors are calculated. Then the shadow rays of the random generated rays are cast and
| their color is multiplied by the intensity value of the intersected object. Eventually the average
| color is calculated and returned for the pixel color.
|
| Location
| Raytracer.cs --> GlossIllumination()

Anti-aliasing
| For every ray that is being cast, 8 more rays are casted that are very close to the original ray. 
| Each of these rays are traced and their colors are averaged into a variable and eventually the color
| of the original ray is also added to the average color. This completely averaged color is then returned
| and the pixel takes the color of this value.
|
| Location
| Raytracer.cs --> AntiAliasing()

Textures
| 
|
|
|

Refraction/absorption
|
|
|
|

Acceleration structure
|
|
|
|

__________________________________

MATERIALS
__________________________________

Team Codermind tutorial
| As a general guideline for specific subjects we used the extensive tutorial on raytracing by the 
| Codermind Team. For example, we used this tutorial to see how the Fresnel calculated had to be done.

Stackoverflow --> Glossy reflection in ray tracing
| Since our own glossy reflection code did not seem to work, we started looking around on the internet.
| Having tried numerous examples we ended up using one from stackoverflow. However, the glossy effect
| is still far from working properly. 