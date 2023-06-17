using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class Corazon : MonoBehaviour
{
    public GameObject objetoA;
    public GameObject objetoB;
    public float distanciaUmbral = 0.5f;
    public string separadorTag = "Separador";
    public float velocidadMovimiento = 1f;
    public GameObject Linea;

    public ResetObjects resetObjectsScript;
    public Padre padreScript;

    private GameObject separadorObjeto;
    private bool bisturiAMasDeUno = false;
    private bool bisturiAMenosDeUno = false;
    private GameManager gameManager;

    public UI_Panel uiPanel;

    public TMP_Text textObject;

    private void Start()
    {
        separadorObjeto = GameObject.FindGameObjectWithTag(separadorTag);
        gameManager = FindObjectOfType<GameManager>();
        resetObjectsScript = FindObjectOfType<ResetObjects>();
        padreScript = GetComponent<Padre>();
        StartCoroutine(ShowTextRoutine());
        Linea.SetActive(false);
        uiPanel = FindObjectOfType<UI_Panel>();
    }

    private void Update()
    {
        if (objetoA != null && objetoB != null && separadorObjeto != null)
        {
            float distanciaAB = Vector3.Distance(objetoA.transform.position, objetoB.transform.position);
            float distanciaASeparador = Vector3.Distance(objetoA.transform.position, separadorObjeto.transform.position);
            float distanciaBSeparador = Vector3.Distance(objetoB.transform.position, separadorObjeto.transform.position);

            Debug.Log("Distancia entre objetoA y objetoB: " + distanciaAB);

            if (distanciaAB < distanciaUmbral)
            {
                if (distanciaASeparador > 1f && !bisturiAMasDeUno)
                {
                    Debug.Log("El bistur� est� a m�s de 1 de distancia.");
                    bisturiAMasDeUno = true;
                    gameManager.ActivarLatido();
                    Linea.SetActive(true);

                }

                if (distanciaASeparador < 0.4f && !bisturiAMenosDeUno)
                {
                    Debug.Log("El bistur� est� a menos de 1 de distancia.");
                    bisturiAMenosDeUno = true;
                    gameManager.ActivarFrecuenciaTaquicardia();
                    uiPanel.ShowLeftTrayMessage();
                    objetoB.transform.Translate(Vector3.right * 0.001f);
                    StartCoroutine(EnableCollisionsRoutine());

                }

                if (distanciaAB <= 0.1 && objetoA.GetComponent<XRGrabInteractable>().isSelected && objetoB.GetComponent<XRGrabInteractable>().isSelected)
                {
                    RestartGame();
                    Debug.Log("es igual a 0");
                }

                if (distanciaASeparador < 0.2f)
                {
                    // Mover objeto A en direcci�n negativa en el eje X
                    //objetoA.transform.Translate(Vector3.left * velocidadMovimiento * Time.deltaTime);
                }

                if (distanciaBSeparador < 0.2f)
                {
                    // Mover objeto B en direcci�n positiva en el eje X
                    // objetoB.transform.Translate(Vector3.right * velocidadMovimiento * Time.deltaTime);
                }

            }
        }
    }

    private IEnumerator ShowTextRoutine()
    {
        textObject.text = "Take the heart in your hand.";
        yield return new WaitForSeconds(5f);
        textObject.text = string.Empty;
    }

    private IEnumerator EnableCollisionsRoutine()
    {
        yield return new WaitForSeconds(0.5f); // Esperar 0.5 segundos
        resetObjectsScript.ResetToInitialState();
    }

    public void RestartGame()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
}