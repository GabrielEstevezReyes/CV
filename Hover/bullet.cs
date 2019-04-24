using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour {
    public float Aceleration = 10f;
    public float VelocidadMaxima = 100f;
    public float Frenado = 500f;
    public float VelocidadGiro = 150f;
    public float AlturaFlotamiento = 1.8f;
    public float VelocidadAjusteAltura = 999f;
    public float VelocidadajusteNormal = 100f;
    private Vector3 NormalAnterior;
    public float Angulo;
    private float Ajustealtura;
    private float VelocidadActual;

    void FixedUpdate()
    {
        VelocidadActual += (VelocidadActual >= VelocidadMaxima) ? 0f : Aceleration * Time.deltaTime;
        NormalAnterior = transform.up;
        transform.rotation = Quaternion.Euler(0, Angulo, 0);

        RaycastHit hit;
        if (Physics.Raycast(transform.position, -NormalAnterior, out hit))
        {
            Vector3 desired_up = Vector3.Lerp(NormalAnterior, hit.normal, Time.deltaTime * VelocidadajusteNormal);
            Quaternion tilt = Quaternion.FromToRotation(transform.up, desired_up);
            transform.rotation = tilt * transform.rotation;
            Ajustealtura = Mathf.Lerp(Ajustealtura, AlturaFlotamiento - hit.distance, Time.deltaTime * VelocidadAjusteAltura);
            transform.localPosition += NormalAnterior * Ajustealtura;              
        }
        transform.position += transform.forward * (VelocidadActual * Time.deltaTime);
    }
}
