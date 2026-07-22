using UnityEngine;

public class checkpoint : MonoBehaviour
{
    public QuizManager quizManager;

    private bool sudahAktif = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !sudahAktif)
        {
            sudahAktif = true;

            quizManager.checkpointSekarang = this;

            quizManager.TampilkanSoal();
        }
    }

    public void ResetCheckpoint()
    {
        sudahAktif = false;
    }
}