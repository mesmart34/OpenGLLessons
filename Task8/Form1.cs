using OpenTK;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Task8
{
    public partial class Form1 : Form
    {
        private Canvas _canvas;
        private Raytracing _raytracing;

        public Form1()
        {
            InitializeComponent();
            _canvas = new Canvas(pictureBox.Width, pictureBox.Height);
            _raytracing = new Raytracing();
            pictureBox.Image = _raytracing.RenderToCanvas(_canvas);
        }

        private void lightTrackBar_Scroll(object sender, EventArgs e)
        {
            foreach (var light in _raytracing._scene.GetLights())
            {
                light.Intensity = ((TrackBar)sender).Value / 100.0f;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pictureBox.Image = _raytracing.RenderToCanvas(_canvas);
        }
    }
}
