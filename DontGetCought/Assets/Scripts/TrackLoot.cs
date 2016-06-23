using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TrackLoot : MonoBehaviour {

    public int winSum = 15000;
    public int collected = 0;
    //UI Stuff
    public Text valueCollected;
    public Text goal;
    public Text notifications;
    public Button restartB;

    void Start()
    {
        setText();
        goal.text = "Goal: " + winSum.ToString();
        notifications.text = "Go, and get me the valuable Stuff!";
        notifications.CrossFadeAlpha(0.0f, 5.5f, false);
    }

    bool GoalReached() {
        // Genug gesammelt?
        if (collected >= winSum) {
            notifications.CrossFadeAlpha(1.0f, 0.01f, false);
            notifications.text = "You got what you where here for!Get your ass out of there!";
            notifications.CrossFadeAlpha(0.0f, 5.5f, false);
            return true;
        }
        return false;
    }

    public bool ExitLevel() {
        // Genug gesammelt?
        if (collected >= winSum)
        {
            notifications.CrossFadeAlpha(1.0f, 0.01f, false);
            notifications.text = "Yeah! Good boy! You got me what I wanted!";
            notifications.CrossFadeAlpha(0.0f, 5.5f, false);
            GameObject.FindGameObjectWithTag("MenuManager").GetComponent<MenuManager>().LoadScene("WinScreen");
            return true;
        }
        else {
            return false;
        }
    }

    public void collectTreasure(GameObject treasure) {
        collected += treasure.GetComponent<TreasureVariables>().value;
        setText();
        Destroy(treasure);
        GoalReached();
    }

    void setText() {
        valueCollected.text = "Collected Value: " + collected.ToString();
    }

}
