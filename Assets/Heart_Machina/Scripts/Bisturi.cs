using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Bisturi : MonoBehaviour
{
    private Vector3 previousPosition;
    public UI_Panel uiPanel;
    private bool hasShownCutMessage = false;

    private void Start()
    {
        StartCoroutine(WaitForGravity());
    }

    private IEnumerator WaitForGravity()
    {
        yield return new WaitForSeconds(0.5f);
        previousPosition = transform.position;
        StartCoroutine(UpdatePositionRoutine());
    }

    private IEnumerator UpdatePositionRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);

            if (transform.position != previousPosition && !hasShownCutMessage)
            {
                previousPosition = transform.position;

                if (uiPanel != null)
                {
                    uiPanel.ShowCutMessage();
                    hasShownCutMessage = true;
                }
            }
        }
    }
}