using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;  // Importar TextMeshPro

public class GameManager : MonoBehaviour
{
    public float velocidad = 2;
    public Renderer bg;
    public GameObject col1;
    public GameObject piedra1;
    public GameObject piedra2;
    public GameObject moneda;
    public GameObject gema;
    public GameObject potion;

    public bool start = false;
    public bool gameOver = false;
    private int puntaje = 0;
    private int contadorMonedas = 0;
    private int recordPuntos = 0;

    public GameObject menuInicio;
    public GameObject menuGameOver;

    public List<GameObject> suelo;
    public List<GameObject> obstaculos;
    public List<GameObject> monedas = new List<GameObject>();
    public List<GameObject> gemas = new List<GameObject>();
    public List<GameObject> potions = new List<GameObject>();

    public TMP_Text marcadorPuntos; // UI del marcador de puntos
     public TMP_Text recordText; // UI del marcador de record

    public Jugador jugador;

    private void Start()
    {
        recordPuntos = PlayerPrefs.GetInt("Record", 0); // Cargar récord guardado
        // Crear Mapa
        for (int i = 0; i < 21; i++)
        {
            suelo.Add(Instantiate(col1, new Vector2(-10 + i, -3), Quaternion.identity));
        }

        // Crear Obstáculos
        obstaculos.Add(Instantiate(piedra1, new Vector2(15, -2), Quaternion.identity));
        obstaculos.Add(Instantiate(piedra2, new Vector2(20, -2), Quaternion.identity));

        // Generar monedas
        for (int i = 0; i < 5; i++)
        {
            SpawnMoneda();
        }

        // Inicializar marcador en la UI
        ActualizarMarcador();
    }

    private void Update()
    {
        if (!start && !gameOver)
        {
            menuInicio.SetActive(true);
            if (Input.GetKeyDown(KeyCode.X))
            {
                start = true;
            }
        }
        else if (gameOver)
        {
            menuGameOver.SetActive(true);
              
            int record = PlayerPrefs.GetInt("Record", 0);

               if (puntaje > recordPuntos)
        {
            recordPuntos = puntaje;
            PlayerPrefs.SetInt("Record", recordPuntos);
        }

        recordText.text = "Récord: " + record.ToString();

            if (Input.GetKeyDown(KeyCode.X))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
        else
        {
            menuInicio.SetActive(false);
            menuGameOver.SetActive(false);

            // Mover BG
            bg.material.mainTextureOffset += new Vector2(0.015f, 0) * velocidad * Time.deltaTime;

            // Mover Mapa
            foreach (GameObject sueloObj in suelo)
            {
                if (sueloObj.transform.position.x <= -10)
                {
                    sueloObj.transform.position = new Vector3(10f, -3, 0);
                }
                sueloObj.transform.position += new Vector3(-1, 0, 0) * velocidad * Time.deltaTime;
            }

            // Mover Obstáculos
            foreach (GameObject obstaculo in obstaculos)
            {
                if (obstaculo.transform.position.x <= -10)
                {
                    float randomObs = Random.Range(10, 18);
                    obstaculo.transform.position = new Vector3(randomObs, -2, 0);
                }
                obstaculo.transform.position += new Vector3(-1, 0, 0) * velocidad * Time.deltaTime;
            }

            // Mover monedas
            foreach (GameObject moneda in monedas)
            {
                if (moneda.transform.position.x <= -10)
                {
                    float newX = Random.Range(10, 18);
                    float newY = Random.Range(-1.5f, 0f);
                    moneda.transform.position = new Vector3(newX, newY, 0);
                }
                moneda.transform.position += new Vector3(-1, 0, 0) * velocidad * Time.deltaTime;
            }

            // Mover gemas
            foreach (GameObject gema in gemas)
            {
                if (gema.transform.position.x <= -10)
                {
                    float newX = Random.Range(10, 18);
                    float newY = Random.Range(-1.5f, 0f);
                    gema.transform.position = new Vector3(newX, newY, 0);
                }
                gema.transform.position += new Vector3(-1, 0, 0) * velocidad * Time.deltaTime;
            }

             // Mover potions
            foreach (GameObject potion in potions)
            {
                if (potion.transform.position.x <= -10)
                {
                    float newX = Random.Range(10, 18);
                    float newY = Random.Range(-1.5f, 0f);
                    potion.transform.position = new Vector3(newX, newY, 0);
                }
                potion.transform.position += new Vector3(-1, 0, 0) * velocidad * Time.deltaTime;
            }
        }
    }

    // Generar monedas en posiciones aleatorias
    void SpawnMoneda()
    {
        float randomX = Random.Range(10, 18);
        float randomY = Random.Range(-1.5f, 0f);

        GameObject nuevaMoneda = Instantiate(moneda, new Vector2(randomX, randomY), Quaternion.identity);
        monedas.Add(nuevaMoneda);

    }

    // Generar gema en una posición aleatoria
    void SpawnGema()
    {
        float randomX = Random.Range(10, 18);
        float randomY = Random.Range(-1.5f, 0f);

        GameObject nuevaGema = Instantiate(gema, new Vector2(randomX, randomY), Quaternion.identity);
        gemas.Add(nuevaGema);

    }

        // Generar gema en una posición aleatoria
    void SpawnPotion()
    {
        float randomX = Random.Range(10, 18);
        float randomY = Random.Range(-1.5f, 0f);

   GameObject nuevaPotion = Instantiate(potion, new Vector2(randomX, randomY), Quaternion.identity);
        potions.Add(nuevaPotion);
    }

    // Método para actualizar la UI del marcador de puntos
    void ActualizarMarcador()
    {
        if (marcadorPuntos != null)
        {
            marcadorPuntos.text = "Puntos: " + puntaje.ToString();
        }
    }

    // Método para detectar colisiones con monedas
   public void RecogerMoneda(GameObject monedaRecogida)
    {
        puntaje += 1;
        contadorMonedas += 1;
        monedas.Remove(monedaRecogida);
        Destroy(monedaRecogida);
        ActualizarMarcador();
        SpawnMoneda();

        // Cada 10 monedas recogidas, genera una gema
        if (contadorMonedas % 10 == 0)
        {
            SpawnGema();
        }

        // Cada 20 monedas recogidas, genera una poción
        if (contadorMonedas % 20 == 0)
        {
            SpawnPotion();
        }

    }

    // Método para detectar colisiones con gemas
    public void RecogerGema(GameObject gemaRecogida)
    {
        puntaje += 100; // Aumentar puntos en 100
        gemas.Remove(gemaRecogida);
        Destroy(gemaRecogida);
        ActualizarMarcador();
    }

      public void RecogerPocion(GameObject pocionRecogida)
    {
        potions.Remove(pocionRecogida);
        Destroy(pocionRecogida);
        jugador.ActualizarVidas(1);
    }
}
