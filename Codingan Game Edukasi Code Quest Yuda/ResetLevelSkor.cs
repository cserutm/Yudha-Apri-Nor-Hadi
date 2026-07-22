using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetLevelSkor : MonoBehaviour
{
    [Header("Data Level")]
    public string prefixLevel = "algoritma";

    public int jumlahSoal = 10;

    // =========================
    // RESET SKOR LEVEL
    // =========================
    public void ResetSkorLevel()
    {
        for (int i = 1; i <= jumlahSoal; i++)
        {
            PlayerPrefs.DeleteKey(prefixLevel + "_" + i + "_score");
            PlayerPrefs.DeleteKey(prefixLevel + "_" + i + "_done");
        }

        PlayerPrefs.Save();

        Debug.Log("Skor level " + prefixLevel + " berhasil direset");

        // reload scene agar UI langsung update
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}