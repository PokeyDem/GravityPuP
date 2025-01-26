using UnityEngine;

public class SlimeAnimatonController : MonoBehaviour
{
    private Animator animator;
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetCollisionSide() {
        animator.SetTrigger("CollisionSide");
    }

    public void SetCollisionUp() {
        animator.SetTrigger("CollisionUp");
    }
}
