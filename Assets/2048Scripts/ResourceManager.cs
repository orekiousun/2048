using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 资源管理类，负责加载资源
/// <summary>
public class ResourceManager
{
    // 将GameObject定义在字典中
    private static Dictionary<int, GameObject> goArray;

    // 类被加载时调用，将资源中的GameObject放入goArray字典中
    static ResourceManager()
    {
        goArray = new Dictionary<int, GameObject>();
        GameObject[] temp = Resources.LoadAll<GameObject>("Pictures");
        foreach (var item in temp)
        {
            goArray.Add(int.Parse(item.name), item);
        }
    }

    public static GameObject LoadObject(int number)
    {
        return goArray[number];
    }
}
