using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilBase : MonoBehaviour
{
    [SerializeField]
    private int _projectilDamage = 5;
    [SerializeField]
    private float _timeToDeactivate = 2f;
    [SerializeField]
    private float _speed;

    private Vector2 direction = new Vector2(1, 0);

    private Coroutine _currentCoroutine;
    private float _side = 1;

    private void Start()
    {
        _currentCoroutine = StartCoroutine(TimeToDeactivate());
    }

    private void Update()
    {
        transform.Translate(direction * _speed * Time.deltaTime * _side);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var enemy = collision.gameObject.GetComponent<EnemyBase>();

        if(enemy != null)
        {
            enemy.Damage(_projectilDamage);       
        }

        Deactivate();
    }

    public void Init(Transform shootPosition, Transform playerSide)
    {
        transform.position = shootPosition.position;

        _side = playerSide.localScale.x;

        gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        if (_currentCoroutine != null)
            StopCoroutine(TimeToDeactivate());
    }

    private IEnumerator TimeToDeactivate()
    {
        yield return new WaitForSeconds(_timeToDeactivate);
        Deactivate();
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }  
}