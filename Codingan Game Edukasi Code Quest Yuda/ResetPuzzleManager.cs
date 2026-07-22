using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPuzzleManager : MonoBehaviour
{
    public drag[] semuaPuzzle;

    public void ResetSemua()
    {
        foreach (drag item in semuaPuzzle)
        {
            item.ResetPuzzle();
        }
    }
}