using UnityEngine;

public static class LevelData
{
    // =========================
    // SIMPAN SKOR SOAL
    // =========================
    public static void SaveScore(string idSoal, int score)
    {
        PlayerPrefs.SetInt(idSoal + "_score", score);
        PlayerPrefs.SetInt(idSoal + "_done", 1);

        PlayerPrefs.Save();
    }

    // =========================
    // AMBIL SKOR SOAL
    // =========================
    public static int GetScore(string idSoal)
    {
        return PlayerPrefs.GetInt(idSoal + "_score", 0);
    }

    // =========================
    // CEK SOAL SUDAH DIKERJAKAN
    // =========================
    public static bool IsDone(string idSoal)
    {
        return PlayerPrefs.GetInt(idSoal + "_done", 0) == 1;
    }

    // =========================
    // TOTAL SKOR PER LEVEL
    // =========================
    public static int GetTotalScore(string prefix, int jumlahSoal)
    {
        int total = 0;

        for (int i = 1; i <= jumlahSoal; i++)
        {
            total += GetScore(prefix + "_" + i);
        }

        return total;
    }
}