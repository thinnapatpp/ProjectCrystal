using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ImageMover : MonoBehaviour
{
    public RectTransform[] uiElement;  // Reference to your UI element
    public Transform[] targetPosition;   // The position you want to move the UI element to
    public float moveSpeed = 1.0f;   // Speed of the movement
    private bool isMoving = false;
    private float startTime;
    [SerializeField] private int curIndex = -1;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isMoving) // Change this condition to trigger the movement as you like
        {
            if(curIndex >= targetPosition.Length -1)
            {
                curIndex = 0;
            }
            else
            {
                curIndex++;
            }
           
            StartMoving();
        }

        if (isMoving)
        {
            float journeyLength = Vector3.Distance(uiElement[0].position, targetPosition[curIndex].position);
            float journeyTime = journeyLength / moveSpeed;
            float distanceCovered = (Time.time - startTime) * moveSpeed;

            if (distanceCovered < journeyLength)
            {
                float journeyFraction = distanceCovered / journeyLength;
                uiElement[0].position = Vector3.Lerp(uiElement[0].position, targetPosition[curIndex].position, journeyFraction);
            }
            else
            {
                isMoving = false;
            }
        }
    }

    void StartMoving()
    {
        isMoving = true;
        startTime = Time.time;
    }
}
