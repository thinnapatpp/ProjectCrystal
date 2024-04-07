using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpManager : MonoBehaviour
{
    //[SerializeField] private GameObject warpTarget;
    [SerializeField] private Vector3 warpDestination;
    [SerializeField] private GameObject player;
    public void Warp()
    {
        Debug.Log("Before Warp");
        //GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.transform.position = new Vector3(0, 0, 0);
            Debug.Log("Warping player");
        }
        Debug.Log("After Warp");
    }
}
