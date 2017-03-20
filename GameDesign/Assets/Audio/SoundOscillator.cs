using UnityEngine;
using System.Collections;

public class SoundOscillator : MonoBehaviour {

    public float frequency = 262;
    double increment, phase;
    double sampling_frequency = 48000.0;

    public float gain;
    public float volume = 0.1f;

    public float[] frequencies;
    public int currentFrequency;
    
    private void OnAudioFilterRead(float[] data, int channels)
    {
        increment = frequency * 2.0 * Mathf.PI / sampling_frequency;

        for (int i = 0; i < data.Length; i += channels){
            phase += increment;
            data[i] = (float)(gain * Mathf.Sin((float)phase));

            if(channels == 2)
            {
                data[i + 1] = data[i];
            }

            if(phase > (Mathf.PI * 2))
            {
                phase = 0.0;
            }
        }
    }
}
