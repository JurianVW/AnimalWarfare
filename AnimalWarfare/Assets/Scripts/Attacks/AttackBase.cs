﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackBase : MonoBehaviour
{
    public abstract void Attack(Animal currentAnimal, Animal targetAnimal);
}
