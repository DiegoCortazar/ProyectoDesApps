using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    // Start is called before the first frame update

    public Text contadorTime;
    private float tiempo = 0f;
    private Rigidbody rb;
    public float speed;
    private Vector3 position;
    private Vector3 posicion;
    private float finalTime;
    public GameObject cuboMovible;

    public Transform particulas;
    private ParticleSystem sistemaParticulas;
    private AudioSource audioRecoleccion;

    // public Text textoContador;
    public Text textoGanaste;

    private int cubes = 16;
    private int contador = 0;

    private int puntaje = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sistemaParticulas = particulas.GetComponent<ParticleSystem>();
        sistemaParticulas.Stop();
        audioRecoleccion = GetComponent<AudioSource>();
        contadorTime.text = " " + tiempo;

        // textoContador.text = "Contador: " + contador.ToString();
        textoGanaste.text = "";
        // StartCoroutine("Movimiento");
    }

    // Update is called once per frame
    void Update()
    {

        tiempo += Time.deltaTime;
        contadorTime.text = " " + tiempo.ToString("f0");
        if (cubes == 15)
        {
            //SceneManager.LoadScene(1);
            textoGanaste.text = "Nivel Finalizado En: "+finalTime.ToString("f0") + " Segundos";
        }
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(movement * speed);
    }

    public IEnumerator DetenerParticulas(ParticleSystem part)
    {
        yield return new WaitForSecondsRealtime(2);
        part.Stop();
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Recolectable"))
        {
            //other.gameObject.SetActive(false);
            posicion = other.gameObject.transform.position;
            particulas.position = posicion;
            sistemaParticulas.Play();
            cubes -= 1;
            contador = contador + 1;

            puntaje = puntaje + 5;
            audioRecoleccion.Play();

            Debug.Log("Tiempo Ant" + tiempo);
            Debug.Log("2 Segundos Descontados");

            tiempo = tiempo - 2;
            Debug.Log("Tiempo Nuevo: " + tiempo);

            Destroy(other.gameObject);
            StartCoroutine(DetenerParticulas(sistemaParticulas));

            if (cubes == 15)
            {
                finalTime = tiempo;
            }


            Debug.Log("Cubos Recolectados: " + contador);
            Debug.Log("5 Puntos por Objeto Bueno");
            Debug.Log("Puntaje: " + puntaje);


        }
        if (other.gameObject.CompareTag("BadRecolectable"))
        {
            puntaje = puntaje - 3;
            Debug.Log("3 Puntos Menos por Objeto MALO");
            Destroy(other.gameObject);
            Debug.Log("Puntaje: " + puntaje);
        }
        else
        {

        }
    }
}