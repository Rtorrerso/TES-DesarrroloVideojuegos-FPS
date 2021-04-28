using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vida : MonoBehaviour
{

    public float valor = 100;
    public Animator CAZombie;

    public GameObject TXTFLOAT;
    // Start is called before the first frame update
    void Start()
    {
        CAZombie = this.GetComponent<Animator>();
        TXTFLOAT = GameObject.Find("TxtFlotante");
        TXTFLOAT.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RecibirDaño(float dano)
    {
        valor -= dano;
    var MiTexto = TXTFLOAT.GetComponent<TextMesh>();
    MiTexto.text = valor.ToString();
    TXTFLOAT.SetActive(true);


        if (valor <= 0)
        {
            valor = 0;
            CAZombie.CrossFadeInFixedTime("fallingback",0.1f);
            Destroy(this.gameObject,3f);
        }
    }

}
