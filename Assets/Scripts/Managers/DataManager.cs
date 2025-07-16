

using UnityEngine;

public class DataManager : MonoBehaviour
{

    public static DataManager _instance;
    public static DataManager Instance => _instance;


    public int cachedCoin = 0;

    public void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void Start()
    {
        Setup();
    }

    public void Setup()
    {
        cachedCoin = CoinData;
    }

    private const string COIN_DATA = "CoinData";
    public int CoinData{
        get => PlayerPrefs.GetInt(COIN_DATA, 0);
        set => PlayerPrefs.SetInt(COIN_DATA, value);
    }

    public void AddCoin(int addCoinAmount)
    {
        cachedCoin += addCoinAmount;
        CoinData = cachedCoin;
    }


}
