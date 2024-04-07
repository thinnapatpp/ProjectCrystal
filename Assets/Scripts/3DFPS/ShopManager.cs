using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [System.Serializable]
    public struct CarModule
    {
        public int price;
        public int motorPower;
        public int energyConsumeRate;
    }


    [SerializeField] private GameObject shopCanvas;
    [SerializeField] private FPSPlayer playerController;
    [SerializeField] private CarController carController;
    [SerializeField] private GameObject gameplayCanvas;
    [SerializeField] private List<CarModule> carModuleAvailableList = new List<CarModule>();
    [SerializeField] private ModuleButton carModButtonTemplate;
    [SerializeField] private GameObject shopVTL;
    [SerializeField] private Button resetMarketButton;
    private bool isOpenShop;
    public bool GetIsOpenShop => isOpenShop;
    private bool isPlayerActive;

    private void Start()
    {
        LoadCarModuleToShop();
        resetMarketButton.onClick.AddListener(() => ResetShop());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J) && !carController.isOnCar)
        {
            
            if (shopCanvas.activeSelf) // close
            {
                isOpenShop = false;
                shopCanvas.SetActive(false);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = false;
                gameplayCanvas.SetActive(true);
            }
            else // open
            {
                isOpenShop = true;
                //playerController.SetEnable(false, true);
                
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
                shopCanvas.SetActive(true);
                gameplayCanvas.SetActive(false);
            }
        }
        if(InventoryManager.Instance.AlphaCrystalCount > 0)
        {
            resetMarketButton.interactable = true;
        }
        else
        {
            resetMarketButton.interactable = false;
        }
    }

    private void ResetShop()
    {
        carModuleAvailableList.Clear();
        for (int i = 0; i < 2; i++)
        {
            carModuleAvailableList.Add(new CarModule
            {
                price = Random.Range(1,12) * 100,
                motorPower = Random.Range(1, 5) * 100,
                energyConsumeRate = Random.Range(1, 14) * 5
            });
        }
        InventoryManager.Instance.AlphaCrystalCount--;
        DialogueManager.Instance.StartDialogue(8);
    }

    public void LoadCarModuleToShop()
    {
        foreach (CarModule carMod in carModuleAvailableList)
        {
            ModuleButton newCarModButton = Instantiate(carModButtonTemplate, shopVTL.transform);
            newCarModButton.Init(carMod);
        }
        carModButtonTemplate.gameObject.SetActive(false);
    }
}
