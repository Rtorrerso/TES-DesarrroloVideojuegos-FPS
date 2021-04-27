using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicaDeArma : MonoBehaviour
{
    [Header("Generales")]
    public AudioSource audiosource;
    public Animator animator;
    

    [Header("Sonidos")]
    public AudioClip SonDisparo;
    public AudioClip SonCarga;
    public AudioClip SonSinBalas;
    public AudioClip SonCartuchoSale;
    public AudioClip SonCartuchoEntra;


    [Header("Valores Iniciales")]
    // balas en el arma
    public int BalasEnCartucho;
    // max balas que tiene la mochila
    public int MaxBalas = 100;
    // max balas en el alimentador
    public int TamanoCartucho = 12;

    // cuantas balas quedan en la mochila
    public int BalasRestantes;
    public float RitmoDeDisparo = 0.3f;
    public bool  TiempoNoDisparo = false;
    public bool PuedeDisparar = false;
    public bool recargando = false;



    // Start is called before the first frame update
    void Start()
    {
        audiosource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();

        BalasEnCartucho = TamanoCartucho;
        BalasRestantes = MaxBalas;

        Invoke("HabilitarArma",0.3f);
        
    }

    void HabilitarArma()
    {
        PuedeDisparar = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Fire1"))
        {
            RevisarDisparo();
            
        }

        if(Input.GetButtonDown("Reload"))
        {
            RevisarRecarga();
            animator.CrossFadeInFixedTime("Reload",0.1f);
        }
    }

    void RevisarDisparo()
    {
        if(!PuedeDisparar) return;
        if(TiempoNoDisparo) return;
        //if(recargando) return;
        if(BalasEnCartucho > 0)
        {
            Disparar();
        }
        else
        {
            SinBalas();
        }

    }

    void Disparar()
    {
            TiempoNoDisparo = true;
            ReproducirAnimacionDisparo();
            BalasEnCartucho--;
            StartCoroutine(RevisarTiempoNoDisparo());
        
    }

    IEnumerator RevisarTiempoNoDisparo()
    {
        yield return new WaitForSeconds(RitmoDeDisparo);
        TiempoNoDisparo = false;
    }

    void SinBalas()
    {
        audiosource.PlayOneShot(SonSinBalas);
        TiempoNoDisparo = true;
        StartCoroutine(RevisarTiempoNoDisparo());
    }

    void RevisarRecarga()
    {
        if(BalasRestantes >0 && BalasEnCartucho < TamanoCartucho)
        {
            Recargar();
        }
    }

    void Recargar()
    {
        if(recargando) return;
        recargando = true;

        int balasParaRecargar = TamanoCartucho - BalasEnCartucho;
        int restarBalas = (BalasRestantes >= balasParaRecargar) ? balasParaRecargar : BalasRestantes;
        
        //balas restantes restar la variable restarlas
        BalasRestantes -= restarBalas;
        BalasEnCartucho += balasParaRecargar;
        recargando = false;
    }

public virtual void ReproducirAnimacionDisparo()
{
        audiosource.PlayOneShot(SonDisparo);
        animator.CrossFadeInFixedTime("Fire", 0.1f);
        
}


   void SonCargaOn()
   {
       audiosource.PlayOneShot(SonCarga);
   }

    void CartuchoSaleOn()
    {
        audiosource.PlayOneShot(SonCartuchoSale);
    }

    void CartuchoEntraOn()
    {
        audiosource.PlayOneShot(SonCartuchoEntra);
    }

}
