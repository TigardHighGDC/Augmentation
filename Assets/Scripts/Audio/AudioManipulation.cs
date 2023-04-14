// Copyright (c) TigardHighGDC
// SPDX-License SPDX-License-Identifier: Apache-2.0

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManipulation : MonoBehaviour
{
    public static AudioClip BitCrusher(AudioClip original, float compression)
    {
        AudioClip newAudioClip =
            AudioClip.Create("CorruptedGun", original.samples, original.channels, original.frequency, false);

        // Acts as a fake audio compressor. Compression is the power of 2 Hz.
        float[] samples = new float[original.samples * original.channels];
        original.GetData(samples, 0);
        int chunk = original.samples / (int)Mathf.Pow(2, compression);

        for (int i = 0; i < samples.Length; i += chunk)
        {
            float chunkSum = 0.0f;

            for (int j = i; j < (i + chunk) && j < samples.Length; j++)
            {
                chunkSum += samples[j];
            }

            chunkSum /= chunk;

            for (int j = i; j < (i + chunk) && j < samples.Length; j++)
            {
                samples[j] = chunkSum;
            }
        }

        newAudioClip.SetData(samples, 0);
        return newAudioClip;
    }
}
