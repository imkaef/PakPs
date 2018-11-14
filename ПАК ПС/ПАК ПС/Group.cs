using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ПАК_ПС
{
    class Group : Figure
    {
        List<Figure> figs=new List<Figure>();

        public override Figure Clone()
        {
            Group g = new Group();
            foreach (var f in figs)
                g.Add(f.Clone());
            return g;
        }

        public override void Draw(Graphics g)
        {
            foreach (var f in figs)
            {
                f.Draw(g);
            }
        }
        public override void Move(float ax, float ay)
        {
            foreach (var f in figs)
            {
                f.Move(f.X + ax - X, f.Y + ay - Y);
            }
            base.Move(ax, ay);
        }

        public override void Resize(float aw, float ah)
        {
            float kx = 0;
            float ky = 0;
            foreach (var f in figs)
            {
                kx = aw / W;
                ky = ah / H;
                f.Resize(kx * f.W,ky*f.H);
                f.Move(X + kx * (f.X - X), Y + ky * (f.Y - Y));
            }
            base.Resize(aw, ah);
        }

        public void Add(Figure f)
        {
            if (f == null) return;
            if (figs.Contains(f)) return;
            if (f == this) return;
            figs.Add(f);
            float Xmin = f.X,Ymin=f.Y,Xmax=0,Ymax=0;
            foreach (var g in figs)
            {
                if (Xmin > g.X)
                {
                    Xmin = g.X;
                }
                if (Ymin>g.Y)
                {
                    Ymin = g.Y;
                }
                if (g.X+g.W>Xmax)
                {
                    Xmax = g.X + g.W;
                }
                if (g.Y+g.H>Ymax)
                {
                    Ymax = g.Y + g.H;
                }
            }
            base.Move(Xmin, Ymin);
            base.Resize(Xmax - Xmin, Ymax - Ymin);
        }

        public void Ungroup(Picture P)
        {
            foreach (var f in figs)
            {
                P.Add(f);
            }
        }

        public override bool Touch(float ax, float ay)
        {
            foreach (var f in figs)
            {
                if (f.Touch(ax, ay))
                {
                    return true;
                }
            }
            return false;
        }

        public void RemoveFromPicture(Picture pic)
        {
            foreach (var f in figs)
            {
                pic.Remove(f);
            }
        }

        public void RemoveGroup(Figure f)
        {
            
            if (figs.Contains(f))
            {
                Clear();
            }
            // перетащить из него элементы в картинку затем удалить группу
        }

        public void Clear()
        {
            figs.Clear();
        }

    }
    class GroupCreator : Creator
    {
        public override Figure Create()
        {
            Group gr = new Group();
            Ellipse e = new Ellipse();
            e.Resize(30, 30);
            Rectangle r = new Rectangle();
            r.Resize(30, 30);
            r.Move(20, 20);
            gr.Add(e);
            gr.Add(r);
            return gr;
        }
    }
}
