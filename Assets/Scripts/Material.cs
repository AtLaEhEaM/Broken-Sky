using UnityEngine;

public class MaterialReplacer : MonoBehaviour
{
    public Material[] materials;

    void Start()
    {
        foreach (Transform child in transform)
        {
            Renderer rend = child.GetComponent<Renderer>();
            if (rend != null && materials.Length > 0)
            {
                rend.material = materials[Random.Range(0, materials.Length)];
            }
        }
    }
}
