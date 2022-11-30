using UnityEngine;

public class Character : MonoBehaviour
{

    public GameEntityState[] States { get; private set; }

    private void Start()
    {
        States = GetComponents<GameEntityState>();
    }

    public Vector2 GetPosition()
    {
        return transform.position;
    }
}
