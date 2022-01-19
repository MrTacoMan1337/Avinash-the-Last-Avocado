using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{
        private Vector3 screenPoint; private Vector3 offset;
        private Grid grid;
    void Start()
        {
            grid = FindObjectOfType<Grid>();
        }


        void Update()
        {
            screenPoint = Camera.main.WorldToScreenPoint(transform.position);
            offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        }



    void OnMouseDown()
    {
        
    }

    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        curPosition = grid.GetNearestPointOnGrid(curPosition);
        curPosition.y = .5f;
        transform.position = curPosition;
    }



}


