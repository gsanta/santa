using UnityEngine;

public class Character : MonoBehaviour
{
    public GameEntityState[] States { get; private set; }
    public Movement Movement { get; private set; }

    [SerializeField] private bool _isPlayer;

    public Rigidbody2D Rigidbody2D;

    private void Awake()
    {
        States = GetComponents<GameEntityState>();
        Movement = GetComponent<Movement>();
        Rigidbody2D = GetComponent<Rigidbody2D>();
        CharacterStore.GetInstance().AddCharacter(this);
    }

    public bool IsPlayer() {
        return _isPlayer;
    }

    public Vector2 GetPosition()
    {
        return transform.position;
    }
}
