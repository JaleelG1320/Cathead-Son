using System.Collections.Generic;
using UnityEngine;
using System.Linq;
[System.Serializable]
public abstract class Interactable : MonoBehaviour
{
    public static List<Interactable> interactables = new List<Interactable>();
    public abstract void Interact();

}
