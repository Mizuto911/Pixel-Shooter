using System.Collections;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public float duration = 0.5f;
    public AnimationCurve curve;
    
    public IEnumerator Shaking()
    {
        Vector3 startingPosition = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float strength = curve.Evaluate(elapsedTime/duration);
            transform.position = startingPosition + Random.insideUnitSphere*strength;
            yield return null;
        }

        transform.position = startingPosition;
    }
}
