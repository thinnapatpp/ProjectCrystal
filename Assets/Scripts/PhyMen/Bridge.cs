using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour
{
    public Transform pointA; // Set this in the Inspector to the starting point.
    public Transform pointB; // Set this in the Inspector to the ending point.
    public float bridgeWidth = 1.0f; // Set the bridge width.
    public GameObject bridgeSegmentPrefab; // Assign the cube segment prefab in the Inspector.

    void Start()
    {
        Vector3 startPos = pointA.position;
        Vector3 endPos = pointB.position;
        Vector3 bridgeDirection = (endPos - startPos).normalized;
        float bridgeLength = Vector3.Distance(startPos, endPos);
        int numberOfSegments = Mathf.FloorToInt(bridgeLength / bridgeWidth);

        for (int i = 0; i < numberOfSegments; i++)
        {
            Vector3 segmentPosition = startPos + bridgeDirection * (i * bridgeWidth + bridgeWidth / 2);

            GameObject bridgeSegment = Instantiate(bridgeSegmentPrefab, segmentPosition, Quaternion.identity);
            bridgeSegment.transform.localScale = new Vector3(1, bridgeWidth, 1); // Adjust the scale as needed.
        }
    }
}
