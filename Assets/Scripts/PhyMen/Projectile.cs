using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private GameObject hitParticle;
    [SerializeField] private GameObject targetHitParticle;
    private GameManager gm;

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
        Destroy(gameObject, 3.0f);
    }

    ////private void OnCollisionEnter(Collision collision)
    ////{
    ////    if (!collision.gameObject.CompareTag("Unmark"))
    ////    {
    ////        Instantiate(hitParticle, gameObject.transform.position, Quaternion.identity);
    ////        Instantiate(targetHitParticle, gameObject.transform.position, Quaternion.identity);
    ////        //collision.gameObject.GetComponent<TargetAim>().HitTarget();
    ////        gm.AssignBridgeMark(gameObject.transform);
    ////        Destroy(gameObject);
    ////    }
    ////    else
    ////    {
            
    ////        Instantiate(hitParticle, gameObject.transform.position, Quaternion.identity);
    ////        Destroy(gameObject);
    ////    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Zone"))
        {

        }
        else if (!other.gameObject.CompareTag("Unmark"))
        {
            Vector3 projectileTranform = gameObject.transform.position;
            gm.AssignBridgeMark(projectileTranform);
            Instantiate(hitParticle, gameObject.transform.position, Quaternion.identity);
            Instantiate(targetHitParticle, gameObject.transform.position, Quaternion.identity);
            //collision.gameObject.GetComponent<TargetAim>().HitTarget();
            Destroy(gameObject);
        }
        else
        {

            Instantiate(hitParticle, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }


}
