using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickJoint : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        // creates joint
        FixedJoint joint = gameObject.AddComponent<FixedJoint>(); 
        // sets joint position to point of contact
        joint.anchor = transform.position; 
        // conects the joint to the other object
        joint.connectedBody = other.gameObject.GetComponent<Rigidbody>(); 
        // Stops objects from continuing to collide and creating more joints
        joint.enableCollision = false; 
        other.gameObject.GetComponentInParent<Stickman>().IgnoreCollisions(GetComponent<Collider>());
    }
}
