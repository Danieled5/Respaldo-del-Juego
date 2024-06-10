using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Daño : MonoBehaviour
{
    private HP pHealth; // Referencia al componente HP
    public float damage;
    public float tiempoEntreDaño = 2f; // Intervalo de tiempo entre cada aplicación de daño

    public bool playerInside = false; // Indica si el jugador está dentro del área de activación

    // Buscar el componente HP en el GameObject "Butter" al iniciar
    void Start()
    {
        // Intenta encontrar el GameObject llamado "Butter"
        GameObject butter = GameObject.Find("Butter");

        // Si encontramos el GameObject "Butter", intentamos encontrar el componente HP en él
        if (butter != null)
        {
            pHealth = butter.GetComponent<HP>();
        }

        // Si no encontramos el componente HP, mostramos un mensaje de error
        if (pHealth == null)
        {
            Debug.LogError("El componente HP no está adjunto al GameObject 'Butter' o el GameObject 'Butter' no se encontró.");
        }

        if (playerInside)
        {
            InvokeRepeating("ApplyDamage", 0f, tiempoEntreDaño);
        }
    }

    // Aplicar el daño al jugador si está dentro del área de activación
    public void ApplyDamage()
    {
        if (pHealth != null)
        {
            pHealth.health -= damage;
        }
        else
        {
            Debug.LogError("El componente HP no está adjunto al GameObject 'Butter' o el GameObject 'Butter' no se encontró.");
        }
    }

    // Iniciar la repetición del daño cuando el jugador entra en el área de activación
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInside = true;
            InvokeRepeating("ApplyDamage", 0f, tiempoEntreDaño);
        }
    }

    // Detener la repetición del daño si el jugador sale del área de activación
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInside = false;
            CancelInvoke("ApplyDamage");
        }
    }
}
