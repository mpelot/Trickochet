using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour
{
    public Animator animator;

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.name.Equals("Ball"))
            StartCoroutine(Flash());
    }

    IEnumerator Flash() {
        animator.Play(gameObject.name);
        yield return new WaitForSeconds(.5f);
        animator.Play(gameObject.name + "Idle");
    }
}
