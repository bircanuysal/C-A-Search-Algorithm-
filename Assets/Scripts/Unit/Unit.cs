using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour , IUnit , IObjectPoolable
{
    [SerializeField]
    private UnitType _unitType;

    private string _buildName;

    private Sprite _image;

    private int _health;

    private bool _selectable;

    private bool _canMove;

    private PoolableObjectTypes _poolableObjectTypes;

    public UnitType unitType
    {
        get => _unitType;
        set => _unitType = value;
    }
    public Sprite image
    {
        get => _image;
        set => _image = value;
    }
    public int health
    {
        get => _health;
        set => _health = value;
    }
    public bool selectable
    {
        get => _selectable;
        set => _selectable = value;
    }
    public bool canMove
    {
        get => _canMove;
        set => _canMove = value;
    }
    public PoolableObjectTypes poolableObjectTypes
    {
        get => _poolableObjectTypes;
        set => _poolableObjectTypes = value;
    }

    protected MapManager mapManager = MapManager.Instance;
    protected virtual void Start()
    {
        Initialize();
        SetSprite();
    }
    private void OnEnable()
    {
        EventManager.ClickEvents.OnMouseClicked.AddListener(SelectedUnit);
    }
    private void OnDisable()
    {
        EventManager.ClickEvents.OnMouseClicked.RemoveListener(SelectedUnit);
    }
    protected virtual void Initialize()
    {
    }
    protected virtual void SetSprite()
    {
    }
    public void OnReturnToPool()
    {
        
    }

    public PoolableObjectTypes PoolableObjectType()
    {
        return poolableObjectTypes;
    }

    protected virtual void SelectedUnit(GameObject gameObject)
    {
    }
}
