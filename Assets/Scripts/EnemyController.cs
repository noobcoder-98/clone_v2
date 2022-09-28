using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int health;
    public int damage;
    public float movementSpeed;
    public float damageCooldown;

    private bool isStopped;
    private bool isCollided;
    Animator animator;
    public void ReceiveDamge(int damage) {
        if (health - damage <= 0) {
            transform.parent.GetComponent<SpawnPoint>().enemies.Remove(gameObject);
            Destroy(gameObject);
        } else {
            health -= damage;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isStopped)
            transform.Translate(new Vector3(0, movementSpeed * -1, 0));
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.layer == 8) {
            animator.SetBool("Attack", true);
            StartCoroutine(AttackObject(collision));
            isStopped = true;
        } else if (collision.gameObject.layer == 11){
            isCollided = true;
            animator.SetBool("Attack", true);
            StartCoroutine(AttackPlayer(collision));
            isStopped = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.layer == 11) {
            isCollided = false;
            StopCoroutine(AttackPlayer(collision));
            animator.SetBool("Attack", false);
            isStopped = false;
        }
    }

    IEnumerator AttackPlayer(Collider2D collision) {
        if (collision == null) {
            isStopped = false;
            animator.SetBool("Attack", false);
        } else {
            if (!isCollided)
                yield break;
            collision.gameObject.GetComponent<PlayerController>().ReceiveDamge(damage);
            yield return new WaitForSeconds(damageCooldown);
            StartCoroutine(AttackPlayer(collision));
        }

    }

    IEnumerator AttackObject(Collider2D collision) {
        if (collision == null) {
            isStopped = false;
            animator.SetBool("Attack", false);
        } else {
            collision.gameObject.GetComponent<WallController>().ReceiveDamge(damage);
            yield return new WaitForSeconds(damageCooldown);
            StartCoroutine(AttackObject(collision));
        }
        
    }
}
