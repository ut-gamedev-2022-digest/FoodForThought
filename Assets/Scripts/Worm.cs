using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worm : MonoBehaviour
{
    public bool Fly = true;
    private Animator animator;

    private void Awake()
    {
        Fly = true;
        animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        if (animator != null)
        {
            if (Fly) {
                animator.SetTrigger("Fly");
            }
            else
            {
                animator.SetTrigger("Walk");
            }
        }
    }

}
