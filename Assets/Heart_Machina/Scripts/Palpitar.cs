using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Palpitar : MonoBehaviour
{
    public Text heartText;

    private void Start()
    {
        GenerateHeart();
    }

    private void GenerateHeart()
    {
        string heartSymbol = "<color=red><size=72>♥</size></color>";
        string heartTitle = "<color=white>Heartbeat Monitoring</color>";

        string heartTextString = string.Format("{0} {1}", heartSymbol, heartTitle);
        heartText.text = heartTextString;
    }
}