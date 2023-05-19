// Copyright (c) TigardHighGDC
// SPDX-License SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

public class PlayerStats
{
    public float MaxHealth;
    public List<string> StartingWeapons;
    public int Money;

    public PlayerStats()
    {
        MaxHealth = 150.0f;
        StartingWeapons = new List<string>();
        Money = 99999;
    }

    public string ToJson()
    {
        return JsonConvert.SerializeObject(
            this, Formatting.Indented,
            new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
    }
}
