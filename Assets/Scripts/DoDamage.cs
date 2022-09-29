using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoDamage : MonoBehaviour
{
    private int damageAmount = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDamageAmount(int damageAmount){
        this.damageAmount = damageAmount;
    }

    void OnTriggerEnter2D(Collider2D other){
        if(gameObject.tag != other.gameObject.tag)
            Debug.Log("Eu sou o " + gameObject.name + " e causo " 
                + this.damageAmount + " de dano no " 
                + other.gameObject.name + ".");
    }
}
