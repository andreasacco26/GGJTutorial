using UnityEngine;

public class InGameEditor : MonoBehaviour
{
    [SerializeField] GameObject panel;

    private void TogglePanel() => panel.SetActive(!panel.activeSelf);

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) TogglePanel();
    }
}
