# UnityCv
A base project to use computer vision algorithms provided by OpenCV inside the Unity game engine.
The plugin was built on MacOS and therefore this project works on either **MacOS** or as an **IOS build**.
The code for the plugin can be found [here](https://github.com/sonnyky/UnityCvPlugin). Please refer to the code for details on the implementation.

# What's inside
### Save image as black and white
<p align="center">
<img src="https://github.com/sonnyky/UnityCv/blob/master/bw.png" width="400">
</p>
Saves an image as black and white

### Image transformation

#### Preprocessing, detecting edges
<p align="center">
<img src="https://github.com/sonnyky/UnityCv/blob/master/edges.jpg" width="400">
</p>
First we detect the edges of the image.

#### Preprocessing, detecting outer hull
<p align="center">
<img src="https://github.com/sonnyky/UnityCv/blob/master/contours.jpg" width="400">
</p>
Next, we detect the outer hull and set it as ROI.

#### Rotation, scaling and overlay on a new image, at an offset
<p align="center">
<img src="https://github.com/sonnyky/UnityCv/blob/master/output_transformed.png" width="400">
</p>
Finally, rotates, scales and translates an image overlaying it on a new image. 
<br>
For more details on the implementation, please see the code(https://github.com/sonnyky/UnityCvPlugin).

### Compare image similarity with shape matching
There is no sample image to show, this function will return the similarity value. The lower the better.

### Compare image similarity with feature matching (SIFT)
<p align="center">
<img src="https://github.com/sonnyky/UnityCv/blob/master/matches.jpg" width="400">
</p>

#### The reference and comparison images
<p align="center">
<img src="https://github.com/sonnyky/UnityCv/blob/master/ref.jpg" width="400">
</p>

<p align="center">
<img src="https://github.com/sonnyky/UnityCv/blob/master/cmp.jpg" width="400">
</p>

Fun fact, OpenCV and Unity has a different axes system, and also RGB order (it's BRG in OpenCV).
This is why the input and comparison images are upside down, and the color is wrong.
However, the SIFT algorithm is not affected by this and outputs correctly.
Only the final matched image was converted to show the correct coloring and orientation.

# Usage

Check out the plugin [code](https://github.com/sonnyky/UnityCvPlugin). And make sure to have the bundle in the correct OSX path in Unity.
Refer to the project for details.
