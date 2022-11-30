using Unity.VisualScripting;
using UnityEngine;

public class PlayerFactory : MonoBehaviour
{
    [SerializeField] Player playerPrefab;

    [SerializeField] Transform container;

    private void Start()
    {
        //Create();
    }

    public Player Create()
    {
        Player player = Instantiate(playerPrefab, container);

        player.AddComponent<Movement>();
        
        var playerInput = player.AddComponent<PlayerInput>();

        return player;
    }
}
