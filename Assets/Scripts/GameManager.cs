using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [Tooltip("Reference to the ball object."), SerializeField] GameObject ball;
    [Tooltip("Maximum allowed speed from which force applied to the ball is calculated."), SerializeField] float speed;
    private readonly List<GameObject> allBricks;
    private Rigidbody2D ballRb;

    public static GameManager Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        ballRb = ball.GetComponent<Rigidbody2D>();
    }

    void Start() => Reset();

    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            Debug.Log("User requested reset!");
            Reset();
        }
    }

    private void SetRandomBallDirection()
    {
        Vector2 force = new Vector2(Random.Range(-.5f, .5f), -1f);
        if (ballRb.velocity.magnitude < 0.01f)
        {
            ballRb.AddForce(force.normalized * speed);
        }
    }

    private void ResetBall()
    {
        Transform ballTransform = ball.GetComponent<Transform>();
        ballRb.velocity = Vector3.zero;
        ballRb.angularVelocity = 0f;
        ballTransform.SetPositionAndRotation(Vector3.zero, Quaternion.Euler(Vector3.zero));
    }

    private void ResetBricks()
    {
        foreach (GameObject brick in allBricks)
        {
            if (!brick.activeSelf)
            {
                Brick bd = brick.GetComponent<Brick>();
                if (bd != null)
                {
                    bd.Activate();
                }
            }
        }
    }

    public void Reset()
    {
        ResetBricks();
        ResetBall();
        SetRandomBallDirection();
    }

    public void AddBrick(GameObject obj) => allBricks.Add(obj);
    public List<GameObject> GetAllBricks() { return allBricks; }

    public void ToggleBrickParticles()
    {
        foreach (GameObject brick in allBricks)
        {
            Brick bd = brick.GetComponent<Brick>();
            if (bd != null)
            {
                bd.ToggleParticles();
            }
        }
    }
}
