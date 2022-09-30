using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject draggingObject;
    public GameObject currentContainter;
    public int GameHealth;

    public bool IsGameOver { get; set; }
    public bool IsWin { get; set; }
    public int Score { get; set; }
    public int MaxScore { get; set; }

    private void Awake()
    {
        instance = this; 
    }

    public void PlaceObject()
    {
        if (draggingObject != null && currentContainter != null)
        {
            GameObject ret = Instantiate(draggingObject.GetComponent<ObjectDragging>().card.objectGame, currentContainter.transform);
            ret.transform.position = currentContainter.transform.position;
            //ret.transform.localScale = new Vector3(0.5f, 1.5f, 1);
            currentContainter.GetComponent<ObjectContainer>().isFull = true;
        }
    }
}
