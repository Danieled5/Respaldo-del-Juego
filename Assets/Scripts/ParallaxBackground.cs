using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField] private Transform[] backgrounds; // Arreglo que contiene las transformaciones de las capas de fondo
    [SerializeField] private float[] parallaxScales; // Escala de parallax para cada capa de fondo
    [SerializeField] private float smoothing = 1f; // Suavizado del efecto de parallax
    private Transform camTransform; // Transform de la cámara
    private Vector3 previousCamPos; // Posición anterior de la cámara

    private void Awake()
    {
        camTransform = Camera.main.transform; // Obtiene el transform de la cámara principal
    }

    private void Start()
    {
        previousCamPos = camTransform.position; // Guarda la posición inicial de la cámara
    }

    private void Update()
    {
        // Para cada capa de fondo
        for (int i = 0; i < backgrounds.Length; i++)
        {
            // Calcula el parallax entre la posición actual de la cámara y la posición anterior
            float parallax = (previousCamPos.x - camTransform.position.x) * parallaxScales[i];

            // Calcula la posición objetivo para la capa de fondo
            float backgroundTargetPosX = backgrounds[i].position.x + parallax;

            // Crea una posición objetivo basada en la posición actual de la capa de fondo y la posición objetivo en el eje x
            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

            // Interpola suavemente hacia la posición objetivo
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
        }

        // Actualiza la posición anterior de la cámara al final del frame
        previousCamPos = camTransform.position;
    }
}
    