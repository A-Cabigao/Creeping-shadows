using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    private Player player;
    private Image image;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        image = GetComponent<Image>();
    }

    private void OnEnable()
    {
        player.PlayerTookDamage += ReduceImageFill;
    }

    private void OnDisable()
    {
        player.PlayerTookDamage -= ReduceImageFill;
    }

    // Reduces image fill when player takes damage
    private void ReduceImageFill()
    {
        float hpPercentage = (float)player.GetPlayerHealth() / player.GetPlayerMaxHealth();
        image.fillAmount = hpPercentage;
    }
}
