using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ColorPalette
{
    public string name;
    public Color backgroundColor;
    public Color bricksColor;
    public Color paddleColor;
    public Color ballColor;
}

public class ChangeColors : MonoBehaviour
{
    [Tooltip("List of available palettes to switch."), SerializeField] List<ColorPalette> colorPalettes;
    [Tooltip("The paddle renderer."), SerializeField] SpriteRenderer paddleRenderer;
    private static readonly string originalPaletteName = "original";
    private static readonly string alternativePaletteName = "alternative";
    private string currentPaletteName = originalPaletteName;
    private Camera mainCamera;

    void Start() => mainCamera = Camera.main;

    public void SelectPalette(string paletteName)
    {
        ColorPalette palette = colorPalettes.Find((x) => x.name == paletteName);
        if (paletteName == null)
        {
            Debug.Log("Could not find palette '" + paletteName + "'!");
            return;
        }

        mainCamera.backgroundColor = palette.backgroundColor;
        GameManager.Instance.Ball().GetComponent<SpriteRenderer>().color = palette.ballColor;
        paddleRenderer.color = palette.paddleColor;
        foreach (GameObject brick in GameManager.Instance.GetAllBricks())
        {
            brick.GetComponent<SpriteRenderer>().color = palette.bricksColor;
        }

        currentPaletteName = palette.name;
    }

    public void ToggleColors()
    {
        if (currentPaletteName == originalPaletteName) SelectPalette(alternativePaletteName);
        else SelectPalette(originalPaletteName);
    }
}
