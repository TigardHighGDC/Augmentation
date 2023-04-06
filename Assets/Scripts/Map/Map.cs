// Copyright (c) TigardHighGDC
// SPDX-License SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

public class Map
{
    public List<Node> Nodes;
    public List<Point> Path;
    public string BossNodeName;
    public string ConfigName;

    public Map(string configName, string bossNodeName, List<Node> nodes, List<Point> path)
    {
        ConfigName = configName;
        BossNodeName = bossNodeName;
        Nodes = nodes;
        Path = path;
    }

    public Node GetBossNode()
    {
        return Nodes.FirstOrDefault(n => n.NodeType == NodeType.Boss);
    }

    public float DistanceBetweenFirstAndLastLayers()
    {
        var bossNode = GetBossNode();
        var firstLayerNode = Nodes.FirstOrDefault(n => n.Point.Y == 0);

        if (bossNode == null || firstLayerNode == null)
        {
            return 0.0f;
        }

        return bossNode.Position.y - firstLayerNode.Position.y;
    }

    public Node GetNode(Point point)
    {
        return Nodes.FirstOrDefault(n => n.Point.Equals(point));
    }

    public string ToJson()
    {
        return JsonConvert.SerializeObject(
            this, Formatting.Indented,
            new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
    }
}
