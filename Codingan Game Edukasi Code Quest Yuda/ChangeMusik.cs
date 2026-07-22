using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMusik : MonoBehaviour
{
    [Header("Index Musik")]
    public int indexMusik;

    void Start()
    {
        if (MusikControl.Instance != null)
        {
            MusikControl.Instance.ChangeMusik(indexMusik);
        }
    }
}