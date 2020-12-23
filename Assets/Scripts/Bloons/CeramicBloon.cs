﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeramicBloon : DoubleBloon
{
    private int healthPoints = 10;
    public override void LayerPop(int power, GameObject _parentPopProjectle)
    {
        if (healthPoints > 1)
        {
            healthPoints -= 1;
            parentPopProjectle = _parentPopProjectle;
        }
        else
        {
            base.LayerPop(power, _parentPopProjectle);
        }
    }
}