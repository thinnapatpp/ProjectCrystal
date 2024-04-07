using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObject/DialogueSO", menuName = "DialogueSO")]
public class DialogueSO : ScriptableObject
{
    [SerializeField] private List<string> dialogueText = new List<string>();
    public List<string> GetDialogueList => dialogueText;

}
