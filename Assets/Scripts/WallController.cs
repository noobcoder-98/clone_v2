using System.Collections;
using System.Collections.Generic;
using UnityEditor.TextCore.Text;
using UnityEngine;

public class WallController : MonoBehaviour
{
    public int health;

    private ObjectContainer container;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
