using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing.Drawing2D;

namespace SebisControls {
    public partial class MyImageViewer : UserControl {
        private float _currScale = 1.0f;
        private Image _currentImage = null;
        private Point _mouseLocation = new Point(-1, -1);
        private Point _mouseClickLocation = new Point(-1, -1);
        private bool _isMouseDown = false;
        private Timer drawTimer = new Timer();

        public MyImageViewer() {
            InitializeComponent();

            drawTimer.Interval = 10;
            drawTimer.Tick += OnDrawTimer;
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            currentImageViewer.Size = new Size(0, 0);
        }

        public void LoadImage(Image image, object tag) {
            try {
                _currentImage = image;
                currentImageViewer.Image = _currentImage;
                currentImageViewer.Tag = tag;
                //origSizeButton_Click(null, null);
                fitWidthButton_Click(null, null);
            }
            catch (Exception ex) {
                MessageBox.Show(this, "Exception while loading image", ex.Message,
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                Trace.WriteLine("Exception while loading image: " + ex.Message);
            }
        }

        public void UnloadCurrentImage() {
            if (_currentImage != null) {
                //_currentImage.Dispose();
                _currentImage = null;
            }
            if (currentImageViewer.Image != null) {
                //currentImageViewer.Image.Dispose();
                currentImageViewer.Image = null;
            }

            currentImageViewer.Tag = null;
            selectedImageLabel.Text = "";
        }

        public object getImageTag() {
            return currentImageViewer.Tag;
        }

        public void setLabelText(string text) {
            selectedImageLabel.Text = text;
        }

        private void updatePictureBoxSize() {
            //Console.WriteLine("updateScrollBars, size of picture box before = " + currentImageViewer.Size + " scale = " + _currScale);
            currentImageViewer.Width = (int)Math.Round((double)_currentImage.Width * _currScale);
            currentImageViewer.Height = (int)Math.Round((double)_currentImage.Height * _currScale);
            //-SystemInformation.HorizontalScrollBarHeight;

            if (currentImageViewer.Width < imagePanel.ClientSize.Width) {
                currentImageViewer.Width = imagePanel.ClientSize.Width;
            }
            if (currentImageViewer.Height < imagePanel.ClientSize.Height) {
                currentImageViewer.Height = imagePanel.ClientSize.Height;
            }

        }

        private void zoomInButton_Click(object sender, EventArgs e) {
            //currentImageViewer.SizeMode = PictureBoxSizeMode.AutoSize;
            const float step = 0.1f;
            _currScale *= (1 + step);
            updatePictureBoxSize();

            //DrawStuff(buffGrafx.Graphics);
            currentImageViewer.Refresh();
        }

        private void zoomOutButton_Click(object sender, EventArgs e) {
            //currentImageViewer.SizeMode = PictureBoxSizeMode.AutoSize;
            const float step = 0.1f;
            _currScale *= (1 - step);
            updatePictureBoxSize();

            //DrawStuff(buffGrafx.Graphics);
            currentImageViewer.Refresh();
        }

        //private void currentImageViewer_Wheel(object sender, MouseEventArgs e)
        //{
        //    ((HandledMouseEventArgs)e).Handled = true;

        //    const float step = 0.1f;
        //    Console.WriteLine("sign delta = "+Math.Sign(e.Delta));

        //    _currScale *= (1 + step*Math.Sign(e.Delta));
        //    Console.WriteLine("currScale = " + _currScale);

        //    //DrawStuff(buffGrafx.Graphics);
        //    currentImageViewer.Refresh();
        //}

        private void fit2PageButton_Click(object sender, EventArgs e) {
            if (_currentImage == null) return;

            //float hp = imagePanel.Height - currentImageViewer.Location.Y;
            float hp = imagePanel.Height;
            float wp = imagePanel.Width;
            //currentImageViewer.Height = (int)hp;
            //currentImageViewer.Width = (int)wp;

            if (wp > hp)
                _currScale = hp / _currentImage.Height;
            else
                _currScale = wp / _currentImage.Width;

            updatePictureBoxSize();

            //Console.WriteLine("impanel height = " + imagePanel.Height + " loc = " + currentImageViewer.Location.Y);
            //Console.WriteLine("fit2Page, currScale = " + currScale);

            //DrawStuff(buffGrafx.Graphics);


            imagePanel.Refresh();
            currentImageViewer.Refresh();
        }

        private void fitWidthButton_Click(object sender, EventArgs e) {
            if (_currentImage == null) return;
            float wp = imagePanel.Width;
            _currScale = wp / _currentImage.Width;
            // have to take into account the size of the scrollbar if it will appear:
            if ((int)(_currScale * _currentImage.Height) > imagePanel.Height) {
                wp -= SystemInformation.VerticalScrollBarWidth;
                _currScale = wp / (_currentImage.Width);
            }

            updatePictureBoxSize();

            //DrawStuff(buffGrafx.Graphics);
            currentImageViewer.Refresh();
        }

        private void fitHeightButton_Click(object sender, EventArgs e) {
            if (_currentImage == null) return;
            float hp = imagePanel.Height;
            _currScale = hp / _currentImage.Height;

            // have to take into account the size of the scrollbar if it will appear:
            if ((int)(_currScale * _currentImage.Width) > imagePanel.Width) {
                hp -= SystemInformation.HorizontalScrollBarHeight;
                _currScale = hp / (_currentImage.Height);
            }

            updatePictureBoxSize();

            //DrawStuff(buffGrafx.Graphics);
            currentImageViewer.Refresh();
        }

        private void origSizeButton_Click(object sender, EventArgs e) {
            _currScale = 1;
            updatePictureBoxSize();

            //DrawStuff(buffGrafx.Graphics);
            currentImageViewer.Refresh();
        }

        //private void currentImageViewer_LoadCompleted(object sender, AsyncCompletedEventArgs e)
        //{
        //    UpdateSelectedImageInfo();
        //}

        private void loupeCb_CheckedChanged(object sender, EventArgs e) {
            //if (!HasCurrentImage()) return;

            if (loupeCb.Checked) {
                //_currentImage = Image.FromFile(GetCurrentImagePath());
                //currentImageViewer.Image = _currentImage;

                drawTimer.Start();
            }
            else {
                drawTimer.Stop();
            }
        }

        private void OnDrawTimer(object sender, EventArgs e) {
            //Console.WriteLine("onDrawTimer!");
            var sw = Stopwatch.StartNew();
            //DrawStuff(buffGrafx.Graphics);
            currentImageViewer.Refresh();
            sw.Stop();
            //Console.WriteLine("Elapsed time of drawstuff: " + sw.Elapsed);
            //currentImageViewer.Refresh();
        }

        private void currentImageViewer_Paint(object sender, PaintEventArgs e) {
            //imagePanel.Refresh();
            //return;

            Console.WriteLine("currentImageViewer_Paint, size of picture box = " + currentImageViewer.Size);

            var g = e.Graphics;
            g.CompositingMode = CompositingMode.SourceCopy;
            g.PixelOffsetMode = PixelOffsetMode.None;
            g.CompositingQuality = CompositingQuality.AssumeLinear;
            g.InterpolationMode = InterpolationMode.NearestNeighbor;
            g.SmoothingMode = SmoothingMode.None;

            g.Clear(currentImageViewer.BackColor);

            //Console.WriteLine("currentImageViewer_Paint");

            if (_currentImage == null) return;
            //Console.WriteLine("currentImageViewer_Paint2");


            // set resolution of image to display resolution to display at correct size:
            var bm = _currentImage as Bitmap;
            bm.SetResolution(g.DpiX, g.DpiY);

            g.ScaleTransform(_currScale, _currScale);

            //var visRect = new RectangleF(imagePanel.HorizontalScroll.Value / _currScale, imagePanel.VerticalScroll.Value / _currScale, imagePanel.Width / _currScale, imagePanel.Height / _currScale);
            var visRect = new Rectangle((int)(imagePanel.HorizontalScroll.Value / _currScale), (int)(imagePanel.VerticalScroll.Value / _currScale), (int)(imagePanel.Width / _currScale), (int)(imagePanel.Height / _currScale));

            Console.WriteLine("hp = " + imagePanel.HorizontalScroll.Value + " vp = " + imagePanel.VerticalScroll.Value);

            //g.DrawImage(_currentImage, 0, 0, visRect, GraphicsUnit.Pixel);

            g.DrawImage(_currentImage, 0, 0);
            //g.DrawImageUnscaled(_currentImage, 0, 0);

            //g.DrawImage(bm.GetHbitmap());



            //textureBrush.ResetTransform();
            //textureBrush.ScaleTransform(_currScale, _currScale);
            //g.FillRectangle(textureBrush, 0, 0, _currentImage.Width, _currentImage.Height);

            //g.DrawImageUnscaledAndClipped(_currentImage, visRect);
            var pen = new Pen(new SolidBrush(Color.Black));
            g.DrawRectangle(pen, new Rectangle(0, 0, _currentImage.Width, _currentImage.Height));

            // draw loupe image if button down:
            if (loupeCb.Checked && _mouseLocation.X >= 0 && _mouseLocation.Y >= 0) {
                // the size of the selection rectangle for the loupe (unscaled!):
                const int selRectWidth = 300;
                const int selRectHeight = 200;
                // selection rectangle and image rectangle:
                var selectionRect = new Rectangle(_mouseLocation.X, _mouseLocation.Y, (int)(selRectWidth * _currScale), (int)(selRectHeight * _currScale));
                var imageRect = new Rectangle((int)(_mouseLocation.X / _currScale), (int)(_mouseLocation.Y / _currScale), selRectWidth, selRectHeight);
                // panel size:
                int hp = imagePanel.Height - SystemInformation.HorizontalScrollBarHeight;
                int wp = imagePanel.Width - SystemInformation.VerticalScrollBarWidth;
                // determine if stuff has to be drawn right and/or down and modify rectangle accordingly:
                int biggerWith = (selectionRect.Width > imageRect.Width) ? selectionRect.Width : imageRect.Width;
                int totalHeight = (selectionRect.Height + imageRect.Height);
                bool drawRight = (_mouseLocation.X + biggerWith <= wp);
                bool drawDown = (_mouseLocation.Y + totalHeight <= hp);
                int drawingPosX = _mouseLocation.X;
                int drawingPoxY = _mouseLocation.Y + selectionRect.Height;

                if (!drawRight) {
                    selectionRect.X -= selectionRect.Width;
                    imageRect.X -= imageRect.Width;
                    drawingPosX -= imageRect.Width;
                }
                if (!drawDown) {
                    selectionRect.Y -= selectionRect.Height;
                    imageRect.Y -= imageRect.Height;
                    drawingPoxY = selectionRect.Y - imageRect.Height;
                }
                // reset similarity transform (ie scale) and draw selection rectangle, and the image under the loupe:
                g.ResetTransform();
                g.DrawRectangle(new Pen(new SolidBrush(Color.Red)), selectionRect);
                if (_isMouseDown || true) {
                    g.DrawImage(_currentImage, drawingPosX, drawingPoxY, imageRect, GraphicsUnit.Pixel);
                    g.DrawRectangle(pen, drawingPosX, drawingPoxY, imageRect.Width, imageRect.Height);
                }
            } // end if loupe
        }

        private void currentImageViewer_MouseEnter(object sender, EventArgs e) {
            //Console.WriteLine("focusing");
            //currentImageViewer.Focus(); // needed ??? FIXME --> RESETS SCROLLBARS TO ZERO!!
            imagePanel.Focus();
        }

        private void currentImageViewer_MouseLeave(object sender, EventArgs e) {
            _mouseLocation = new Point(-1, -1);
        }

        private void currentImageViewer_MouseMove(object sender, MouseEventArgs e) {
            if (_currentImage == null) return;

            if (_isMouseDown) {
                imagePanel.AutoScrollPosition = new Point(-imagePanel.AutoScrollPosition.X + _mouseClickLocation.X - e.X,
                    -imagePanel.AutoScrollPosition.Y + _mouseClickLocation.Y - e.Y);
            }

            _mouseLocation = new Point(e.X, e.Y);
        }

        private void currentImageViewer_MouseDown(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                Console.WriteLine("moving down: " + e.Location);
                _mouseClickLocation = e.Location;
                _isMouseDown = true;
            }

            //Console.WriteLine("isMouseDown = " + isMouseDown);
        }

        private void currentImageViewer_MouseUp(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                _isMouseDown = false;
                _mouseClickLocation = new Point(-1, -1);
            }

            //Console.WriteLine("isMouseDown = " + isMouseDown);
        }
    }
}
