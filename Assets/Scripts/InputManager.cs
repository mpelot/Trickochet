using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    public Player player;
    private int v = 0;
    private int h = 0;
    private bool mouse0, mouse1;

    void Update() {
        if (Input.GetKey("w"))
            v = 1;
        else if (Input.GetKey("s"))
            v = -1;
        else
            v = 0;
        if (Input.GetKey("d"))
            h = 1;
        else if (Input.GetKey("a"))
            h = -1;
        else
            h = 0;
        if (Input.GetKeyDown("mouse 0"))
            mouse0 = true;
        if (Input.GetKeyDown("mouse 1"))
            mouse1 = true;
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }

    private void FixedUpdate() {
        player.Move(v, h, mouse0, mouse1);
        mouse0 = false;
        mouse1 = false;
    }
}
