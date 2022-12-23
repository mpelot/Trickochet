using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public ParticleSystem ps;
    public Animator animator;
    public Animator healthAnimator;
    public ScoreManager sm;
    public SoundManager soundManager;
    public AudioClip jabClip;
    public AudioClip hitClip;
    public AudioClip dashClip;
    public AudioClip preciseClip;
    public LayerMask ballLayer;
    public float speed = 5;
    public float rotateSpeed = 300f;
    public float dashSpeed = 5;
    public int health = 3;
    public Vector3 startingPosition = Vector3.zero;
    private Quaternion lastAngle = Quaternion.identity;
    public bool canMove = true;
    private bool canFire = true;
    private bool canDash = true;
    private bool invincible = false;
    private Vector3 v = Vector3.zero;
    bool hit = false;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.name.Equals("Ball") && !hit) {
            hit = true;
            canDash = true;
            
            bool precise = true;
            Collider2D[] colliders = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y + .5f), 1f, ballLayer);
            for (int i = 0; i < colliders.Length; i++) {
                if (colliders[i].gameObject != gameObject) {
                    precise = false;
                }
            }
            if (precise) {
                sm.AddScore(200);
                soundManager.PlaySound(preciseClip);
            } else {
                sm.AddScore(100);
                soundManager.PlaySound(jabClip);
            }
            lastAngle = transform.rotation;
            collision.gameObject.GetComponent<Ball>().launch(transform.rotation);
        }
    }

    void Update() {
        var mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;
        mouseWorldPos -= transform.position;
        float angle = Mathf.Atan2(mouseWorldPos.y, mouseWorldPos.x) * Mathf.Rad2Deg;
        var targetRotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, .5f, ballLayer);
        for (int i = 0; i < colliders.Length; i++) {
            if (colliders[i].gameObject != gameObject && !invincible) {
                Hurt();
            }
        }
    }

    public void Move(int vertical, int horizontal, bool mouse0, bool mouse1) {
        Vector3 targetVelocity = new Vector2(horizontal * speed, vertical * speed);
        if (canMove) {
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref v, .1f);

        if (mouse0 && canFire)
            StartCoroutine(Fire());
        if (mouse1 && canDash)
            StartCoroutine(Dash(vertical, horizontal));
        }
    }

    IEnumerator Fire() {
        animator.Play("Jab");
        canFire = false;
        yield return new WaitForSeconds(.2f);
        animator.Play("PlayerIdle");
        hit = false;
        canFire = true;
    }

    IEnumerator Dash(int v, int h) {
        if (h != 0 || v != 0)
            soundManager.PlaySound(dashClip);
        rb.velocity = new Vector2(h, v).normalized * dashSpeed;
        canDash = false;
        yield return new WaitForSeconds(.5f);
        canDash = true;
    }

    private void Hurt() {
        health--;
        healthAnimator.Play(health + "");
        soundManager.PlaySound(hitClip);
        if (health > 0) {
            StartCoroutine(Damaged());
        } else {
            Reset();
        }
    }
    IEnumerator Damaged() {
        animator.Play("Hurt");
        invincible = true;
        canFire = false;
        yield return new WaitForSeconds(.25f);
        animator.Play("PlayerIdle");
        invincible = false;
        canFire = true;
    }

    private void Reset() {
        health = 3;
        healthAnimator.Play("3");
        transform.position = startingPosition;
        transform.rotation = Quaternion.identity;
        rb.velocity = Vector3.zero;
        sm.End();
    }
}
