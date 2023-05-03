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
        if (player != null)
        {
            Vector3 targetPos = player.position + offset;
            targetPos.x = Mathf.Clamp(targetPos.x, xLimit.x, xLimit.y);
            targetPos.y = Mathf.Clamp(targetPos.y, yLimit.x, yLimit.y);
            transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velosity, smoothTime);   
        }
    }
}
