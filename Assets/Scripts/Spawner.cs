using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Prefab del enemigo a spawnear
    public float spawnInterval = 2f; // Intervalo de tiempo entre cada spawn
    public int maxEnemies = 5; // Número máximo de enemigos en pantalla
    public Transform player; // Referencia al jugador para que los enemigos sepan hacia dónde ir

    private int currentEnemies = 0;
    private float timer = 0f;

    void Update()
    {
        // Si todavía no hemos alcanzado el máximo de enemigos y ha pasado el intervalo de spawn
        if (currentEnemies < maxEnemies && Time.time - timer > spawnInterval)
        {
            SpawnEnemy(); // Llamamos a la función para spawnear un enemigo
            timer = Time.time; // Reiniciamos el timer
        }
    }

    void SpawnEnemy()
    {
        // Creamos una instancia del enemigo en la posición del spawner
        GameObject newEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        // Configuramos la referencia al jugador para que el enemigo sepa hacia dónde ir
        AI enemyScript = newEnemy.GetComponent<AI>();
        if (enemyScript != null)
        {
            enemyScript.SetPlayer(player.gameObject);
        }
        currentEnemies++; // Incrementamos el contador de enemigos
    }

    // Método para reducir el contador de enemigos cuando uno es derrotado
    public void EnemyDefeated()
    {
        currentEnemies--;
    }
}
