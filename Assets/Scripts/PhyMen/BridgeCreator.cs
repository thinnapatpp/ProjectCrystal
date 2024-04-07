using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeCreator : MonoBehaviour
{
    public GameObject bridgePrefab; // The prefab of the bridge cube
    public float bridgeWidth = 1.0f; // Width of the bridge

    private Vector3 startPos;
    private Vector3 endPos;

    [SerializeField]
    private Transform start;
    [SerializeField]
    private Transform end;

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonDown(0)) // Left mouse button click
        //{
        //    if (startPos == Vector3.zero)
        //    {
        //        startPos = GetMouseWorldPosition();
        //    }
        //    else if (endPos == Vector3.zero)
        //    {
        //        endPos = GetMouseWorldPosition();
        //        CreateBridge();
        //    }
        //}
    }

    private void Start()
    {
        CreateBridge();
    }

    //Vector3 GetMouseWorldPosition()
    //{
    //    Vector3 mousePosition = Input.mousePosition;
    //    mousePosition.z = Camera.main.transform.position.y;
    //    return Camera.main.ScreenToWorldPoint(mousePosition);
    //}

    void CreateBridge()
    {
        Vector3 bridgeDirection = end.position - start.position;
        Vector3 bridgeCenter = start.position + bridgeDirection * 0.5f;

        GameObject bridge = Instantiate(bridgePrefab, bridgeCenter, Quaternion.identity);
        bridge.transform.forward = bridgeDirection.normalized;

        Vector3 scale = bridge.transform.localScale;
        scale.z = bridgeDirection.magnitude / bridgeWidth;
        scale.y = 0.2f;
        scale.x = bridgeWidth;
        bridge.transform.localScale = scale;
    }
}
