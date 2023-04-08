using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public string Room;

    public void OnStartButton()
    {
        SceneManager.LoadScene(Room);
    }
}
