
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private Character _player;

    private Animator _animator;

    private void Start()
    {
        _player = GetComponent<Character>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        if (Input.GetMouseButtonDown(0))
        {
            _player.Movement.SetJabbing();
        }

        if (Input.GetMouseButtonDown(1))
        {
            Invoke("ResetHitting", 0.3f);
            _animator.SetBool("isHooking", true);
        }

        _player.Movement.SetDirection(new Vector2(x, y));
    }

    private void ResetHitting()
    {
        _animator.SetBool("isJabbing", false);
        _animator.SetBool("isHooking", false);
    }
}
