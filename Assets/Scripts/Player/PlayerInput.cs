
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
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
    }
}
