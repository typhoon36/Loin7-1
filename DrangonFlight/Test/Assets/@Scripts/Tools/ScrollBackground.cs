using UnityEngine;

public class ScrollBackground : MonoBehaviour
{

    public float scrollSpeed = 1f;
     Material BackGroundMtrl;

    void Start()
    {

        BackGroundMtrl = GetComponent<Renderer>().material;
    }



    void Update()
    {

        Vector2 newOffset = BackGroundMtrl.mainTextureOffset;


        newOffset.Set(0, newOffset.y + (scrollSpeed * Time.deltaTime));


        BackGroundMtrl.mainTextureOffset = newOffset;
    }
}
