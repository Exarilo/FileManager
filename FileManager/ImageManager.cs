using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



//https://epochabuse.com/image-tone/
namespace FileManager
{
    class ImageManager : ManageFile, IFile
    {
        private Bitmap originalBitmap;
        private Bitmap bitmapCopy;
        private Color[,] pixelsColor;

        public ImageManager(Bitmap originalBitmap,Form form) :base(form)
        {
            this.form = form;
            this.originalBitmap = originalBitmap;
            bitmapCopy = (Bitmap)originalBitmap.Clone();
            pixelsColor = new Color[originalBitmap.Width, originalBitmap.Height];
            
            //pixelsColor = getPixels(originalBitmap);
        }

        #region Usefull
        private Color[,] getPixels(Bitmap bitmap)
        {

            Color[,] tabPixel = new Color[bitmap.Width, bitmap.Height];
            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    Color pixel = bitmap.GetPixel(i, j);
                    tabPixel[i, j] = pixel;
                }
            }
            return tabPixel;
        }
        private Bitmap createBitmap(Color[,] tabColor)
        {
            Bitmap bitmap = new Bitmap(tabColor.GetLength(0), tabColor.GetLength(1));
            for (int i = 0; i < tabColor.GetLength(0); i++)
            {
                for (int j = 0; j < tabColor.GetLength(1); j++)
                {
                    bitmap.SetPixel(i, j, tabColor[i, j]);
                }
            }
            return bitmap;
        }
        #endregion

        #region NoAction
        private void Rotate()
        {
            //Bitmap img = createBitmap(pixelsColor);

            bitmapCopy.RotateFlip(RotateFlipType.Rotate90FlipNone);
            //pixelsColor = getPixels(img);
            UpdateMainComponent(bitmapCopy);
        }
        public void Binarizeimg()
        {
            //Bitmap img = createBitmap(pixelsColor);

            Color curColor;
            int ret;
            for (int iX = 0; iX < bitmapCopy.Width; iX++)
            {
                for (int iY = 0; iY < bitmapCopy.Height; iY++)
                {
                    curColor = bitmapCopy.GetPixel(iX, iY);
                    ret = (int)(curColor.R * 0.299 + curColor.G * 0.578 + curColor.B * 0.114);
                    if (ret > 150)
                    {
                        ret = 0;
                    }
                    else
                    {
                        ret = 255;
                    }
                    bitmapCopy.SetPixel(iX, iY, Color.FromArgb(ret, ret, ret));
                }
            }
            //pixelsColor = getPixels(bitmapCopy);
            UpdateMainComponent(bitmapCopy);
        }
        public void Invert()
        {
            //Bitmap img = createBitmap(pixelsColor);
            Color c;
            for (int i = 0; i < bitmapCopy.Width; i++)
            {
                for (int j = 0; j < bitmapCopy.Height; j++)
                {
                    c = bitmapCopy.GetPixel(i, j);
                    bitmapCopy.SetPixel(i, j, Color.FromArgb(255 - c.R, 255 - c.G, 255 - c.B));
                }
            }
            //pixelsColor = getPixels(bitmapCopy);
            UpdateMainComponent(bitmapCopy);
        }
        public void ImageSmooth()
        {
            //Bitmap image = createBitmap(pixelsColor);
            int w = bitmapCopy.Width;
            int h = bitmapCopy.Height;
            BitmapData image_data = bitmapCopy.LockBits(
                new Rectangle(0, 0, w, h),
                ImageLockMode.ReadOnly,
                PixelFormat.Format24bppRgb);
            int bytes = image_data.Stride * image_data.Height;
            byte[] buffer = new byte[bytes];
            byte[] result = new byte[bytes];
            Marshal.Copy(image_data.Scan0, buffer, 0, bytes);
            bitmapCopy.UnlockBits(image_data);
            for (int i = 2; i < w - 2; i++)
            {
                for (int j = 2; j < h - 2; j++)
                {
                    int p = i * 3 + j * image_data.Stride;
                    for (int k = 0; k < 3; k++)
                    {
                        List<int> vals = new List<int>();
                        for (int xkernel = -2; xkernel < 3; xkernel++)
                        {
                            for (int ykernel = -2; ykernel < 3; ykernel++)
                            {
                                int kernel_p = k + p + xkernel * 3 + ykernel * image_data.Stride;
                                vals.Add(buffer[kernel_p]);
                            }
                        }
                        result[p + k] = (byte)(vals.Sum() / vals.Count);
                    }
                }
            }
            Bitmap res_img = new Bitmap(w, h);
            BitmapData res_data = res_img.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);
            Marshal.Copy(result, 0, res_data.Scan0, bytes);
            //pixelsColor = getPixels(bitmapCopy);
            UpdateMainComponent(bitmapCopy);
        }


        public void LightCorrection()
        {
            //Bitmap image = createBitmap(pixelsColor);
            int w = bitmapCopy.Width;
            int h = bitmapCopy.Height;
            BitmapData image_data = bitmapCopy.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            int bytes = image_data.Stride * image_data.Height;
            byte[] buffer = new byte[bytes];
            byte[] result = new byte[bytes];
            Marshal.Copy(image_data.Scan0, buffer, 0, bytes);
            bitmapCopy.UnlockBits(image_data);
            double y = 2;
            for (int i = 0; i < bytes; i++)
            {
                double normalized = (double)buffer[i] / 255;
                result[i] = (byte)(Math.Pow(normalized, y) * 255);
            }
            Bitmap res_img = new Bitmap(w, h);
            BitmapData res_data = res_img.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);
            Marshal.Copy(result, 0, res_data.Scan0, bytes);
            res_img.UnlockBits(res_data);
            //pixelsColor = getPixels(res_img);
            UpdateMainComponent(res_img);
        }


        public void MidPoint()
        {
            int w = bitmapCopy.Width;
            int h = bitmapCopy.Height;

            BitmapData image_data = bitmapCopy.LockBits(
                new Rectangle(0, 0, w, h),
                ImageLockMode.ReadOnly,
                PixelFormat.Format24bppRgb);
            int bytes = image_data.Stride * image_data.Height;
            byte[] buffer = new byte[bytes];
            Marshal.Copy(image_data.Scan0, buffer, 0, bytes);
            bitmapCopy.UnlockBits(image_data);

            int r = 1;
            int wres = w - 2 * r;
            int hres = h - 2 * r;

            Bitmap result_image = new Bitmap(wres, hres);
            BitmapData result_data = result_image.LockBits(
                new Rectangle(0, 0, wres, hres),
                ImageLockMode.WriteOnly,
                PixelFormat.Format24bppRgb);
            int res_bytes = result_data.Stride * result_data.Height;
            byte[] result = new byte[res_bytes];
            for (int x = r; x < w - r; x++)
            {
                for (int y = r; y < h - r; y++)
                {
                    int pixel_location = x * 3 + y * image_data.Stride;
                    int res_pixel_loc = (x - r) * 3 + (y - r) * result_data.Stride;
                    double[] median = new double[3];
                    byte[][] neighborhood = new byte[3][];

                    for (int c = 0; c < 3; c++)
                    {
                        neighborhood[c] = new byte[(int)Math.Pow(2 * r + 1, 2)];
                        int added = 0;
                        for (int kx = -r; kx <= r; kx++)
                        {
                            for (int ky = -r; ky <= r; ky++)
                            {
                                int kernel_pixel = pixel_location + kx * 3 + ky * image_data.Stride;
                                neighborhood[c][added] = buffer[kernel_pixel + c];
                                added++;
                            }
                        }
                    }

                    for (int c = 0; c < 3; c++)
                    {
                        result[res_pixel_loc + c] = (byte)((neighborhood[c].Min() + neighborhood[c].Max()) / 2);
                    }
                }
            }

            Marshal.Copy(result, 0, result_data.Scan0, res_bytes);
            result_image.UnlockBits(result_data);
            UpdateMainComponent(result_image);
        }

        #endregion

        #region ActionRGB
        public void FilterRGB(int value, string tag)
        {

            //Bitmap img = createBitmap(pixelsColor);
            Color c;
            for (int i = 0; i < bitmapCopy.Width; i++)
            {
                for (int j = 0; j < bitmapCopy.Height; j++)
                {
                    c = bitmapCopy.GetPixel(i, j);
                    int cR = c.R;
                    int cG = c.G;
                    int cB = c.B;

                    switch (tag)
                    {
                        case "Red":
                            cR = value;
                            break;

                        case "Green":
                            cG = value;
                            break;

                        case "Blue":
                            cB = value;
                            break;

                        default:
                            break;
                    }

                    if (cR < 0) cR = 10;
                    if (cR > 255) cR = 245;

                    if (cG < 0) cG = 10;
                    if (cG > 255) cG = 245;

                    if (cB < 0) cB = 10;
                    if (cB > 255) cB = 255;
                    bitmapCopy.SetPixel(i, j, Color.FromArgb((byte)cR, (byte)cG, (byte)cB));
                }
            }

            //pixelsColor = getPixels(img);
            UpdateMainComponent(bitmapCopy);
        }
        #endregion

        #region ActionValue
        public void SetContrast(double contrast)
        {
            //Bitmap img = createBitmap(pixelsColor);
            if (contrast < -100) contrast = -100;
            if (contrast > 100) contrast = 100;
            contrast = (100.0 + contrast) / 100.0;
            contrast *= contrast;
            Color c;
            for (int i = 0; i < bitmapCopy.Width; i++)
            {
                for (int j = 0; j < bitmapCopy.Height; j++)
                {
                    c = bitmapCopy.GetPixel(i, j);
                    double pR = c.R / 255.0;
                    pR -= 0.5;
                    pR *= contrast;
                    pR += 0.5;
                    pR *= 255;
                    if (pR < 0) pR = 0;
                    if (pR > 255) pR = 255;

                    double pG = c.G / 255.0;
                    pG -= 0.5;
                    pG *= contrast;
                    pG += 0.5;
                    pG *= 255;
                    if (pG < 0) pG = 0;
                    if (pG > 255) pG = 255;

                    double pB = c.B / 255.0;
                    pB -= 0.5;
                    pB *= contrast;
                    pB += 0.5;
                    pB *= 255;
                    if (pB < 0) pB = 0;
                    if (pB > 255) pB = 255;

                    bitmapCopy.SetPixel(i, j, Color.FromArgb((byte)pR, (byte)pG, (byte)pB));
                }
            }
            //pixelsColor = getPixels(bitmapCopy);
            UpdateMainComponent(bitmapCopy);
        }
        #endregion

        #region SettingsMethods
        public void FilterRGBSettings()
        {
            try
            {
                Panel panel = form.Controls.Find("panelBot", true).FirstOrDefault() as Panel;
                CustomRGBSettings RGB = new CustomRGBSettings();
                RGB.addToPanel(panel);
                RGB.addAction(FilterRGB);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }
        public void ContrastSettings()
        {
            try
            {
                Panel panel = form.Controls.Find("panelBot", true).FirstOrDefault() as Panel;
                CustomValueSettings RGB = new CustomValueSettings();
                RGB.addToPanel(panel);
                RGB.addAction(SetContrast);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        #endregion

        public override void FillPanelWithControl(Panel panel, object o)
        {
            base.FillPanelWithControl(panel, o);
            if (o.GetType().Equals(typeof(Bitmap)))
            {
                PictureBox pictureBox = new PictureBox();
                pictureBox.Dock = DockStyle.Fill;
                pictureBox.Image?.Dispose();
                pictureBox.Image = (Bitmap)(o as Bitmap).Clone();
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                panel.Controls.Add(pictureBox);
                mainComponent = pictureBox;
            }
        }
        public override void createButtons(Panel panel)
        {
            base.createButtons(panel);

            CustomButton btBinarize = new CustomButton("Binarize", Binarizeimg);
            CustomButton btInvert = new CustomButton("Inverse", Invert);
            CustomButton btRotate = new CustomButton("Rotate", Rotate);
            CustomButton btFilterRGB = new CustomButton("Filter RGB", FilterRGBSettings);
            CustomButton btContrast = new CustomButton("Contrast", ContrastSettings);
            CustomButton btSmooth = new CustomButton("Smoothing", ImageSmooth);
            CustomButton btLight = new CustomButton("Light Correction", LightCorrection);
            CustomButton btMidPoint = new CustomButton("Mid Point", MidPoint);

            listButtons.Add(btBinarize);
            listButtons.Add(btInvert);
            listButtons.Add(btRotate);
            listButtons.Add(btFilterRGB);
            listButtons.Add(btContrast);
            listButtons.Add(btSmooth);
            listButtons.Add(btLight);
            listButtons.Add(btMidPoint);

            base.setButtonLocation(panel);
        }
    }
}
