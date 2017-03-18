using UnityEngine;
using System.Collections;

public class Oscillator : MonoBehaviour {

    public float frequency = 262;
    double increment, phase;
    double sampling_frequency = 48000.0;

    public float gain;
    public float volume = 0.1f;

    public float[] frequencies;
    public int currentFrequency;

    private void Start()
    {
        DontDestroyOnLoad(this);
        frequencies = new float[8];
        frequencies[0] = 262;
        frequencies[1] = 294;
        frequencies[2] = 330;
        frequencies[3] = 349;
        frequencies[4] = 392;
        frequencies[5] = 440;
        frequencies[6] = 494;
        frequencies[7] = 523;
    }

 

    private void FixedUpdate()
    {
        frequency = frequencies[currentFrequency];
        if((Time.time % 1) == 0)
        {
            currentFrequency = Random.Range(0, 7);
        }
        currentFrequency = currentFrequency % frequencies.Length;
    }

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
