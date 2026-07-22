using UnityEngine;

public class exit : MonoBehaviour
{
    // Fungsi untuk keluar game
    public void KeluarGame()
    {
        Debug.Log("Game Ditutup");

        // Jika build game
        Application.Quit();

        // Jika dijalankan di Unity Editor
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}