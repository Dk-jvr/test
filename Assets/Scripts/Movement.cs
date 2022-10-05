using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Movement : MonoBehaviour
{
    private int _coinCount = 0;
    private Queue<Vector2> vectors = new Queue<Vector2>();
    [SerializeField]
    private float _speed = 5f;
    private bool _isMoving = false;
    [SerializeField]
    Camera _camera;

    void Start()
    {
        
    }
    void Update()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            vectors.Enqueue(_camera.ScreenToWorldPoint(touch.position));
        }
        if (!_isMoving && vectors.Count > 0)
            StartCoroutine(Moving(vectors.Dequeue()));

    }
    IEnumerator Moving(Vector2 vector)
    {
        _isMoving = true;
        float distance = Vector2.Distance(transform.position, vector), passedDist = 0;
        while (passedDist < distance)
        {
            transform.position = Vector2.Lerp(transform.position, vector, _speed * Time.deltaTime);
            passedDist += _speed * Time.deltaTime;
            yield return null;
        }
        transform.position = vector;
        _isMoving = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "coin")
        {
            Destroy(collision.gameObject);
            _coinCount += 1;
        }
        if(collision.tag == "spike")
        {
            Destroy(gameObject);
        }
    }
}
