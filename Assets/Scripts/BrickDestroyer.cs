using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickDestroyer : MonoBehaviour
{
    public void Activate() => gameObject.SetActive(true);
    public void Dectivate() => gameObject.SetActive(false);

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ball")) Dectivate();
    }

    void Start() => GameManager.Instance.AddBrick(gameObject);
}
