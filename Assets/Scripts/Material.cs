using UnityEngine;

public class AssignMaterialAndBoxCollider : MonoBehaviour
{
    public Material[] materials;
    public bool addmat = false;

    void Start()
    {
        if (materials.Length == 0) return;

        Renderer[] renderers = GetComponentsInChildren<Renderer>();

        foreach (Renderer rend in renderers)
        {
            if (rend != null && rend.sharedMaterial != null)
            {
                GameObject obj = rend.gameObject;
                if (addmat)
                {
                    rend.material = materials[Random.Range(0, materials.Length)];
                }
                if (obj.GetComponent<Collider>() == null)
                {
                    if (addmat)
                        obj.AddComponent<BoxCollider>();
                    else
                        obj.AddComponent<MeshCollider>();
                }
                obj.layer = 3;
            }
        }
    }
}
