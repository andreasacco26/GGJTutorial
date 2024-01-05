using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [Tooltip("Ball prefab object."), SerializeField] GameObject ball;
    [Tooltip("Maximum allowed speed from which force applied to the ball is calculated."), SerializeField] float speed;
    private List<GameObject> allBricks = new List<GameObject>();
    private GameObject currentBall;

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

    private void ResetBall()
    {
        if (currentBall != null) Destroy(currentBall);
        currentBall = Instantiate(ball);
        Transform ballTransform = currentBall.GetComponent<Transform>();
        Rigidbody2D ballRb = currentBall.GetComponent<Rigidbody2D>();
        ballTransform.SetPositionAndRotation(Vector3.zero, Quaternion.Euler(Vector3.zero));
        Vector2 force = new Vector2(Random.Range(-.5f, .5f), -1f);
        if (ballRb.velocity.magnitude < 0.01f)
        {
            ballRb.AddForce(force.normalized * speed);
        }
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
