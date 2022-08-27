using System;
using System.Collections.Generic;
using System.Text;

namespace Console2048
{
    /// <summary>
    /// 处理游戏核心算法，与界面无关
    /// </summary>
    class GameCore
    {

        public int[,] game;
        private int[] temp;
        private List<Location> emptyLocationList;
        private int[] randArr;

        static Random random = new Random();   // 创建一个随机数工具
        public GameCore()
        {
            game = new int[4, 4];
            temp = new int[4];
            emptyLocationList = new List<Location>();
            GenerateNumber();
        }

        public void GenerateNumber()
        {
            randArr = new int[10] { 2, 2, 2, 2, 2, 2, 2, 2, 2, 4 };
            int number1 = random.Next(0, 16);
            int number2 = random.Next(0, 16);
            while (number1 == number2)
            {
                number2 = random.Next(0, 16);
            }
            game[number1 / 4, number1 % 4] = randArr[random.Next(0, 10)];
            game[number2 / 4, number2 % 4] = randArr[random.Next(0, 10)];
        }

        
        public void MovePosition(Move dir)
        {
            if (dir == Console2048.Move.Up)
            {
                GetKeyW();
            }
            else if(dir == Console2048.Move.Down)
            {
                GetKeyS();
            }
            else if(dir == Console2048.Move.Left)
            {
                GetKeyA();
            }
            else
            {
                GetKeyD();
            }
        }

        private void GetKeyW()
        {
            for (int i = 0; i < game.GetLength(1); i++)
            {
                Array.Clear(temp, 0, 4);
                // temp = new int[game.GetLength(0)];
                for (int j = 0; j < game.GetLength(0); j++)
                {
                    temp[j] = game[j, i];
                }
                RemoveZero();
                MergeArray();
                RemoveZero();
                for (int j = 0; j < game.GetLength(0); j++)
                {
                    game[j, i] = temp[j];
                }
            }
        }
        private void GetKeyA()
        {
            for (int i = 0; i < game.GetLength(0); i++)
            {
                Array.Clear(temp, 0, 4);
                // temp = new int[game.GetLength(1)];
                for (int j = 0; j < game.GetLength(1); j++)
                {
                    temp[j] = game[i, j];
                }
                RemoveZero();
                MergeArray();
                RemoveZero();
                for (int j = 0; j < game.GetLength(1); j++)
                {
                    game[i, j] = temp[j];
                }
            }
        }
        private void GetKeyS()
        {
            for (int i = 0; i < game.GetLength(1); i++)
            {
                Array.Clear(temp, 0, 4);
                // temp = new int[game.GetLength(0)];
                int k = 0;
                for (int j = game.GetLength(0) - 1; j >= 0; j--)
                {
                    temp[k] = game[j, i];
                    k++;
                }
                RemoveZero();
                MergeArray();
                RemoveZero();
                // printArray(temp);
                k = 0;
                for (int j = game.GetLength(0) - 1; j >= 0; j--)
                {
                    game[j, i] = temp[k];
                    k++;
                }
            }
        }
        private void GetKeyD()
        {
            for (int i = 0; i < game.GetLength(0); i++)
            {
                Array.Clear(temp, 0, 4);
                // temp = new int[game.GetLength(1)];
                int k = 0;
                for (int j = game.GetLength(1) - 1; j >= 0; j--)
                {
                    temp[k] = game[i, j];
                    k++;
                }
                RemoveZero();
                MergeArray();
                RemoveZero();
                k = 0;
                for (int j = game.GetLength(1) - 1; j >= 0; j--)
                {
                    game[i, j] = temp[k];
                    k++;
                }
            }
        }


        private void RemoveZero()
        {
            int j = 0;
            for (int i = 0; i < temp.Length; i++)
            {
                if (temp[i] != 0)
                {
                    if (j != i)
                    {
                        temp[j] = temp[i];
                        temp[i] = 0;
                    }
                    j++;
                }
            }
        }
        private void MergeArray()   // 将相同数据合在一起
        {
            for (int j = 0; j < temp.Length - 1; j++)
            {
                if (temp[j] == temp[j + 1])
                {
                    temp[j] += temp[j + 1];
                    temp[j + 1] = 0;
                }
            }
        }
        public void GenNewNumber()
        {
            if (IsFull())
            {
                Console.WriteLine("移动失败");
            }
            else
            {
                CalculateEmpty();
                // Console.WriteLine("COUNT = " + emptyLocationList.Count.ToString());
                int number = random.Next(0, emptyLocationList.Count);
                // 生成新的数
                int i = emptyLocationList[number].RIndex;
                // Console.WriteLine("I = " + i.ToString());
                int j = emptyLocationList[number].CIndex;
                // Console.WriteLine("J = " + j.ToString());
                game[i, j] = randArr[random.Next(0, 10)];
            }
        }
        
        private void CalculateEmpty()
        {
            emptyLocationList.Clear();   // 每次统计空位置前先清空列表
            for (int i = 0; i < game.GetLength(0); i++)
            {
                for (int j = 0; j < game.GetLength(1); j++)
                {
                    if (game[i, j] == 0)
                        // 将多个基本数据类型封装为一个自定义数据类型
                        emptyLocationList.Add(new Location(i, j));
                }
            }
        }


        public bool IsSucess()
        {
            foreach (var item in game)
            {
                if (item == 2048)
                {
                    return true;
                }
            }
            return false;
        }
        public bool IsFull()
        {
            int count = 0;
            foreach (var item in game)
            {
                if (item != 0)
                {
                    count++;
                }
            }
            if (count == game.GetLength(0) * game.GetLength(1))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool IsFail()
        {
            if (IsFull())  // 填满
            {
                int flag = 0;
                for (int i = 0; i < game.GetLength(0) - 1; i++)
                {
                    for (int j = 0; j < game.GetLength(1) - 1; j++)
                    {
                        if (game[i, j] == game[i + 1, j] || game[i, j] == game[i, j + 1])
                        {
                            flag = 1;
                            return false;
                        }
                    }
                }
                for (int i = 0; i < game.GetLength(1) - 1; i++)  // 判断最下面一行
                {
                    if (game[game.GetLength(0) - 1, i] == game[game.GetLength(0) - 1, i + 1])
                    {
                        flag = 1;
                        return false;
                    }
                }
                for (int i = 0; i < game.GetLength(0) - 1; i++)   // 判断最右边一列
                {
                    if (game[i, game.GetLength(1) - 1] == game[i + 1, game.GetLength(1) - 1])
                    {
                        flag = 1;
                        return false;
                    }
                }
                if (flag == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else  // 未填满
            {
                return false;
            }
        }

    }
}
