using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ayudante : MonoBehaviour
{
    public Transform jugador; 
    public float velocidad = 3f; 
    public float distanciaSeguir = 2f;  
    public float distanciaAtaque = 5f; 
    public float velocidadAtaque = 5f; 
    private Transform enemigoObjetivo; 

    void Update()
    {
        if (enemigoObjetivo == null)
        {
            // Seguir al jugador
            float distanciaJugador = Vector2.Distance(transform.position, jugador.position);
            if (distanciaJugador > distanciaSeguir)
            {
                Vector2 direccion = (jugador.position - transform.position).normalized;
                transform.position = Vector2.MoveTowards(transform.position, jugador.position, velocidad * Time.deltaTime);
            }

            // Buscar enemigos cercanos
            Collider2D[] enemigosCercanos = Physics2D.OverlapCircleAll(transform.position, distanciaAtaque);
            foreach (Collider2D col in enemigosCercanos)
            {
                if (col.CompareTag("Enemy"))
                {
                    enemigoObjetivo = col.transform;
                    break;
                }
            }
        }
        else
        {
            // Perseguir y atacar al enemigo
            Vector2 direccionEnemigo = (enemigoObjetivo.position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, enemigoObjetivo.position, velocidadAtaque * Time.deltaTime);

            // Verificar si ha alcanzado al enemigo
            if (Vector2.Distance(transform.position, enemigoObjetivo.position) < 0.1f)
            {
                Destroy(enemigoObjetivo.gameObject);
                enemigoObjetivo = null; // Resetear el objetivo después de destruir el enemigo
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        // Dibujar el área de detección de enemigos en el editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distanciaAtaque);
    }
}
