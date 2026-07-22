using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuLatihan : MonoBehaviour
{
    // =========================
    // LATIHAN ALGORITMA
    // =========================
    public void BukaLatihanAlgoritma()
    {
        SceneManager.LoadScene("Lthn_susu");
    }

    // =========================
    // LATIHAN VARIABEL
    // =========================
    public void BukaLatihanVariabel()
    {
        SceneManager.LoadScene("Lthn2_namakota");
    }

    // =========================
    // LATIHAN OPERATOR
    // =========================
    public void BukaLatihanOperator()
    {
        SceneManager.LoadScene("Lthn4_jumlahdata");
    }

    // =========================
    // LATIHAN TIPE DATA
    // =========================
    public void BukaLatihanTipeData()
    {
        SceneManager.LoadScene("Lthn3_dataangka");
    }

    // =========================
    // LATIHAN PERCABANGAN
    // =========================
    public void BukaLatihanPercabangan()
    {
        SceneManager.LoadScene("Lthn5_logadmin");
    }

    // =========================
    // LATIHAN PERULANGAN
    // =========================
    public void BukaLatihanPerulangan()
    {
        SceneManager.LoadScene("Lthn6_refweb");
    }
}
