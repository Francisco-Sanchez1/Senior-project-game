using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CoinDisplay : MonoBehaviour
{

    public PlayerCntrl player;
    public TextMeshProUGUI coinText;

    void Update()
    {
        if (player != null)
        {
            coinText.text = player.coins.ToString();
        }
    }
}
