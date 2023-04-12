// Copyright (c) TigardHighGDC
// SPDX-License SPDX-License-Identifier: Apache-2.0

using DG.Tweening;
using UnityEngine;

public class ScrollNonUI : MonoBehaviour
{
    public float TweenBackDuration = 0.3f;
    public Ease TweenBackEase;
    public bool FreezeX;
    public FloatMinMax XConstraints = new FloatMinMax();
    public bool FreezeY;
    public FloatMinMax YConstraints = new FloatMinMax();

    private Vector2 offset;
    private Vector3 pointerDisplacement;
    private float zDisplacement;
    private bool dragging;
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
        zDisplacement = -mainCamera.transform.position.z + transform.position.z;
    }

    private void Update()
    {
        if (!dragging)
        {
            return;
        }

        Vector3 mousePos = MouseInWorldCoords();
        transform.position =
            new Vector3((FreezeX) ? transform.position.x : mousePos.x - pointerDisplacement.x,
                        (FreezeY) ? transform.position.y : mousePos.y - pointerDisplacement.y, transform.position.z);
    }

    public void OnMouseDown()
    {
        pointerDisplacement = -transform.position + MouseInWorldCoords();
        transform.DOKill();
        dragging = true;
    }

    public void OnMouseUp()
    {
        dragging = false;
        TweenBack();
    }

    private Vector3 MouseInWorldCoords()
    {
        Vector3 screenMousePos = Input.mousePosition;
        screenMousePos.z = zDisplacement;
        return mainCamera.ScreenToWorldPoint(screenMousePos);
    }

    private void TweenBack()
    {
        if (FreezeY)
        {
            if (transform.localPosition.x >= XConstraints.min && transform.localPosition.x <= XConstraints.max)
            {
                return;
            }

            float targetX = (transform.localPosition.x < XConstraints.min) ? XConstraints.min : XConstraints.max;
            transform.DOLocalMoveX(targetX, TweenBackDuration).SetEase(TweenBackEase);
        }
        else if (FreezeX)
        {
            if (transform.localPosition.y >= YConstraints.min && transform.localPosition.y <= YConstraints.max)
            {
                return;
            }

            float targetY = (transform.localPosition.y < YConstraints.min) ? YConstraints.min : YConstraints.max;
            transform.DOLocalMoveY(targetY, TweenBackDuration).SetEase(TweenBackEase);
        }
    }
}
