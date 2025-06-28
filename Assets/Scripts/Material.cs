using UnityEngine;

public class AssignMaterialAndBoxCollider : MonoBehaviour
{
    public Material[] materials;
    public bool addmat = false;
    private BreakablePlatforms breakablePlatforms;
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
                    {
                        obj.AddComponent<BoxCollider>();
                        int chance = Random.Range(0, 100);
                        if(chance <= 10)
                        {
                            obj.AddComponent<BreakablePlatforms>();
                        }
                    }
                    else
                        obj.AddComponent<MeshCollider>();
                }
                obj.layer = 3;
            }
        }
    }
}
