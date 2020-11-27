﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    bool ApplyDamage(Rigidbody body, float force);
}
