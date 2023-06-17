using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject scriptsContainer;
    public GameObject muerteObject;
    public GameObject latidoObject;
    public GameObject taquicardiaObject;

    public Animator animatorA;
    public Animator animatorB;

    private Frecuencia frecuenciaComponente;
    private List<Frecuencia_Muerte> frecuenciaMuerteComponentes;
    private Frecuencia_Taquicardia frecuenciaTaquicardiaComponente;
    private Animator latidoAnimator;
    private Animator taquicardiaAnimator;

    public TMP_Text textObject;

    private void Start()
    {
        muerteObject.SetActive(false);
        latidoObject.SetActive(false);
        taquicardiaObject.SetActive(false);

        //textObject.text = "Take the heart in your hand.";
        //Now take the scalpel that is on the right tray with your free hand.
        //Cut along the dotted line.
        //You have 5 seconds before the heart drops, put the scalpel on the left tray.
        //Take both parts of the heart.
        //Now you can reattach the two parts of the heart.


        frecuenciaComponente = scriptsContainer.GetComponentInChildren<Frecuencia>();
        frecuenciaMuerteComponentes = new List<Frecuencia_Muerte>(scriptsContainer.GetComponentsInChildren<Frecuencia_Muerte>());
        frecuenciaTaquicardiaComponente = scriptsContainer.GetComponentInChildren<Frecuencia_Taquicardia>();

        if (frecuenciaComponente != null)
            frecuenciaComponente.enabled = false;

        foreach (Frecuencia_Muerte frecuenciaMuerteComponente in frecuenciaMuerteComponentes)
        {
            frecuenciaMuerteComponente.enabled = false;
        }

        if (frecuenciaTaquicardiaComponente != null)
            frecuenciaTaquicardiaComponente.enabled = false;

        latidoAnimator = latidoObject.GetComponent<Animator>();
        taquicardiaAnimator = taquicardiaObject.GetComponent<Animator>();
    }

    public void ActivarMuerte()
    {
        muerteObject.SetActive(true);
        latidoObject.SetActive(false);
        taquicardiaObject.SetActive(false);
        Muerte_anim();

        foreach (Frecuencia_Muerte frecuenciaMuerteComponente in frecuenciaMuerteComponentes)
        {
            frecuenciaMuerteComponente.enabled = true;
        }

        if (frecuenciaComponente != null)
            frecuenciaComponente.enabled = false;

        if (frecuenciaTaquicardiaComponente != null)
            frecuenciaTaquicardiaComponente.enabled = false;

        if (latidoAnimator != null)
            latidoAnimator.enabled = false;

        if (taquicardiaAnimator != null)
            taquicardiaAnimator.enabled = false;

        Debug.Log("Se ha activado la muerte del corazón.");
    }

    public void ActivarLatido()
    {
        muerteObject.SetActive(false);
        latidoObject.SetActive(true);
        taquicardiaObject.SetActive(false);
        Latido_anim();

        foreach (Frecuencia_Muerte frecuenciaMuerteComponente in frecuenciaMuerteComponentes)
        {
            frecuenciaMuerteComponente.enabled = false;
        }

        if (frecuenciaComponente != null)
            frecuenciaComponente.enabled = true;

        if (frecuenciaTaquicardiaComponente != null && taquicardiaObject.activeSelf)
            frecuenciaTaquicardiaComponente.enabled = false;

        if (latidoAnimator != null)
        {
            latidoAnimator.enabled = true;
            latidoAnimator.speed = 1f; // Velocidad original del latido
        }

        if (taquicardiaAnimator != null)
            taquicardiaAnimator.enabled = false;

        Debug.Log("Se ha activado el latido del corazón.");
    }

    public void ActivarFrecuenciaTaquicardia()
    {
        muerteObject.SetActive(false);
        latidoObject.SetActive(false);
        taquicardiaObject.SetActive(true);
        Taquicardia_anim();

        if (frecuenciaComponente != null)
            frecuenciaComponente.enabled = false;

        foreach (Frecuencia_Muerte frecuenciaMuerteComponente in frecuenciaMuerteComponentes)
        {
            frecuenciaMuerteComponente.enabled = false;
        }

        if (frecuenciaTaquicardiaComponente != null && taquicardiaObject.activeSelf)
            frecuenciaTaquicardiaComponente.enabled = true;

        if (latidoAnimator != null)
            latidoAnimator.enabled = false;

        if (taquicardiaAnimator != null)
        {
            taquicardiaAnimator.enabled = true;
            taquicardiaAnimator.speed = 2f; // Velocidad duplicada para la taquicardia
        }

        Debug.Log("Se ha activado la frecuencia de taquicardia.");
    }

    public void Latido_anim()
    {
        // Lógica para activar la animación de latido
        if (animatorA != null)
        {
            animatorA.SetBool("Latido", true);
            animatorA.speed = 1f; // Velocidad normal
        }

        if (animatorB != null)
        {
            animatorB.SetBool("Latido", true);
            animatorB.speed = 1f; // Velocidad normal
        }
    }

    public void Taquicardia_anim()
    {
        // Lógica para activar la animación de taquicardia
        if (animatorA != null)
        {
            animatorA.SetBool("Latido", true);
            animatorA.speed = 3f; // Triplica la velocidad
        }

        if (animatorB != null)
        {
            animatorB.SetBool("Latido", true);
            animatorB.speed = 3f; // Triplica la velocidad
        }
    }

    public void Muerte_anim()
    {
        // Lógica para activar la animación de muerte
        if (animatorA != null)
        {
            animatorA.SetBool("Latido", false); // Desactiva la animación de latido
            animatorA.speed = 0f; // Detiene la animación
        }

        if (animatorB != null)
        {
            animatorB.SetBool("Latido", false); // Desactiva la animación de latido
            animatorB.speed = 0f; // Detiene la animación
        }
    }

    public void ResetGameManager()
    {
        muerteObject.SetActive(false);
        latidoObject.SetActive(true);
        taquicardiaObject.SetActive(false);
        Latido_anim();

        foreach (Frecuencia_Muerte frecuenciaMuerteComponente in frecuenciaMuerteComponentes)
        {
            frecuenciaMuerteComponente.enabled = false;
        }

        if (frecuenciaComponente != null)
            frecuenciaComponente.enabled = true;

        if (frecuenciaTaquicardiaComponente != null && taquicardiaObject.activeSelf)
            frecuenciaTaquicardiaComponente.enabled = false;

        if (latidoAnimator != null)
        {
            latidoAnimator.enabled = true;
            latidoAnimator.speed = 1f; // Velocidad original del latido
        }

        if (taquicardiaAnimator != null)
            taquicardiaAnimator.enabled = false;
    }
}