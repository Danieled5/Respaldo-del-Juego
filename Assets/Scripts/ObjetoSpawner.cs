using System.Collections;
using UnityEngine;

public class ObjetoSpawner : MonoBehaviour
{
    public GameObject objetoPrefab;
    public float intervaloSpawn = 2f;
    public float velocidadCaida = 5f;
    public string sortingLayerName = "Foreground"; // Nombre de la Sorting Layer
    public int orderInLayer = 0; // Orden en la capa

    private void Start()
    {
        // Comienza la corutina para spawnear objetos en intervalos constantes
        StartCoroutine(SpawnObjetos());
    }

    // Corutina para spawnear objetos en intervalos constantes
    private IEnumerator SpawnObjetos()
    {
        // Ciclo infinito para spawnear objetos continuamente
        while (true)
        {
            // Espera el intervalo de spawn antes de crear un nuevo objeto
            yield return new WaitForSeconds(intervaloSpawn);

            // Crea una instancia del objeto en la posición del spawner
            GameObject nuevoObjeto = Instantiate(objetoPrefab, transform.position, Quaternion.identity);

            // Ajusta el Sorting Layer y Order in Layer del SpriteRenderer
            SpriteRenderer sr = nuevoObjeto.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.sortingLayerName = sortingLayerName;
                sr.sortingOrder = orderInLayer;
            }
            else
            {
                Debug.LogWarning("El objeto prefab no tiene un SpriteRenderer adjunto.");
            }

            // Obtén el componente Rigidbody del objeto recién creado y establece su velocidad de caída
            Rigidbody2D rb = nuevoObjeto.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = Vector2.down * velocidadCaida;
            }
            else
            {
                Debug.LogWarning("El objeto prefab no tiene un Rigidbody2D adjunto.");
            }

            // Programa la destrucción del objeto después de cierto tiempo
            Destroy(nuevoObjeto, 6f);
        }
    }
}
