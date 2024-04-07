using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;
using TMPro;

public class DayManager : MonoBehaviour
{

    [SerializeField] private PlayableDirector dayPassTimeline;
    [SerializeField] private TextMeshProUGUI fundingText;
    [SerializeField] private TextMeshProUGUI dayText;
    [SerializeField] private TextMeshProUGUI temperText;
    private bool isDayPassing = false;
    private int currentDate = 1;

    private static DayManager instance;
    public static DayManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<DayManager>();
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject(typeof(DayManager).Name);
                    instance = singletonObject.AddComponent<DayManager>();
                }
            }
            return instance;
        }
    }

    private void Update()
    {
        fundingText.text = InventoryManager.Instance.Currency.ToString() + "$";
    }

    public void OnDayPass()
    {
        if (isDayPassing)
        {
            return;
        }
        isDayPassing = true;
        dayPassTimeline.Play();
        StartCoroutine(OnDayPassSequence());
    }

    private IEnumerator OnDayPassSequence()
    {
        int datePass = Random.Range(1, 5);
        int temperature = Random.Range(-10, 20);
        currentDate = currentDate + datePass;
        yield return new WaitForSeconds(3.0f);
        InventoryManager.Instance.Currency = 0;
        for (int i = 0; i < InventoryManager.Instance.GreenCystalCount; i++)
        {
            InventoryManager.Instance.Currency += 150;
        }
        InventoryManager.Instance.GreenCystalCount = 0;
        //fundingText.text = InventoryManager.Instance.Currency.ToString() + "$";
        dayText.text = "Day " + currentDate;
        temperText.text = temperature + "°C";
        yield return new WaitForSeconds(3.0f);
        isDayPassing = false;
    }
}
