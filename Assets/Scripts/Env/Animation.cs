using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour
{
    public string animationName;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();

        animator.Play(animationName);
    }
}
