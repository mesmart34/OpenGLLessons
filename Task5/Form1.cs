using System.Drawing.Imaging;

namespace Task5
{
    struct Pixel
    {
        public Pixel(Color color, float brightness)
        {
            this.color = color;
            this.brightness = brightness;
        }

        public Color color;
        public float brightness;
    }

    public partial class Form1 : Form
    {
        private Bitmap image;
        private List<Pixel> pixels;

        public Form1()
        {
            InitializeComponent();
            Width = 800;
            Height = 800;
            image = LoadPicture("../../../texture.jpg");
            pictureBox1.Image = image;
            pixels = GetProcessedPixels(image);
            ProcessPicture();
        } 

        private Bitmap LoadPicture(string path)
        {
            return (Bitmap)Image.FromFile(path);
        }

        private List<Pixel> GetProcessedPixels(Bitmap image)
        {
            var result = new List<Pixel>();
            for(var x = 0; x < image.Width; x++)
            {
                for (var y = 0; y < image.Height; y++)
                {
                    var color = image.GetPixel(x, y);
                    var brightness = GetColorBrightness(color);
                    result.Add(new Pixel(color, brightness));
                }
            }
            return result;
        }

        private float GetColorBrightness(Color color)
        {
            return (color.R + color.G + color.B) / 3.0f;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var x = int.Parse(textBox1.Text);
            var y = int.Parse(textBox2.Text);
            if (x < 0 || y < 0 || x > image.Width || y > image.Height)
                return;
            var data = pixels[x + y * pictureBox1.Width];
            label3.Text = data.color.ToString();
            label4.Text = "Brightness: " + data.brightness.ToString();
        }

        private void ProcessPicture()
        {
            var bitmap = new Bitmap(image.Width, image.Height, PixelFormat.Format24bppRgb);
            for(var i = 0; i < pixels.Count; i++)
            {
                var x = i % image.Width;
                var y = i / image.Width;
                var color = Color.FromArgb(255 - pixels[i].color.A, 255 - (pixels[i].color.R / 2), 255 - pixels[i].color.G, 255 - pixels[i].color.B);
                bitmap.SetPixel(x, y, color);
            }
            pictureBox2.Image = bitmap;
        }
    }
}