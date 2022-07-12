using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour
{
    [SerializeField] private int _pointsCount;
    [SerializeField] GameObject _crosshair;
    [SerializeField] private LineRenderer _lineRenderer;
    private List<Vector3> _linePoints = new List<Vector3>();
    public static Trajectory Instance;

    void Awake()
    {
        Instance = this;
    }
    public void Draw(Vector3 force, Rigidbody ridigBody, Vector3 startPoint)
    {
        _crosshair.SetActive(false);
        Vector3 velocity = (force / ridigBody.mass) * Time.fixedDeltaTime;
        float flightTime = (2 * velocity.y) / Physics.gravity.y;
        float stepTime = flightTime / _pointsCount;
        _linePoints.Clear();

        for (int i = 0; i < _pointsCount; i++)
        {
            float stepTimePassed = stepTime * i;

            Vector3 movementVector = new Vector3 (
                velocity.x * stepTimePassed,
                velocity.y * stepTimePassed - 0.5f * Physics.gravity.y * stepTimePassed * stepTimePassed,
                velocity.z * stepTimePassed);

            Vector3 newPoint = -movementVector + startPoint;

            if (_linePoints.Count > 1)
            {
                RaycastHit hit;
                if (Physics.Raycast(_linePoints[i-1], newPoint - _linePoints[i-1], out hit, (newPoint - _linePoints[i-1]).magnitude))
                {
                    _linePoints.Add(hit.point);
                    _crosshair.transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z - 0.01f);
                    _crosshair.SetActive(true);
                    break;
                }
            }
            _linePoints.Add(newPoint);
        }

        _lineRenderer.positionCount = _linePoints.Count;
        _lineRenderer.SetPositions(_linePoints.ToArray());
    }

    public void Clear()
    {
        _crosshair.SetActive(false);
        _lineRenderer.positionCount = 0;
    }
}
