using UnityEngine;
using Panda;

public class Mogall : MonoBehaviour
{
    public bool isRageOn { get; private set; }

    private void Start()
    {
        isRageOn = false;
    }

    [Task]
    private void ActivateRage()
    {
        isRageOn = true;
        Task.current.Complete(true);
    }
}
