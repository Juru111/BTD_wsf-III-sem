﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavesMenager : MonoBehaviour
{
    private List<GameObject> EmptyList;
    private Vector3 startPoint;

    void Start()
    {
        startPoint = GameBox.instance.waypoints[0].position;

        //robocze spawnowanie balonów
        StartCoroutine(WIPSpawning());
        //GameBox.instance.PoolingMenager.SummonBloon(BloonTypes.MOAB, 10, startPoint, 0, 0, false, false, null);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator WIPSpawning()
    {
        for (int i = 1; i < 10; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                BloonTypes bloonType = (BloonTypes)i;
                GameBox.instance.poolingMenager.SummonBloon(bloonType, (int)bloonType % 100, startPoint, 0, 0, true, 0, new List<GameObject>());
                yield return new WaitForSeconds(0.5f);
            }
            yield return new WaitForSeconds(0.5f);
            for (int j = 0; j < 5; j++)
            {
                BloonTypes bloonType = (BloonTypes)i;
                GameBox.instance.poolingMenager.SummonBloon(bloonType, (int)bloonType % 100, startPoint, 0, 0, false, 0, new List<GameObject>());
                yield return new WaitForSeconds(0.5f);
            }
            yield return new WaitForSeconds(1);
        }
        yield return new WaitForSeconds(10);
        for (int i = 1; i < 100; i++)
        {
            BloonTypes bloonType = (BloonTypes)Random.Range(1, 10);
            GameBox.instance.poolingMenager.SummonBloon(bloonType, (int)bloonType % 100, startPoint, 0, 0, false, 0, new List<GameObject>());
            yield return new WaitForSeconds(0.2f);
        }

    }



    public void DebugLogging()
    {
        Debug.Log("Debug Logging from WavesMenager");
    }
}
