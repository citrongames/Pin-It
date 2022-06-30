using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NewTypes;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float _timer;
    [SerializeField] private GameObject _objectToSpawn;
    [SerializeField] private Direction _direction;
    [SerializeField] private Vector2 _forceRandom;

    void Start()
    {
        StartCoroutine(Spawn(_objectToSpawn, _timer));
    }

    void Update()
    {
        
    }

    IEnumerator Spawn(GameObject objectToSpawn, float time)
    {
        while (true)
        {
            yield return new WaitForSeconds(time);

            GameObject obj = Instantiate(objectToSpawn, transform.position, Quaternion.identity);
            obj.GetComponent<Stickman>().AddForce(_direction, _forceRandom);
        }
    }
}
