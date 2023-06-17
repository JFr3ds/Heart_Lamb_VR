using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Frecuencia_Muerte : MonoBehaviour
{
    public GameObject muerteObject;
    public GameObject latidoObject;
    public Text counterText;
    public float decreaseSpeed = 1f;

    private bool isMuerteActive;
    private float counterValue;

    public Frecuencia frecuenciaComponent; // Referencia al componente "frecuencia"

    private void Start()
    {
        if (!float.TryParse(counterText.text, out counterValue))
        {
            counterValue = 0f; // Valor predeterminado si la conversión falla
        }

        frecuenciaComponent = GetComponent<Frecuencia>(); // Buscar el componente "frecuencia" en el objeto actual
    }

    private void Update()
    {
        if (muerteObject.activeSelf && !isMuerteActive)
        {
            isMuerteActive = true;
            counterValue = 0f; // Establecer el valor del contador en 0
            counterText.text = counterValue.ToString("0"); // Actualizar el texto del contador a 0
            StartCoroutine(DecreaseCounter());

            // Desactivar el componente "frecuencia" si existe
            if (frecuenciaComponent != null)
            {
                frecuenciaComponent.enabled = false;
            }
        }
        else if (!muerteObject.activeSelf && isMuerteActive)
        {
            isMuerteActive = false;
            StopCoroutine(DecreaseCounter());

            // Activar el componente "frecuencia" si existe
            if (frecuenciaComponent != null)
            {
                frecuenciaComponent.enabled = true;
            }
        }
    }

    private IEnumerator DecreaseCounter()
    {
        while (counterValue > 0f)
        {
            counterValue -= decreaseSpeed * Time.deltaTime;
            counterText.text = counterValue.ToString("0");
            yield return null;
        }

        counterValue = 0f;
        counterText.text = counterValue.ToString("0");
    }
}