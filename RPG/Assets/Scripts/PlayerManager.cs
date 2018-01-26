using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    public GameObject Player;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("More than one instance of PlayerManager fouund");
            return;
        }

        Instance = this;
    }
}
