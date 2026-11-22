using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Gestor del nivel: lista dinámica de enemigos activos, sonidos globales y Game Over + reinicio.
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private GameObject panelDerrota;
    [SerializeField] private float esperaReinicio = 2f;
    [SerializeField] private AudioClip sonidoDerrota;

    private readonly List<Enemigo> enemigosActivos = new List<Enemigo>();
    private AudioSource audioSource;
    private bool partidaTerminada;

    public int EnemigosRestantes => enemigosActivos.Count;
    public event Action<int> EnemigosCambiaron;

    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
        if (panelDerrota != null) panelDerrota.SetActive(false);
    }

    private void OnDestroy()
    {
        if (Instance == this) Instance = null;
    }

    public void RegistrarEnemigo(Enemigo enemigo)
    {
        if (enemigo == null || enemigosActivos.Contains(enemigo)) return;
        enemigosActivos.Add(enemigo);
        EnemigosCambiaron?.Invoke(enemigosActivos.Count);
    }

    public void QuitarEnemigo(Enemigo enemigo)
    {
        if (enemigosActivos.Remove(enemigo))
            EnemigosCambiaron?.Invoke(enemigosActivos.Count);
    }

    public void GameOver()
    {
        if (partidaTerminada) return;
        partidaTerminada = true;

        if (panelDerrota != null) panelDerrota.SetActive(true);
        Sonar(sonidoDerrota);
        StartCoroutine(ReiniciarNivel());
    }

    private IEnumerator ReiniciarNivel()
    {
        yield return new WaitForSeconds(esperaReinicio);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Sonidos de objetos que se destruyen (rayo, enemigo): si el AudioSource estuviera
    // en el objeto destruido, el sonido se cortaría. Por eso suenan desde acá.
    public static void Sonar(AudioClip clip)
    {
        if (clip == null) return;

        if (Instance != null && Instance.audioSource != null)
            Instance.audioSource.PlayOneShot(clip);
        else if (Camera.main != null)
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
    }
}
