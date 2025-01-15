using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Neonlightstarttrigger : MonoBehaviour
{
    [SerializeField] private GameObject Light1;
    [SerializeField] private GameObject Light2;
    [SerializeField] private GameObject Light3;
    [SerializeField] private Material lightsOffMaterial; //da ne svijetle svjetla kad se ugase
    private Animator lightAnimator;
    private Animator light2Animator;
    private Animator SurgeryAnimator;
    private MeshRenderer lightsOffRenderer;
    private MeshRenderer lightsOffRenderer2;
    
    private void Start(){

        lightAnimator = Light1.transform.Find("AnimatedLight").GetComponent<Animator>();
        light2Animator = Light2.transform.Find("AnimatedLight").GetComponent<Animator>();
        lightsOffRenderer = Light1.GetComponent<MeshRenderer>();
        lightsOffRenderer2 = Light2.GetComponent<MeshRenderer>();
        SurgeryAnimator = Light3.GetComponent<Animator>();


    }
    private void OnTriggerEnter(Collider other){
        Debug.Log("usao si u triger");
        if (other.CompareTag("player")){
        lightAnimator.SetTrigger("flicker");
        light2Animator.SetTrigger("flicker2");
        StartCoroutine(ChangeMaterialWithDelay(lightsOffRenderer,lightsOffMaterial, 1.5f));
        StartCoroutine(ChangeMaterialWithDelay(lightsOffRenderer2, lightsOffMaterial, 3f));
        StartCoroutine(StartSurgeryAnimation(SurgeryAnimator, 3f));
        
        

        }
    }
    //korutina za promjenu materijala s odgodom
    private IEnumerator ChangeMaterialWithDelay(Renderer renderer, Material material, float delayTime)
    {
        //ceka se delay
        yield return new WaitForSeconds(delayTime);

        //promjena materijala nakon odgode
        renderer.material = material;
    }
    private IEnumerator StartSurgeryAnimation(Animator SurgeryAnimator, float delayTime){
        yield return new WaitForSeconds(delayTime);
        SurgeryAnimator.SetTrigger("lightoff"); //animacija za svjetlo iznad operacijske fotelje
    }
}
