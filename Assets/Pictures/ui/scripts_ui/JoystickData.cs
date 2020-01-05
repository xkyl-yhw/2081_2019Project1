﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickData : MonoBehaviour
{
    public float radians;   //弧度
    public float angle;     //角度
    public float angle360;  //0~360 0为右 90为上 180为左 270为下
    public float power;     //0~1 拖拽的力度
}
