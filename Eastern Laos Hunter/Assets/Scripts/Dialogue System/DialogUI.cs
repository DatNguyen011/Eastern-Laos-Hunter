using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogUI : Singleton<DialogUI>
{
    public TMP_Text textLabel;
    private TypeWirteEffect typeWirteEffect;
    public void InitDialog(DialogueObject dialogueObject)
    {
        typeWirteEffect = GetComponent<TypeWirteEffect>();
        ShowDialogue(dialogueObject);
    }

    public void ShowDialogue(DialogueObject dialogueObject)
    {
        StartCoroutine(StepThroundDialog(dialogueObject));
    }

    IEnumerator StepThroundDialog(DialogueObject dialogueObject)
    {
        foreach(string Dilogue in dialogueObject.Dialogue)
        {
            yield return typeWirteEffect.Run(Dilogue, textLabel);
            yield return new WaitUntil(() => Input.anyKey );
        }
        CloseDialog();
    }

    public void CloseDialog()
    {
        textLabel.text= string.Empty;
        NPCs.Instance.DestroyNPC();
    }

}
