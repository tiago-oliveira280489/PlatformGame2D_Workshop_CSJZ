using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator anim;
    public float speed;
    public float intensity;
    bool isJump;
    public float health;
    public bool vunerability;
    public SpriteRenderer sr;
    private GameController gc;
    float direction;
    // Start is called before the first frame update
    void Start()
    {
        gc = FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        //return direction in X axis (values -1 (left) and 1 (right))
        direction = Input.GetAxis("Horizontal");//left, right, a or d

        if (direction > 0)
        {
            transform.eulerAngles = new Vector2(0, 0);
        }else if (direction < 0)
        {
            transform.eulerAngles = new Vector2(0, 180);
        }
        else if (direction == 0 && isJump == false)
        {
            anim.SetInteger("transition", 0);
        }

        if (direction != 0 && isJump == false)
        {
            anim.SetInteger("transition", 1);
        }

        Jump();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(direction * speed, rb.velocity.y);
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isJump == false)
        {
            rb.AddForce(Vector2.up * intensity, ForceMode2D.Impulse);
            anim.SetInteger("transition", 2);
            isJump = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            isJump = false;
        }
    }

    public void Damage()
    {
        if(vunerability == false)
        {
            gc.LossHealth(health);
            health--;
            vunerability = true;
            StartCoroutine(Respawn());
        }
    }

    IEnumerator Respawn()
    {
        sr.enabled = false;
        yield return new WaitForSeconds(0.2f);
        sr.enabled = true;
        yield return new WaitForSeconds(0.2f);
        sr.enabled = false;
        yield return new WaitForSeconds(0.2f);
        sr.enabled = true;
        yield return new WaitForSeconds(0.2f);
        sr.enabled = false;
        yield return new WaitForSeconds(0.2f);
        sr.enabled = true;
        yield return new WaitForSeconds(0.2f);
        vunerability = false;
    }
}


