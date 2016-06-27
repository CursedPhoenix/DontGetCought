using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public Transform mainMenu, optionsMenu;

    public void LoadScene(string name) {
        SceneManager.LoadScene(name);
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void OptionsMenu(bool clicked) {
        if (clicked == true) {
            optionsMenu.gameObject.SetActive(clicked);
            mainMenu.gameObject.SetActive(!clicked);
        }
        else {
            optionsMenu.gameObject.SetActive(clicked);
            mainMenu.gameObject.SetActive(!clicked);
        }
    }

}
