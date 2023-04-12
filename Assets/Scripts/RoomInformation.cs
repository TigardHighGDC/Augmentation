// Copyright (c) TigardHighGDC
// SPDX-License SPDX-License-Identifier: Apache-2.0

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomInformation : MonoBehaviour
{
    public string MusicType;
    private MusicPlayer musicPlayer;

    private void Start()
    {
        musicPlayer = GameObject.Find("MusicPlayer").GetComponent<MusicPlayer>();

        if (musicPlayer.CurrentMusic != MusicType)
        {
            musicPlayer.FadeInTransition(MusicType);
        }

        Destroy(gameObject);
    }
}
