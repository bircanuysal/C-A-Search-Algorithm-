using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour , IUnit , IObjectPoolable
{
    [SerializeField]
    private UnitType _unitType;

    private string _buildName;

    private Image _image;

    private int _health;

    private bool _selectable;

    private bool _canMove;

    private PoolableObjectTypes _poolableObjectTypes;

    public UnitType unitType
    {
        get => _unitType;
    }
    public Image image
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

    public void OnReturnToPool()
    {
        
    }

    public PoolableObjectTypes PoolableObjectType()
    {
        return poolableObjectTypes;
    }
}
