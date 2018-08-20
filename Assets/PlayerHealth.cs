using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//use the user interface text box

public class PlayerHealth : MonoBehaviour {

    [SerializeField] int health = 10;
    [SerializeField] int healthDecrease = 1;
    [SerializeField] Text healthText;

    private void Start(){
        healthText.text = health.ToString();
    }

    private void Update(){
        healthText.text = health.ToString();
    }

    private void OnTriggerEnter(Collider other){
        health = health - healthDecrease;
    }



}
