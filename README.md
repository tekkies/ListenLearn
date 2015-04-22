# ListenLearn

Experimental project so far.
Code can be run using the NUnit tests defined.

## Listen

So far, running some Fast Fourier Transform (FFT) test cases.

	[TestCase(5.25, 5)] //Input = 10.25Hz, Expect peak at 10Hz

Input waveform (positive only)
	
	0   *                                                            
	1   *           **          **          **          **           
	2  ***         ***          **          **          ***         *
	3  ***         ***         ***          ***         ***         *
	4  ***         ****        ****        ****         ***         *
	5  ****        ****        ****        ****        ****        **
	6 *****       *****        ****        ****        *****       **
	7 *****       *****       *****        *****       *****       **
	8 *****       *****       ******      ******       *****       **
	9 *****       ******      ******      ******      ******       **
	 ----------------------------------------------------------------

Output spectrum

	0     *                          
	1     *                          
	2     *                          
	3     *                          
	4     *                          
	5     *                          
	6     *                          
	7     **                         
	8     **                         
	9   ******                       
	 --------------------------------

## Learn

Initial results of standard XOR neural network learning using back propagation:

    Nodes Attempts Epochs       
        2      1.9   2135           
        3      1.1   2033

By changing the number of middle layer nodes, learning can be more successful and faster.  If it's not learned within 3000 epochs, it probably never will, so start the learning process again.


# Credits
Standing on the shoulders of giants, including but not limited to:

* [AForge](http://www.aforgenet.com/)
* [Nunit](http://www.nunit.org/)
* [Autofac](http://autofac.org/)
