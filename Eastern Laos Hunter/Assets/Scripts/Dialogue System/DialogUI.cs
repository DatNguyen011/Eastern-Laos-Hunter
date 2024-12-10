using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogUI : Singleton<DialogUI>
{
    public GameObject dialogBox;
    public TMP_Text textLabel;
    public DialogueObject TestDialog;
    private TypeWirteEffect typeWirteEffect;
    void Start()
    {
        InitDialog();
    }

    public void InitDialog()
    {
        typeWirteEffect = GetComponent<TypeWirteEffect>();
        //CloseDialog();
        ShowDialogue(TestDialog);
    }

    public void ShowDialogue(DialogueObject dialogueObject)
    {
        StartCoroutine(StepThroundDialog(dialogueObject));
    }

    IEnumerator StepThroundDialog(DialogueObject dialogueObject)
    {
        foreach(string Dilogue in dialogueObject.Dialogue)
        {
            //??i Run ch?y xong m?i ti?p t?c
            yield return typeWirteEffect.Run(Dilogue, textLabel);
            yield return new WaitUntil(() => Input.anyKey );
        }
        CloseDialog();
    }

    public void CloseDialog()
    {
        dialogBox.SetActive(false);
        textLabel.text= string.Empty;
    }

}
