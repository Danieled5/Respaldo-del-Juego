using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackDuration = 0.2f; // Duración del ataque
    public Collider2D attackCollider; // Collider para el área de ataque

    void Start()
    {
        // Asegúrate de que el Collider está desactivado al inicio
        attackCollider.enabled = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(PerformAttack());
        }
    }

    IEnumerator PerformAttack()
    {
        // Activar el Collider de ataque
        attackCollider.enabled = true;

        // Esperar durante la duración del ataque
        yield return new WaitForSeconds(attackDuration);

        // Desactivar el Collider de ataque
        attackCollider.enabled = false;
    }
}
