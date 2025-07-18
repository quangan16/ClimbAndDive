using UnityEngine;


public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    public float climbCoin;
    public float coinCollectRate = 0.7f;

    public PlayerController player;
    public Ladder ladder;
    public CameraController cameraController;

    public void Start()
    {
        SetMaxFPS(60);
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }


    public void SetMaxFPS(int maxFPS)
    {
        Application.targetFrameRate = maxFPS;
    }

    public void Update()
    {
        CalculateCoin();
    }

    public void CalculateCoin()
    {
        if (player.IsClimbing)
        {
            climbCoin = (int)player.CurrentHeightToGround * coinCollectRate;
        }

    }

    public void CollectCoin(int collectCoinAmount)
    {
        DataManager.Instance.AddCoin(collectCoinAmount);
    }


}
