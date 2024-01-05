using System;
using DG.Tweening;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [Header("Movement")]
    [Tooltip("Smoothing coefficient for the paddle movement."), SerializeField] float smoothTime = 0.3f;
    [Tooltip("Maximum paddle movement speed."), SerializeField] float maxMoveSpeed = 10.0f;
    [Header("Particle Effects")]
    [Tooltip("Particle effect prefab for impacts on the paddle."), SerializeField] GameObject confettiPrefab;
    [Header("Tweening variables")]
    [Tooltip("Mouse offset weight when computing the scale tween."), SerializeField, Range(1f, 3f)] float scaleCoefficient;

    private Vector2 currentVelocity;
    private Camera mainCamera;
    private bool tweeningActive = false;
    private bool confettiActive = false;
    private float initialScaleY;
    private float screenBoundsX;
    private float paddleLength;

    void Awake() => mainCamera = Camera.main;

    void Start()
    {
        initialScaleY = gameObject.transform.localScale.y;
        screenBoundsX = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z)).x;
        paddleLength = gameObject.GetComponent<BoxCollider2D>().size.x;
    }

    void Update()
    {
        Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        float mouseOffsetX = Math.Abs(mousePosition.x - transform.position.x);
        Vector2 newPosition = Vector2.SmoothDamp(transform.position, mousePosition, ref currentVelocity, smoothTime, mouseOffsetX * maxMoveSpeed);
        // The new position should never update the Y value.
        newPosition.y = transform.position.y;
        newPosition.x = Mathf.Clamp(newPosition.x, paddleLength - screenBoundsX, screenBoundsX - paddleLength);
        transform.position = newPosition;

        if (tweeningActive) TweenScaleY(mouseOffsetX);
    }

    private void TweenScaleY(float offset)
    {
        float newScale = Mathf.Clamp(1 / (scaleCoefficient * offset), 0f, initialScaleY);
        transform.DOScaleY(newScale, smoothTime);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Ball")) return;
        if (confettiActive) Utilities.InstantiateAndDestroy(confettiPrefab, other.transform, 1f);
    }

    public void TogglePaddleTweening() => tweeningActive = !tweeningActive;
    public void TogglePaddleConfetti() => confettiActive = !confettiActive;
}
