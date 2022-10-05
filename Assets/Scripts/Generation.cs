using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Generation : MonoBehaviour
{
    [SerializeField]
    private GameObject _coin;
    [SerializeField]
    private GameObject _spike;
    public int _count;
    [SerializeField]
    Camera cam;
    

    void Start()
    {
        Spawn(_coin);
        Spawn(_spike);
    }
    private void Spawn(GameObject obj)
    {
        float height = 2f * cam.orthographicSize;
        float width = height * cam.aspect;
        float radius = obj.GetComponent<CircleCollider2D>().radius;
        for (int i = 0; i < _count; i++)
        {
            Vector2 pos = new Vector2(Random.Range(-width / 2 + 1, width / 2 - 1), Random.Range(-height / 2 + 1, height / 2 - 1));
            while(!CheckDistance(pos, radius))
            {
                pos = new Vector2(Random.Range(-width / 2 + 1, width / 2 - 1), Random.Range(-height / 2 + 1, height / 2 - 1));
            }
            Instantiate(obj, new Vector3(pos.x, pos.y, 0), Quaternion.identity, transform);


        }
    }
    private bool _isCollided(Vector2 v1, Vector2 v2, float radius1, float radius2)
    {
        return Vector2.Distance(v1, v2) < radius1 + radius2;
    }
    private bool CheckDistance(Vector2 pos, float radius)
    {
        foreach (Transform go in GetComponentsInChildren<Transform>())
        {
            if (go.name == "Generator")
                continue;
            if (_isCollided(pos, go.position, go.GetComponent<CircleCollider2D>().radius, radius))
            {
                return false;
            }
            
        }
        return true;
    }
    void Update()
    {
        
    }
}
