using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Circle
{
    class CircleClass
    {
        public int X;
        public int Y;
        public int radius;
        public int diameter;

        public int realX;
        public int realY;
        public int realRadius;

        public CircleClass(int x, int y, int radius_, int diameter_, int rX, int rY, int rRad)
        {
            X = x;
            Y = y;
            radius = radius_;
            diameter = diameter_;
            realX = rX;
            realY = rY;
            realRadius = rRad;
        }

        public bool CheckDot(MyPoint p)
        {
            int XPos = realX + realRadius;
            int XNeg = realX - realRadius;
            int YPos = realX + realRadius;
            int YNeg = realY - realRadius;

            bool x = XNeg <= p.realX && p.realX <= XPos;
            bool y = YNeg <= p.realY && p.realY <= YPos;

            return x && y;
        }
    }
}
