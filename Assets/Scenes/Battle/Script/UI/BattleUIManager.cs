using UnityEngine;
using UnityEngine.UI;

public class BattleUIManager : MonoBehaviour
{
    [SerializeField,Header("敵HP画像")]
    private Image _enemyHpImage;
    [SerializeField, Header("敵スタミナ画像")]
    private Image _enemyStaminaImage;
    [SerializeField, Header("プレイヤーHP画像")]
    private Image _playerHpImage;
    [SerializeField, Header("プレイヤースタミナ画像")]
    private Image _playerStaminaImage;
    [SerializeField, Header("ポーチ画像")]
    private GameObject[] _porch;
    [SerializeField, Header("薬草画像")]
    private GameObject _harbImage;
    [SerializeField, Header("アイテムのテキスト")]
    private Text _itemText;

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

    public void PlayerHPUI(float hp, float maxHP)
    {
        _playerHpImage.fillAmount = (float)hp / maxHP;
    }

    public void PlayerStaminaUI(float stamina, float maxStamina)
    {
        _playerStaminaImage.fillAmount = (float)stamina / maxStamina;
    }

    public void ChangePorch()
    {
        _harbImage.SetActive(true);
    }

    public void ChangeItemText(string name, int num)
    {
        _itemText.text = name + num;
    }
}
