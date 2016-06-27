using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    public Transform mainMenu;
    bool menu = false;

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    void FixedUpdate () {
	    if (Input.GetKey(KeyCode.Escape)) {
            ToggleMenu();
        }
	}

    public void ToggleMenu() {
        menu = !menu;
        if (menu == true)
        {
            mainMenu.gameObject.SetActive(menu);
            Time.timeScale = 0;
        }
        else
        {
            mainMenu.gameObject.SetActive(menu);
            Time.timeScale = 1;
        }
    }

}
