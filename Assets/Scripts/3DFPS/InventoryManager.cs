using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryManager : MonoBehaviour
{

    private static InventoryManager instance;
    public static InventoryManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<InventoryManager>();
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject(typeof(InventoryManager).Name);
                    instance = singletonObject.AddComponent<InventoryManager>();
                }
            }
            return instance;
        }
    }

    [SerializeField] private int greenCrystalCount = 0;
    [SerializeField] private int crystalCount = 0;
    private int alphaCrystalCount;
    private int carPowerCount = 0;
    [SerializeField] private TextMeshProUGUI crystalCountText;
    [SerializeField] private TextMeshProUGUI carPowerText;
    [SerializeField] private int currency;

    public int Currency
    {
        get { return currency; }
        set { currency = value; }
    }

    public int CystalCount
    {
        get { return crystalCount; }
        set { crystalCount = value; }
    }

    public int GreenCystalCount
    {
        get { return greenCrystalCount; }
        set { greenCrystalCount = value; }
    }

    public int AlphaCrystalCount
    {
        get { return alphaCrystalCount; }
        set { alphaCrystalCount = value; }
    }

    public int CarPower
    {
        get { return carPowerCount; }
        set { carPowerCount = value; }
    }

    private void Update()
    {
        crystalCountText.text = "Crystal Count: " + greenCrystalCount.ToString();
        if (carPowerCount > 0)
        {
            carPowerText.text = "Preparing Terminal...\n\n\nPower: <color=#00ff00ff>Ready</color>";
            carPowerText.text = "Preparing Terminal...\n\n\nPower: <color=#00ff00ff>" + carPowerCount + "% </color>";
        }
        else
        {
            carPowerText.text = "Preparing Terminal...\n\n\nPower: <color=#ff0000ff>" + carPowerCount + "% </color>";
            //carPowerText.text = "Preparing Terminal...\n\n\nPower: <color=#ff0000ff>0% (Low Power) </color>";
        }

    }
}
