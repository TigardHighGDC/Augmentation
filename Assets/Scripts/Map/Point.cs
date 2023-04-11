// Copyright (c) TigardHighGDC
// SPDX-License SPDX-License-Identifier: Apache-2.0

using System;

public class Point : IEquatable<Point>
{
    public int X;
    public int Y;

    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }

    public bool Equals(Point other)
    {
        if (ReferenceEquals(null, other))
        {
            return false;
        }
        else if (ReferenceEquals(this, other))
        {
            return true;
        }
        else
        {
            return X == other.X && Y == other.Y;
        }
    }
}
