using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Rigidbody2D rb;
    public SoundManager sm;
    public AudioClip clip;
    public float speed = 20;
    private Vector2 direction = new Vector2(0, 0);
    public float duration = .1f;

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.name.Equals("Bully"))
        {
            direction = Vector2.Reflect(direction, transform.position - collision.transform.position).normalized;
        }
        else if (collision.gameObject.name.Equals("Left") || collision.gameObject.name.Equals("Right"))
            direction += Vector2.left * direction.x * 2;
        else
            direction += Vector2.down * direction.y * 2;
        sm.PlaySound(clip);
    }

    void Update() {
        rb.velocity = direction * speed;
    }

    public void launch(Quaternion r) {
        StartCoroutine(Freeze());
        direction = r * Vector2.up;
    }

    IEnumerator Freeze() {
        var original = Time.timeScale;
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = original;
    }

    public void Reset() {
        direction = new Vector2(0, 0);
    }
}
