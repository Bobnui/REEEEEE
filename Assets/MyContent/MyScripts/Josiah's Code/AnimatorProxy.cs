using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used to control animation controllers.
/// </summary>
[RequireComponent(typeof(Animator))]
public class AnimatorProxy : MonoBehaviour
{

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetBoolTrue(string boolName)
    {
        animator.SetBool(boolName, true);
    }

    public void SetBoolFalse(string boolName)
    {
        animator.SetBool(boolName, false);
    }

    public void SetTrigger(string triggerName)
    {
        animator.SetTrigger(triggerName);
    }
    public void SetFloat(string name, float value)
    {
        animator.SetFloat(name, value);
    }
    public void PlayFootstep()
    {
        SFXManager.Instance.PlayClip("footstep", transform, 1, false);
    }
}
