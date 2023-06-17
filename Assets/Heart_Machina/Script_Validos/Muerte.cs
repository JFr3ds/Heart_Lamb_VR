using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Muerte : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    public float speed = 5f;
    public Frecuencia_Muerte frecuenciaMuerte; // Referencia al componente Frecuencia_Muerte
    public AudioSource audioSource; // Referencia al componente AudioSource
    public AudioClip audioClip; // Clip de audio a reproducir

    private TrailRenderer trailRenderer;
    private bool isMoving = true;

    private void Start()
    {
        trailRenderer = GetComponent<TrailRenderer>();
        trailRenderer.Clear();
    }

    private void Update()
    {
        if (isMoving && startPoint != null && endPoint != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPoint.position, speed * Time.deltaTime);
            trailRenderer.AddPosition(transform.position);

            if (transform.position == endPoint.position)
            {
                isMoving = false;
                StartCoroutine(ResetPosition());
            }
        }
    }

    private IEnumerator ResetPosition()
    {
        yield return new WaitForSeconds(2f); // Esperar 2 segundos antes de reiniciar la posición

        transform.position = startPoint.position;
        trailRenderer.Clear();

        isMoving = true;

        // Activar el componente Frecuencia_Muerte
        if (frecuenciaMuerte != null)
        {
            frecuenciaMuerte.enabled = true;
        }

        // Reproducir el sonido
        if (audioSource != null && audioClip != null)
        {
            audioSource.clip = audioClip;
            audioSource.Play();
        }
    }
}