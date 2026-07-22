using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public NovaAI nova;

    [Header("Scene")]
    public string namaSceneMenu = "Level 1"; // menu level
    
    [Header("Soal")]
    public string idSoal;

    [Header("UI Skor")]
    public TMP_Text skorLevelText;
    public TMP_Text skorTotalText;

    [Header("Puzzle")]
    public drag[] semuaPuzzle;
    public Transform[] slots;

    [Header("Timer UI")]
    public Slider timerBar;
    public Image fillTimer;

    [Header("Warna Timer")]
    public Color warnaHijau = Color.green;
    public Color warnaKuning = Color.yellow;
    public Color warnaMerah = Color.red;

    [Header("Panel")]
    public GameObject panelBenar;
    public GameObject panelSalah;
    public GameObject panelWaktuHabis;
    public GameObject panelBelumLengkap;

    [Header("Button")]
    public Button tombolRun;

    [Header("Timer")]
    public float waktu = 60f;

    private float waktuSekarang;
    private bool waktuHabis = false;
    private bool gameSelesai = false;

    private int skorLevel = 0;

    void Start()
    {
        waktuSekarang = waktu;

        timerBar.maxValue = waktu;
        timerBar.value = waktuSekarang;

        panelBenar.SetActive(false);
        panelSalah.SetActive(false);
        panelWaktuHabis.SetActive(false);
        panelBelumLengkap.SetActive(false);

        UpdateUI();
    }

    void Update()
    {
        if (waktuHabis || gameSelesai)
            return;

        waktuSekarang -= Time.deltaTime;

        if (waktuSekarang <= 0)
        {
            waktuSekarang = 0;
            waktuHabis = true;

            panelWaktuHabis.SetActive(true);
            tombolRun.interactable = false;
        }

        UpdateUI();
    }

    void UpdateUI()
    {
        skorLevelText.text = "Skor: " + skorLevel;

        if (GameData.instance != null)
            skorTotalText.text = "Total : " + GameData.instance.skorTotal;

        timerBar.value = waktuSekarang;

        float persen = waktuSekarang / waktu;

        if (persen > 0.5f)
            fillTimer.color = warnaHijau;
        else if (persen > 0.25f)
            fillTimer.color = warnaKuning;
        else
            fillTimer.color = warnaMerah;
    }

    // =========================
    // CEK JAWABAN
    // =========================
    public void CekJawaban()
{
    if (waktuHabis || gameSelesai)
        return;

    panelBenar.SetActive(false);
    panelSalah.SetActive(false);
    panelBelumLengkap.SetActive(false);

    // =========================
    // CEK JUMLAH SLOT TERISI
    // =========================
    int jumlahTerisi = 0;

    foreach (drag item in semuaPuzzle)
    {
        if (item.currentSlot != null)
        {
            jumlahTerisi++;
        }
    }

    // jika belum semua terisi
    if (jumlahTerisi < slots.Length)
    {
        panelBelumLengkap.SetActive(true);
        return;
    }

    // =========================
    // CEK JAWABAN
    // =========================
    bool semuaBenar = true;

    for (int i = 0; i < slots.Length; i++)
    {
        drag itemDiSlot = null;

        foreach (drag item in semuaPuzzle)
        {
            if (item.currentSlot == slots[i])
            {
                itemDiSlot = item;
                break;
            }
        }

        // salah urutan
        if (itemDiSlot == null || itemDiSlot.idPuzzle != i + 1)
        {
            semuaBenar = false;
        }
    }
    // =========================
    // HASIL
    // =========================
    if (semuaBenar)
{
    if (!gameSelesai)
    {
        skorLevel = 100;

        LevelData.SaveScore(idSoal, skorLevel);

        if (GameData.instance != null)
            GameData.instance.TambahSkor(skorLevel);
    }

    gameSelesai = true;

    panelBenar.SetActive(true);
    tombolRun.interactable = false;
}
    else
{
    skorLevel = 0;
    panelSalah.SetActive(true);
}
}
    // =========================
    // RESET LEVEL
    // =========================
    public void ResetGame()
    {
        foreach (drag item in semuaPuzzle)
        {
            item.ResetPuzzle();
        }

        skorLevel = 0;
        gameSelesai = false;

        panelSalah.SetActive(false);
        panelBelumLengkap.SetActive(false);

        UpdateUI();
    }

    public void StopGame()
{
    SceneManager.LoadScene(namaSceneMenu);
}

public void LanjutLevel()
{
    int currentIndex = SceneManager.GetActiveScene().buildIndex;
    int nextIndex = currentIndex + 1;

    if (nextIndex < SceneManager.sceneCountInBuildSettings)
    {
        SceneManager.LoadScene(nextIndex);
    }
    else
    {
        // kalau sudah level terakhir
        SceneManager.LoadScene(namaSceneMenu);
    }
}
    public void CobaLagi()
    {
        ResetGame();

        waktuSekarang = waktu;
        waktuHabis = false;

        timerBar.value = waktuSekarang;
        fillTimer.color = warnaHijau;

        panelWaktuHabis.SetActive(false);
        tombolRun.interactable = true;
    }

    public void TutupPanelSalah()
    {
        panelSalah.SetActive(false);
    }

    public void SusunLagi()
    {
        panelBelumLengkap.SetActive(false);
    }
}
