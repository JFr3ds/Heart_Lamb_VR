using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Presion : MonoBehaviour
{
    public Transform[] waypoints;
    public float normalSpeed = 5f;
    public float altaSpeed = 100f;
    public float waitDurationBeforeResetNormal = 2f;
    public float waitDurationBeforeResetAlta = 1f;
    public GameObject Normal;
    public GameObject Alta;
    public GameObject Taquicardia;
    public GameObject Latido;
    public TrailRenderer trailRenderer;
    public Text valueText;

    private int currentIndex;
    private Transform currentWaypoint;
    private float floatValue;

    private void Start()
    {
        trailRenderer.Clear();
        trailRenderer.enabled = false;

        CheckActivation();

        StartMovement();
    }

    private void Update()
    {
        if (currentWaypoint != null)
        {
            float currentSpeed = Normal.activeSelf ? normalSpeed : altaSpeed;

            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint.position, currentSpeed * Time.deltaTime);
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
                    if (Normal.activeSelf)
                    {
                        StartWaitTimerBeforeReset(waitDurationBeforeResetNormal);
                    }
                    else if (Alta.activeSelf)
                    {
                        StartWaitTimerBeforeReset(waitDurationBeforeResetAlta);
                    }
                    trailRenderer.enabled = false;
                }
            }
        }
    }

    private void CheckActivation()
    {
        bool taquicardiaActive = Taquicardia.activeSelf;
        bool latidoActive = Latido.activeSelf;

        if (taquicardiaActive && !latidoActive)
        {
            ActivateAlta();
        }
        else if (latidoActive)
        {
            ActivateNormal();
        }
    }

    private void ActivateNormal()
    {
        Normal.SetActive(true);
        Alta.SetActive(false);

        floatValue = Random.Range(50f, 60f);
        floatValue = Mathf.Clamp(floatValue, 50f, 60f); // Asegurar que el valor esté dentro del rango
        UpdateValueText();
    }

    private void ActivateAlta()
    {
        Alta.SetActive(true);
        floatValue += Random.Range(71f, 100f);
        floatValue = Mathf.Clamp(floatValue, 100f, 120f); // Asegurar que el valor esté dentro del rango
        UpdateValueText();
    }

    private void UpdateValueText()
    {
        valueText.text = floatValue.ToString("F2");
    }

    private void StartMovement()
    {
        currentIndex = 0;
        currentWaypoint = waypoints[currentIndex];
        transform.position = currentWaypoint.position;
        trailRenderer.Clear();
        trailRenderer.enabled = true;
    }

    private void StartWaitTimerBeforeReset(float waitDuration)
    {
        StartCoroutine(WaitAndReset(waitDuration));
    }

    private IEnumerator WaitAndReset(float waitDuration)
    {
        yield return new WaitForSeconds(waitDuration);

        CheckActivation();
        StartMovement();
    }
}