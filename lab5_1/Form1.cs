using lab5_1.Objects;

namespace lab5_1
{
    public partial class Form1 : Form
    {
        List<BaseObject> objects = new();
        Player player;
        Marker marker;
        CollectingPoint point1;
        CollectingPoint point2;
        BadArea area;
        Random rnd = new Random();
        public Form1()
        {
            InitializeComponent();
            
            player = new Player(pictureBox1.Width / 2, pictureBox1.Height / 2, 0, 0);
            area = new BadArea(rnd.Next(20, 480), rnd.Next(20, 480), 0, 1);
            objects.Add(area);
            player.OnOverlap += (p, obj) =>
            {
                txtLog.Text = $"[{DateTime.Now:HH:mm:ss:ff}] ????? ????????? ? {obj}\n" + txtLog.Text;
            };
            player.OnMarkerOverlap += (m) =>
            {
                objects.Remove(m);
                marker = null;
            };
            int pointCounter = 0;
            player.OnPointOverlap += (m) =>
            {
                pointCounter++;
                m.X = rnd.Next(20, 480);
                m.Y = rnd.Next(20, 480);
                m.Size = 1;
                txtPoints.Text = $"????: {pointCounter}";
            };
            area.OnPlayerOverlap += (m) =>
            {
                area.X = rnd.Next(20, 480);
                area.Y = rnd.Next(20, 480);
                area.Size = 1;
                pointCounter--;
                txtPoints.Text = $"????: {pointCounter}";
            };
            marker = new Marker(pictureBox1.Width / 2 + 50, pictureBox1.Height / 2 + 50, 0, 0);

            objects.Add(marker);
            objects.Add(player);
            point1 = new CollectingPoint(rnd.Next(20, 480), rnd.Next(20, 480), 0, 1);
            point2 = new CollectingPoint(rnd.Next(20, 480), rnd.Next(20, 480), 0, 1);
            objects.Add(point1);
            objects.Add(point2);
            updatePoints();
            

        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.Clear(Color.White);

            updatePlayer();
            foreach (var obj in objects.ToList())
            {
                if (obj != player && player.Overlaps(obj, g))
                {
                    player.Overlap(obj);
                    obj.Overlap(player);
                    
                }
            }
            foreach (var obj in objects)
            {
                g.Transform = obj.GetTransform();
                obj.Render(g);
            }
        }

        private void updatePlayer()
        {
            if (marker != null)
            {
                float dx = marker.X - player.X;
                float dy = marker.Y - player.Y;

                float length = MathF.Sqrt(dx * dx + dy * dy);
                dx /= length;
                dy /= length;

                player.vX += dx * 0.5f;
                player.vY += dy * 0.5f;

                player.Angle = 90 - MathF.Atan2(player.vX, player.vY) * 180 / MathF.PI;
            }

            player.vX += -player.vX * 0.1f;
            player.vY += -player.vY * 0.1f;

            player.X += player.vX;
            player.Y += player.vY;
        }

        private void updatePoints()
        {
            point1.onDeath += onPointDeath;
            point2.onDeath += onPointDeath;

        }
        private void onPointDeath (CollectingPoint m)
        {
            m.X = rnd.Next(20, 480);
            m.Y = rnd.Next(20, 480);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
            pictureBox1.Invalidate();
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (marker == null)
            {
                marker = new Marker(0, 0, 0, 0);
                objects.Add(marker);
            }
            marker.X = e.X;
            marker.Y = e.Y;
        }
    }
}