using UnityEngine;

public class Parallax : MonoBehaviour
{
    Material background;
    private float distance;
    private float speed = 0.02f;

    void Start()
    {
        background = GetComponent<Renderer>().material;
    }

    private void FixedUpdate()
    {
        distance = distance + speed;
        background.SetTextureOffset("_MainTex", Vector2.up * distance);
    }
}
