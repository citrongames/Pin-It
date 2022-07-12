using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float _timer;
    [SerializeField] private GameObject _objectToSpawn;
    [SerializeField] private Color _color;
    [SerializeField] private Vector2 _forceRandom;

    void Start()
    {
        StartCoroutine(Spawn(_objectToSpawn, _timer));
    }

    IEnumerator Spawn(GameObject objectToSpawn, float time)
    {
        while (true)
        {
            yield return new WaitForSeconds(time);

            Stickman stickman = Instantiate(objectToSpawn, transform.position, Quaternion.identity).GetComponent<Stickman>();
            stickman.ChangeColor(_color);
            stickman.AddForce(transform.up, _forceRandom);
        }
    }
}
