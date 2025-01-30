using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public AudioSource openSource;
    public AudioSource closeSource;
    bool toggle = false;
    public Animator dooranimation;
    
    [SerializeField] private bool closeTrigger = false;
    public void close() {
        toggle=false;
        dooranimation.ResetTrigger("open");
        dooranimation.SetTrigger("close");
        closeSource.Play();
        
    
    
    }
    public void open() {
        toggle=false;
        dooranimation.ResetTrigger("close");
        dooranimation.SetTrigger("open");
        openSource.Play();
        
    
    
    }
    public void openClose(){
        toggle = !toggle;
        if(toggle == false){
            dooranimation.ResetTrigger("open");
            dooranimation.SetTrigger("close");
        }
        else{
            dooranimation.ResetTrigger("close");
            dooranimation.SetTrigger("open");
        }
    }
    
}
