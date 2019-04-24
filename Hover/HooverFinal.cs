using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HooverFinal : MonoBehaviour {

    public float Aceleration = 400f;
    public float VelocidadMaxima = 600;
    public float Frenado = 500f;
    public float VelocidadGiro = 150f;
    public float AlturaFlotamiento = 4.0f;   
    public float VelocidadAjusteAltura = 999f;
    public float VelocidadajusteNormal = 8f;  
    private Vector3 NormalAnterior;
    public float Angulo;
    private float Ajustealtura;
    private float VelocidadActual;
    public Collider col;
    public int play;

    void FixedUpdate()
    {
        if (Input.GetAxis("Vertical") == -1) 
        {
            VelocidadActual += (VelocidadActual >= VelocidadMaxima) ? 0f : Aceleration * Time.deltaTime / 10;
        }
        else
        {
            if (VelocidadActual > 0)
            {
                VelocidadActual -= Frenado * Time.deltaTime;
            }
            else
            {
                VelocidadActual = 0f;
            }
        }   
        Angulo += VelocidadGiro * Time.deltaTime * Input.GetAxis("Horizontal");
        NormalAnterior = transform.up;
        transform.rotation = Quaternion.Euler(0, Angulo, 0);

        RaycastHit hit;
        if (Physics.Raycast(transform.position, -NormalAnterior, out hit))
        {
            //if (hit.transform.tag == "Escenario") {
                Debug.DrawLine(transform.position, hit.point);
                Vector3 desired_up = Vector3.Lerp(NormalAnterior, hit.normal, Time.deltaTime * VelocidadajusteNormal);
                Quaternion tilt = Quaternion.FromToRotation(transform.up, desired_up);
                transform.rotation = Quaternion.Lerp(transform.rotation, tilt * transform.rotation, 1000);
                //transform.rotation = tilt * transform.rotation;
                Ajustealtura = Mathf.Lerp(Ajustealtura, AlturaFlotamiento - hit.distance, Time.deltaTime * VelocidadAjusteAltura);
                transform.localPosition += NormalAnterior * Ajustealtura;
            //}                
        }
        transform.position += -transform.forward * (VelocidadActual * Time.deltaTime);
    }
}
