using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class Interactable : MonoBehaviour
{
    public UnityEvent OnInteract;

    public bool Interact(){
        OnInteract.Invoke();
        return true;
    }

    public void DestroySelf(){
        Destroy(gameObject);
    }
}
