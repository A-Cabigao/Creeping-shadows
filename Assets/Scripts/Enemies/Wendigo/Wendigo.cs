using UnityEngine;
using System;
using Panda;

public class Wendigo : MonoBehaviour
{
    public Action rageActivated;

    [Task]
    private void ActivateRage()
    {
        Task.current.Complete(true);
        Debug.Log("rage");
        rageActivated?.Invoke();
    }
}
