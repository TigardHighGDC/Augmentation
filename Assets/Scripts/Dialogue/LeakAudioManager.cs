using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeakAudioManager : MonoBehaviour
{
    public Door DoorInfo;

    private bool hasStarted = false;
    private TextAsset InkJSON;

    void Start()
    {
        int level = GameObject.FindGameObjectWithTag("Tracker").GetComponent<RunTracker>().LevelsCompleted;

        TextAsset[] options = Resources.LoadAll<TextAsset>("Dialogue/LeakedDialogue");

        switch (level)
        {
            case 1:
                InkJSON = options[0];
                break;
            case 2:
                InkJSON = options[1];
                break;
            case 3:
                InkJSON = options[2];
                break;
            case 4:
                InkJSON = options[3];
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasStarted && !DoorInfo.IsLocked)
        {
            hasStarted = true;
            DialogueManager.GetInstance().EnterDialogue(InkJSON);
        }
    }
}
