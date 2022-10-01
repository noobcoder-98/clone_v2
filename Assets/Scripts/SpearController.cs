using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearController : MonoBehaviour
{
    public float movementSpeed;
    public int damage;

    bool _isCollided;
    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, movementSpeed, 0));
        if (transform.position.magnitude > 1000.0f) {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Enemy") && !_isCollided) {
            _isCollided = true;
            collision.gameObject.GetComponent<EnemyController>().ReceiveDamge(damage);
            gameObject.SetActive(false);
            
        } 
        else if (collision.CompareTag("TopLimit")) {
            gameObject.SetActive(false);
        }
    }

    public void Launch()
    {
        _isCollided = false;
        transform.Translate(new Vector3(0, movementSpeed, 0));
    }
}
