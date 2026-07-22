using UnityEngine;

public class JawabanBenar : MonoBehaviour
{
    public GameObject panelSoal;

    public void JawabBenar()
    {
        // tutup panel
        panelSoal.SetActive(false);
        // lanjut game
        Time.timeScale = 1f;
    }
}