using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool inMenu = false;

    private void Awake()
    {
        
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void InMenu()
    {
        inMenu = true;
    }

    public void OutMenu()
    {
        inMenu = false;
    }

    public bool GetMenu()
    {
        return inMenu;
    }
}
