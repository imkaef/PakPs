using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ПАК_ПС
{
    class Figure
    {
        private float x, y;
        private float w, h;
        public float X { get { return x; } }
        public float Y { get { return y; } }
        public float W { get { return w; } }
        public float H { get { return h; } }
        public virtual void Move(float ax, float ay)
        {
            x = ax; y = ay;
        }
        public virtual void Resize(float aw, float ah)
        {
            w = aw; h = ah;
        }
        public virtual void Draw(Graphics g)
        { }
        public virtual Figure Clone()
        {
            return null;
        }
        public virtual bool Touch(float ax, float ay)
        {
            return (ax >= X && ax <= X + W && ay >= Y && ay <= Y + H);
        }
 
    }
    abstract class Creator
    {
        abstract public Figure Create();
    }

    class PrototypeCreator : Creator
    {
        public Figure Prototype { get; set; }
        public override Figure Create()
        {
            return Prototype.Clone();
        }
    }
}
