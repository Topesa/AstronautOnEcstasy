using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject controlPanel;

    private void Update()
    {
        PressKey();
    }

    private void PressKey()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Debug.Log("Exit test");
            Application.Quit();
            return;
        }

        if (Input.anyKeyDown)
        {
            if (Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Mouse1))
            {
                return;
            }
            else
            {
                SceneManager.LoadScene(1);
            }
        }
    }

    public void EnableDisalbePanel()
    {
        if (controlPanel.activeInHierarchy)
        {
            controlPanel.SetActive(false);
        }
        else
        {
            controlPanel.SetActive(true);
        }
    }

}
