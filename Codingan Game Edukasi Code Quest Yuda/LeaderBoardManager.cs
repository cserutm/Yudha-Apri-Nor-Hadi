using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class LeaderboardManager : MonoBehaviour
{
    [System.Serializable]
    public class DataScore
    {
        public string nama;
        public int skor;
    }

    [System.Serializable]
    public class ListScore
    {
        public List<DataScore> scores =
            new List<DataScore>();
    }

    [Header("UI")]
    public TMP_Text textLeaderboard;

    private ListScore daftarScore =
        new ListScore();

    private void Start()
    {
        LoadScore();

        TampilkanLeaderboard();
    }

    // TAMBAH SCORE BARU
    public void TambahScore(string nama, int skor)
    {
        DataScore dataBaru = new DataScore();

        dataBaru.nama = nama;
        dataBaru.skor = skor;

        daftarScore.scores.Add(dataBaru);

        // URUTKAN DARI SKOR TERBESAR
        daftarScore.scores =
            daftarScore.scores
            .OrderByDescending(x => x.skor)
            .ToList();

        // BATASI TOP 10
        if (daftarScore.scores.Count > 10)
        {
            daftarScore.scores.RemoveAt(10);
        }

        SaveScore();

        TampilkanLeaderboard();
    }

    // TAMPILKAN LEADERBOARD
    void TampilkanLeaderboard()
{
    textLeaderboard.text =
    textLeaderboard.text +=
        "NO     NAMA        SKOR\n";

    for (int i = 0; i < daftarScore.scores.Count; i++)
    {
        string nomor =
            (i + 1).ToString().PadRight(10);

        string nama =
            daftarScore.scores[i]
            .nama
            .PadRight(10);

        string skor =
            daftarScore.scores[i]
            .skor
            .ToString();

        textLeaderboard.text +=
            nomor + nama + skor + "\n";
    }
}

    // SAVE DATA
    void SaveScore()
    {
        string json =
            JsonUtility.ToJson(daftarScore);

        PlayerPrefs.SetString(
            "Leaderboard",
            json
        );

        PlayerPrefs.Save();
    }

    // LOAD DATA
    void LoadScore()
    {
        if (PlayerPrefs.HasKey("Leaderboard"))
        {
            string json =
                PlayerPrefs.GetString("Leaderboard");

            daftarScore =
                JsonUtility.FromJson<ListScore>(json);
        }
    }
}