using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NewTypes;

public class Stickman : MonoBehaviour
{
    [SerializeField] private List<Color> _randomColors;
    private Collider[] _colliders;
    private Rigidbody[] _rigidBodies;

    void Awake()
    {
        _colliders = GetComponentsInChildren<Collider>();
        _rigidBodies = GetComponentsInChildren<Rigidbody>();

        SkinnedMeshRenderer[] meshes = GetComponentsInChildren<SkinnedMeshRenderer>();
        int randomColor = Random.Range(0, _randomColors.Count-1);
        foreach(SkinnedMeshRenderer mesh in meshes)
        {
            mesh.material.color = _randomColors[randomColor];
        }
    }

    public void IgnoreCollisions(Collider collider)
    {
        StopPhysics();
        foreach(Collider col in _colliders)
        {
            Physics.IgnoreCollision(col, collider, true);
        }
    }

    private void StopPhysics()
    {
        foreach(Rigidbody rigidbody in _rigidBodies)
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
        }
    }

    public void EnableGravity()
    {
        foreach(Rigidbody rigidbody in _rigidBodies)
        {
            rigidbody.useGravity = true;
        }
    }

    public void AddForce(Direction direction, Vector2 forceRandom)
    {
        Vector3 dir = Vector3.zero;
        switch(direction)
        {
            case Direction.Left:
                dir = Vector3.left;
                break;
            case Direction.Center:
                dir = Vector3.zero;
                break;
            case Direction.Right:
                dir = Vector3.right;
                break;
        }
        _rigidBodies[0].AddForce((Vector3.up + dir) * Random.Range(forceRandom.x, forceRandom.y), ForceMode.Impulse);
        _rigidBodies[0].AddTorque(Vector3.up * 4000f, ForceMode.Impulse);
    }
}
