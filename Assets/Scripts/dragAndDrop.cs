using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class dragAndDrop : MonoBehaviour
{
    private bool dragging = false;
    private Vector3 offset;

    private void Update()
    {
        if(dragging)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
        }
    }

    public void OnMouseDown()
    {

        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dragging = true;
    }

    public void OnMouseUp()
    {
        dragging= false;
    }
}
