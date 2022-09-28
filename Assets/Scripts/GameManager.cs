using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject draggingObject;
    public GameObject currentContainter;

    public static GameManager instance;

    private void Awake()
    {
        instance = this; 
    }

    public void PlaceObject()
    {
        if (draggingObject != null && currentContainter != null)
        {
            GameObject ret = Instantiate(draggingObject.GetComponent<ObjectDragging>().card.objectGame, currentContainter.transform);
            Debug.Log(ret.transform.position);
            ret.transform.position = currentContainter.transform.position;
            Debug.Log(ret.transform.position);
            Debug.Log(currentContainter.transform.position);
            currentContainter.GetComponent<ObjectContainer>().isFull = true;
        }
    }
}
