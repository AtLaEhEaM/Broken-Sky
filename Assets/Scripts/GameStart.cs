using UnityEngine;

public class GameStart : MonoBehaviour
{
    public GameObject Player;
    public Transform StartingPos;
    //private CharacterController characterController;

    void Awake()
    {
        //characterController = Player.GetComponent<CharacterController>();
        //characterController.enabled = false;
        Player.transform.position = StartingPos.position;
        //characterController.enabled = true;
    }
}
