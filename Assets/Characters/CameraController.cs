using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]

public class CameraController : MonoBehaviour
{
    public List<Transform> characters;

    public Vector3 offset;
    public float smoothTime = 0.5f;
    public float minZoom = 40f;
    public float maxZoom = 10f;
    public float zoomLimiter = 50f;

    private Vector3 velocity;
    private Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        if (characters.Count == 0)
        {
            return;
        }
        
        Move();
        Zoom();
        
    }

    void Zoom()
    {
        float newZoom = Mathf.Lerp(maxZoom, minZoom, GetGreatestDistance() / zoomLimiter);

        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, newZoom, Time.deltaTime);
    }

    float GetGreatestDistance()
    {
        var bounds = new Bounds(characters[0].position, Vector3.zero);

        for (int i = 0; i < characters.Count; i++)
        {
            bounds.Encapsulate(characters[i].position);
        }

        return bounds.size.x;
    }

    void Move()
    {
        Vector3 centerPoint = GetCenterPoint();
        Vector3 newPosition = centerPoint + offset;

        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
    }

    Vector3 GetCenterPoint()
    {
        if (characters.Count == 1)
        {
            return characters[0].position;
        }

        var bounds = new Bounds(characters[0].position, Vector3.zero);

        for (int i = 0; i < characters.Count; i++)
        {
            bounds.Encapsulate(characters[i].position);
        }

        return bounds.center;
    }
}
