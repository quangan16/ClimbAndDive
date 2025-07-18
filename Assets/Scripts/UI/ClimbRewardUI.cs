
using TMPro;
using UnityEngine;

public class ClimbRewardUI: MonoBehaviour
{
    [SerializeField] private TMP_Text coinTxt;

    [SerializeField] private GameObject climbRewardUI;

    public void Start()
    {
        GameManager.Instance.player.fsm.ClimbState.OnPlayerStartClimb += ()=>gameObject.SetActive(true);
        GameManager.Instance.player.fsm.ClimbState.OnPlayerEndClimb += ()=> gameObject.SetActive(false);
        GameManager.Instance.player.fsm.ClimbState.OnPlayerClimbing += UpdateRewardCoinText;
    }

    public void UpdateRewardCoinText()
    {
        if(climbRewardUI.activeSelf == false || coinTxt ==null) return;
        coinTxt.text = Mathf.FloorToInt(GameManager.Instance.climbCoin).ToString();
    }


}
