using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetAim : MonoBehaviour
{
    private GameManager gm;
    public void HitTarget()
    {
        gm = FindObjectOfType<GameManager>();
        gm.increaseMastery();
        Destroy(gameObject);
        Destroy(gameObject);
    }
}
