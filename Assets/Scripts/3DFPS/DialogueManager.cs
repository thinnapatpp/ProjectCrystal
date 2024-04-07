using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{

    private static DialogueManager instance;
    public static DialogueManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<DialogueManager>();
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject(typeof(DialogueManager).Name);
                    instance = singletonObject.AddComponent<DialogueManager>();
                }
            }
            return instance;
        }
    }

    [SerializeField] private List<DialogueSO> dialogueSOList = new List<DialogueSO>();
    [SerializeField] private TextMeshProUGUI dialogueTextGUI;
    [SerializeField] private GameObject dialoguePanel;
    private Coroutine currentCoroutine;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        dialoguePanel.SetActive(false);
    }

    private void Start()
    {
        StartDialogue(0);
    }

    public void StartDialogue(int index)
    {
        if (currentCoroutine != null)
            StopCoroutine(currentCoroutine);
        currentCoroutine = StartCoroutine(RunDialogue(dialogueSOList[index]));
    }
    public void StartDialogue(DialogueSO dso)
    {
        if (currentCoroutine != null)
            StopCoroutine(currentCoroutine);
        currentCoroutine = StartCoroutine(RunDialogue(dso));
    }

    private IEnumerator RunDialogue(DialogueSO dso)
    {
        int i = 0;
        dialoguePanel.SetActive(true);
        while (dso.GetDialogueList.Count > i)
        {
            dialogueTextGUI.text = dso.GetDialogueList[i];
            i++;
            yield return new WaitForSeconds(4.5f);
        }
        dialoguePanel.SetActive(false);
    }


}
