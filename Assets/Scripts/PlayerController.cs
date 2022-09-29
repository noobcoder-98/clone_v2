using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 3.0f;
    public float attackCooldown = 1.0f;
    public GameObject spear;
    public int health = 200;
    public Transform movePoint;
    public Transform launchPoint;

    Animator animator;
    Vector2 lookDirection = new Vector2(1, 0);
    float horizontal;
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
        animator = GetComponent<Animator>();
        cooldown = attackCooldown;
        movePoint.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, movePoint.position) <= 0.001f)
        {
            
            if (Mathf.Abs(horizontal) == 1f && Mathf.Abs(horizontal * 1.4f + movePoint.position.x) < 5)
            {

                movePoint.position += new Vector3(horizontal * 1.35f, 0f, 0f);
            }
        }
        cooldown -= Time.deltaTime;

        Vector2 move = new Vector2(horizontal, 0);
        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f)) {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }
        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        if (Input.GetKeyDown(KeyCode.S) && cooldown <= 0) {
            Launch();
            cooldown = attackCooldown;
        }

    }

    private void Launch() {
        GameObject spearInstance = Instantiate(spear, launchPoint.position, Quaternion.identity);
        Spear spearObj = spearInstance.GetComponent<Spear>();
        spearObj.Launch();
        animator.SetTrigger("Attack");
    }
}
