using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ModuleButton : MonoBehaviour
{
    [SerializeField] private Button moduleButton;
    [SerializeField] private CarController carController;
    [SerializeField] private TextMeshProUGUI moduleName;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private TextMeshProUGUI detailText;
    private int moudulePrice;

    public void Init(ShopManager.CarModule carMod)
    {
        //priceText.text = carMod.price.ToString();
        moduleName.text = "CarModule #" + Random.Range(1088, 17831).ToString();
        detailText.text = carMod.price.ToString() + "$\n" + "Energy Consume% " + carMod.energyConsumeRate.ToString() + "\n" + "+MotorPower " + carMod.motorPower.ToString();
        moudulePrice = carMod.price;
        moduleButton.onClick.AddListener(() => ModuleApply(carMod));
    }

    private void Update()
    {
        if(InventoryManager.Instance.Currency >= moudulePrice){
            moduleButton.interactable = true;
        }
        else
        {
            moduleButton.interactable = false;
        }
    }

    private void ModuleApply(ShopManager.CarModule carMod)
    {
        if(InventoryManager.Instance.Currency >= carMod.price)
        {
            carController.ApplyModole(carMod);
            InventoryManager.Instance.Currency -= carMod.price;
            moduleButton.interactable = false;
        }
        else
        {
            DialogueManager.Instance.StartDialogue(7);
        }
        
    }
}
