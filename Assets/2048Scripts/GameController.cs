using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Console2048;
/// <summary>
///
/// <summary>
public class GameController : MonoBehaviour
{
    private GameCore core;
    private bool w;
    private bool s;
    private bool a;
    private bool d;
    public Text scoreText;
    public Text bestText;
    private int bestScore = 0;
    public GameObject over;
    public GameObject sucess;
    private Button btn_Start;
    private bool target = false;

    private int CalculateScore()
    {
        int score = 0;
        foreach (var item in core.game)
        {
            score += item;
        }
        if (score > bestScore)
            bestScore = score;
        return score;
    }

    private void SetScore()
    {
        scoreText.GetComponent<Text>().text = CalculateScore().ToString();
    }

    private void SetBestScore()
    {
        bestText.GetComponent<Text>().text = bestScore.ToString();
    }

    private void InitNumber()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                CreateObject(i, j, core.game[i, j]);
            }
        }
    }
    
    private void DestroyObject()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                Destroy(this.transform.GetChild(i * 4 + j).gameObject);
            }
        }
    }

    private void CreateObject(int i, int j, int number)
    {
        GameObject go = Instantiate(ResourceManager.LoadObject(number));
        NumberObject action = go.AddComponent<NumberObject>();
        go.name = "(" + i.ToString() + "," + j.ToString() + ")";
        go.transform.SetParent(this.transform);
    }


    private void GenerateNewNumber()
    {
        core.GenNewNumber();
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                CreateObject(i, j, core.game[i, j]);
            }
        }
    }

    public bool OnCLick()
    {
        return true;
    }

    private void Init()
    {
        core = new GameCore();
        InitNumber();
    }

    private void OnStartButtonClick()
    {
        target = true;
    }

    private void Start()
    {
        btn_Start = GameObject.Find("Button").GetComponent<Button>();//通过Find查找名称获得我们要的Button组件
        btn_Start.onClick.AddListener(OnStartButtonClick);//监听点击事件
        Init();
    }



    private void Update()
    {
        w = Input.GetKeyDown(KeyCode.W);
        s = Input.GetKeyDown(KeyCode.S);
        a = Input.GetKeyDown(KeyCode.A);
        d = Input.GetKeyDown(KeyCode.D);
        if (w != false || s != false || a != false || d != false)
        {
            Move temp = Move.Up;
            if (a == true) temp = Move.Left;
            if (d == true) temp = Move.Right;
            if (w == true) temp = Move.Up;
            if (s == true) temp = Move.Down;
            core.MovePosition(temp);
            DestroyObject();
            GenerateNewNumber();
        }
        if (core.IsFail())
        {
            over.SetActive(true);
        }
        if (core.IsSucess())
        {
            sucess.SetActive(true);
        }
        if(target == true)
        {
            target = false;
            DestroyObject();
            Init();
            over.SetActive(false);
            sucess.SetActive(false);
        }
        SetScore();
        SetBestScore();
    }
}
