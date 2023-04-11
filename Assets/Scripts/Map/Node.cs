// Copyright (c) TigardHighGDC
// SPDX-License SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using UnityEngine;

public class Node
{
    public Point Point;
    public List<Point> Incoming = new List<Point>();
    public List<Point> Outgoing = new List<Point>();

    [JsonConverter(typeof(StringEnumConverter))]
    public NodeType NodeType;
    public string BlueprintName;
    public Vector2 Position;

    public Node(NodeType nodeType, string blueprintName, Point point)
    {
        NodeType = nodeType;
        BlueprintName = blueprintName;
        Point = point;
    }

    public void AddIncoming(Point point)
    {
        if (!Incoming.Any(element => element.Equals(point)))
        {
            Incoming.Add(point);
        }
    }

    public void AddOutgoing(Point point)
    {
        if (!Outgoing.Any(element => element.Equals(point)))
        {
            Outgoing.Add(point);
        }
    }

    public void RemoveIncoming(Point point)
    {
        Incoming.RemoveAll(element => element.Equals(point));
    }

    public void RemoveOutgoing(Point point)
    {
        Outgoing.RemoveAll(element => element.Equals(point));
    }

    public bool HasNoConnections()
    {
        return Incoming.Count == 0 && Outgoing.Count == 0;
    }
}
