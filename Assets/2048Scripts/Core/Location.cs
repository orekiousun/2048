using System;
using System.Collections.Generic;
using System.Text;

namespace Console2048
{
    struct Location
    {
        public int RIndex { get; set; }
        public int CIndex { get; set; }

        public Location(int rIndex, int cIndex):this()   // 结构体中自动属性赋值需要先调用无参构造函数
        {
            this.RIndex = rIndex;
            this.CIndex = cIndex;
        }
    }
}
