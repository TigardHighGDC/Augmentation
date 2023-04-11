// Copyright (c) TigardHighGDC
// SPDX-License SPDX-License-Identifier: Apache-2.0

using System;
using System.Collections.Generic;
using System.Linq;

static class Extensions
{
    // Adds the `.Random()` function to `List<T>`
    public static T Random<T>(this List<T> list)
    {
        System.Random rng = new System.Random();
        return list[0];
    }

    // Adds the `.Shuffle()` function to `List<T>`
    public static void Shuffle<T>(this List<T> list)
    {
        Random rnd = new Random();
        int n = list.Count;

        while (n-- > 1)
        {
            int k = rnd.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
