﻿using UnityEngine;

public class DestroyOnStart : MonoBehaviour
{
    void Start()
    {
        Destroy(this.gameObject);
    }
}
