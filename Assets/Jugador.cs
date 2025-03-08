    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI; // Necesario para manejar UI

    public class Jugador : MonoBehaviour
    {
        public GameManager gameManager;
        public float fuerzaSalto;

        private Rigidbody2D rb;
        private Animator anim;
        private bool puedeSaltar = true;

        public int vidas = 5; 
        public int maxVidas = 5; 
        public Image[] corazones; 
        public Sprite corazonLleno;
        public Sprite corazonVacio;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            ActualizarCorazones(); // Inicializa la UI correctamente
        }

        void Update()
        {
            if (gameManager.start)
            {
                if (Input.GetKeyDown(KeyCode.Space) && puedeSaltar)
                {
                    anim.SetBool("estaSaltando", true);
                    rb.AddForce(new Vector2(0, fuerzaSalto));
                    puedeSaltar = false;
                }
            }

            if (gameManager.gameOver)
            {
                Destroy(gameObject);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Suelo"))
            {
                anim.SetBool("estaSaltando", false);
                puedeSaltar = true;
            }

            if (collision.gameObject.CompareTag("Obstaculo"))
            {
                PerderVida();
            }
        }

        void PerderVida()
        {
            ActualizarVidas(-1); // Resta una vida

            if (vidas <= 0)
            {
                gameManager.gameOver = true;
            }
        }

        // Método para actualizar vidas sumando o restando
        public void ActualizarVidas(int cantidad)
        {
            vidas += cantidad;

            if (vidas > maxVidas) vidas = maxVidas;
            if (vidas < 0) vidas = 0;

            ActualizarCorazones();
        }

        // Método para actualizar la UI de los corazones
        void ActualizarCorazones()
        {
            for (int i = 0; i < corazones.Length; i++)
            {
                if (i < vidas)
                    corazones[i].sprite = corazonLleno;
                else
                    corazones[i].sprite = corazonVacio;
            }
        }
    }
