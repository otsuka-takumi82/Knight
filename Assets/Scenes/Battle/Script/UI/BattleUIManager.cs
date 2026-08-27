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
    [SerializeField, Header("カーソル画像")]
    private Image _cursleImage;
    [SerializeField, Header("カーソル判定")]
    private GameObject _curslejadge;
    [SerializeField, Header("プレイヤースタミナ画像")]
    private Image _playerStaminaImage;
    [SerializeField, Header("ポーチ画像")]
    private GameObject[] _porch;
    [SerializeField, Header("薬草画像")]
    private GameObject _harbImage;
    [SerializeField, Header("熟成薬草画像")]
    private GameObject _highHarbImage;
    [SerializeField, Header("干し肉画像")]
    private GameObject _meatImage;
    [SerializeField, Header("アイテムのテキスト")]
    private Text[] _itemText;

    float[] _scale = new float[2];
    Vector2 _mousePos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        _cursleImage.raycastTarget = false;
        _scale[0] = _cursleImage.rectTransform.localScale.x;
        _scale[1] = _cursleImage.rectTransform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        _mousePos = Input.mousePosition;
        _cursleImage.rectTransform.position = _mousePos;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(_mousePos);
        _curslejadge.transform.position = mousePos;
        
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

    public void ChangePorch(string item, int num)
    {
        if(item == "薬草")
        {
            _harbImage.transform.position = _porch[num].transform.position;
        }
        else if(item == "熟成薬草")
        {
            _highHarbImage.transform.position = _porch[num].transform.position;
        }
        else if (item == "干し肉")
        {
            _meatImage.transform.position = _porch[num].transform.position;
        }

    }

    public void ChangeItemText(int UInum, string name, int num)
    {
        _itemText[UInum].text = name + num;
    }

    public void ChangeCursleDirection(DirectionAttack.AttackType type)
    {
        if(type == DirectionAttack.AttackType.RightUp)
        {
            _cursleImage.rectTransform.localScale = new Vector2(_scale[0], _scale[1]);
        }
        else if (type == DirectionAttack.AttackType.LeftUp)
        {
            _cursleImage.rectTransform.localScale = new Vector2(_scale[0] * -1, _scale[1]);
        }
        else if (type == DirectionAttack.AttackType.RightDown)
        {
            _cursleImage.rectTransform.localScale = new Vector2(_scale[0] * 1, _scale[1] * -1);
        }
        else if (type == DirectionAttack.AttackType.LeftDown)
        {
            _cursleImage.rectTransform.localScale = new Vector2(_scale[0] * -1, _scale[1] * -1);
        }
    }
}
