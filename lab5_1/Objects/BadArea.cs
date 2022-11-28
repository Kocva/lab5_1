using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab5_1.Objects
{
    internal class BadArea : BaseObject
    {
        public Action<Player> OnPlayerOverlap;
        Random rnd = new Random();


        public BadArea(float x, float y, float angle, float size) : base(x, y, angle, size)
        {
            Size = size;
            X = x;
            Y = y;
        }
        public override void Render(Graphics g)
        {
            var color = Color.FromArgb(50, Color.Red);
            var b = new SolidBrush(color);
            g.FillEllipse(b, -1 * Size, -1 * Size, 2 * Size, 2 * Size);
            this.Size += 1;
        }
        public override GraphicsPath GetGraphicsPath()
        {
            var path = base.GetGraphicsPath();
            path.AddEllipse(-1 * Size, -1 * Size, 2 * Size, 2 * Size);
            return path;
        }
        public override void Overlap(BaseObject obj)
        {
            base.Overlap(obj);
            if (obj is Player)
            {
                OnPlayerOverlap(obj as Player);
            }
        }
    }
}
