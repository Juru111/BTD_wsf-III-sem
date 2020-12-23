﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Bloon : MonoBehaviour
{
    #region Dane potrzebne do spawnu kolejnych balownów
    [SerializeField]
    protected BloonTypes bloonName;
    [SerializeField]
    protected int layersLeft = 1;
    [SerializeField]
    protected bool isCammo;
    [SerializeField]
    protected bool isRegrow;
    [field: SerializeField]
    public int myNextWaypoint { get; protected set; } = 0;
    [field: SerializeField]
    public float distanceToWaypoint { get; protected set; }
    [field: SerializeField]
    public GameObject parentPopProjectle { get; protected set; } = null;
    #endregion

    [SerializeField]
    protected float movementSpeed = 3.5f;
    [SerializeField]
    protected GameObject popIcon;
    [SerializeField]
    protected GameMenager gameMenaner;

    protected virtual void Awake()
    {
        gameMenaner = FindObjectOfType<GameMenager>();
        //GameBox.instance.GameMenager;
        //które leprze? i dlaczego to drugie nie działa (w Starcie)
    }

    protected virtual void Update()
    {
        Move();
    }

    protected void Move()
    {

        //Gdy bloon dojdzie do kolejnego waypointa bierze kolejny
        distanceToWaypoint -= movementSpeed * Time.deltaTime;
        if (distanceToWaypoint <= 0)
        {
            myNextWaypoint++;
            if(myNextWaypoint <= GameBox.instance.waypoints.Length - 1)
            {
                ChangeWaypoint();
            }
            else
            {
                int hpToLose = 0;
                switch(bloonName)
                {
                    case BloonTypes.Red: hpToLose = 1; break;
                    case BloonTypes.Blue: hpToLose = 2; break;
                    case BloonTypes.Green: hpToLose = 3; break;
                    case BloonTypes.Yellow: hpToLose = 4; break;
                    case BloonTypes.Pink: hpToLose = 5; break;
                    case BloonTypes.Black: hpToLose = 11; break;
                    case BloonTypes.White: hpToLose = 11; break;
                    case BloonTypes.Lead: hpToLose = 23; break;
                    case BloonTypes.Zebra: hpToLose = 23; break;
                    case BloonTypes.Rainbow: hpToLose = 47; break;
                    case BloonTypes.Ceramic: hpToLose = 104; break;
                    case BloonTypes.MOAB: hpToLose = 616; break;
                    default: Debug.LogError("Niepoprawny bloon uciekł!"); break;
                }
                gameMenaner.LoseHp(hpToLose);

                GameBox.instance.PoolingMenager.ReturnBloon(gameObject, bloonName);
                return;
            }
        }

        //Zmiana pozycji - ruch właściwy
        transform.position = Vector2.MoveTowards(transform.position, GameBox.instance.waypoints[myNextWaypoint].transform.position, movementSpeed * Time.deltaTime);
    }

    protected virtual void ChangeWaypoint()
    {
        distanceToWaypoint = Vector3.Distance(transform.position, GameBox.instance.waypoints[myNextWaypoint].transform.position);
    }

    public virtual void SetMe(BloonTypes _bloonName, int _layersLeft, Vector3 _position, int _myNextWayPoint,
                                float _distanceToWaypoint, bool _isCammo, bool _isRegrow, GameObject _parentPopProjectle)
    {
        bloonName = _bloonName;
        layersLeft = _layersLeft;
        transform.position = _position;
        myNextWaypoint = _myNextWayPoint;
        distanceToWaypoint = _distanceToWaypoint;
        isCammo = _isCammo;
        isRegrow = _isRegrow;
        parentPopProjectle = _parentPopProjectle;

        if(layersLeft < (int)bloonName % 100)
        {
            LayerPop(0, parentPopProjectle);
        }
    }

    public virtual void LayerPop(int power, GameObject parentPopProjectle)
    {
        layersLeft -= power;
        StartCoroutine(ShowPoP());

        if(bloonName != BloonTypes.Red)
        {
            BloonTypes newBloonName = (BloonTypes)((float)bloonName % 100 - 1);
            GameBox.instance.PoolingMenager.SummonBloon(newBloonName, layersLeft, transform.position, myNextWaypoint, distanceToWaypoint, isCammo, isRegrow, parentPopProjectle);
        }
        else
        {
            Debug.Log("Red dead");
        }

        gameMenaner.changeMoneyBalance(1);
        GameBox.instance.PoolingMenager.ReturnBloon(gameObject, bloonName);
    }


    protected virtual IEnumerator ShowPoP()
    {
        popIcon.transform.Rotate(0f, 0f, Random.Range(0f, 360.0f));
        popIcon.SetActive(true);
        yield return new WaitForSeconds(0.11f);
        popIcon.SetActive(false);
    }

}