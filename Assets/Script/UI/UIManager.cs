using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField,Header("敵HP画像")]
    private Image _enemyHpImage;
    [SerializeField, Header("敵スタミナ画像")]
    private Image _enemyStaminaImage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnemyHPUI(float hp, float maxHP)
    {
        _enemyHpImage.fillAmount = (float)hp / maxHP;
    }

    public void EnemyStaminaUI(float stamina, float maxStamina)
    {
        _enemyStaminaImage.fillAmount = (float)stamina / maxStamina;
    }
}
