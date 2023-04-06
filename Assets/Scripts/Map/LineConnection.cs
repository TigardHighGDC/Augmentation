// Copyright (c) TigardHighGDC
// SPDX-License SPDX-License-Identifier: Apache-2.0

using UnityEngine;
using UnityEngine.UI.Extensions;

[System.Serializable]
public class LineConnection
{
    public LineRenderer LineRenderer;
    public UILineRenderer UILineRenderer;
    public MapNode From;
    public MapNode To;

    public LineConnection(LineRenderer lr, UILineRenderer uilr, MapNode from, MapNode to)
    {
        LineRenderer = lr;
        UILineRenderer = uilr;
        From = from;
        To = to;
    }

    public void SetColor(Color color)
    {
        if (LineRenderer == null)
        {
            return;
        }

        var gradient = LineRenderer.colorGradient;
        var colorKeys = gradient.colorKeys;

        for (int i = 0; i < colorKeys.Length; i++)
        {
            colorKeys[i].color = color;
        }

        gradient.colorKeys = colorKeys;
        LineRenderer.colorGradient = gradient;

        if (UILineRenderer != null)
        {
            UILineRenderer.color = color;
        }
    }
}
