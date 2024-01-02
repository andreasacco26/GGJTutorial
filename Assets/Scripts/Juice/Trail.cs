using UnityEngine;

[RequireComponent(typeof(TrailRenderer))]
public class Trail : MonoBehaviour
{
    private TrailRenderer tr;
    void Start()
    {
        tr = GetComponent<TrailRenderer>();
        if (tr.enabled) { tr.enabled = false; }
    }

    public void ToggleTrailRenderer() => tr.enabled = !tr.enabled;
}
