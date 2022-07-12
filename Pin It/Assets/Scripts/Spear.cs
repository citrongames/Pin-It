using UnityEngine;

public class Spear : MonoBehaviour
{
    [SerializeField] private float _startAngle;
    [SerializeField] private float _throwForce;
    [SerializeField] private float _timeToReset;
    private Rigidbody _rigidBody;
    private Transform _trajectoryStart;
    private GameObject _stickJoint;
    private Trajectory _trajectory;
    private bool _isWaitingThrow;
    private Vector3 _startPos;
    private Quaternion _startRot;

    public float StartAngle
    {
        get => _startAngle;
    }

    public static Spear Instance;
    private void Awake()
    {
        Instance = this;
        _rigidBody = GetComponent<Rigidbody>();
        _trajectoryStart = GameObject.Find("trajectory_start").transform;
        _stickJoint = GameObject.Find("stick_joint");
        _trajectory = GetComponent<Trajectory>();
        _isWaitingThrow = true;

        _startPos = transform.position;
        _startRot = transform.rotation;
    }

    private void OnCollisionEnter(Collision other) 
    {
        switch(other.gameObject.tag)
        {
            case "Wall":
                StopPhysics();
                Invoke("Reset", _timeToReset);
                break;
            case "Reset":
                Reset();
                break;
            case "Object":
                Attach(other.gameObject);
                break;
            default:
                Debug.LogError("No collision for " + other.gameObject.tag);
                break;
        }
        
    }

    private void Attach(GameObject objectToAttach)
    {
        FixedJoint joint = _stickJoint.AddComponent<FixedJoint>(); 
        joint.anchor = _stickJoint.transform.position; 
        joint.connectedBody = objectToAttach.GetComponent<Rigidbody>(); 
        joint.enableCollision = false; 
        Stickman stickman = objectToAttach.GetComponentInParent<Stickman>();
        stickman.IgnoreCollisions(gameObject.GetComponentInChildren<Collider>());
        stickman.ClearConstraints();
        objectToAttach.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
    }

    public void LookAt(Vector3 target)
    {
        if (_isWaitingThrow)
        {
            transform.LookAt(target, Vector3.up);
            _trajectory.Draw(target * _throwForce, _rigidBody, _trajectoryStart.position);
        }
    }

    public void Throw(Vector3 target)
    {
        if (_isWaitingThrow)
        {
            _rigidBody.useGravity = true;
            _rigidBody.AddForce(target * _throwForce, ForceMode.Force);
            _trajectory.Clear();
            _isWaitingThrow = false;
        }
    }

    private void StopPhysics()
    {
        _rigidBody.velocity = Vector3.zero;
        _rigidBody.angularVelocity = Vector3.zero;
        _rigidBody.useGravity = false;
    }

    private void Reset()
    {
        StopPhysics();
        FixedJoint[] joints = _stickJoint.GetComponents<FixedJoint>(); 
        if (joints.Length > 0)
        {
            foreach(FixedJoint joint in joints)
            {
                if(joint.connectedBody != null)
                {
                    Stickman stickman = joint.connectedBody.gameObject.GetComponentInParent<Stickman>();
                    if (stickman != null) stickman.ClearConstraints();
                    joint.connectedBody = null; 
                }
            }
        }

        transform.position = _startPos;
        transform.rotation = _startRot;
        _isWaitingThrow = true;
    }
}
