using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerObject : MonoBehaviour
{
    [SerializeField] private GameObject parentObj;
    [SerializeField] private int powerStore;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InventoryManager im = FindObjectOfType<InventoryManager>();
            im.CarPower += powerStore;
            DialogueManager.Instance.StartDialogue(4);
            Destroy(parentObj);
        }
    }
}
