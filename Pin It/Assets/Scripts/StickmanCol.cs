using UnityEngine;

public class StickmanCol : MonoBehaviour
{
    [SerializeField] private float _time;
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Reset")
            DestroyObject();
        else if (other.gameObject.tag == "Ground")
            Invoke("DestroyObject", _time);
    }

    private void DestroyObject()
    {
        Destroy(GetComponentInParent<Stickman>().gameObject);
    }
}
