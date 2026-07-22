using UnityEngine;
using System.Collections;

public class DeadZone : MonoBehaviour
{
    public QuizManager quizManager;

    [Header("Respawn")]
    public Transform respawnPoint;

    private bool sudahMati = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !sudahMati)
        {
            sudahMati = true;

            // Kurangi nyawa
            quizManager.nyawa -= 1;

            if (quizManager.nyawa < 0)
                quizManager.nyawa = 0;

            quizManager.UpdateNyawa();

            // Jika nyawa habis
            if (quizManager.nyawa <= 0)
            {
                quizManager.GameOver();
                return;
            }

            // Kurangi skor
            quizManager.skor -= 20;

            if (quizManager.skor < 0)
                quizManager.skor = 0;

            quizManager.UpdateSkor();

            // Disable movement sementara
            PlayerMovement player =
                collision.GetComponent<PlayerMovement>();

            player.enabled = false;

            // Respawn
            StartCoroutine(
                RespawnPlayer(collision.transform, player)
            );
        }
    }

    private IEnumerator RespawnPlayer(
        Transform playerTransform,
        PlayerMovement player
    )
    {
        // Tunggu player jatuh dulu
        yield return new WaitForSeconds(0.5f);

        // Pindah ke spawn
        playerTransform.position = respawnPoint.position;

        // Reset velocity
        Rigidbody2D rb =
            playerTransform.GetComponent<Rigidbody2D>();

        rb.velocity = Vector2.zero;

        // Aktifkan movement lagi
        player.enabled = true;

        // Reset checkpoint terakhir
        if (quizManager.checkpointSekarang != null)
        {
            quizManager.checkpointSekarang.ResetCheckpoint();
        }

        sudahMati = false;
    }
}