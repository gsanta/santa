
using UnityEngine;

public class Player : MonoBehaviour
{
    public Movement Movement { get; private set; }

    private void Start()
    {
        Movement = GetComponent<Movement>();
    }
}
