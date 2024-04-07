using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TriggerZone : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI notificationText;
    [SerializeField] private GameObject unlockCarObject;
    [SerializeField] private GameObject car;
    [SerializeField] private bool isCarObtained;
    [SerializeField] private bool isWin;
    [SerializeField] private TextMeshProUGUI reqCrysText;

    private void Start()
    {
        reqCrysText.text = "4";
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InventoryManager im = FindObjectOfType<InventoryManager>();
            if(im.CystalCount >= 4 && !isCarObtained)
            {
                isCarObtained = true;
                unlockCarObject.SetActive(false);
                car.SetActive(true);
                reqCrysText.text = "10";
                //StartCoroutine(PlayTextNotification("Car Unlocked!"));
                DialogueManager.Instance.StartDialogue(2);

            }
            if (im.CystalCount >= 10 && !isWin)
            {
                isWin = true;
                unlockCarObject.SetActive(false);
                //StartCoroutine(PlayTextNotification("You Win"));
                DialogueManager.Instance.StartDialogue(3);
            }
        }
    }
    private IEnumerator PlayTextNotification(string message)
    {
        notificationText.text = message;
        yield return new WaitForSeconds(3.0f);
        notificationText.text = "";
    }
}
