
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private Player _player;

    private void Start()
    {
        _player = GetComponent<Player>();
    }

    private void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        _player.Movement.SetDirection(new Vector2(x, y));
    }
}
