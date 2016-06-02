using UnityEngine;
using System.Collections;

public class TreasureVariables : MonoBehaviour {

    public string type;
    public int value;

	// Use this for initialization
	void Start () {
        string nameOfObject = this.gameObject.name;
        if (nameOfObject.Contains("Painting")) {
            type = "Painting";
            SetValue(3000, 4000);
        }
        else if (nameOfObject.Contains("JewelCase")) {
            type = "JewelCase";
            SetValue(500, 750);
        }
	}

    void SetValue (int min, int max) {
        value = Random.Range(min, max);
    }

}
