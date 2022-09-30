using System.Collections;
using System.Collections.Generic;
using UnityEditor.TextCore.Text;
using UnityEngine;

public class WallController : MonoBehaviour
{
    public int health;

    public void ReceiveDamge(int damage) {
        if (health - damage <= 0) {
            ObjectContainer container = transform.parent.GetComponent<ObjectContainer>();
            container.isFull = false;
            Destroy(gameObject);
        } else {
            health -= damage;
        }
    }
}
