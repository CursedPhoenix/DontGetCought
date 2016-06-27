using UnityEngine;
using System.Collections;

public class CloseDistColl_S : MonoBehaviour {
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            GameObject.FindGameObjectWithTag("MenuManager").GetComponent<MenuManager>().LoadScene("Busted");
        }
    }
}
