using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIResultPanel : MonoBehaviour {
    public static UIResultPanel instance;

    public Text score;
    public GameObject resultPanel;
    public Text textResult;
    private void Awake() {
        instance = this;
    }

    public void SetScoreText(string txt) {
        if (score) {
            score.text = txt;
        }
    }
    public void ShowResult(bool isShow, string text) {
        if (resultPanel) {
            resultPanel.SetActive(isShow);
            textResult.text = text;
        }
    }
}
