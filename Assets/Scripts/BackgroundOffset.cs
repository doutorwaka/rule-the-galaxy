using UnityEngine;

public class BackgroundOffset : MonoBehaviour
{
    public float offsetVelocity = 0.3f;
    private float offSetVelocityDivider = 1000f;
    private Material backgroundMaterial;

    // Start is called before the first frame update
    void Start()
    {
        MeshRenderer mr = gameObject.GetComponent<MeshRenderer>();
        backgroundMaterial = mr.material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setNewOffset(Vector2 amount){
        Vector2 offSet = backgroundMaterial.mainTextureOffset;
        offSet = amount;
        backgroundMaterial.mainTextureOffset = offSet;
    }

    public void setNewOffset(Vector3 amount){
        Vector2 amount2d = new Vector2(amount.x, amount.y);
        Vector2 offSet = backgroundMaterial.mainTextureOffset;
        offSet = amount2d * offsetVelocity / offSetVelocityDivider;
        backgroundMaterial.mainTextureOffset = offSet;
    }
}
