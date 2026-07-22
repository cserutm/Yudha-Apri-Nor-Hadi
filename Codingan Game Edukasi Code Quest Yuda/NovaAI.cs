using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NovaAI : MonoBehaviour
{
    [Header("UI Hint")]
    public TMP_Text textHintCounter;
    // ======================================================
    // 🤖 ANIMASI NOVA
    // ======================================================
    [Header("Animasi Nova")]
    public float scaleSpeed = 2f;
    public float scaleAmount = 0.05f;

    private Vector3 scaleAwal;

    // ======================================================
    // 🧩 REFERENSI PUZZLE
    // ======================================================
    [Header("Referensi Puzzle")]
    public drag[] semuaPuzzle;
    public Transform[] slots;

    // ======================================================
    // 💬 BUBBLE CHAT NOVA
    // ======================================================
    [Header("Bubble Chat")]
    public TMP_Text bubbleText;
    public GameObject bubblePanel;

    [TextArea(2,5)]
    public string[] idleChat;

    // ======================================================
    // 📋 PANEL PETUNJUK
    // ======================================================
    [Header("Panel Bantuan AI")]
    public GameObject panelPetunjuk;
    public TMP_Text teksPetunjuk;

    // ======================================================
    // 📝 DATA LANGKAH
    // ======================================================
    [Header("Data Puzzle")]
    public string[] namaLangkah;

    [TextArea(3,8)]
    public string[] penjelasanLangkah;

    // ======================================================
    // 🎯 STATUS GAME
    // ======================================================
    [Header("Status Game")]
    public bool puzzleSelesai;

    // ======================================================
    // 💡 SISTEM HINT
    // ======================================================
    [Header("Sistem Hint")]
    public int maksimalHint = 3;

    [SerializeField]
    private int jumlahHintDipakai = 0;

    // ======================================================
    // ▶ START
    // ======================================================
    void Start()
    {
        scaleAwal = transform.localScale;

        panelPetunjuk.SetActive(false);

        StartCoroutine(RandomChat());

        UpdateHintUI();
    }

    // ======================================================
    // 💬 RANDOM CHAT NOVA
    // ======================================================
    IEnumerator RandomChat()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(5f, 10f));

            if (!panelPetunjuk.activeSelf)
            {
                bubblePanel.SetActive(true);

                bubbleText.text =
                    idleChat[Random.Range(0, idleChat.Length)];
            }
        }
    }
    // ======================================================
    // 🤖 MINTA PETUNJUK
    // ======================================================
    public void MintaPetunjuk()
{
    panelPetunjuk.SetActive(true);
    panelPetunjuk.transform.SetAsLastSibling();

    // =========================
    // CEK TIAP SLOT
    // =========================
    for (int i = 0; i < slots.Length; i++)
    {
        drag itemDiSlot = null;

        // cari puzzle di slot
        foreach (drag item in semuaPuzzle)
        {
            if (item.currentSlot == slots[i])
            {
                itemDiSlot = item;
                break;
            }
        }

        // =========================
        // SLOT KOSONG
        // =========================
        if (itemDiSlot == null)
        {
            // 🔥 HINT TIDAK BERKURANG
            teksPetunjuk.text =
                "Nova mendeteksi langkah ke-" + (i + 1) +
                " masih kosong.\n\n" +

                "Langkah yang benar:\n" +
                namaLangkah[i] + "\n\n" +

                "Penjelasan:\n" +
                penjelasanLangkah[i];
            return;
        }

        // =========================
        // LANGKAH SALAH
        // =========================
        if (itemDiSlot.idPuzzle != i + 1)
        {
            // =========================
            // CEK BATAS HINT
            // =========================
            if (jumlahHintDipakai >= maksimalHint)
            {
                teksPetunjuk.text =
                    "NOVA AI ASSISTANT\n" +
                    "Bantuan Nova pada level ini sudah habis.\n\n" +
                    "Maksimal penggunaan hint hanya 3 kali setiap level";

                return;
            }

            // 🔥 HINT BERKURANG
            jumlahHintDipakai++;
            UpdateHintUI();

            teksPetunjuk.text =
                "Terdapat kesalahan pada langkah ke-" + (i + 1) + ".\n\n" +

                "Langkah yang benar:\n" +
                namaLangkah[i] + "\n\n" +

                "Penjelasan:\n" +
                penjelasanLangkah[i];
            return;
        }
    }

    // =========================
    // SEMUA BENAR
    // =========================
    puzzleSelesai = true;

    teksPetunjuk.text =
        "HEBAT!\n\n" +
        "Semua langkah puzzle coding sudah benar.\n\n" +
        "Nova bangga karena kamu berhasil menyusun algoritma dengan tepat.";
}
    // ======================================================
    // ❌ TUTUP PANEL
    // ======================================================
    public void TutupPanel()
    {
        panelPetunjuk.SetActive(false);
    }

    // ======================================================
    // 🔄 RESET HINT LEVEL
    // HANYA DIPANGGIL SAAT PINDAH LEVEL
    // ======================================================
    public void ResetHintLevel()
    {
        jumlahHintDipakai = 0;
        UpdateHintUI();
    }

    // ======================================================
    // 📊 AMBIL SISA HINT
    // ======================================================
    public int GetSisaHint()
    {
        return maksimalHint - jumlahHintDipakai;
    }

    // ======================================================
    // ✨ ANIMASI NOVA
    // ======================================================

    void UpdateHintUI()
{
    textHintCounter.text =
        "Sisa Bantuan : " +
        jumlahHintDipakai + "/" + maksimalHint;
}

    void Update()
    {
        float scale =
            1 + Mathf.Sin(Time.time * scaleSpeed) * scaleAmount;

        transform.localScale = scaleAwal * scale;
    }
}