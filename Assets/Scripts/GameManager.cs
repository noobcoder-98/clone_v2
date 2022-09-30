using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject draggingObject;
    public GameObject currentContainter;
    public float GameHealth;

    public bool IsGameOver { get; set; }
    public bool IsWin { get; set; }
    private int _score;
    public int Score {
        get { return _score; }
        set {
            if (IsGameOver || IsWin)
                return;
            _score = value;
            UIResultPanel.instance.SetScoreText("Score: " + _score);
        }
    }
    public float CurrentHealth { get; set; }

    public int MaxScore { get; set; }

    private void Start() {
        Score = 0;
        CurrentHealth = GameHealth;
        IsGameOver = false;
        IsWin = false;
        UIResultPanel.instance.SetScoreText("Score: " + _score);
    }
    private void Awake()
    {
        instance = this; 
    }

    public void Replay() {
        SceneManager.LoadScene("MainScene");
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
