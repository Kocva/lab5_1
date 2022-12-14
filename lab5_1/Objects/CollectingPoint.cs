using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab5_1.Objects
{
    internal class CollectingPoint : BaseObject
    {
        Random rnd = new Random();
        public Action<CollectingPoint> onDeath; 
        public CollectingPoint(float x, float y, float angle, float size) : base(x, y, angle, size)
        {
            Size = size;
            X = x;
            Y = y;
        }

        public override void Render(Graphics g)
        {
            g.FillEllipse(new SolidBrush(Color.Green), -20 * Size, -20 * Size, 40 * Size, 40 * Size);
            this.Size -= (float)0.01;
            if (Size <=0.02 )
            {
                Size = 1;
                onDeath(this);
            }
            

        }

        public override GraphicsPath GetGraphicsPath()
        {
            var path = base.GetGraphicsPath();
            path.AddEllipse(-20 * Size, -20 * Size, 40 * Size, 40 * Size);
            return path;
        }

        
    }
}
