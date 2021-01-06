﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImputMenager : MonoBehaviour
{
    [SerializeField]
    private Camera mainCamera;
    [SerializeField]
    private LayerMask clickableLayerMask;
    private IClickable lastclicedObject;

    // Update is called once per frame
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            HandleLeftMouseButtonClicked();
        }
        //Debug.Log(Input.mousePosition);
    }

    private void HandleLeftMouseButtonClicked()
    {
        if (Input.mousePosition.x < 836 && Input.mousePosition.y > 100)
        {
            RaycastHit2D hit = Physics2D.Raycast(mainCamera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, distance: 100, layerMask: clickableLayerMask);
            if (hit.collider != null)
            {
                IClickable currentTarget = hit.collider.gameObject.GetComponent<IClickable>();
                currentTarget?.OnSelected();
                SetSelectedObject(currentTarget);
            }
            else
            {
                SetSelectedObject(null);
            }
        }
    }

    private void SetSelectedObject(IClickable clicable)
    {
        lastclicedObject?.OnDeselect();

        if(lastclicedObject != clicable)
        {
            lastclicedObject = clicable;
        }
        else
        {
            lastclicedObject = null;
        }

        lastclicedObject?.OnSelected();
    }
}
