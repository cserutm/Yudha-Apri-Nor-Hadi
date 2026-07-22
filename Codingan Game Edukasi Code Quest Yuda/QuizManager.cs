using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class QuizManager : MonoBehaviour
{
    [Header("Skor")]
    public TMP_Text textSkor;
    public int skor = 0;

    [Header("Nyawa")]
    public int nyawa = 3;
    
    public Image[] hearts;
    public Sprite heartFull;
    public Sprite heartEmpty;

    [Header("Game Over")]
    public GameObject panelGameOver;

    [Header("Feedback")]
    public GameObject imageBenar;
    public GameObject imageSalah;

    [Header("Finish")]
    public GameObject panelFinish;
    public TMP_Text textSkorAkhir;

    [Header("Panel Input Nama")]
    public GameObject panelInputNama;

    [Header("Leaderboard")]
    public TMP_InputField inputNama;
    public LeaderboardManager leaderboardManager;

    private bool sudahSimpan = false;

    [Header("Respawn")]
    public Transform player;
    public Transform respawnPoint;

    [Header("Checkpoint")]
    public checkpoint checkpointSekarang;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip soundBenar;
    public AudioClip soundSalah;
    public AudioClip soundFinish;  
    public AudioClip soundGameOver;
    public AudioClip soundPopupSoal;

    [System.Serializable]
    public class Soal
    {
        [TextArea]
        public string pertanyaan;

        public string jawabanA;
        public string jawabanB;
        public string jawabanC;
        public string jawabanD;

        public int jawabanBenar;
    }

    [Header("Daftar Soal")]
    public List<Soal> daftarSoal = new List<Soal>();

    [Header("UI")]
    public GameObject panelSoal;

    public TMP_Text textPertanyaan;

    public TMP_Text textA;
    public TMP_Text textB;
    public TMP_Text textC;
    public TMP_Text textD;

    public Button tombolA;
    public Button tombolB;
    public Button tombolC;
    public Button tombolD;

    private Soal soalSekarang;

    // 🔥 Mencegah soal muncul berulang
    private List<int> soalSudahKeluar = new List<int>();

    // 🔥 Mencegah double click jawaban
    private bool sudahMenjawab = false;

    private void Start()
    {
        panelSoal.SetActive(false);
        panelGameOver.SetActive(false);

        imageBenar.SetActive(false);
        imageSalah.SetActive(false);

        panelFinish.SetActive(false);

        if (panelInputNama != null)
            panelInputNama.SetActive(false);

        UpdateSkor();
        UpdateNyawa();
    }

    public void TampilkanSoal()
    {
        panelSoal.SetActive(true);
        Time.timeScale = 0f;

        // Sound popup soal
        if(audioSource != null && soundPopupSoal != null)
        {
         audioSource.PlayOneShot(soundPopupSoal);
        }
        GenerateSoal();
    }
    void GenerateSoal()
    {
        // 🔥 Cek jika semua soal sudah keluar
        // Jika semua soal sudah keluar
        if (soalSudahKeluar.Count >= daftarSoal.Count)
{
        Debug.Log("Reset soal...");

        // Kosongkan daftar soal yang sudah keluar
        soalSudahKeluar.Clear();
}
        sudahMenjawab = false;

        int randomIndex;

        do
        {
            randomIndex = Random.Range(0, daftarSoal.Count);
        }
        while (soalSudahKeluar.Contains(randomIndex));

        soalSudahKeluar.Add(randomIndex);

        soalSekarang = daftarSoal[randomIndex];

        textPertanyaan.text = soalSekarang.pertanyaan;

        textA.text = soalSekarang.jawabanA;
        textB.text = soalSekarang.jawabanB;
        textC.text = soalSekarang.jawabanC;
        textD.text = soalSekarang.jawabanD;

        // 🔥 Hapus listener lama
        tombolA.onClick.RemoveAllListeners();
        tombolB.onClick.RemoveAllListeners();
        tombolC.onClick.RemoveAllListeners();
        tombolD.onClick.RemoveAllListeners();

        // 🔥 Tambah listener baru
        tombolA.onClick.AddListener(() => CekJawaban(0));
        tombolB.onClick.AddListener(() => CekJawaban(1));
        tombolC.onClick.AddListener(() => CekJawaban(2));
        tombolD.onClick.AddListener(() => CekJawaban(3));
    }

    void CekJawaban(int jawabanDipilih)
    {
        // 🔥 Mencegah spam klik
        if (sudahMenjawab)
            return;

        sudahMenjawab = true;

        // BENAR
        if (jawabanDipilih == soalSekarang.jawabanBenar)
        {
            audioSource.PlayOneShot(soundBenar);
            skor += 20;

            UpdateSkor();

            StartCoroutine(FeedbackBenar());
        }

        // SALAH
else
{
    audioSource.PlayOneShot(soundSalah);

    // Kurangi skor
    skor -= 10;

    if (skor < 0)
        skor = 0;

    UpdateSkor();

    // Kurangi nyawa
    nyawa -= 1;

    if (nyawa < 0)
        nyawa = 0;

    UpdateNyawa();

    // Jika nyawa habis
    if (nyawa <= 0)
    {
        GameOver();
        return;
    }

    StartCoroutine(FeedbackSalah());
}
}

    IEnumerator FeedbackBenar()
    {
        imageBenar.SetActive(true);

        yield return new WaitForSecondsRealtime(0.5f);

        imageBenar.SetActive(false);

        panelSoal.SetActive(false);

        Time.timeScale = 1f;
    }

    IEnumerator FeedbackSalah()
{
    imageSalah.SetActive(true);

    yield return new WaitForSecondsRealtime(0.5f);

    imageSalah.SetActive(false);

    panelSoal.SetActive(false);

    // RESET CHECKPOINT
    checkpointSekarang.ResetCheckpoint();

    // Respawn player
    player.position = respawnPoint.position;

    Rigidbody2D rb =
        player.GetComponent<Rigidbody2D>();

    rb.velocity = Vector2.zero;

    Time.timeScale = 1f;
}

    public void UpdateSkor()
    {
        textSkor.text = "Skor : " + skor;
    }

    public void LevelSelesai()
    {
        audioSource.PlayOneShot(soundFinish);
        skor += 50;

        UpdateSkor();

        panelFinish.SetActive(true);

        textSkorAkhir.text = "Skor : " + skor;

        Time.timeScale = 0f;
    }
    public void GameOver()
{
    StartCoroutine(GameOverCoroutine());
}

        IEnumerator GameOverCoroutine()
{
    // Sound game over
    if(audioSource != null && soundGameOver != null)
    {
        audioSource.PlayOneShot(soundGameOver);
    }

    // Tunggu audio sebentar
    yield return new WaitForSecondsRealtime(0.2f);

    panelGameOver.SetActive(true);

    Time.timeScale = 0f;
}
    public void NextLevel()
    {
        Time.timeScale = 1f;

        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;

        // 🔥 Cek apakah scene berikutnya ada
        if (nextScene < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextScene);
        }
        else
        {
            SceneManager.LoadScene("menu adventure");
        }
    }

    public void KembaliMenu()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene("menu adventure");
    }

    public void BukaInputNama()
    {
        if (panelInputNama != null)
            panelInputNama.SetActive(true);
    }

    public void TutupInputNama()
    {
        if (panelInputNama != null)
            panelInputNama.SetActive(false);
    }

    public void SimpanScore()
    {
        if (sudahSimpan)
            return;

        string nama = inputNama.text;

        if (string.IsNullOrEmpty(nama))
            nama = "Player";

        // 🔥 Cegah error jika leaderboard belum diisi
        if (leaderboardManager != null)
        {
            leaderboardManager.TambahScore(nama, skor);
        }
        else
        {
            Debug.LogWarning("Leaderboard Manager belum diisi!");
        }

        sudahSimpan = true;

        panelInputNama.SetActive(false);
    }
    public void UpdateNyawa()
{
    for (int i = 0; i < hearts.Length; i++)
    {
        if (i < nyawa)
        {
            hearts[i].sprite = heartFull;
        }
        else
        {
            hearts[i].sprite = heartEmpty;
        }
    }
}
public void RestartLevel()
{
    Time.timeScale = 1f;

    SceneManager.LoadScene(
        SceneManager.GetActiveScene().buildIndex
    );
}
}