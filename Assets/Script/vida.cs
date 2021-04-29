using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vida : MonoBehaviour
{

    public float valor = 100;
    public Animator CAZombie;

    public Transform TxtFloat;  //Asignarlo en el inspector
    public GameObject TxtFlotante;
    public Transform MiraJugador;
    public float velocidad = 5f;
   
    

    // Aumentado fuera de clase 27 Abril
    

    void Start()
    {
        CAZombie = this.GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //this.transform.LookAt(MiraJugador);
        Vector3 girar =  MiraJugador.transform.position -this.transform.position ;
        Quaternion rotacion = Quaternion.LookRotation(girar);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotacion, velocidad*Time.deltaTime);
    }

 

    public void RecibirDaño(float dano)
    {
        valor -= dano;
        var MiTexto = TxtFlotante.GetComponent<TextMesh>();
        MiTexto.text = valor.ToString();
    
        GameObject textoFloat = Instantiate(TxtFlotante,TxtFloat.position,TxtFloat.rotation) as GameObject;
        
        Destroy(textoFloat,1f);

    


        if (valor <= 0)
        {
            valor = 0;
            CAZombie.CrossFadeInFixedTime("fallingback",0.1f);
            Destroy(this.gameObject,3f);
        }
    }



}

