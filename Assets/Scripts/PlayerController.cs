using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float fuerza_propulcion = 2000f;
    private float fuerza_giro = 100f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
            Propulsar();

        if (girandoIzquierda)
            GirarIzquierda();
        else if (girandoDerecha)
            GirarDerecha();   
    }
    private void Propulsar()
    {
        print("Propulsando - Tecla Space");

        rb.AddForce(
            transform.up * fuerza_propulcion * Time.deltaTime, 
            ForceMode.Impulse
            );
    }
    private void GirarIzquierda()
    {
        print("Girando a la izquierda - Tecla A");
        rb.AddTorque(
            Vector3.forward * fuerza_giro * Time.deltaTime, 
            ForceMode.Impulse
        );
    }
    private void GirarDerecha()
    {
        print("Girando a la derecha - Tecla D");
        rb.AddTorque(
            Vector3.back * fuerza_giro * Time.deltaTime, 
            ForceMode.Impulse
        );
    }
}
