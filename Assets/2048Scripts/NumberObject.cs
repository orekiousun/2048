using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
///
/// <summary>
public class NumberObject : MonoBehaviour
{
    void Start()
    {
        
    }

    public GameObject SetObject(int number)
    {
        return ResourceManager.LoadObject(number);
    }
}
