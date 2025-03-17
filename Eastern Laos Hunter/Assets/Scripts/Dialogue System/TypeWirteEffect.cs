using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TypeWirteEffect : Singleton<TypeWirteEffect>
{

    public Coroutine Run(string indexText, TMP_Text textlabel)
    {
        return StartCoroutine(WriteText(indexText, textlabel));
    }

    IEnumerator WriteText(string indexText, TMP_Text textlabel)
    {
        textlabel.text = string.Empty;
        float t = 0;
        int charIndex = 0;
        while (charIndex < indexText.Length)
        {

            t += Time.deltaTime * 40f;
            charIndex = Mathf.FloorToInt(t);
            charIndex = Mathf.Clamp(charIndex, 0, indexText.Length);
            textlabel.text = indexText.Substring(0, charIndex);
            yield return null;
        }
        textlabel.text = indexText;
    }
}
