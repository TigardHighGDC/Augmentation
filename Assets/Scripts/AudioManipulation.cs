using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManipulation : MonoBehaviour
{
    public static void BitCrusher(AudioClip original, int compression)
    {
        float[] samples = new float[original.samples * original.channels];
        original.GetData(samples, 0);
        int chunk = original.samples / (int)Mathf.Pow(2, compression);
        for (int i = 0; i < samples.Length; i += chunk)
        {
            float chunkSum = 0f;
            for (int u = i; u < i + chunk && u < samples.Length; u++)
            {
                chunkSum += samples[u];
            }
            chunkSum /= chunk;
            for (int u = i; u < i + chunk && u < samples.Length; u++)
            {
                samples[u] = chunkSum;
            }
        }
        original.SetData(samples, 0);
    }
}
