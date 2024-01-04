using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField] GameObject particlesPrefab;
    private bool particlesActive = false;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            if (particlesActive)
            {
                GameObject particleObj = Instantiate(particlesPrefab, other.transform);
                Destroy(particleObj, 1f);
            }
            Dectivate();
        }
    }

    void Start() => GameManager.Instance.AddBrick(gameObject);

    public void Activate() => gameObject.SetActive(true);
    public void Dectivate() => gameObject.SetActive(false);
    public void ToggleParticles() => particlesActive = !particlesActive;
}
