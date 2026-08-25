using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
#region 武器構造体
public enum WeponEnum
{
    DefautSword,
    Sword,
    Mace
}
[Serializable]
public struct Wepon
{
    public string _name;
    public WeponEnum _weponState;
    public float _weponPower;
    public bool _isCrafted;
    public int _repairPal;
}
#endregion
public class GameManager : MonoBehaviour
{
    public List<Wepon> _wepon;
    public enum PlayerState
    { 
        Nomal,
        Power,
        Guald
    }
    public enum Item
    {
        None,
        Harb,
        HighHarb,
        Meat
    }
    [SerializeField, Header("プレイヤー状態")]
    public PlayerState _playerState;
    /// <summary>
    /// １～のステージ番号。
    /// １には祈り、２には戦闘、３には鍛冶が入ってる
    /// </summary>
    [SerializeField]
    public List<int> _stageNum;
    [SerializeField, Header("武器画像")]
    public Sprite[] _swordImage;
    [SerializeField, Header("ステージ画像")]
    public Sprite[] _stageImage;
    [SerializeField, Header("時間の画像")]
    Sprite[] _daySprite;
    [SerializeField, Header("時間による背景")]
    SpriteRenderer _dayBack;
    [SerializeField, Header("アイテム画像")]
    public Sprite[] _itemImage;
    [SerializeField]
    public Item[] _item;
    [SerializeField, Header("未作成エラー")]
    public GameObject[] _allUI;
    [SerializeField, Header("設定パネル")]
    public GameObject _settingPanel;
    //[SerializeField]
    //public Text _dayNum;

    /// <summary>
    /// １～の時間指定変数
    /// </summary>
    public int _currentTimeNum;
    public int _currentDayNum;
    public int _currentEquipped = 0;
    public int _currentMake = 0;
    public int _currentFight = 0;
    public int _harb;
    public int _highHarb;
    public int _meat = 5;
    public Vector3 _defaultTransform;

    public delegate void Pause(bool paused);
    public Pause _pauseReseum = default;
    bool _isPaused;
    private void Awake()
    {
        if (FindObjectsByType<GameManager>(
    FindObjectsSortMode.None).Length > 1)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += CheckTime;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= CheckTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseReseum();
        }
    }

    #region アイテム追加。多態性アイテム作るときにここも
    public void AddItem(int addItem, int num)
    {
        addItem += num;
    }
    public void AddHarb(int num)
    {
        _harb += num;
    }
    public void AddHighHarb(int num)
    {
        _highHarb += num;
    }
    #endregion
    public Wepon CurrentWepon
    {
        get
        {
            return _wepon[_currentEquipped];
        }
    }
    public void AddRepair(int num)
    {
        Wepon wepon;
        wepon = _wepon[_currentEquipped];
        wepon._repairPal =  Mathf.Clamp(wepon._repairPal + num,0,2);
        _wepon[_currentEquipped] = wepon;
    }
    public void ChangeState(PlayerState buff)
    {
        _playerState = buff;
    }
    /// <summary>
    /// アイテムポーチ変更メソッド
    /// </summary>
    /// <param name="item">変更するアイテムの種類</param>
    /// <param name="num">アイテムポーチの番号（１から）</param>
    public void ChangeItem(Item item, int num)
    {
        _item[num] = item;
    }
    public bool State(PlayerState state)
    {
        return _playerState == state;
    }
    public void AllUI(GameObject gameObject)
    {
        GameObject gb = GameObject.FindGameObjectWithTag("Canvas");
        Canvas cv = gb.GetComponent<Canvas>();
        gb = Instantiate(gameObject,new Vector3(transform.position.x, transform.position.y, transform.position.z),Quaternion.identity);
        gb.transform.SetParent(cv.transform, false);
    }
    public void AllUISet(GameObject gameObject)
    {
        GameObject gb = GameObject.FindGameObjectWithTag("Canvas");
        Canvas cv = gb.GetComponent<Canvas>();
        
            if (gameObject.activeSelf)
            {
                gameObject.SetActive(false);
            }
            else
            {
                gameObject.SetActive(true);
            }
        gameObject.transform.SetParent(cv.transform, false);
    }

    public void UnCreated()
    {
        AllUI(_allUI[0]);
    }

    public void EquipUI()
    {
        AllUI(_allUI[1]);
    }
    public void SettingUI()
    {
        AllUI(_allUI[2]);
    }
    public void BrackOut()
    {
        AllUI(_allUI[3]);
    }
    public void SettingPanel()
    {
        AllUISet(_settingPanel);
    }
    public void PauseReseum()
    {
        SettingPanel();
        //if(!_isPaused)
        //{
        //    _isPaused = true;
        //}
        //else
        //{
        //    _isPaused = false;
        //}
        _isPaused = !_isPaused;
        if(_pauseReseum != null)
        {
            _pauseReseum(_isPaused);
        }
    }

    public void SetMyPearent(GameObject child)
    {
        child.transform.SetParent(gameObject.transform, false);
    }
    public void SetSetting()
    {
        SetMyPearent(_settingPanel);
    }
    public void CheckTime(Scene scene, LoadSceneMode node)
    {
        if(_currentTimeNum <= 1 || _currentTimeNum == 5)
        {
            _dayBack.sprite = _daySprite[0];
        }
        else if( _currentTimeNum == 2)
        {
            _dayBack.sprite = _daySprite[1];
        }
        else if (_currentTimeNum >= 3 || _currentTimeNum < 5)
        {
            _dayBack.sprite = _daySprite[2];
        }
    }
}
