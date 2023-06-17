using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Panel : MonoBehaviour
{
    public GameObject objetoA;
    public GameObject objetoB;
    public GameObject APlusB;
    public GameObject Mano_IZ;
    public GameObject Mano_Der;
    public GameObject scalpel;

    public TMP_Text textObject;

    private bool hasShownScalpelMessage = false;
    private bool hasShownCutMessage = false;
    private bool hasShownLeftTrayMessage = false;

    void Update()
    {
        if (Mano_IZ != null && Mano_Der != null && APlusB != null && scalpel != null)
        {
            Bounds aPlusBBounds = GetBounds(APlusB);

            if (aPlusBBounds.Contains(Mano_IZ.transform.position) || aPlusBBounds.Contains(Mano_Der.transform.position))
            {
                Debug.Log("La mano izquierda o derecha está cerca o dentro del objeto A+B.");
                if (!hasShownScalpelMessage)
                {
                    ShowScalpelMessage();
                }
            }

            if (!objetoA.transform.IsChildOf(APlusB.transform) || !objetoB.transform.IsChildOf(APlusB.transform))
            {
                Debug.Log("El objeto A+B ya no es padre de objetoA y objetoB.");
                if (!hasShownLeftTrayMessage)
                {
                    ShowLeftTrayMessage();
                }
            }

           /* Bounds scalpelBounds = GetBounds(scalpel);

            if (scalpelBounds.Contains(Mano_IZ.transform.position) || scalpelBounds.Contains(Mano_Der.transform.position))
            {
                Debug.Log("La mano izquierda o derecha está cerca o dentro del objeto scalpel.");
                if (!hasShownCutMessage)
                {
                    ShowCutMessage();
                }
            }*/
        }
    }

    private Bounds GetBounds(GameObject gameObject)
    {
        Renderer[] renderers = gameObject.GetComponentsInChildren<Renderer>();

        if (renderers.Length > 0)
        {
            Bounds bounds = renderers[0].bounds;
            for (int i = 1; i < renderers.Length; i++)
            {
                bounds.Encapsulate(renderers[i].bounds);
            }

            return bounds;
        }

        return new Bounds();
    }

    private void ShowScalpelMessage()
    {
        hasShownScalpelMessage = true;
        textObject.text = "Now take the scalpel that is on the right tray with your free hand.";
        StartCoroutine(HideMessageAfterDelay(5f));
    }

    public void ShowCutMessage()
    {
        hasShownCutMessage = true;
        textObject.text = "Cut along the dotted line.";
        StartCoroutine(HideMessageAfterDelay(2f));
    }

    public void ShowLeftTrayMessage()
    {
        hasShownLeftTrayMessage = true;
        textObject.text = "Put the scalpel on the left tray.";
        StartCoroutine(HideMessageAfterDelay(10f));
    }

    private IEnumerator HideMessageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        textObject.text = "";
    }
}
