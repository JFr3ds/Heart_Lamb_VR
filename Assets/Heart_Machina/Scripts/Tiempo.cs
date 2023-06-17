using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Tiempo : MonoBehaviour
{
    public Text dateTimeText;

    private void Start()
    {
        UpdateDateTimeText();
    }

    private void Update()
    {
        // Actualizar la fecha y hora cada segundo (puedes ajustar el intervalo según tus necesidades)
        if (DateTime.Now.Second == 0)
        {
            UpdateDateTimeText();
        }
    }

    private void UpdateDateTimeText()
    {
        string dateTimeString = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        dateTimeText.text = dateTimeString;
    }
}