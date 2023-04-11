// Copyright (c) TigardHighGDC
// SPDX-License SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class MapGenerator
{
    private static MapConfig config;

    // clang-format off
    private static List<NodeType> randomNodes = new List<NodeType> {
        // Contains every valid node type that can be randomly selected from
        NodeType.Mystery, 
        NodeType.Store, 
        NodeType.Treasure, 
        NodeType.MinorEnemy, 
        NodeType.RestSite
    };
    // clang-format on

    private static List<float> layerDistances;
    private static List<List<Point>> paths;
    private static List<List<Node>> nodes = new List<List<Node>>();

    public static Map GetMap(MapConfig conf)
    {
        if (conf == null)
        {
            Assert.Boolean(false, "MapConfig is null");
        }

        config = conf;
        nodes.Clear();

        // Create new layer distances
        layerDistances = new List<float>();

        foreach (MapLayer layer in config.Layers)
        {
            layerDistances.Add(layer.DistanceFromPreviousLayer.GetValue());
        }

        // Create and place new layers
        for (int i = 0; i < conf.Layers.Count; i++)
        {
            MapLayer layer = config.Layers[i];
            List<Node> nodesOnThisLayer = new List<Node>();

            // Offset of this layer to make all the nodes centered
            float offset = layer.NodesApartDistance * config.GridWidth / 2.0f;

            for (int j = 0; j < config.GridWidth; j++)
            {
                NodeType nodeType = Random.Range(0.0f, 1.0f) < layer.RandomizeNodes ? GetRandomNode() : layer.NodeType;
                string blueprintName = config.NodeBlueprints.Where(b => b.NodeType == nodeType).ToList().Random().name;
                Node node = new Node(nodeType, blueprintName, new Point(j, i)) {
                    Position = new Vector2(-offset + j * layer.NodesApartDistance, GetDistanceToLayer(i))
                };
                nodesOnThisLayer.Add(node);
            }

            nodes.Add(nodesOnThisLayer);
        }

        // Generate node paths
        Point finalNode = GetFinalNode();
        paths = new List<List<Point>>();
        int numOfStartingNodes = config.numOfStartingNodes.GetValue();
        int numOfPreBossNodes = config.numOfPreBossNodes.GetValue();

        List<int> candidateXs = new List<int>();

        for (int i = 0; i < config.GridWidth; i++)
        {
            candidateXs.Add(i);
        }

        candidateXs.Shuffle();
        IEnumerable<int> startingXs = candidateXs.Take(numOfStartingNodes);
        List<Point> startingPoints = (from x in startingXs select new Point(x, 0)).ToList();

        candidateXs.Shuffle();
        IEnumerable<int> preBossXs = candidateXs.Take(numOfPreBossNodes);
        List<Point> preBossPoints = (from x in preBossXs select new Point(x, finalNode.Y - 1)).ToList();

        int numOfPaths = Mathf.Max(numOfStartingNodes, numOfPreBossNodes) + Mathf.Max(0, config.ExtraPaths);

        for (int i = 0; i < numOfPaths; ++i)
        {
            Point startNode = startingPoints[i % numOfStartingNodes];
            Point endNode = preBossPoints[i % numOfPreBossNodes];
            List<Point> path = Path(startNode, endNode);
            path.Add(finalNode);
            paths.Add(path);
        }

        // Randomize node positions
        for (int i = 0; i < nodes.Count; i++)
        {
            List<Node> list = nodes[i];
            MapLayer layer = config.Layers[i];
            float distToNextLayer = (i + 1 >= layerDistances.Count) ? 0.0f : layerDistances[i + 1];
            float distToPreviousLayer = layerDistances[i];

            foreach (Node node in list)
            {
                float xRnd = Random.Range(-1.0f, 1.0f);
                float yRnd = Random.Range(-1.0f, 1.0f);

                float x = xRnd * layer.NodesApartDistance / 2.0f;
                float y = (yRnd < 0) ? distToPreviousLayer * yRnd / 2.0f : distToNextLayer * yRnd / 2.0f;

                node.Position += new Vector2(x, y) * layer.RandomizePosition;
            }
        }

        // Create node connections
        foreach (List<Point> path in paths)
        {
            for (int i = 0; i < path.Count - 1; ++i)
            {
                Node node = GetNode(path[i]);
                Node nextNode = GetNode(path[i + 1]);
                node.AddOutgoing(nextNode.Point);
                nextNode.AddIncoming(node.Point);
            }
        }

        // Remove paths that cross each other
        for (int i = 0; i < config.GridWidth - 1; ++i)
        {
            for (int j = 0; j < config.Layers.Count - 1; ++j)
            {
                Node node = GetNode(new Point(i, j));
                Node right = GetNode(new Point(i + 1, j));
                Node top = GetNode(new Point(i, j + 1));
                Node topRight = GetNode(new Point(i + 1, j + 1));

                if ((node == null || node.HasNoConnections()) || (right == null || right.HasNoConnections()) ||
                    (top == null || top.HasNoConnections()) || (topRight == null || topRight.HasNoConnections()))
                {
                    continue;
                }

                if (!node.Outgoing.Any(e => e.Equals(topRight.Point)) || !right.Outgoing.Any(e => e.Equals(top.Point)))
                {
                    continue;
                }

                // Cross path found
                node.AddOutgoing(top.Point);
                top.AddIncoming(node.Point);

                right.AddOutgoing(topRight.Point);
                topRight.AddIncoming(right.Point);

                float rnd = Random.Range(0.0f, 1.0f);

                // Randomize which path to remove
                if (rnd < 0.2f)
                {
                    node.RemoveOutgoing(topRight.Point);
                    topRight.RemoveIncoming(node.Point);

                    right.RemoveOutgoing(top.Point);
                    top.RemoveIncoming(right.Point);
                }
                else if (rnd < 0.6f)
                {
                    node.RemoveOutgoing(topRight.Point);
                    topRight.RemoveIncoming(node.Point);
                }
                else
                {
                    right.RemoveOutgoing(top.Point);
                    top.RemoveIncoming(right.Point);
                }
            }
        }

        List<Node> nodesList =
            nodes.SelectMany(n => n).Where(n => n.Incoming.Count > 0 || n.Outgoing.Count > 0).ToList();
        string bossNodeName = config.NodeBlueprints.Where(b => b.NodeType == NodeType.Boss).ToList().Random().name;

        return new Map(conf.name, bossNodeName, nodesList, new List<Point>());
    }

    private static float GetDistanceToLayer(int layerIndex)
    {
        if (layerIndex < 0 || layerIndex > layerDistances.Count)
        {
            return 0.0f;
        }
        else
        {
            return layerDistances.Take(layerIndex + 1).Sum();
        }
    }

    private static Node GetNode(Point p)
    {
        if (p.Y >= nodes.Count)
        {
            return null;
        }
        else if (p.X >= nodes[p.Y].Count)
        {
            return null;
        }
        else
        {
            return nodes[p.Y][p.X];
        }
    }

    private static Point GetFinalNode()
    {
        int y = config.Layers.Count - 1;

        if (config.GridWidth % 2 == 1)
        {
            return new Point(config.GridWidth / 2, y);
        }

        return (Random.Range(0, 2) == 0) ? new Point(config.GridWidth / 2, y) : new Point(config.GridWidth / 2 - 1, y);
    }

    // Generates a random path bottom up
    private static List<Point> Path(Point fromPoint, Point toPoint)
    {
        int toRow = toPoint.Y;
        int toCol = toPoint.X;

        int lastNodeCol = fromPoint.X;

        List<Point> path = new List<Point> { fromPoint };
        List<int> candidateCols = new List<int>();

        for (int i = 1; i < toRow; ++i)
        {
            candidateCols.Clear();

            int verticalDistance = toRow - i;
            int horizontalDistance;

            int forwardCol = lastNodeCol;
            horizontalDistance = Mathf.Abs(toCol - forwardCol);

            if (horizontalDistance <= verticalDistance)
            {
                candidateCols.Add(lastNodeCol);
            }

            int leftCol = lastNodeCol - 1;
            horizontalDistance = Mathf.Abs(toCol - leftCol);

            if (leftCol >= 0 && horizontalDistance <= verticalDistance)
            {
                candidateCols.Add(leftCol);
            }

            int rightCol = lastNodeCol + 1;
            horizontalDistance = Mathf.Abs(toCol - rightCol);

            if (rightCol < config.GridWidth && horizontalDistance <= verticalDistance)
            {
                candidateCols.Add(rightCol);
            }

            int RandomCandidateIndex = Random.Range(0, candidateCols.Count);
            int candidateCol = candidateCols[RandomCandidateIndex];
            Point nextPoint = new Point(candidateCol, i);

            path.Add(nextPoint);
            lastNodeCol = candidateCol;
        }

        path.Add(toPoint);

        return path;
    }

    private static NodeType GetRandomNode()
    {
        return randomNodes[Random.Range(0, randomNodes.Count)];
    }
}
