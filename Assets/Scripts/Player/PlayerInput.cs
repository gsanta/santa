
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private Player _player;

    public void Construct(Player player)
    {
        _player = player;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            _player.Movement.HorizontalMovement = -1.0f;
        } 
        else if (Input.GetKeyDown(KeyCode.D))
        {
            _player.Movement.HorizontalMovement = 1.0f;
        } else
        {
            _player.Movement.HorizontalMovement = 0f;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            _player.Movement.VerticalMovement = 1.0f;
        } else if (Input.GetKeyDown(KeyCode.S))
        {
            _player.Movement.VerticalMovement = -1.0f;
        } else
        {
            _player.Movement.VerticalMovement = 0f;
        }
    }
}
