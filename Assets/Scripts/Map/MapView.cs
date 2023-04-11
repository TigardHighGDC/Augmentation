// Copyright (c) TigardHighGDC
// SPDX-License SPDX-License-Identifier: Apache-2.0

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapView : MonoBehaviour
{
    [Header("Map Settings")]
    public MapManager MapManager;

    public List<MapConfig> AllMapConfigs;
    public GameObject NodePrefab;

    public float OrientationOffset;

    [Header("Background Settings")]
    public Sprite Background;
    public Color32 BackgroundColor = Color.white;
    public float XSize;
    public float YOffset;

    [Header("Line Settings")]
    public GameObject LinePrefab;
    [Range(3, 10)]
    public int LinePointsCount = 10;
    public float OffsetFromNodes = 0.5f;

    [Header("Colors")]
    public Color32 VisitedColor = Color.white;
    public Color32 LockedColor = Color.gray;
    public Color32 LineVisitedColor = Color.white;
    public Color32 LineLockedColor = Color.gray;

    private GameObject firstParent;
    private GameObject mapParent;
    private List<List<Point>> paths;
    private Camera cam;

    [HideInInspector]
    public List<MapNode> MapNodes = new List<MapNode>();

    private List<LineConnection> lineConnections = new List<LineConnection>();

    public static MapView Instance;

    public Map Map { get; private set; }

    private void Awake()
    {
        Instance = this;
        cam = Camera.main;
    }

    public void ShowMap(Map m)
    {
        if (m == null)
        {
            Assert.Boolean(false, "Map is null");
            return;
        }

        Map = m;

        // Clear the current map
        if (firstParent != null)
        {
            Destroy(firstParent);
        }

        MapNodes.Clear();
        lineConnections.Clear();

        // Create new map parent object
        firstParent = new GameObject("OuterMapParent");
        mapParent = new GameObject("MapParentWithAScroll");
        mapParent.transform.SetParent(firstParent.transform);
        ScrollNonUI scrollNonUi = mapParent.AddComponent<ScrollNonUI>();
        scrollNonUi.FreezeX = true;
        scrollNonUi.FreezeY = false;
        BoxCollider boxCollider = mapParent.AddComponent<BoxCollider>();
        boxCollider.size = new Vector3(100, 100, 1);

        // Instantiate each node
        foreach (Node node in m.Nodes)
        {
            // For each node the object and blueprint must be instantiated even though they will be an exact copy in the
            // beginning
            GameObject mapNodeObject = Instantiate(NodePrefab, mapParent.transform);
            MapNode mapNode = mapNodeObject.GetComponent<MapNode>();
            NodeBlueprint blueprint = GetBlueprint(node.BlueprintName);
            mapNode.SetUp(node, blueprint);
            mapNode.transform.localPosition = node.Position;
            MapNodes.Add(mapNode);
        }

        // Draw the paths between nodes
        if (LinePrefab != null)
        {
            foreach (MapNode node in MapNodes)
            {
                foreach (Point connection in node.Node.Outgoing)
                {
                    MapNode to = GetNode(connection);
                    GameObject lineObject = Instantiate(LinePrefab, mapParent.transform);
                    LineRenderer lineRenderer = lineObject.GetComponent<LineRenderer>();
                    Vector3 fromPoint = node.transform.position +
                                    (to.transform.position - node.transform.position).normalized * OffsetFromNodes;

                    Vector3 toPoint = to.transform.position +
                                  (node.transform.position - to.transform.position).normalized * OffsetFromNodes;

                    lineObject.transform.position = fromPoint;
                    lineRenderer.useWorldSpace = false;

                    lineRenderer.positionCount = LinePointsCount;

                    for (int i = 0; i < LinePointsCount; i++)
                    {
                        lineRenderer.SetPosition(
                            i, Vector3.Lerp(Vector3.zero, toPoint - fromPoint, (float)i / (LinePointsCount - 1)));
                    }

                    lineConnections.Add(new LineConnection(lineRenderer, null, node, to));
                }
            }
        }

        // Add the scroll element to the map
        float span = MapManager.CurrentMap.DistanceBetweenFirstAndLastLayers();
        MapNode bossNode = MapNodes.FirstOrDefault(node => node.Node.NodeType == NodeType.Boss);
        firstParent.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y, 0.0f);
        float offset = OrientationOffset;

        if (scrollNonUi != null)
        {
            scrollNonUi.YConstraints.max = 0;
            scrollNonUi.YConstraints.min = -(span + 2.0f * offset);
        }

        firstParent.transform.localPosition += new Vector3(0, offset, 0);

        SetAttainableNodes();
        SetLineColors();

        if (Background != null)
        {
            GameObject backgroundObject = new GameObject("Background");
            backgroundObject.transform.SetParent(mapParent.transform);
            backgroundObject.transform.localPosition =
                new Vector3(bossNode.transform.localPosition.x, span / 2.0f, 0.0f);
            backgroundObject.transform.localRotation = Quaternion.identity;
            SpriteRenderer sr = backgroundObject.AddComponent<SpriteRenderer>();
            sr.color = BackgroundColor;
            sr.drawMode = SpriteDrawMode.Sliced;
            sr.sprite = Background;
            sr.size = new Vector2(XSize, span + YOffset * 2.0f);
        }
    }

    public void SetAttainableNodes()
    {
        foreach (MapNode node in MapNodes)
        {
            node.SetState(NodeStates.Locked);
        }

        if (MapManager.CurrentMap.Path.Count == 0)
        {
            // Each node in the first layer is attainable
            foreach (MapNode node in MapNodes.Where(n => n.Node.Point.Y == 0))
            {
                node.SetState(NodeStates.Attainable);
            }
        }
        else
        {
            foreach (Point point in MapManager.CurrentMap.Path)
            {
                MapNode mapNode = GetNode(point);

                if (mapNode != null)
                {
                    mapNode.SetState(NodeStates.Visited);
                }
            }

            Point currentPoint = MapManager.CurrentMap.Path[MapManager.CurrentMap.Path.Count - 1];
            Node currentNode = MapManager.CurrentMap.GetNode(currentPoint);

            foreach (Point point in currentNode.Outgoing)
            {
                MapNode mapNode = GetNode(point);

                if (mapNode != null)
                {
                    mapNode.SetState(NodeStates.Attainable);
                }
            }
        }
    }

    public void SetLineColors()
    {
        foreach (LineConnection connection in lineConnections)
        {
            connection.SetColor(LineLockedColor);
        }

        if (MapManager.CurrentMap.Path.Count == 0)
        {
            return;
        }

        Point currentPoint = MapManager.CurrentMap.Path[MapManager.CurrentMap.Path.Count - 1];
        Node currentNode = MapManager.CurrentMap.GetNode(currentPoint);

        foreach (Point point in currentNode.Outgoing)
        {
            LineConnection lineConnection = lineConnections.FirstOrDefault(conn => conn.From.Node == currentNode &&
                                                                        conn.To.Node.Point.Equals(point));
            lineConnection?.SetColor(LineVisitedColor);
        }

        if (MapManager.CurrentMap.Path.Count <= 1)
            return;

        for (int i = 0; i < MapManager.CurrentMap.Path.Count - 1; i++)
        {
            Point current = MapManager.CurrentMap.Path[i];
            Point next = MapManager.CurrentMap.Path[i + 1];
            LineConnection lineConnection = lineConnections.FirstOrDefault(conn => conn.From.Node.Point.Equals(current) &&
                                                                        conn.To.Node.Point.Equals(next));
            lineConnection?.SetColor(LineVisitedColor);
        }
    }

    private MapNode GetNode(Point point)
    {
        return MapNodes.FirstOrDefault(n => n.Node.Point.Equals(point));
    }

    private MapConfig GetConfig(string configName)
    {
        return AllMapConfigs.FirstOrDefault(c => c.name == configName);
    }

    private NodeBlueprint GetBlueprint(NodeType type)
    {
        MapConfig config = GetConfig(MapManager.CurrentMap.ConfigName);
        return config.NodeBlueprints.FirstOrDefault(n => n.NodeType == type);
    }

    private NodeBlueprint GetBlueprint(string blueprintName)
    {
        MapConfig config = GetConfig(MapManager.CurrentMap.ConfigName);
        return config.NodeBlueprints.FirstOrDefault(n => n.name == blueprintName);
    }
}
