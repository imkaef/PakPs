using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ПАК_ПС
{
    class Ellipse : Figure
    {
        public override void Draw(Graphics g)
        {
            g.DrawEllipse(new Pen(Color.Black), X, Y, W, H);
        }
        public override bool Touch(float ax, float ay)
        {
            float a = W / 2;
            float b = H / 2;
            return (((ax - (X + a)) * (ax - (X + a))) / (a * a) + ((ay - (Y + b)) * (ay - (Y + b))) / (b * b) <= 1);
        }
        public override Figure Clone()
        {
            Ellipse e = new Ellipse();
            e.Resize(W, H);
            e.Move(X, Y);
            return e;
        }
    }
    class EllipseCreator : Creator
    {
        public override Figure Create()
        {
            Ellipse ell = new Ellipse();
            ell.Resize(20, 20);
            return ell;
        }
    }
}
