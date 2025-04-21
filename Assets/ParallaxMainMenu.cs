using UnityEngine;

public class ParallaxMainMenu : MonoBehaviour
{
    Material background;
    private float distance;
    private float speed = 0.005f;

    void Start()
    {
        background = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        distance = distance + speed;
        background.SetTextureOffset("_MainTex", Vector2.right * distance);
    }
}
