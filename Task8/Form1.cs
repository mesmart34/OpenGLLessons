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
    }
}
