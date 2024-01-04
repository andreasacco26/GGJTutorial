using UnityEngine;

public class Brick : MonoBehaviour
{
    [Header("Particle Effects")]
    [Tooltip("Particle effect prefab for impacts on the paddle."), SerializeField] GameObject particlesPrefab;
    private bool particlesActive = false;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            if (particlesActive) Utilities.InstantiateAndDestroy(particlesPrefab, other.transform, 1f);
            Dectivate();
        }
    }

    void Start() => GameManager.Instance.AddBrick(gameObject);

    public void Activate() => gameObject.SetActive(true);
    public void Dectivate() => gameObject.SetActive(false);
    public void ToggleParticles() => particlesActive = !particlesActive;
}
