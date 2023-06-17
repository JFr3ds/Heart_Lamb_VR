using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corte : MonoBehaviour
{
    private List<GameObject> elementos = new List<GameObject>();
    private int currentIndex = 0;
    private float waitTime = 0.1f; // Tiempo de espera reducido
    private LineRenderer lineRenderer;

    private GameManager gameManager; // Referencia al GameManager

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        AutoAssignElements();
        StartCoroutine(DrawLineCoroutine());

        gameManager = FindObjectOfType<GameManager>(); // Obtener la instancia del GameManager
    }

    private void AutoAssignElements()
    {
        for (int i = 1; i <= 18; i++) // Cambiado a 18 elementos
        {
            string childName = i.ToString();
            Transform child = transform.Find(childName);
            if (child != null)
            {
                elementos.Add(child.gameObject);
            }
        }
    }

    private IEnumerator DrawLineCoroutine()
    {
        while (true)
        {
            // Verificar si la taquicardia está activa en el GameManager
            bool taquicardiaActiva = gameManager != null && gameManager.taquicardiaObject != null && gameManager.taquicardiaObject.activeSelf;

            // Obtener los dos elementos consecutivos
            GameObject startElement = elementos[currentIndex];
            GameObject endElement = elementos[(currentIndex + 1) % elementos.Count];

            // Calcular la distancia entre los elementos
            float distance = Vector3.Distance(startElement.transform.position, endElement.transform.position);

            // Configurar el LineRenderer
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, startElement.transform.position);
            lineRenderer.SetPosition(1, endElement.transform.position);

            // Esperar un breve tiempo
            yield return new WaitForSeconds(waitTime);

            // Incrementar el índice actual
            currentIndex++;

            // Reiniciar el LineRenderer y el índice si se llega al último elemento o si la taquicardia está activa
            if (currentIndex >= elementos.Count || taquicardiaActiva)
            {
                currentIndex = 0;
                lineRenderer.positionCount = 0;

                // Verificar si la taquicardia está activa y destruir el objeto actual
                if (taquicardiaActiva)
                {
                    Destroy(gameObject);
                    yield break; // Salir del coroutine
                }

                yield return new WaitForSeconds(waitTime); // Esperar un breve tiempo antes de comenzar el nuevo recorrido
            }
        }
    }
}