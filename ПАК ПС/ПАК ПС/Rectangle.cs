using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ПАК_ПС
{
    class Rectangle : Figure
    {
        public override void Draw(Graphics g)
        {
            g.DrawRectangle(new Pen(Color.Black), X, Y, W, H);
        }
        public override Figure Clone()
        {
            Rectangle r = new Rectangle();
            r.Resize(W, H);
            r.Move(X, Y);
            return r;
        }
    }
    class RectangleCreator : Creator
    {
        public override Figure Create()
        {
            Rectangle rec = new Rectangle();
            rec.Resize(30, 30);
            return rec;
        }
    }
}
