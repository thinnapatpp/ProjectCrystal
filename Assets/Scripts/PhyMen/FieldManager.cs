using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FieldManager : MonoBehaviour
{
    public List<Transform> spawnTranform = new List<Transform>();
    [SerializeField] private GameObject targetTemplatePrefab;
    private GameObject target;
    public float totalTime = 60.0f;
    private float timeRemaining;
    //public TextMeshProUGUI countdownText;
    [SerializeField] private GameManager gm;
    [SerializeField] private Image timeImg;
    void Start()
    {
        target = Instantiate(targetTemplatePrefab, spawnTranform[Random.Range(0, spawnTranform.Count-1)].position, Quaternion.identity);
        timeRemaining = totalTime;
    }
    void Update()
    {
        SpawnTarget();
        timeRemaining -= Time.deltaTime;
        timeRemaining = Mathf.Clamp(timeRemaining, 0.0f, totalTime);
        //countdownText.text = "Time: " + Mathf.Ceil(timeRemaining).ToString();
        timeImg.fillAmount = timeRemaining / totalTime;
        //if (timeRemaining <= 0.0f)
        //{
        //    gm.SelectField();
        //    SceneManager.LoadScene(0);
        //}
    }

    private void SpawnTarget()
    {
        if (target == null)
        {
            target = Instantiate(targetTemplatePrefab, spawnTranform[Random.Range(0, spawnTranform.Count - 1)].position, Quaternion.identity);
        }
    }
}
