using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Menu : MonoBehaviour {

    public TextMeshProUGUI start;
    public ScoreManager sm;
    public GameObject scoreDisplay;
    public GameObject HealthDisplay;
    public GameObject curtain;
    private bool flickering = true;

    void Start() {
        StartCoroutine(Flicker());
    }

    void Update(){
        if (Input.GetKeyDown("return")) {
            flickering = false;
            gameObject.SetActive(false);
            curtain.SetActive(false);
            sm.Begin();
            scoreDisplay.gameObject.SetActive(true);
            HealthDisplay.SetActive(true);
            Debug.Log("enter");
        }
    }
    IEnumerator Flicker() {
        while (flickering) {
            start.gameObject.SetActive(false);
            yield return new WaitForSeconds(.5f);
            start.gameObject.SetActive(true);
            yield return new WaitForSeconds(.5f);
        }
    }
}
