using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsScript : MonoBehaviour
{
    //public GameObject panel;
    public GameObject settings;
    public GameObject controls;

    public void StartGame()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void Settings()
    {
        settings.SetActive(true);
        controls.SetActive(false);
    }

    public void Controls()
    {
        //panel.SetActive(!panel.activeSelf);
        controls.SetActive(true);
        settings.SetActive(false);
    }
}