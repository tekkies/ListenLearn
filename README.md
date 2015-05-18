# ListenLearn - Experimental project

* Status: Mothballed - basic principles can be demonstrated
* Code can be run using the NUnit tests defined from Visual Studio 2014.
* Original idea: Would it be possible for an app running on a mobile device be able to identify which machines are running in a laundry room, based on input from the device microphone.
* Components 
  * Listen: Use FFT to take an audio waveform and extract the prominent frequencies
  * Learn: Using a neural network, try to identify a spectrum, and thus the machine making the sound
  * Client: Xamarin Android app to record & store sound samples 
* Next
  * Incrementally extend SpectrumLearnerTest, simulating more complex/noisy waveforms.
  * Experiment with network and learning configuration & how it affects learning  

## Initial Results

### Listening

#### Pure waveforms

AnalyserTest_AnalyseSin runs some Fast Fourier Transform (FFT) test cases based on pure waveforms.

	[TestCase(5.25, 5)] //Input = 10.25Hz, Expect peak at 10Hz
    AnalyserTest_AnalyseSin

Input waveform (positive only displayed)
	
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

#### Recorded Waveforms

AudioSampleLoaderTest_FindPeakFrequency identifies peak frequencies in recordings from an (out-of-tune) piano.

    [TestCase(@"C4.44100.sample", 2048, 243, 263)]
    [TestCase(@"D4.44100.sample", 1024, 274, 294)]
    [TestCase(@"E4.44100.sample", 1024, 308, 328)]
    [TestCase(@"E4.44100.sample", 512, 298, 338)]
    public void AudioSampleLoaderTest_FindPeakFrequency(string file, int samples, int expectedLower, int expectedUpper)

## Learn

### XOR
Initial results of standard XOR neural network learning using back propagation:

    Nodes Attempts Epochs       
        2      1.9   2135           
        3      1.1   2033

Findings: By changing the number of middle layer nodes, learning can be more successful and faster.  If it's not learned within 3000 epochs, it probably never will, so start the learning process again.

### SpectrumLearner

SpectrumLearnerTest_Learn takes some pure wave input waveforms (with some noise added), converts to a spectrum, then attempts to identify the spectrum.  

# Credits
Standing on the shoulders of giants, including but not limited to:

* [AForge](http://www.aforgenet.com/) - maths
* [Nunit](http://www.nunit.org/) - unit testing
* [Autofac](http://autofac.org/) - dependency injection
