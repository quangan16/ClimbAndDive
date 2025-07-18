

using System.Collections;
using DG.Tweening;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public static EffectManager Instance { get; private set; }

    public GameObject coinPrefab; // Assign the Coin UI Image prefab
    public RectTransform spawnRect;// Assign the Canvas RectTransform
    public Transform coinBar; // Assign the CoinBar UI Image
    public int coinCountPerBatch = 7; // Number of coins to spawn
    public float spreadDuration = 0.15f; // Time to spread out
    public float flyDuration = 1f; // Time to fly to coin bar
    public float delayBetweenCoins = 0.1f; // Delay between each coin's animation
    public float delayBetweenBatches = 0.1f; // Delay between batches of coins
    public float delayBeforeMoveToBar = 0.5f; // Delay before moving coins to the coin bar
    public RectTransform boundRect;


    public void Start()
    {
        // TriggerResourcesEffect(Vector3.zero);
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [ContextMenu("Trigger Resources Effect")]
    public void TestEfx()
    {
        TriggerResourcesEffect(Vector2.zero, coinCountPerBatch);
    }

    public void TriggerResourcesEffect(Vector2 startPosition, int amount = 20)
    {
        print("Trigger Resources Effect");
        var spreadHeight = boundRect.rect.height;
        var spreadWidth = boundRect.rect.width;

        StartCoroutine(SpawnResourcesIcon(coinCountPerBatch));

        IEnumerator SpawnResourcesIcon(int amount)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < amount; j++)
                {
                    var newCoin = Instantiate(this.coinPrefab, boundRect).GetComponent<RectTransform>();
                    newCoin.anchoredPosition = startPosition + Vector2.down * 100 + Random.insideUnitCircle * 100f; // Random start position within a small radius
                    var randomOfsset = new Vector2(Random.Range(-spreadWidth / 2, spreadWidth / 2), Random.Range(-spreadHeight / 2, spreadHeight / 2));
                    newCoin.DOAnchorPos(randomOfsset, spreadDuration)
                        .SetEase(Ease.OutQuad)
                        .OnComplete(() =>
                        {
                            newCoin.transform.DOMove(coinBar.position, flyDuration)
                                .SetEase(Ease.InBack).SetDelay(delayBeforeMoveToBar)
                                .OnComplete(() =>
                                {
                                    Destroy(newCoin.gameObject);
                                });
                        });
                    yield return new WaitForSeconds(delayBetweenCoins);
                }
                yield return new WaitForSeconds(delayBetweenBatches);
            }


            yield return null;
        }


    }
}

