using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electro : MonoBehaviour
{
    public TrailRenderer trailRenderer;
    public float scaleFactor = 1f;
    public AnimationCurve waveform;
    public int pointCount = 101;

    private void Start()
    {
        GenerateElectrocardiogram();
    }

    private void GenerateElectrocardiogram()
    {
        float step = 1f / (pointCount - 1);

        for (int i = 0; i < pointCount; i++)
        {
            float t = i * step;
            float x = t;
            float y = waveform.Evaluate(t) * scaleFactor;

            Vector3 position = new Vector3(x, y, 0f);
            trailRenderer.AddPosition(position);
        }
    }
}