using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Frecuencia_Taquicardia : MonoBehaviour
{
    public Image image;
    public Text positionText;
    public float minValue = 72222f;
    public float minDisplayValue = 78f;
    public float maxDisplayValue = 145f;
    public float transitionSpeed = 1f;

    private float maxValue = float.MinValue;
    private float displayValue;
    private bool isSuspended;
    public Frecuencia_Muerte frecuenciaMuerte; // Referencia al componente "Frecuencia_Muerte"

    private void Start()
    {
        ResetCounter();

        frecuenciaMuerte = GetComponent<Frecuencia_Muerte>(); // Obtener referencia al componente "Frecuencia_Muerte"
    }

    private void Update()
    {
        if (isSuspended || (frecuenciaMuerte != null && frecuenciaMuerte.enabled))
        {
            return;
        }

        RectTransform rectTransform = image.rectTransform;
        Vector3 screenPosition = RectTransformUtility.WorldToScreenPoint(Camera.main, rectTransform.position);

        float normalizedValue = Mathf.InverseLerp(minValue, maxValue, screenPosition.x);
        float clampedValue = Mathf.Clamp(normalizedValue, 0f, 1f);
        float targetDisplayValue = Mathf.Lerp(minDisplayValue, maxDisplayValue, clampedValue);
        displayValue = Mathf.Lerp(displayValue, targetDisplayValue, Time.deltaTime * transitionSpeed);

        int roundedValue = Mathf.RoundToInt(displayValue);
        positionText.text = roundedValue.ToString();

        if (screenPosition.x > maxValue)
        {
            maxValue = screenPosition.x;
        }

      //  Debug.Log("Real Position: " + screenPosition.x);
    }

    private void ResetCounter()
    {
        displayValue = 0f;
        positionText.text = displayValue.ToString();
        maxValue = float.MinValue;
    }

    public void SuspendFunctions()
    {
        isSuspended = true;
    }

    public void ResumeFunctions()
    {
        isSuspended = false;
    }
}