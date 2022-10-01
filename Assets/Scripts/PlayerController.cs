using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 3.0f;
    public float coolDownTime = 1.0f;
    public GameObject spear;
    public int health = 200;
    public Transform movePoint;
    public Transform launchPoint;

    Animator _animator;
    Vector2 _lookDirection = new Vector2(1, 0);
    float _horizontal;
    float _coolDownTime;
    float _currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _coolDownTime = coolDownTime;
        movePoint.parent = null;
        _currentHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.IsWin || GameManager.instance.IsGameOver) 
            return;

        _horizontal = Input.GetAxisRaw("Horizontal");

        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, movePoint.position) <= 0.001f)
        {
            
            if (Mathf.Abs(_horizontal) == 1f && Mathf.Abs(_horizontal * 1.4f + movePoint.position.x) < 5)
            {
                movePoint.position += new Vector3(_horizontal * 1.35f, 0f, 0f);
            }
        }
        _coolDownTime -= Time.deltaTime;

        Vector2 move = new Vector2(_horizontal, 0);
        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f)) {
            _lookDirection.Set(move.x, move.y);
            _lookDirection.Normalize();
        }
        _animator.SetFloat("Look X", _lookDirection.x);
        _animator.SetFloat("Look Y", _lookDirection.y);
        _animator.SetFloat("Speed", move.magnitude);

        if (Input.GetKeyDown(KeyCode.S) && _coolDownTime <= 0) {
            Launch();
            _coolDownTime = coolDownTime;
        }

    }

    public void ReceiveDamge(int damage)
    {
        if (GameManager.instance.IsGameOver || GameManager.instance.IsWin) return;

        _currentHealth = Mathf.Clamp(_currentHealth - damage, 0, health);
        UIHealthBar.instance.SetValue(_currentHealth / (float) health);

        if (_currentHealth == 0)
        {
            Destroy(gameObject);
            GameManager.instance.IsGameOver = true;
            UIResultPanel.instance.ShowResult(true, "GAME OVER");
        }
    }

    private void Launch() {
        GameObject spearInstance = ObjectPool.instance.GetSpear();
        if (spearInstance == null) 
            return;
        spearInstance.transform.position = launchPoint.position;
        spearInstance.transform.rotation = Quaternion.identity;
        spearInstance.SetActive(true);
        SpearController spearObj = spearInstance.GetComponent<SpearController>();
        spearObj.Launch();
        _animator.SetTrigger("Attack");
    }
}
