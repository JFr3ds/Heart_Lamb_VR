using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Latido1 : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 5f;
    public float waitDurationBeforeReset = 1f;
    public Frecuencia_Taquicardia frecuencia;
    public AudioSource audioSource; // Referencia al componente AudioSource
    public AudioClip audioClip; // Clip de audio a reproducir

    private int currentIndex;
    private Transform currentWaypoint;
    private TrailRenderer trailRenderer;

    private void Start()
    {
        if (waypoints.Length > 0)
        {
            currentIndex = 0;
            currentWaypoint = waypoints[currentIndex];
        }

        trailRenderer = GetComponent<TrailRenderer>();
        trailRenderer.Clear();
        trailRenderer.enabled = false;

        ActivateFrecuencia();

        StartMovement();
    }

    private void Update()
    {
        if (currentWaypoint != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint.position, speed * Time.deltaTime);
            trailRenderer.AddPosition(transform.position);

            if (transform.position == currentWaypoint.position)
            {
                currentIndex++;
                if (currentIndex < waypoints.Length)
                {
                    currentWaypoint = waypoints[currentIndex];
                }
                else
                {
                    currentWaypoint = null;
                    StartWaitTimerBeforeReset();
                    trailRenderer.enabled = false;
                }
            }
        }
    }

    private void ActivateFrecuencia()
    {
        if (frecuencia != null)
        {
            frecuencia.enabled = true;
        }
    }

    private void StartMovement()
    {
        StartCoroutine(MoveToNextWaypoint());
    }

    private IEnumerator MoveToNextWaypoint()
    {
        while (currentWaypoint != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint.position, speed * Time.deltaTime);
            trailRenderer.AddPosition(transform.position);

            if (transform.position == currentWaypoint.position)
            {
                currentIndex++;
                if (currentIndex < waypoints.Length)
                {
                    currentWaypoint = waypoints[currentIndex];
                }
                else
                {
                    currentWaypoint = null;
                    StartWaitTimerBeforeReset();
                    trailRenderer.enabled = false;
                }
            }

            yield return null;
        }
    }

    private void StartWaitTimerBeforeReset()
    {
        StartCoroutine(WaitAndReset());
    }

    private IEnumerator WaitAndReset()
    {
        yield return new WaitForSeconds(waitDurationBeforeReset);

        currentIndex = 0;
        currentWaypoint = waypoints[currentIndex];
        transform.position = currentWaypoint.position;
        trailRenderer.Clear();
        trailRenderer.enabled = true;

        StartMovement();

        // Reproducir el sonido al reiniciar en la primera waypoint
        if (audioSource != null && audioClip != null)
        {
            audioSource.clip = audioClip;
            audioSource.Play();
        }
    }
}