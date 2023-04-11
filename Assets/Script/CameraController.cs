using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform player;
    Vector3 velosity = Vector3.zero;
    [Range(0, 1)]
    public float smoothTime;
    public Vector3 offset;
    public Vector2 yLimit;
    public Vector2 xLimit;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void LateUpdate()
    {
        Vector3 targetPosition = player.position + offset;
        targetPosition.y = Mathf.Clamp(targetPosition.y, yLimit.x, yLimit.y);
        targetPosition.x = Mathf.Clamp(targetPosition.x, xLimit.x, xLimit.y);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velosity, smoothTime);
    }
}
