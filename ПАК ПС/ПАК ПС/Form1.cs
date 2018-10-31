using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ПАК_ПС
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            g = panel.CreateGraphics();
            picture = new Picture();
            dict = new Dictionary<string, Creator>();
            dict["Rectangle"] = new RectangleCreator();
            dict["Ellipse"] = new EllipseCreator();
            dict["Group"] = new GroupCreator();
            dict["Select"] = null;
            creator = null;
          
        }
        int Nm = 0;
        Graphics g;
        Creator creator;
        Picture picture;
        int oldX;
        int oldY;
        bool CtrPress=false;
        Dictionary<string, Creator> dict;

        private void button_click(object sender, EventArgs e)
        {
            //My function for copy "creator " on object to Dictionary
            string nn = (sender as ToolStripButton).Text;
            if (dict.ContainsKey(nn))
                creator = dict[nn];
        }

        private void SelectTool_Click(object sender, EventArgs e)
        {
            creator = null;
        }
        private void RectangleTool_Click(object sender, EventArgs e)
        {
            creator = new RectangleCreator();
        }
        private void EllipseTool_Click(object sender, EventArgs e)
        {
            creator = new EllipseCreator();
        }
        private void GroupTool_Click(object sender, EventArgs e)
        {
            creator = new GroupCreator();
        }

        private void panel_MouseDown(object sender, MouseEventArgs e)
        {
            oldX = e.X;
            oldY = e.Y;
            
            if (creator != null)
            {
                Figure figure = creator.Create();
                figure.Move(e.X, e.Y);
                picture.Add(figure);
                figure.Draw(g);
            }
            else
            {
                Figure selected = picture.Select(e.X, e.Y);
                if (selected != null)
                {
                    checkBox1.CheckState = CheckState.Checked;
                    if (CtrPress)
                    {
                        picture.TmpGr.Add(selected);
                        picture.manipulator.Attach(picture.TmpGr);
                    }
                    else
                    {
                        if (picture.manipulator.selected != picture.TmpGr)
                            picture.TmpGr.Clear();
                    }
                }
                else
                {
                    picture.manipulator.Attach(null);
                    checkBox1.CheckState = CheckState.Unchecked;
                    picture.TmpGr.Clear();
                }


            }
            
            Refresh();
        }

        private void panel_SizeChanged(object sender, EventArgs e)
        {
            g = panel.CreateGraphics();
            if (picture != null) picture.Draw(g);
        }

        private void panel_Paint(object sender, PaintEventArgs e)
        {
            picture.Draw(e.Graphics);
        }

        private void panel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                picture.manipulator.Drag(e.X - oldX, e.Y - oldY);
                picture.manipulator.Update();

                Refresh();
            }
                oldX = e.X;
                oldY = e.Y;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void addToBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripButton Btn = new ToolStripButton();
            Btn.Text = "New "+Nm.ToString();
            Btn.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStrip1.Items.Add(Btn);
            Btn.Click += button_click;
            PrototypeCreator Cr = new PrototypeCreator();
            Cr.Prototype = picture.manipulator.selected.Clone();
            dict[Btn.Text] = Cr;
            Nm++;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode== Keys.ControlKey)
            {
                CtrPress = true;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey)
            {
                CtrPress = false;
            }
        }

        private void combineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrototypeCreator Cr = new PrototypeCreator();
            Cr.Prototype = picture.manipulator.selected.Clone();
            picture.Add(Cr.Prototype);
            picture.TmpGr.Clear();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

 
    }
}
