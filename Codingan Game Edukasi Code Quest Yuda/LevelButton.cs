using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{
    [Header("Data Soal")]
    public string idSoal;

    [Header("Nama Scene")]
    public string namaScene;

    [Header("UI")]
    public TMP_Text textSkor;

    [Header("Icon")]
    public GameObject iconSelesai;

    void Start()
    {
        int skor = LevelData.GetScore(idSoal);

        // jika sudah pernah dikerjakan
        if (LevelData.IsDone(idSoal))
        {
            textSkor.text = "Skor : " + skor;

            if (iconSelesai != null)
                iconSelesai.SetActive(true);
        }
        else
        {
            textSkor.text = "Belum Dikerjakan";

            if (iconSelesai != null)
                iconSelesai.SetActive(false);
        }
    }

    // =========================
    // BUKA SCENE SOAL
    // =========================
    public void BukaLevel()
    {
        SceneManager.LoadScene(namaScene);
    }
}