using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPuzzle : MonoBehaviour
{
    public drag[] semuaPuzzle;
    public Transform[] slots;

    public GameObject panelBenar;
    public GameObject panelSalah;

    public void CekJawaban()
    {
        bool benar = true;

        for (int i = 0; i < slots.Length; i++)
        {
            bool cocok = false;

            foreach (drag item in semuaPuzzle)
            {
                if (item.currentSlot == slots[i] && item.idPuzzle == i + 1)
                {
                    cocok = true;
                    break;
                }
            }

            if (!cocok)
            {
                benar = false;
                break;
            }
        }

        // 🔥 TAMPILKAN POPUP
        if (benar)
        {
            panelBenar.SetActive(true);
            panelSalah.SetActive(false);
        }
        else
        {
            panelBenar.SetActive(false);
            panelSalah.SetActive(true);
        }
    }
}