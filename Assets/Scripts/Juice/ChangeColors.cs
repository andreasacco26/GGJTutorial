using UnityEngine;

public class ChangeColors : MonoBehaviour
{
    [SerializeField] Color backgroundColor;
    [SerializeField] Color bricksColor;
    [SerializeField] Color paddleColor;
    [SerializeField] Color ballColor;

    [SerializeField] SpriteRenderer ballRenderer;
    [SerializeField] SpriteRenderer paddleRenderer;
    private Camera mainCamera;

    void Start() => mainCamera = Camera.main;

    private void ToggleColor(SpriteRenderer renderer, Color newColor, Color originalColor)
    {
        if (renderer.color == newColor) { renderer.color = originalColor; }
        else { renderer.color = newColor; }
    }

    public void ToggleColors()
    {
        if (mainCamera.backgroundColor == backgroundColor)
        {
            mainCamera.backgroundColor = Color.black;
        }
        else
        {
            mainCamera.backgroundColor = backgroundColor;
        };

        ToggleColor(ballRenderer, ballColor, Color.white);
        ToggleColor(paddleRenderer, paddleColor, Color.gray);

        foreach (GameObject brick in GameManager.Instance.GetAllBricks())
        {
            ToggleColor(brick.GetComponent<SpriteRenderer>(), bricksColor, Color.white);
        }
    }
}
