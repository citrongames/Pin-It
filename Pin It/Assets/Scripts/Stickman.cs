using UnityEngine;

public class Stickman : MonoBehaviour
{
    private Collider[] _colliders;
    private Rigidbody[] _rigidBodies;
    private SkinnedMeshRenderer[] _meshes;

    void Awake()
    {
        _colliders = GetComponentsInChildren<Collider>();
        _rigidBodies = GetComponentsInChildren<Rigidbody>();
        _meshes = GetComponentsInChildren<SkinnedMeshRenderer>();
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

    public void SetGravity(bool gravity)
    {
        foreach(Rigidbody rigidbody in _rigidBodies)
        {
            rigidbody.useGravity = gravity;
        }
    }

    public void AddForce(Vector3 direction, Vector2 forceRandom)
    {
        _rigidBodies[0].AddForce((direction) * Random.Range(forceRandom.x, forceRandom.y), ForceMode.Impulse);
        _rigidBodies[0].AddTorque(Vector3.up * 4000f, ForceMode.Impulse);
    }

    public void ChangeColor(Color color)
    {
        foreach(SkinnedMeshRenderer mesh in _meshes)
        {
            mesh.material.color = color;
        }
    }

    public void ClearConstraints()
    {
        foreach(Rigidbody rigidBody in _rigidBodies)
        {
            rigidBody.constraints = RigidbodyConstraints.None;
        }
    }
}
