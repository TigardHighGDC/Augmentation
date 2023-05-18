// Copyright (c) TigardHighGDC
// SPDX-License SPDX-License-Identifier: Apache-2.0

using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MapNode : MonoBehaviour
{
    public SpriteRenderer SpriteRenderer;
    public Image Image;
    public SpriteRenderer VisitedSpriteRenderer;
    public Image CircleImage;
    public Image VisitedCircleImage;

    public Node Node { get; private set; }
    public NodeBlueprint Blueprint { get; private set; }

    private float initialScale;
    private const float HoverScaleFactor = 1.2f;
    private float mouseDownTime;

    private const float MaxClickDuration = 0.5f;

    public void SetUp(Node node, NodeBlueprint blueprint)
    {
        Node = node;
        Blueprint = blueprint;

        if (SpriteRenderer != null)
        {
            SpriteRenderer.sprite = blueprint.Sprite;
        }

        if (Image != null)
        {
            Image.sprite = blueprint.Sprite;
        }

        if (node.NodeType == NodeType.Boss)
        {
            transform.localScale *= 1.5f;
        }

        if (SpriteRenderer != null)
        {
            initialScale = SpriteRenderer.transform.localScale.x;
        }

        if (Image != null)
        {
            initialScale = Image.transform.localScale.x;
        }

        if (VisitedSpriteRenderer != null)
        {
            VisitedSpriteRenderer.color = MapView.Instance.VisitedColor;
            VisitedSpriteRenderer.gameObject.SetActive(false);
        }

        if (CircleImage != null)
        {
            CircleImage.color = MapView.Instance.VisitedColor;
            CircleImage.gameObject.SetActive(false);
        }

        SetState(NodeStates.Locked);
    }

    public void SetState(NodeStates state)
    {
        if (VisitedSpriteRenderer != null)
        {
            VisitedSpriteRenderer.gameObject.SetActive(false);
        }

        if (CircleImage != null)
        {
            CircleImage.gameObject.SetActive(false);
        }

        switch (state)
        {
            case NodeStates.Locked:
                if (SpriteRenderer != null)
                {
                    SpriteRenderer.DOKill();
                    SpriteRenderer.color = MapView.Instance.LockedColor;
                }

                if (Image != null)
                {
                    Image.DOKill();
                    Image.color = MapView.Instance.LockedColor;
                }
                break;
            case NodeStates.Visited:
                if (SpriteRenderer != null)
                {
                    SpriteRenderer.DOKill();
                    SpriteRenderer.color = MapView.Instance.VisitedColor;
                }

                if (Image != null)
                {
                    Image.DOKill();
                    Image.color = MapView.Instance.VisitedColor;
                }

                if (VisitedSpriteRenderer != null)
                {
                    VisitedSpriteRenderer.gameObject.SetActive(true);
                }

                if (CircleImage != null)
                {
                    CircleImage.gameObject.SetActive(true);
                }
                break;
            case NodeStates.Attainable:
                if (SpriteRenderer != null)
                {
                    SpriteRenderer.color = MapView.Instance.LockedColor;
                    SpriteRenderer.DOKill();
                    SpriteRenderer.DOColor(MapView.Instance.VisitedColor, 0.5f).SetLoops(-1, LoopType.Yoyo);
                }

                if (Image != null)
                {
                    Image.color = MapView.Instance.LockedColor;
                    Image.DOKill();
                    Image.DOColor(MapView.Instance.VisitedColor, 0.5f).SetLoops(-1, LoopType.Yoyo);
                }
                break;
            default:
                Assert.Boolean(false, "Unhandled node state: " + state);
                break; // Will not be reached
        }
    }

    public void OnMouseDown()
    {
        mouseDownTime = Time.time;
    }

    public void OnMouseUp()
    {
        if (Time.time - mouseDownTime < MaxClickDuration)
        {
            MapPlayerTracker.Instance.SelectNode(this);
        }
    }

    public void ShowSwirlAnimation()
    {
        if (VisitedCircleImage == null)
        {
            return;
        }

        const float fillDuration = 0.3f;
        VisitedCircleImage.fillAmount = 0;

        DOTween.To(() => VisitedCircleImage.fillAmount, x => VisitedCircleImage.fillAmount = x, 1.0f, fillDuration);
    }
}
