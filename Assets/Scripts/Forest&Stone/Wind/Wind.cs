using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    private bool _isTrigger = true;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>(); 
    }

    void Update()
    {   if(_isTrigger)
        StartCoroutine(WaitAnim());
    }

    public IEnumerator WaitAnim()
    {
        _isTrigger = false;
        int randomWait = Random.Range(5, 10);
        yield return new WaitForSeconds(randomWait);
        animator.Play("Armature|Wind",0, 0.25f);
        _isTrigger = true;
    }
}
