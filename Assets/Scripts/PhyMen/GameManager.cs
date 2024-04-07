using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject gameplayCanvas;
    [SerializeField] private GameObject fieldSelectCanvas;
    [SerializeField] private GameObject player;
    [SerializeField] private TextMeshProUGUI masteryText;
    [SerializeField] private TextMeshProUGUI arrowText;
    public int aimMastery = 0;

    private Vector3 pointA;
    private Vector3 pointB;
    private bool mark1 = false;
    private bool mark2 = false;
    private float bridgeWidth = 0.1f;
    private GameObject bridge;
    [SerializeField] private GameObject bridgeSegmentPrefab;
    private List<GameObject> bridgeSegmentList = new List<GameObject>();
    [SerializeField] private GameObject mark1Canvas;
    [SerializeField] private GameObject mark2Canvas;
    [SerializeField] private GameObject nani;
    public int arrowCount = 0;


    void Start()
    {
        SelectField();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
        arrowText.text = "Arrow: " + arrowCount;
    }

    private void GameInit()
    {
        fieldSelectCanvas.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        gameplayCanvas.SetActive(true);
        player.GetComponent<PlayerController>().enabled = true;
        player.GetComponentInChildren<CameraController>().enabled = true;
    }

    public void SelectField()
    {
        fieldSelectCanvas.SetActive(true);
        gameplayCanvas.SetActive(false);
        player.GetComponent<PlayerController>().enabled = false;
        player.GetComponentInChildren<CameraController>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void InitAimTrainingField()
    {
        GameInit();
    }

    public void increaseMastery()
    {
        aimMastery++;
        masteryText.text = aimMastery.ToString();
    }

    public void SetArrowAmount(int amount)
    {
        arrowCount = amount;
    }

    public int GetArrowAmount()
    {
        return arrowCount;
    }
    private void GenerateBridge()
    {
        Vector3 bridgeDirection = pointB - pointA;
        Vector3 bridgeCenter = pointA + bridgeDirection * 0.5f;
        if (bridge != null)
        {
            Destroy(bridge);
        }
        Debug.Log(bridgeDirection.magnitude);
        if (bridgeDirection.magnitude < 1)
        {
            //scale.z = 1;
        }
        if (bridgeDirection.magnitude < 25)
        {
            bridge = Instantiate(bridgeSegmentPrefab, bridgeCenter, Quaternion.identity);
            bridge.transform.forward = bridgeDirection.normalized;
            Vector3 scale = bridge.transform.localScale;
            scale.z = bridgeDirection.magnitude / 1;
            scale.y = 0.2f;
            scale.x = 1;
            bridge.transform.localScale = scale;
        }
        else
        {
            //Bridge too long
        }
    }

    public void AssignBridgeMark(Vector3 pos)
    {
        if (!mark1)
        {
            mark1 = true;
            pointA = pos;
            if(!mark1Canvas.activeSelf)
            {
                mark1Canvas.SetActive(true);  
            }
            mark1Canvas.transform.position = pointA;
            Debug.Log("Mark1 assigned" + pos);
        }
        else if (mark1 && !mark2)
        {
            
            mark2 = true;
            pointB = pos;
            if (!mark2Canvas.activeSelf)
            {
                mark2Canvas.SetActive(true);
            }
            mark2Canvas.transform.position = pointB;
            Debug.Log("Mark2 assigned" + pos);
            
            GenerateBridge();
            mark1 = false;
            mark2 = false;
            pointA = Vector3.zero;
            pointB = Vector3.zero;
        }
        else
        {
            Debug.LogError("Wrong mark assign");
        }
    }

    public void ExitGame() 
    {
        nani.gameObject.SetActive(true);
    }
}
