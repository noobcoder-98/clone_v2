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

    private bool _isStopped;
    private bool _isCollided;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    //void Update()
    //{
    //    if (!_isStopped)
    //        transform.Translate(new Vector3(0, movementSpeed * -1, 0));
    //}

    private void FixedUpdate()
    {
        if (!_isStopped)
            transform.Translate(new Vector3(0, movementSpeed * -1, 0));
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Wall")) {
            animator.SetBool("Attack", true);
            StartCoroutine(AttackObject(collision));
            _isStopped = true;
        } else if (collision.CompareTag("Player")) {
            _isCollided = true;
            animator.SetBool("Attack", true);
            StartCoroutine(AttackPlayer(collision));
            _isStopped = true;
        } 
        else if (collision.CompareTag("DeathZone"))
        {
            GameManager.instance.GameHealth--;
            if (GameManager.instance.GameHealth <= 0)
            {
                GameManager.instance.IsGameOver = true;
                return;
            }
            UIGameHealthBar.instance.SetValue(GameManager.instance.GameHealth / (float) 5);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            _isCollided = false;
            StopCoroutine(AttackPlayer(collision));
            animator.SetBool("Attack", false);
            _isStopped = false;
        } 
    }

    public void ReceiveDamge(int damage)
    {
        if (health - damage <= 0)
        {
            transform.parent.GetComponent<SpawnPoint>().enemies.Remove(gameObject);
            Destroy(gameObject);
            GameManager.instance.Score++;
            if (GameManager.instance.Score == GameManager.instance.MaxScore)
                GameManager.instance.IsWin = true;
        }
        else
        {
            health -= damage;
        }
    }

    IEnumerator AttackPlayer(Collider2D collision) {
        if (collision == null) {
            _isStopped = false;
            animator.SetBool("Attack", false);
        } else {
            if (!_isCollided)
                yield break;
            collision.gameObject.GetComponent<PlayerController>().ReceiveDamge(damage);
            yield return new WaitForSeconds(damageCooldown);
            StartCoroutine(AttackPlayer(collision));
        }

    }

    IEnumerator AttackObject(Collider2D collision) {
        if (collision == null) {
            _isStopped = false;
            animator.SetBool("Attack", false);
        } else {
            collision.gameObject.GetComponent<WallController>().ReceiveDamge(damage);
            yield return new WaitForSeconds(damageCooldown);
            StartCoroutine(AttackObject(collision));
        }
        
    }
}
