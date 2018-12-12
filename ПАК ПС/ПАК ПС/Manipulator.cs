using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ПАК_ПС
{
    class Manipulator : Figure 
    {
        public Figure selected;
        int active_point = -1;

        public override bool Touch(float ax, float ay)//Определяем куда ткнули в какую точку объекта
        {
            if (selected == null)
            {
                active_point = -1;
                return false;
            }

            if (Math.Abs(ax - X) < 3 && (Math.Abs(ay - Y) < 3))
            {
                active_point = 1;
                return true;
            }
            if (Math.Abs(ax - W - X) < 3 && (Math.Abs(ay - Y) < 3))
            {
                active_point = 2;
                return true;
            }
            if (Math.Abs(ax - X) < 3 && (Math.Abs(ay - H - Y) < 3))
            {
                active_point = 3;
                return true;
            }
            if (Math.Abs(ax - W - X) < 3 && (Math.Abs(ay - H - Y) < 3))
            {
                active_point = 4;
                return true;
            }
            if (selected.Touch(ax, ay))
            {
                active_point = 0;
                return true;
            }
            active_point = -1;
            return false;
        }

        public void Drag(float dx, float dy)//Определяем за какую точку тянем
        {
            switch (active_point)
            {
                case 0:
                    Move(X + dx, Y + dy);
                    break;
                case 1:
                    Move(X+dx , Y+dy);
                    Resize(W-dx, H-dy);
                    break;
                case 2:
                    Move(X, Y + dy);
                    Resize(W + dx, H - dy);
                    break;
                case 3:
                    Move(X + dx, Y);
                    Resize(W-dx , H+dy );
                    break;
                case 4:
                    Resize(W + dx, H + dy);
                    break;
            }
        }

        public void Attach(Figure f)//Выделяем квадратом выделенный объект
        {
            selected = f;
            if (selected != null)
            {
                Move(f.X, f.Y);
                Resize(f.W, f.H);
            }
        }

        public void Update()
        {
            if (selected != null)
            {
                selected.Move(X, Y);
                selected.Resize(W, H);
            }
        }

        public override void Draw(Graphics g)
        {
            if (selected != null)
                g.DrawRectangle(new Pen(Color.Black), X - 2, Y - 2, W + 4, H + 4);
        }
    }
}
