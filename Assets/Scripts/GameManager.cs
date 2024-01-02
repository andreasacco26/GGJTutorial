using UnityEngine;
using System.Collections.Generic;
using UnityEditor.PackageManager.Requests;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject ball;
    [SerializeField] float speed;
    [SerializeField] List<GameObject> allBricks;
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

    // TODO: destory current ball and instantiate another at (0,0,0).
    private void ResetBall()
    {
        Transform ballTransform = ball.GetComponent<Transform>();
        ballRb.velocity = Vector3.zero;
        ballRb.angularVelocity = 0f;
        ballTransform.rotation = Quaternion.Euler(Vector3.zero);
        ballTransform.position = Vector3.zero;
    }

    private void ResetBricks()
    {
        foreach (GameObject brick in allBricks)
        {
            if (!brick.activeSelf)
            {
                BrickDestroyer bd = brick.GetComponent<BrickDestroyer>();
                if (bd != null)
                {
                    Debug.Log("Activating brick!");
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
}
