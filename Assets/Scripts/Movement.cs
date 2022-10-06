using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    [SerializeField]
    private GameObject _gameCanvas;
    [SerializeField]
    private Generation _gen;


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
        if (_gen._count == _coinCount)
        {
            _gameCanvas.transform.GetChild(1).gameObject.SetActive(true);
            _gameCanvas.transform.GetChild(1).gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "You win";
            Time.timeScale = 0f;
        }

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
            _gameCanvas.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Coin Count: " + _coinCount.ToString();
        }
        if(collision.tag == "spike")
        {
            Destroy(gameObject);
            _gameCanvas.transform.GetChild(1).gameObject.SetActive(true);
            _gameCanvas.transform.GetChild(1).gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "You lose";
            Time.timeScale = 0f;
        }
    }
}
