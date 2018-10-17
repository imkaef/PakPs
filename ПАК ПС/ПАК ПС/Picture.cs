using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ПАК_ПС
{
    class Picture
    {
        List<Figure> list;
        public Manipulator manipulator;
        public Group TmpGr = new Group();
        public Picture()
        {
            list = new List<Figure>();
            manipulator = new Manipulator();
        }
        public void Add(Figure f)
        {
            list.Add(f);
        }
        public void Draw(Graphics g)
        {
            g.Clear(Color.White);
            foreach (var f in list)
                f.Draw(g);
            manipulator.Draw(g);
            TmpGr.Draw(g);
        
        }
        public Figure Select(float ax, float ay)
        {
            if (manipulator.Touch(ax,ay))
            {
                return manipulator.selected;
            }
            foreach (var f in list)
                if (f.Touch(ax, ay))
                {
                    manipulator.Attach(f);
                    return f;
                }
            return null;
        }
    }
}
