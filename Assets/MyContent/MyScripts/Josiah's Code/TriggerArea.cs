using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class TriggerArea : MonoBehaviour
{
    [Header("Happens when anything with a rigidbody2d enters this trigger")]
    public UnityEvent OnObjectEnteredArea = new UnityEvent();

    [Header("Happens when an object with the player tag enters this area")]
    public UnityEvent OnPlayerEnteredArea = new UnityEvent();

    [Header("Happens when anything with a rigidbody2d leaves this trigger")]
    public UnityEvent OnObjectLeftArea = new UnityEvent();

    [Header("Happens when an obect with the player tag leaves this trigger")]
    public UnityEvent OnPlayerLeftArea = new UnityEvent();


    public void OnTriggerEnter2D(Collider2D collision)
    {
        OnObjectEnteredArea?.Invoke();
        //Debug.Log("Object entered");
        if (collision.tag == "Player")
        {
            OnPlayerEnteredArea?.Invoke();
            //Debug.Log("Player entered");
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        OnObjectLeftArea?.Invoke();
        //Debug.Log("Object left");
        if (collision.tag == "Player")
        {
            OnPlayerLeftArea?.Invoke();
            //Debug.Log("Player left");
        }
    }
}
