

using System.Collections;
using DG.Tweening;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public static EffectManager Instance { get; private set; }

    public GameObject coinPrefab; // Assign the Coin UI Image prefab
    public RectTransform spawnRect;// Assign the Canvas RectTransform
    public Transform coinBar; // Assign the CoinBar UI Image
    public int coinCount = 20; // Number of coins to spawn
    public float spreadDuration = 0.15f; // Time to spread out
    public float flyDuration = 1f; // Time to fly to coin bar
    public float delayBetweenCoins = 0.1f; // Delay between each coin's animation
    public RectTransform boundRect;


    public void Start()
    {
        TriggerResourcesEffect(Vector3.zero);
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void TriggerResourcesEffect(Vector2 startPosition, int amount = 20)
    {
        var spreadHeight = boundRect.rect.height;
        var spreadWidth = boundRect.rect.width;

        StartCoroutine(SpawnResourcesIcon(amount));

        IEnumerator SpawnResourcesIcon(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                var newCoin = Instantiate(this.coinPrefab, startPosition, Quaternion.identity,spawnRect);

                var randomOfsset = new Vector2(Random.Range(-spreadWidth / 2, spreadWidth / 2), Random.Range(-spreadHeight / 2, spreadHeight / 2));
                newCoin.transform.DOMove(randomOfsset, spreadDuration)
                    .SetEase(Ease.OutBack)
                    .OnComplete(() =>
                    {
                        newCoin.transform.DOMove(coinBar.position, flyDuration)
                            .SetEase(Ease.InBack)
                            .OnComplete(() =>
                            {
                                Destroy(newCoin);
                            });
                    });
                yield return new WaitForSeconds(delayBetweenCoins);
            }

        }


    }




}

