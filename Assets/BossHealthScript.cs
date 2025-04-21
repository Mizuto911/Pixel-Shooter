using UnityEngine;
using UnityEngine.UI;

public class BossHealthScript : MonoBehaviour
{
    private Slider bossHealthSlider;
    public GameObject boss;
    bool filled = false;

    void Start()
    {
        bossHealthSlider = GetComponent<Slider>();
    }

    private void FixedUpdate()
    {
        if (!filled)
        {
            if (bossHealthSlider.value < 200)
            {
                bossHealthSlider.wholeNumbers = false;
                fillUpHealth();
            }
            else
            {
                bossHealthSlider.wholeNumbers = true;
                filled = true;
            }
        }
    }

    public void setBossHealth(int health)
    {
        bossHealthSlider.value = health ;
    }

    public void fillUpHealth()
    {
        bossHealthSlider.value = Mathf.MoveTowards(bossHealthSlider.value, 200, 0.5f);
    }
}
