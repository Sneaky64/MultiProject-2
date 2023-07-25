using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Key : MonoBehaviour, ICollectable
{
    public void Collect()
    {
        Open();
        Destroy(gameObject);
    }
    [SerializeField] Animator animator;
    private void Open()
    {
        animator.SetTrigger("open");
    }
}
