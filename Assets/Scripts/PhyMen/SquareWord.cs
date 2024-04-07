using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Text;

public class SquareWord : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textDisplay;
    public string word;
    private char[] wordCharArray;
    void Start()
    {
        wordCharArray = word.ToCharArray();
        string squareWord = "";
        for (int i = 0; i < wordCharArray.Length + 1; i++)
        {
            squareWord += GetWordLine(wordCharArray, i) + "\n";
        }
        for (int i = wordCharArray.Length - 1; i > 0; i--)
        {
            squareWord += GetWordLine(wordCharArray, i) + "\n";
        }
        textDisplay.text = squareWord;
    }

    private string GetWordLine(char[] wordCharArray, int startChar)
    {
        string tmp = "";
        for (int i = startChar; i < wordCharArray.Length; i++)
        {
            tmp = tmp + wordCharArray[i];
            if (i != wordCharArray.Length - 1)
            {
                tmp = tmp + " ";
            }
        }
        tmp = tmp + "   ";
        if(startChar > 0)
        {
            int index = startChar;
            for (int i = 0; i < index; i++)
            {
                tmp = tmp + wordCharArray[i];
                tmp = tmp + " ";
            }
            for (int i = index-1; i > 0; i--)
            {
                tmp = tmp + wordCharArray[i-1];
                tmp = tmp + " ";
            }

            tmp = tmp + "  ";
        }
        for (int i = wordCharArray.Length - 1; i >= startChar; i--)
        {
            tmp = tmp + wordCharArray[i] + " ";
        }
        return tmp;
    }
}
