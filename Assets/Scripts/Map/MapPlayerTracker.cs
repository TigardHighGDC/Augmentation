// Copyright (c) TigardHighGDC
// SPDX-License SPDX-License-Identifier: Apache-2.0

using System;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class MapPlayerTracker : MonoBehaviour
{
    public bool LockAfterSelecting = false;
    public float EnterNodeDelay = 1.0f;
    public MapManager MapManager;
    public MapView View;

    public static MapPlayerTracker Instance;

    public bool Locked { get; set; }

    private void Awake()
    {
        Instance = this;
    }

    public void SelectNode(MapNode mapNode)
    {
        if (Locked)
        {
            // Node already selected this showing of the map
            return;
        }

        if (MapManager.CurrentMap.Path.Count == 0)
        {
            if (mapNode.Node.Point.Y == 0)
            {
                SendPlayerToNode(mapNode);
            }
            else
            {
                // TODO: Warn player that this node is inaccessible
            }
        }
        else
        {
            var currentPoint = MapManager.CurrentMap.Path[MapManager.CurrentMap.Path.Count - 1];
            var currentNode = MapManager.CurrentMap.GetNode(currentPoint);

            if (currentNode != null && currentNode.Outgoing.Any(Point => Point.Equals(mapNode.Node.Point)))
            {
                SendPlayerToNode(mapNode);
            }
            else
            {
                // TODO: Warn player that this node is inaccessible
            }
        }
    }

    private void SendPlayerToNode(MapNode mapNode)
    {
        Locked = LockAfterSelecting;
        MapManager.CurrentMap.Path.Add(mapNode.Node.Point);
        MapManager.SaveMap();
        View.SetLineColors();
        View.SetAttainableNodes();
        mapNode.ShowSwirlAnimation();

        DOTween.Sequence().AppendInterval(EnterNodeDelay).OnComplete(() => EnterNode(mapNode));
    }

    private static void EnterNode(MapNode mapNode)
    {
        switch (mapNode.Node.NodeType)
        {
        case NodeType.MinorEnemy:
            break;
        case NodeType.EliteEnemy:
            break;
        case NodeType.RestSite:
            break;
        case NodeType.Treasure:
            break;
        case NodeType.Store:
            break;
        case NodeType.Boss:
            break;
        case NodeType.Mystery:
            break;
        default:
            Assert.Boolean(false, "Unknown NodeType: " + mapNode.Node.NodeType);
            break; // Will not be reached
        }
    }
}
