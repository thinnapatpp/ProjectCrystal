using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    [SerializeField] private bool greenCrystal;
    [SerializeField] private bool orangeCrystal;
    [SerializeField] private bool purpleCrystal;
    [SerializeField] private bool alphaCrystal;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InventoryManager im = FindObjectOfType<InventoryManager>();
            if (greenCrystal)
            {
                im.GreenCystalCount++;
                im.CystalCount++;
                DialogueManager.Instance.StartDialogue(5);
            }
            else if (alphaCrystal)
            {
                im.AlphaCrystalCount++;
                DialogueManager.Instance.StartDialogue(6);
            }
            
            Destroy(gameObject);
        }
    }
}
