# ListenLearn

Experimental project so far.

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

# Credits
Standing on the shoulders of giants, including but not limited to:

* [AForge](http://www.aforgenet.com/)
* [Nunit](http://www.nunit.org/)
* [Autofac](http://autofac.org/)
