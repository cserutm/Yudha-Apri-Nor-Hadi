using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TotalSkorLevel : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text totalText;

    [Header("Data Level")]
    public string prefixLevel = "algoritma";

    public int jumlahSoal = 10;

    void Start()
    {
        int total =
            LevelData.GetTotalScore(prefixLevel, jumlahSoal);

        int maksimal = jumlahSoal * 100;

        totalText.text =
            "Total Skor : " + total + " / " + maksimal;
    }
}