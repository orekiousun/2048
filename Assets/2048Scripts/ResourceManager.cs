using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ��Դ�����࣬���������Դ
/// <summary>
public class ResourceManager
{
    // ��GameObject�������ֵ���
    private static Dictionary<int, GameObject> goArray;

    // �౻����ʱ���ã�����Դ�е�GameObject����goArray�ֵ���
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
