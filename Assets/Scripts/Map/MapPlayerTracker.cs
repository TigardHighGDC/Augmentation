// Copyright (c) TigardHighGDC
// SPDX-License SPDX-License-Identifier: Apache-2.0

using System;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            Point currentPoint = MapManager.CurrentMap.Path[MapManager.CurrentMap.Path.Count - 1];
            Node currentNode = MapManager.CurrentMap.GetNode(currentPoint);

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
            // TODO: Use static scene manager. See GH-139.
            SceneManager.LoadScene("Basic Enemies");
            break;
        case NodeType.EliteEnemy:
            // TODO: Use static scene manager. See GH-139.
            SceneManager.LoadScene("Elite Enemies");
            break;
        case NodeType.Store:
            // TODO: Use static scene manager. See GH-139.
            SceneManager.LoadScene("Store");
            break;
        case NodeType.CorruptionRemover:
            // TODO: Use static scene manager. See GH-139.
            SceneManager.LoadScene("Corruption Scene");
            break;
        case NodeType.Boss:
            // TODO: Use static scene manager. See GH-139.
            SceneManager.LoadScene("Boss");
            break;
        default:
            Assert.Boolean(false, "Unknown NodeType: " + mapNode.Node.NodeType);
            break; // Will not be reached
        }
    }
}
