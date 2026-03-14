using UnityEngine;

public class AnimatedWater : MonoBehaviour
{

    [SerializeField]
    private Material _water;

    private float _offsetX;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        _offsetX -= 0.01f;

        _water.mainTextureOffset = new Vector2(_offsetX, 0);

    }
}
