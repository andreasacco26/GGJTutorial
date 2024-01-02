using System;
using UnityEngine;

public class PaddleMovement : MonoBehaviour
{
    [SerializeField] float smoothTime = 0.3f;
    [SerializeField] float maxMoveSpeed = 10.0f;
    [SerializeField] Vector2 currentVelocity;
    private Camera mainCamera;

    void Awake() => mainCamera = Camera.main;

    // TODO: add force and direction to a ball when it hits the paddle.
    void Update()
    {
        Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        float mouseOffsetX = Math.Abs(mousePosition.x - transform.position.x);
        Vector2 newPosition = Vector2.SmoothDamp(transform.position, mousePosition, ref currentVelocity, smoothTime, mouseOffsetX * maxMoveSpeed);
        // The new position should never update the Y value.
        newPosition.y = transform.position.y;
        transform.position = newPosition;
    }
}
