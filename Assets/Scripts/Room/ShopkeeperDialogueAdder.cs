using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopkeeperDialogueAdder : MonoBehaviour
{
    private DialogueTrigger dialogueTrigger;

    private void Start()
    {
        dialogueTrigger = GetComponent<DialogueTrigger>();
        TextAsset[] options = Resources.LoadAll<TextAsset>("Dialogue/Shopkeeper");
        int random = Random.Range(0, options.Length);
        dialogueTrigger.InkJSON = options[random];
    }
}
