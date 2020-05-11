using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace uMouseMove
{
    public partial class frmTimer : Form
    {
        public frmTimer()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true; // Arranco el timer

            btnInit.Enabled = false;
            btnStop.Enabled = true;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false; // Freno el timer

            btnInit.Enabled = true;
            btnStop.Enabled = false;
        }

        private void MoveCursor()
        {
            Random r = new Random();
            int rInt = r.Next(1000,2000); //for ints
            int range = 150;
            double dPosicion = r.NextDouble() * range;
            int iPosicion = (int)dPosicion;

            this.Cursor = new Cursor(Cursor.Current.Handle);

            if (Cursor.Position.Y == 0)
            {
                //Cursor.Position = new Point(iPosicion, 0);
                uMouseMove.MouseOperations.SetCursorPosition(iPosicion, 0);
                uMouseMove.MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftDown);
                uMouseMove.MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftUp);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            MoveCursor();
        }

    }
}
