using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusikControl : MonoBehaviour
{
    public static MusikControl Instance { get; private set; }

    [Header("Daftar Musik")]
    public AudioClip[] clipMusik;

    [Header("Audio Source")]
    public AudioSource audioMusik;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Fungsi ganti musik
    public void ChangeMusik(int indexMusik)
    {
        if (indexMusik < 0 || indexMusik >= clipMusik.Length)
            return;

        // Jika musik berbeda
        if (audioMusik.clip != clipMusik[indexMusik])
        {
            audioMusik.Stop();
            audioMusik.clip = clipMusik[indexMusik];
            audioMusik.Play();
        }
    }
}