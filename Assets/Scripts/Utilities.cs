using UnityEngine;

public class Utilities : MonoBehaviour
{
    public static void InstantiateAndDestroy(GameObject prefab, Transform transform, float timer)
    {
        GameObject obj = Instantiate(prefab, transform);
        Destroy(obj, timer);
    }
}
