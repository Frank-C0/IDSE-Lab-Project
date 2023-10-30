using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private float fuerza_propulcion = 200f;
    private float fuerza_giro = 20f;
    
    private Transform transfomC;
    private const float velocidadRotacion = 0.3f;

    private AudioSource audioSource;
    private ParticleSystem[] propulsionParticlesSystems;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        transfomC = GetComponent<Transform>();
        audioSource = GetComponent<AudioSource>();

        propulsionParticlesSystems = GetComponentsInChildren<ParticleSystem>();
    }
    private bool propulsando = false;
    private bool girandoIzquierda = false;
    private bool girandoDerecha = false;

    void Update()
    {
        propulsando = Input.GetKey(KeyCode.Space);
        girandoIzquierda = Input.GetKey(KeyCode.A);
        girandoDerecha = Input.GetKey(KeyCode.D);

        if (propulsando)
        {
            Propulsar();           
        }
        else
        {
            DejarDePropulsar();
        }

        if (girandoIzquierda)
            GirarIzquierda();
        else if (girandoDerecha)
            GirarDerecha();   
    }
    private void DejarDePropulsar()
    {
        audioSource.Stop();
        StopAllPropulsionParticles();
    }
    private void Propulsar()
    {
        print("Tecla Space - Propulsando");
        rb.AddRelativeForce(    
            Vector3.up * fuerza_propulcion * Time.deltaTime, 
            ForceMode.Impulse
            );
        if (!audioSource.isPlaying)
            audioSource.Play();
        PlayAllPropulsionParticles();
    }
    private void GirarIzquierda()
    {
        print("Tecla A - Girando a la izquierda");
        rb.AddRelativeTorque(
            Vector3.forward * fuerza_giro * Time.deltaTime, 
            ForceMode.Impulse
        );
    }
    private void GirarDerecha()
    {
        print("Tecla D - Girando a la derecha");
        rb.AddRelativeTorque(
            Vector3.back * fuerza_giro * Time.deltaTime, 
            ForceMode.Impulse
        );
    }

    private void GirarIzquierdaTransform()
    {
        print("Tecla A - Girando a la izquierda");
        var rotarIzquierda = transfomC.rotation;
        rotarIzquierda.z += velocidadRotacion * Time.deltaTime;
        transfomC.rotation = rotarIzquierda;
    }
    private void GirarDerechaTransform()
    {

        print("Tecla D - Girando a la derecha");
        var rotarDerecha = transfomC.rotation;
        rotarDerecha.z -= velocidadRotacion * Time.deltaTime;
        transfomC.rotation = rotarDerecha;
    }

    private void StopAllPropulsionParticles()
    {
        foreach (var particleSystem in propulsionParticlesSystems)
        {
            particleSystem.Stop();
        }
    }
    private void PlayAllPropulsionParticles()
    {
        foreach (var particleSystem in propulsionParticlesSystems)
        {
            if (!particleSystem.isPlaying)
            {
                particleSystem.Play();
            }
        }
    }
}
