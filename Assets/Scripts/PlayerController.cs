using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 3.0f;
    public float attackCooldown = 1.0f;
    public GameObject spear;
    public int health = 200;

    Rigidbody2D rigidbody;
    Animator animator;
    Vector2 lookDirection = new Vector2(1, 0);
    float horizontal;
    float vertical;
    float cooldown;
    public void ReceiveDamge(int damage) {
        if (health - damage <= 0) {
            Destroy(gameObject);
        } else {
            health -= damage;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        cooldown = 0;
    }

    // Update is called once per frame
    void Update()
    {
        cooldown += Time.deltaTime;
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        Vector2 move = new Vector2(horizontal, 0);
        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f)) {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }
        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        if (Input.GetKeyDown(KeyCode.S) && cooldown >= attackCooldown) {
            Launch();
            cooldown = 0;
        }

    }
    private void FixedUpdate() {
        Vector2 position = rigidbody.position;
        position.x += speed * horizontal * Time.deltaTime;
        rigidbody.MovePosition(position);
    }

    private void Launch() {
        GameObject spearInstance = Instantiate(spear, rigidbody.position, Quaternion.identity);
        spearInstance.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
        Spear spearObj = spearInstance.GetComponent<Spear>();
        spearObj.Launch();
        animator.SetTrigger("Attack");
    }
}
