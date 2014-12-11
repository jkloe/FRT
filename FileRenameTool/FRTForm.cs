using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using Timer = System.Windows.Forms.Timer;
using SebisControls;

//using Timer = System.Windows.Forms.Timer;

namespace FileRenameTool
{
    public delegate void AddThumbnailDelegate(PictureBox pBox);

    public partial class FRTForm : Form
    {
        public class ThumbnailJob
        {
            public static readonly int DEFAULT_WIDTH = 200;

            public string FileIn;
            public string FileOut;
            public int width = DEFAULT_WIDTH;
        }

        public class ImageData
        {
            public ImageData(string filename, string thumbfilename, int globalIndex, int localIndex, string date, string issueSpecSuffix, bool isTitlepage)
            {
                Filename = filename;
                ThumbFilename = thumbfilename;
                GlobalIndex = globalIndex;
                LocalIndex = localIndex;
                Date = date;
                IssueSpecificSuffix = issueSpecSuffix;
                
                IsTitlepage = isTitlepage;
                Image = null;
            }

            public int GlobalIndex { get; set; }
            public int LocalIndex { get; set; }
            public string Date { get; set; }
            public string IssueSpecificSuffix { get; set; }
            public string Filename { get; set; }
            public string ThumbFilename { get; set; }
            public bool IsTitlepage { get; set; }
            public Image Image { get; set; }

            public new string ToString()
            {
                return "ImageData: LocalIndex = " + LocalIndex + "GlobalIndex = " + GlobalIndex + ", Date = " + Date + ", Filename = " + Filename +
                       ", isTitlepage = " + IsTitlepage;
            }
        }

        //private string imConvertPath = "";
        List<ThumbnailJob>  _thmbJobs=new List<ThumbnailJob>();
        Stopwatch sw = new Stopwatch();
        private const string TITLE = "File-Rename-Tool";
        private const string VERSION = "1.0.3";
        private const string DATE_FORMAT = "ddd.dd.MM.yyyy";
        private const string DATE_NA_STRING = "Date N/A";
        private const int PAGING_OFFSET = 200;

        private static readonly string[] INPUT_EXTS = new string[] {".jpg", ".tif", ".tiff", ".png"};
        private bool _doThmbUpdate = true;
        private bool _isPageReloading = false;

        private float _currScale = 1.0f;
        //private Image img = null;
        private Image _currentImage = null;
        private TextureBrush textureBrush = null;
        //private Bitmap img = new Bitmap("adfad");

        private Point _mouseLocation = new Point(-1,-1);
        private Point _mouseDownLocation = new Point(-1, -1);
        
        private bool _isMouseDown = false;
        //private List<ImageData> _imDatList = new List<ImageData>();

        private List<ImageData> _imDatList = new List<ImageData>();
        

        //private Dictionary<string, ImageAndData> _thumbsCache = new Dictionary<string, ImageAndData>();
        private int _currentPage = -1;

        private BackgroundWorker _loadThmbImgsBackgroundWorker = new BackgroundWorker();
        private bool _restartThmbImgsBackgroundWorker = false;

        private BackgroundWorker _imCacheWorker = new BackgroundWorker();
        private bool _restartImCacheWorker = false;
        private ConcurrentDictionary<string, Image> _imCache = new ConcurrentDictionary<string, Image>();
        public static ManualResetEvent _imCacheCancelledEvent = new ManualResetEvent(false);
        private const int IM_CACHE_LIMIT = 5; // nr of neighbors the imcacher loads
        private const int IM_CACHE_OVERHEAD = 5; // nr of overheader images for the imcacher

        private ToolTip _fnToolTip = new ToolTip();

        private bool isDoubleClicked = false;
        private Timer doubleClickTimer = new Timer();

        public FRTForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //loadImage();
            Init();
        }

        private void Init()
        {
            Text = TITLE + ", v. " + VERSION;
            if (IntPtr.Size == 4)
                Text += ", 32 bit version";
            else
                Text += ", 64 bit version";


            SetGraphicsMagickPath();
            thumbnailFilesFormat.SelectedIndex = 0;
            thumbsWidthCb.SelectedIndex = 2;
            
            infoLabel.Text = "";
            summaryLabel.Text = "";
            warningLabel.Text = "";

            thmbSizeCb.Items.Clear();
            for (int i = 25; i < 500; i+=25 )
            {
                thmbSizeCb.Items.Add(i);                    
            }
            thmbSizeCb.SelectedItem = 125;

            flowLayoutPanel1.MouseWheel += flowLayoutPanel1_Wheel;
            saveWorker.RunWorkerCompleted += saveWorker_Completed;
            useInputFolderButton_Click(this, null);
            //currentImageViewer.MouseWheel += currentImageViewer_Wheel;
            //imagePanel.MouseWheel += currentImageViewer_Wheel;
            
            SetIsThumbnailing(false);
            SetIsSaving(false);

            _loadThmbImgsBackgroundWorker.WorkerReportsProgress = true;
            _loadThmbImgsBackgroundWorker.WorkerSupportsCancellation = true;
            _loadThmbImgsBackgroundWorker.DoWork += LoadThmbImgsDoWork;
            _loadThmbImgsBackgroundWorker.ProgressChanged += LoadThmbImgsProgress;
            _loadThmbImgsBackgroundWorker.RunWorkerCompleted += LoadThmbImgsFinished;

            _imCacheWorker.WorkerReportsProgress = true;
            _imCacheWorker.WorkerSupportsCancellation = true;
            _imCacheWorker.DoWork += ImCacherDoWork;
            _imCacheWorker.ProgressChanged += ImCacherProgress;
            _imCacheWorker.RunWorkerCompleted += ImCacherFinished;


            inputFolderTb.TabIndex = 0;
            inputFolderTb.Focus();

            this.UpdateStyles();

            //buffContext = BufferedGraphicsManager.Current;
            //buffContext.MaximumBuffer = new Size(currentImageViewer.Width+1, currentImageViewer.Height+1);
            //buffGrafx = buffContext.Allocate(currentImageViewer.CreateGraphics(),
            //                 new Rectangle(0, 0, currentImageViewer.Width, currentImageViewer.Height));

            //DrawStuff(buffGrafx.Graphics);

            // test: load default dir:
            //inputFolderTb.Text = @"E:\Testdata\FLTToolTestdata\Kitzbuehler_Nachrichten_1940";
            //LoadCurrentDirectory(true);
        }

        private void inputFolderTb_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoadCurrentDirectory();
            }
            else if (e.KeyCode == Keys.F2) // load some test data
            {
                //inputFolderTb.Text = @"E:\Testdata\FLTToolTestdata\Kitzbuehler_Nachrichten_1940";
                inputFolderTb.Text = @"X:\tmp_sebastian\FRT\Kitzbuehler_Nachrichten_1940_first20";
                LoadCurrentDirectory();
            }

        }

        private void FRTForm_KeyUp(object sender, KeyEventArgs e)
        {
            ////Console.WriteLine("key press");
            //if (e.Shift && e.KeyCode == Keys.F1) // activate admin mode
            //{
            //    inputFolderTb.Text = @"E:\Testdata\FLTToolTestdata\Kitzbuehler_Nachrichten_1940_dummy";
            //    LoadCurrentDirectory(true);
            //    loupeCb.Visible = !loupeCb.Visible;
            //}
            //e.Handled = true;
        }

        //private void InitDirect3D()
        //{
            

        //    PresentParameters present_params = new PresentParameters();

        //    present_params.Windowed = true;
        //    present_params.SwapEffect = SwapEffect.Discard;



        //    m_device = new Device(0, DeviceType.Hardware, this,
        //                          CreateFlags.SoftwareVertexProcessing, present_params);


        //}

        private void SetGraphicsMagickPath()
        {
            const string path = "GraphicsMagick/gm.exe";
            var fi = new FileInfo(path);
            convertPathTextField.Text = !fi.Exists ? "GraphicsMagick (gm.exe) not found!" : fi.FullName;

            //imConvertPath = GetFullPath("gm.exe");
            //convertPathTextField.Text = imConvertPath ?? "Convert tool (convert.exe) not found!";
        }

        public string GetGmPath()
        {
            return convertPathTextField.Text;
        }

        public static string GetFullPath(string fileName)
        {
            if (File.Exists(fileName))
                return Path.GetFullPath(fileName);

            var values = Environment.GetEnvironmentVariable("PATH");
            foreach (var path in values.Split(';'))
            {
                var fullPath = Path.Combine(path, fileName);
                if (File.Exists(fullPath))
                    return fullPath;
            }
            return null;
        }

        public List<String> GetInputFiles(string dirin, string[] exts)
        {
            return (from f in Directory.GetFiles(dirin).OrderBy(f => f) let ext = Path.GetExtension(f) where ext != null from e in exts where e.ToLower().Equals(ext.ToLower()) select f).ToList();
        }

        public string GetThmbFileName(string file)
        {
            var fn = Path.GetFileNameWithoutExtension(file);
            var dir = Path.GetDirectoryName(file);

            //Console.WriteLine("filename = " + fn + " dir = " + dir);

            return dir + "\\thumbnails\\" + fn + "." + (string)thumbnailFilesFormat.SelectedItem;
        }

        public List<String> GetNonExistentFilesNames(List<String> files)
        {
            return files.Where(f => !File.Exists(f)).ToList();
        }

        private void chooseInputDirectoryButton_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog(this) == DialogResult.OK)
            {
                inputFolderTb.Text = folderBrowserDialog1.SelectedPath;
                LoadCurrentDirectory(true);
            }
        }

        private void reloadDirectoryButton_Click(object sender, EventArgs e)
        {
            LoadCurrentDirectory(true);
        }

        private void createThmbButton_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(inputFolderTb.Text))
            {
                MessageBox.Show(this, "Cannot parse input directory!", "Error", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }
            var inFiles = GetInputFiles(inputFolderTb.Text, INPUT_EXTS);
            

            var outFiles = inFiles.Select(GetThmbFileName).ToList();

            //var outFiles = GetConvertOutputFileNames(inFiles, inputFolderTb.Text, inputFolderTb.Text, "thumbnails");

            _thmbJobs = new List<ThumbnailJob>();
            for (int i=0; i<inFiles.Count; ++i)
            {
                if (File.Exists(outFiles[i]) && !overwriteThmbCb.Checked) continue;

                int thumbsWidth = Convert.ToInt32((string)thumbsWidthCb.SelectedItem);
                //Console.WriteLine("thumbs width = "+thumbsWidth);
                var job = new ThumbnailJob { FileIn = inFiles[i], FileOut = outFiles[i], width = thumbsWidth };
                _thmbJobs.Add(job);
            }
            if (_thmbJobs.Count == 0)
            {
                progressBar1.Value = 0;
                progressBar1.Maximum = 0;
                infoLabel.Text = "Nothing to do!";
                return;
            }

            progressBar1.Value = 0;
            progressBar1.Maximum = _thmbJobs.Count;
            infoLabel.Text = "Created thumbnail 0/" + progressBar1.Maximum;
            thmbBackgroundWorker.WorkerReportsProgress = true;
            thmbBackgroundWorker.WorkerSupportsCancellation = true;
            sw.Restart();
            SetIsThumbnailing(true);
            thmbBackgroundWorker.RunWorkerAsync(GetGmPath());
        }

        private void thmbBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            int count = 1;

            foreach (ThumbnailJob j in _thmbJobs)
            {
                CreateThumbnail(j, (string)e.Argument);

                thmbBackgroundWorker.ReportProgress(count++, j.FileOut);

                if (thmbBackgroundWorker.CancellationPending)
                    break;

                //gui.Invoke(new jobFinishedDelegate(gui.jobFinished));
                //count++;
            }
        }

        private void thmbBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            //AddThumbnail((string) e.UserState);
            string txt1 = "Created thumbnail " + progressBar1.Value + "/" + progressBar1.Maximum;

            infoLabel.Text = txt1;
            infoLabel.Text += " Elapsed time = " + sw.Elapsed;
            if (progressBar1.Value == progressBar1.Maximum)
            {
                sw.Stop();
            }
        }

        private void thmbBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            SetIsThumbnailing(false);
            UpdateSummaryLabel();
        }

        public static void CreateThumbnail(ThumbnailJob job, string convertPath)
        {
            //Console.WriteLine("Thumbnailing file " + job.FileIn + " to " + job.FileOut);

            // create output folder if not existent:
            if (!Directory.Exists(Path.GetDirectoryName(job.FileOut)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(job.FileOut));
            }

            var processinfo = new ProcessStartInfo();
            processinfo.FileName = "\"" + convertPath + "\"";
            //Console.WriteLine("FileName = "+processinfo.FileName);

            processinfo.RedirectStandardOutput = true;
            processinfo.UseShellExecute = false;
            processinfo.CreateNoWindow = true;
            processinfo.Arguments = @"convert -thumbnail " + job.width + "x \"" + job.FileIn + "\" \"" + job.FileOut + "\"";

            //Console.WriteLine("Arguments = " + processinfo.Arguments);
            try
            {
                using (Process exeProcess = Process.Start(processinfo))
                {
                    exeProcess.WaitForExit();
                    //Console.WriteLine("exit code of convert-file " + job.FileIn + ": " + exeProcess.ExitCode);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                Console.WriteLine("Exception caught: " + e.Message);
            }
        }

        private void stopThmbButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Stopping!");
            thmbBackgroundWorker.CancelAsync();
        }

        private void thmbSizeCb_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_imDatList.Count == 0) return;

            //LoadCurrentDirectory(false);

            ReloadCurrentPage(true);
        }

        private void LoadCurrentDirectory(bool syncWithXML=true)
        {
            if (!Directory.Exists(inputFolderTb.Text))
            {
                MessageBox.Show(this, "Cannot parse input directory!", "Error", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }
            var inFiles = GetInputFiles(inputFolderTb.Text, INPUT_EXTS);
            //if (inFiles.Count == 0)
            //{
            //    MessageBox.Show(this, "Input directory does not contain any files!", "Error", MessageBoxButtons.OK,
            //                    MessageBoxIcon.Error);
            //    return;
            //}

            _currentImage = null;

            //Console.WriteLine("isBusy = "+_loadThmbImgsBackgroundWorker.IsBusy);
            if (_loadThmbImgsBackgroundWorker.IsBusy)
            {
                _loadThmbImgsBackgroundWorker.CancelAsync();
            }
           
            _imDatList.Clear();
            int count = 0;
            foreach (string f in inFiles)
            {
                _imDatList.Add(new ImageData(f, GetThmbFileName(f), count++, -1, DATE_NA_STRING, "", false));
            }

            flowLayoutPanel1.Controls.Clear();


            int actNrOfPBoxes = PAGING_OFFSET;
            if (actNrOfPBoxes > inFiles.Count) actNrOfPBoxes = inFiles.Count;
            for (int i = 0; i < actNrOfPBoxes; ++i)
            {
                AddThumbnail();
            }

            try
            {
                var sw = Stopwatch.StartNew();
                if (syncWithXML) SyncWithXML();
                sw.Stop();
                Console.WriteLine("Time for SyncWithXML: "+sw.Elapsed);
            }
            catch (Exception e)
            {
                MessageBox.Show(this, "Invalid XML file - error message: " + e.Message, "Cannot read XML input file",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //if (!_loadThmbImgsBackgroundWorker.IsBusy)
            //{
            //    _loadThmbImgsBackgroundWorker.RunWorkerAsync(_imDatList);
            //}
            //else
            //{
            //    _restartThmbImgsBackgroundWorker = true;
            //}

            if (!_imCacheWorker.IsBusy)
            {
                ImCacherStart();
            }
            else
            {
                _restartImCacheWorker = true;
                _imCacheWorker.CancelAsync();
                //_imCacheCancelledEvent.WaitOne();
            }

            _currentPage = 0;
            ReloadCurrentPage();
        }

        private void ImCacherDoWork(object sender, DoWorkEventArgs args)
        {
            //Console.WriteLine("ImCacherDoWork, hasCurentImage = " + HasCurrentImage());
            //if (!HasCurrentImage()) return;

            var worker = sender as BackgroundWorker;

            ImageData iDat;
            if (args.Argument == null)
            {
                Console.WriteLine("ImCacher: argument is null!");
                iDat = new ImageData("", "", -1, -1, "", "", false); // imagedata with index==-1 s.t. next GetNextCheckedIDatIndex returns first titlepage!
            }
            else
            {
                iDat = args.Argument as ImageData;
            }
            Console.WriteLine("ImCacher: started with current file = " + iDat.Filename);


            var neighborList = new List<string>();
            for (int i=0; i<IM_CACHE_LIMIT; ++i)
            {
                if (worker.CancellationPending)
                {
                    _imCacheCancelledEvent.Set();
                    args.Cancel = true;
                    return;
                }

                var nextSel = GetNextCheckedIDatIndex(iDat);
                if (nextSel == _imDatList.Count)
                    return;

                iDat = _imDatList[nextSel];

                neighborList.Add(iDat.Filename);

                Console.WriteLine("ImCacher: trying to load "+iDat.Filename);
                if (!_imCache.ContainsKey(iDat.Filename))
                {
                    Console.WriteLine("ImCacher: file not loaded - loading!");

                    Image im = null;
                    try
                    {
                        //im = Image.FromFile(iDat.Filename); // OLD
                        using (var stream = new FileStream(iDat.Filename, FileMode.Open, FileAccess.Read)) // NEW
                        {
                            im = Image.FromStream(stream);
                        }
                    }
                    catch (OutOfMemoryException ex)
                    {
                        Console.WriteLine("ImCacher: OutOfMemoryException while preloading image!");
                        im = null;
                    }
                    if (im == null)
                        continue;

                    //_imCache.TryAdd(iDat.Filename, Image.FromFile(iDat.Filename));
                    _imCache.TryAdd(iDat.Filename, im);
                    worker.ReportProgress(0, iDat);
                }
                else
                {
                    Console.WriteLine("ImCacher: file already loaded");
                }
            }

            Console.WriteLine("ImCacher: _imCache.Count = " + _imCache.Count);

            // remove images from cache if limit exceeded:
            while (_imCache.Count > (IM_CACHE_LIMIT + IM_CACHE_OVERHEAD) )
            {
                Console.WriteLine("ImCache: removing elements");
                foreach (var key in _imCache.Keys)
                {
                    if (!neighborList.Contains(key))
                    {
                        Image im;
                        Console.WriteLine("ImCache: removing element " + key);
                        if (!_imCache.TryRemove(key, out im))
                        {
                            Console.WriteLine("Error removing element from imCache!");
                        }
                        break;
                    }
                }
            }
            if (worker.CancellationPending)
            {
                _imCacheCancelledEvent.Set();
                args.Cancel = true;
            }
        } // end ImCacherDoWork

        private void ImCacherProgress(object sender, ProgressChangedEventArgs progressChangedEventArgs)
        {
            var iDat = progressChangedEventArgs.UserState as ImageData;
            Console.WriteLine("ImCacherProgress: "+iDat.Filename + " loaded");
        }

        private void ImCacherFinished(object sender, RunWorkerCompletedEventArgs runWorkerCompletedEventArgs)
        {
            if (runWorkerCompletedEventArgs.Cancelled)
                Console.WriteLine("ImCacher was cancelled");
            else
                Console.WriteLine("ImCacherFinished");

            //if (runWorkerCompletedEventArgs.Cancelled)
            if (runWorkerCompletedEventArgs.Cancelled && _restartImCacheWorker)
            {
                Console.WriteLine("restarting imcacher");
                ImCacherStart();
                _restartImCacheWorker = false;
            }
        }

        private void ImCacherStart()
        {
            //if (_imCacheWorker.IsBusy)
            //    throw new Exception("Cannot start ImCacher - it is already busy!");

            _imCacheCancelledEvent.Reset();
            
            Console.WriteLine("Starting ImCacher");
            _imCacheWorker.RunWorkerAsync(imView.getImageTag());

        }

        private void ImCacherClear()
        {
            Console.WriteLine("ImCacherClear: size before: "+_imCache.Count);
            foreach (var val in _imCache)
            {
                val.Value.Dispose();
            }
            _imCache.Clear();
            
        }


        private void LoadThmbImgsDoWork(object sender, DoWorkEventArgs args)
        {
            //Console.WriteLine("now i am workign!!");

            var worker = sender as BackgroundWorker;
            var imList = (List<ImageData>)args.Argument;

            for (int i = 0; i < imList.Count; ++i)    
            {
                if (worker.CancellationPending)
                {
                    args.Cancel = true;
                    break;
                }
                var iDat = imList[i];

                if (iDat.Image != null || !File.Exists(iDat.ThumbFilename)) continue;

                lock (iDat)
                {
                    iDat.Image = ImageFast.FromFile(iDat.ThumbFilename);
                }

                //Console.WriteLine("Loaded image (1) " + iDat.Filename);
                worker.ReportProgress(0, iDat);
            }

            //_thumbsResetEvent.Set();
            //Console.WriteLine("end of dowork");
        }

        private void LoadThmbImgsProgress(object sender, ProgressChangedEventArgs progressChangedEventArgs)
        {
            var iDat = progressChangedEventArgs.UserState as ImageData;
            int locIndex = GetLocalIndex(iDat.GlobalIndex);
            //Console.WriteLine("Loaded image " + iDat.Filename + " locIndex = " + locIndex);
            if (locIndex == -1) return;

            var pBox = flowLayoutPanel1.Controls[locIndex] as PictureBox;
            ReloadThumbnail(pBox);

            //var pBox = flowLayoutPanel1.Controls[locIndex] as PictureBox;
            //pBox.Image = iDat.Image;
            //pBox.ImageLocation = GetThmbFileName(iDat.Filename);

            
        }

        private void LoadThmbImgsFinished(object sender, RunWorkerCompletedEventArgs runWorkerCompletedEventArgs)
        {
            Console.WriteLine("_loadThmbImgsBackgroundWorker finished");

            if (runWorkerCompletedEventArgs.Cancelled && _restartThmbImgsBackgroundWorker)
            {
                Console.WriteLine("restarting _loadThmbImgsBackgroundWorker");
                _loadThmbImgsBackgroundWorker.RunWorkerAsync(_imDatList);
                _restartThmbImgsBackgroundWorker = false;
            }
        }

        private void ReloadCurrentPage(bool onlyUpdateSize=false)
        {
            if (_imDatList.Count==0) return;
            if (_currentPage == -1) return;

            pagingPanel.Enabled = false;

            _isPageReloading = true;

            int startInc = _currentPage * PAGING_OFFSET;
            int endEx = (_currentPage + 1) * PAGING_OFFSET;

            int actualEndEx = endEx;
            if (endEx > _imDatList.Count) actualEndEx = _imDatList.Count;

            for (int i = startInc; i < endEx; ++i)
            {
                if (i-startInc > flowLayoutPanel1.Controls.Count-1) continue;

                var pBox = (PictureBox) flowLayoutPanel1.Controls[i-startInc];
                if (i >= actualEndEx)
                {
                    //Console.WriteLine("i = " + i + " invisible");
                    pBox.Visible = false;
                    continue;
                }
                //Console.WriteLine("i = " + i + " visible");
                pBox.Visible = true;

                var iDat = _imDatList[i];

                if (onlyUpdateSize)
                {
                    ChangePBoxSize(pBox, null);
                    continue;
                }

                _fnToolTip.SetToolTip(pBox, Path.GetFileName(iDat.Filename));
                pBox.Tag = iDat;
                var cb = (CheckBox)pBox.Controls[0];
                cb.Text = (iDat.GlobalIndex+1)+" - "+iDat.Date;
                cb.Checked = iDat.IsTitlepage;

                pBox.Image = null;
                ReloadThumbnail(pBox);
            }

            currentPageTb.Text = ""+(_currentPage + 1);

            nPagesLabel.Text = @"/ " + GetNPagingPages() + ", Files " + (startInc + 1) + "-" + (actualEndEx) + " / " + _imDatList.Count;
            _isPageReloading = false;

            UpdateSummaryLabel();
            UpdateThmbColors();

            pagingPanel.Enabled = true;
        }

        private void ReloadThumbnail(PictureBox thumbnailPBox)
        {
            var iDat = thumbnailPBox.Tag as ImageData;
            
            if (iDat.Image != null)
            {
                thumbnailPBox.SizeMode = PictureBoxSizeMode.Zoom;
                thumbnailPBox.Image = iDat.Image;
                ChangePBoxSize(thumbnailPBox, null);             
            }
            else
            {
                if (File.Exists(iDat.ThumbFilename))
                {
                    lock (iDat)
                    {
                        thumbnailPBox.Image = ImageFast.FromFile(iDat.ThumbFilename);
                    }
                }
                else
                {
                    thumbnailPBox.SizeMode = PictureBoxSizeMode.CenterImage;
                    thumbnailPBox.Image = thumbnailPBox.ErrorImage;
                }
            }
            ChangePBoxSize(thumbnailPBox, null);
        }

        private int GetGlobalIndex(int localPagingIndex)
        {
            return _currentPage*PAGING_OFFSET + localPagingIndex;
        }

        private int GetLocalIndex(int globalIndex)
        {
            int tmp = globalIndex - _currentPage*PAGING_OFFSET;
            if (tmp >= 0 && tmp < PAGING_OFFSET)
                return tmp;

            return -1;
        }

        private int GetPageIndex(int globalIndex)
        {
            return globalIndex/PAGING_OFFSET;
        }

        private void UpdateSummaryLabel()
        {
            summaryLabel.Text = "Nr. of images: " + _imDatList.Count + ", nr. of issues: " +
                                GetNTitlepages();
        }

        private void UpdateWarningLabel()
        {
            warningLabel.Text = "";
            if (HasIssuesWithNoDate())
            {
                warningLabel.Text += "WARNING: There are issues with no date!\n";
            }
            if (HasIssuesWithUnevenNrOfImages())
            {
                warningLabel.Text += "WARNING: There are issues with an uneven nr. of pages (date marked red)!\n";
            }
            if (HasConflictingIssueNames())
            {
                warningLabel.Text += "WARNING: Some issue names are in conflict, ie they have same date and suffix!\n";
            }


        }


        private void flowLayoutPanel1_Scroll(object sender, ScrollEventArgs e)
        {
            //Console.WriteLine("Scroll, position = " + flowLayoutPanel1.AutoScrollPosition + " type = " + e.Type);
            //Console.WriteLine("location = " + flowLayoutPanel1.Location);
            //Console.WriteLine("location of 8 = " + flowLayoutPanel1.Controls[8].Location);

            //if (e.Type != ScrollEventType.ThumbTrack)
            //UpdateThmbImages();
            //foreach (var c in flowLayoutPanel1.Controls)
            //{

            //    Console.WriteLine("visible = " + isPictureBoxVisible((PictureBox) c));

            //}
        }

        private void flowLayoutPanel1_Wheel(object sender, MouseEventArgs e)
        {
            //Console.WriteLine("delta = " + e.Delta);
            //UpdateThmbImages();
        }

        private void flowLayoutPanel1_Resize(object sender, EventArgs e)
        {
            //Trace.WriteLine("Resize!!");
            //UpdateThmbImages();
        }

        private void AddThumbnail()
        {
            int w = Convert.ToInt32(thmbSizeCb.SelectedItem);
            var pBox = new PictureBox
            {
                WaitOnLoad = false,
                Width = w,
                Height = w * 2,
                SizeMode = PictureBoxSizeMode.Zoom,
                BorderStyle = BorderStyle.None,
            };

            //pBox.LoadCompleted += ChangePBoxSize;
            
            pBox.DoubleClick += LoadImage;

            var cb = new CheckBox { Text = "", Width = 200 };
            cb.CheckedChanged += cb_CheckedChanged;
            // a click on the thumbnail should activate checkbox too, but have to distinct from doubleclick:
            pBox.Click += delegate {
                                  //Console.WriteLine("Doubleclicktime = " + SystemInformation.DoubleClickTime);
                                  var timer = new Timer { Interval = SystemInformation.DoubleClickTime + 10 };
                                  timer.Tick += delegate {
                                                        //Trace.WriteLine("isDoubleClick = "+isDoubleClicked);
                                                        if (!isDoubleClicked)
                                                            cb.Checked = !cb.Checked;

                                                        isDoubleClicked = false;
                                                        timer.Stop();
                                                    };
                                  timer.Start();
                              };

            pBox.Controls.Add(cb);

            flowLayoutPanel1.Controls.Add(pBox);
        }

        private void ChangePBoxSize(object sender, EventArgs args)
        {
            var pBox = (PictureBox)sender;
            //Console.WriteLine("changePBoxSize!!");
            //pBox.Width = thmbSizeTrackBar.Value;
            pBox.Width = Convert.ToInt32(thmbSizeCb.SelectedItem);
            if (pBox.Image != null)
                pBox.Height = getAspectRatioHeight(pBox.Image.Width, pBox.Image.Height, pBox.Width) + 50;
        }

        //private bool IsPictureBoxVisible(PictureBox pBox)
        //{
        //    int top = flowLayoutPanel1.Location.Y;
        //    int bottom = flowLayoutPanel1.Location.Y + flowLayoutPanel1.Height;

        //    int yTop = pBox.Location.Y;
        //    int yBottom = pBox.Location.Y + pBox.Height;

        //    //Console.WriteLine("top = " + top + " bottom = " + bottom + " yTop = " + yTop + " yBottom = " + yBottom);

        //    if (yTop >= top && yTop <= bottom) return true;
        //    if (yBottom >= top && yBottom <= bottom) return true;

        //    return false;
        //}

        private void cb_CheckedChanged(object sender, EventArgs e)
        {
            if (_isPageReloading) return;

            var cb = (CheckBox)sender;
            var pBox = (PictureBox)cb.Parent;
            var iDat = (ImageData)pBox.Tag;

            iDat.IsTitlepage = cb.Checked;

            UpdateThmbColors();
            UpdateSummaryLabel();
            UpdateWarningLabel();
            UpdateIndices();
        }

        private void LoadImage(object sender, EventArgs args) {
            isDoubleClicked = true;
            try {
                _currentImage = null;
                //Console.WriteLine("doublclick!!");
                var pBox = (PictureBox)sender;
                var iDat = pBox.Tag as ImageData;

                if (_imCache.ContainsKey(iDat.Filename)) {
                    Console.WriteLine("Found image in cache: " + iDat.Filename);
                    _currentImage = _imCache[iDat.Filename];
                }
                else {
                    Console.WriteLine("Did not find image in cache: " + iDat.Filename);
                    //var stream = new FileStream(iDat.Filename, FileMode.Open, FileAccess.Read);
                    using (var stream = new FileStream(iDat.Filename, FileMode.Open, FileAccess.Read)) {
                        _currentImage = Image.FromStream(stream);
                        _imCache.TryAdd(iDat.Filename, _currentImage);
                    }
                }
                imView.LoadImage(_currentImage, pBox.Tag);

                if (_imCacheWorker.IsBusy) {
                    _restartImCacheWorker = true;
                    Console.WriteLine("imcache worker is busy... cancelling!");
                    _imCacheWorker.CancelAsync();
                    //Console.WriteLine("waiting for cancellation");
                    //_imCacheCancelledEvent.WaitOne();
                    //Console.WriteLine("restarting ImCacher");
                    //ImCacherStart();
                }
                else {
                    Console.WriteLine("starting imcache worker!");
                    ImCacherStart();
                }

                UpdateSelectedImageInfo();
                UpdateThmbColors();
            }
            catch (Exception ex) {
                MessageBox.Show(this, "Exception while loading image", ex.Message,
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                Trace.WriteLine("Exception while loading image: " + ex.Message);
            }
        }

        //private void LoadImage(object sender, EventArgs args)
        //{
        //    isDoubleClicked = true;
        //    try
        //    {
        //        _currentImage = null;
        //        //Console.WriteLine("doublclick!!");
        //        var pBox = (PictureBox) sender;
        //        var iDat = pBox.Tag as ImageData;

        //        if (_imCache.ContainsKey(iDat.Filename))
        //        {
        //            Console.WriteLine("Found image in cache: " + iDat.Filename);
        //            _currentImage = _imCache[iDat.Filename];
        //        }
        //        else
        //        {
        //            Console.WriteLine("Did not find image in cache: " + iDat.Filename);
        //            //var stream = new FileStream(iDat.Filename, FileMode.Open, FileAccess.Read);
        //            using (var stream = new FileStream(iDat.Filename, FileMode.Open, FileAccess.Read)) 
        //            {
        //                 _currentImage = Image.FromStream(stream);
        //                 _imCache.TryAdd(iDat.Filename, _currentImage);
        //            }

        //            //_currentImage = Image.FromFile(iDat.Filename);
        //            //_imCache.TryAdd(iDat.Filename, _currentImage);

        //            //_currentImage = ImageFast.FromFile(iDat.Filename);
        //            //_currentImage = Image.FromStream(stream, true, true);
        //            //stream.Close();
        //        }
        //        currentImageViewer.Image = _currentImage;
        //        imView.getImageTag() = pBox.Tag;

        //        if (_imCacheWorker.IsBusy)
        //        {
        //            _restartImCacheWorker = true;
        //            Console.WriteLine("imcache worker is busy... cancelling!");
        //            _imCacheWorker.CancelAsync();
        //            //Console.WriteLine("waiting for cancellation");
        //            //_imCacheCancelledEvent.WaitOne();
        //            //Console.WriteLine("restarting ImCacher");
        //            //ImCacherStart();
        //        }
        //        else
        //        {
        //            Console.WriteLine("starting imcache worker!");
        //            ImCacherStart();
        //        }

        //        UpdateSelectedImageInfo();
        //        UpdateThmbColors();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(this, "Exception while loading image", ex.Message,
        //                        MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        Trace.WriteLine("Exception while loading image: "+ex.Message);
        //    }
        //}

        private void UnloadCurrentImage()
        {
            imView.UnloadCurrentImage();
        }

        //private void OnImageLoaded(object sender, AsyncCompletedEventArgs asyncCompletedEventArgs)
        //{
        //    UpdateSelectedImageLabel();
        //}

        private void UpdateSelectedImageInfo()
        {
            var iDat = imView.getImageTag() as ImageData;

            // update issue specific suffix:
            issueSpecSuffixTb.Text = iDat.IssueSpecificSuffix;

            // update date:
            DateTime dat;
            if (DateTime.TryParse(iDat.Date, out dat))
                dateTimePicker1.Value = dat;

            // update info label:
            string labelTxt = "Selected Image: " + Path.GetFileName(iDat.Filename) + ", " + iDat.Date;
            if (iDat.LocalIndex != -1)
            {
                labelTxt += ", Image " + (iDat.LocalIndex + 1) + "/" + GetNImagesInIssue(iDat);
            }
            if (_currentImage != null)
            {
                labelTxt += ", Size = " + _currentImage.Width + "x" +
                                           _currentImage.Height;
                Console.WriteLine("res = "+_currentImage.HorizontalResolution+"x"+_currentImage.VerticalResolution);
            }
            imView.setLabelText(labelTxt);
            //if (iDat.IssueSpecificSuffix.Length > 0)
            //{
            //    selectedImageLabel.Text += ", Image spec. suffix: " + iDat.IssueSpecificSuffix;
            //}
        }

        private void UpdateIndices()
        {
            int index = 0;
            
            foreach (var iDat in _imDatList)
            {
                if (iDat.IsTitlepage)
                    index = 0;

                iDat.LocalIndex = index++;
            }
        }

        private void UpdateThmbColors()
        {
            //Console.WriteLine("updating thmb colors");
            int nextSel = -1;
            int prevSel = -1;

            PictureBox selPBox = null;
            if (HasCurrentImage())
            {
                var iDat = (ImageData)imView.getImageTag();
                selPBox = FindPBox(iDat);

                nextSel = GetNextCheckedIDatIndex(iDat);
                prevSel = GetPrevCheckedIDatIndex(iDat);
                //Console.WriteLine("nextSel = " + nextSel + " prevSel= " + prevSel);
            }

            for (int i = 0; i < flowLayoutPanel1.Controls.Count; ++i)
            {
                var pBox = (PictureBox)flowLayoutPanel1.Controls[i];
                if (pBox.Tag == null) continue;

                pBox.Controls[0].ForeColor = GetNImagesInIssue((ImageData)pBox.Tag) % 2 != 0 ? Color.Red : Color.Black;

                int globIndex = GetGlobalIndex(i);

                if (globIndex >= prevSel && globIndex < nextSel && prevSel!=-1)
                {
                    pBox.BackColor = Color.LightSkyBlue;
                }
                else
                {
                    pBox.BackColor = flowLayoutPanel1.BackColor;
                }

                var iDat = pBox.Tag as ImageData;
                if (iDat.IsTitlepage)
                {
                    pBox.BackColor = Color.Blue;
                }


                pBox.BorderStyle = BorderStyle.None;
            }
            if (selPBox != null)
            {
                selPBox.BorderStyle = BorderStyle.Fixed3D;
            }
        }

        private string[] getOrigFileName(string thumbnailFn)
        {
            var fi = new FileInfo(thumbnailFn);
            DirectoryInfo parent = fi.Directory.Parent;
            Console.WriteLine(Path.GetFileNameWithoutExtension(fi.Name));
            string[] files = Directory.GetFiles(parent.FullName, Path.GetFileNameWithoutExtension(fi.Name) + ".*");

            return files;
        }

        private int getAspectRatioHeight(int widthOrig, int heightOrig, int newWidth)
        {
            return heightOrig * newWidth / widthOrig;
        }


        private void LoadPBox(int index)
        {
            int newPageIndex = GetPageIndex(index);
            if (_currentPage != newPageIndex)
            {
                _currentPage = newPageIndex;
                ReloadCurrentPage();
            }
            var pBox = flowLayoutPanel1.Controls[GetLocalIndex(index)];
            LoadImage(pBox, null);
            flowLayoutPanel1.ScrollControlIntoView(pBox);            
        }

        private void jumpToFirstIssueButton_Click(object sender, EventArgs e)
        {
            int index = GetFirstCheckedIDatIndex();
            if (index == -1) return;

            LoadPBox(index);
        }

        private void jumpToLastIssueButton_Click(object sender, EventArgs e)
        {
            int index = GetLastCheckedIDatIndex();
            if (index == -1) return;

            LoadPBox(index);
        }

        private void jumpToPrevIssueButton_Click(object sender, EventArgs e)
        {
            if (!HasCurrentImage()) return;

            int prevIndex = GetPrevCheckedIDatIndex(imView.getImageTag() as ImageData);
            //Console.WriteLine("prevIndex = " + prevIndex);
            if (prevIndex == -1) return;
            prevIndex = GetPrevCheckedIDatIndex(_imDatList[prevIndex-1]);
            if (prevIndex == -1) return;

            LoadPBox(prevIndex);
        }

        private void jumpToNextIssueButton_Click(object sender, EventArgs e)
        {
            if (!HasCurrentImage()) return;

            int nextIndex = GetNextCheckedIDatIndex(imView.getImageTag() as ImageData);
            if (nextIndex == _imDatList.Count) return;

            LoadPBox(nextIndex);
        }

        private void changeIssue_Click(object sender, EventArgs e)
        {
            if (!HasCurrentImage()) return;

            int prevIndex = GetPrevCheckedIDatIndex((ImageData)imView.getImageTag());
            int nextIndex = GetNextCheckedIDatIndex((ImageData)imView.getImageTag());
            //Console.WriteLine("prevIndex = " + prevIndex + " nextIndex = "+nextIndex);

            //getPublicationCycleList();
            ChangeTimeAndIndex(prevIndex, nextIndex, dateTimePicker1.Value);
            imView.setLabelText("Selected Image: " + Path.GetFileName(GetCurrentImagePath()) + ", " + ((ImageData)imView.getImageTag()).Date);

            UpdateWarningLabel();
        }

        private void changeIssueAndFollowing_Click(object sender, EventArgs e)
        {
            if (!HasCurrentImage()) return;
            var iDat = (ImageData)imView.getImageTag();

            int prevIndex = 0;
            int nextIndex = 0;
            var currentDate = dateTimePicker1.Value;
            var currentDOW = currentDate.DayOfWeek;
            var publicationCycleMap = getPublicationCycleList();

            do
            {
                prevIndex = GetPrevCheckedIDatIndex(iDat);
                nextIndex = GetNextCheckedIDatIndex(iDat);
                if (prevIndex == -1) return;
                ChangeTimeAndIndex(prevIndex, nextIndex, currentDate);
                // increment date according to publication cycle:
                //Console.WriteLine("currentDOW: " + currentDOW);
                if (!publicationCycleMap.ContainsKey(currentDOW)) // jump to next day in publication cycle if current day is not in there
                {
                    while (!publicationCycleMap.ContainsKey(currentDOW))
                    {
                        currentDOW++;
                        if ((int)currentDOW > 7) currentDOW = DayOfWeek.Monday;
                        currentDate = currentDate.AddDays(1);
                    }
                }
                else // increment days according to publication cycle
                {
                    currentDate = currentDate.AddDays(publicationCycleMap[currentDOW]);
                    currentDOW = currentDate.DayOfWeek;                    
                }

                //Console.WriteLine("Day: " + currentDate.ToString(DATE_FORMAT));

                if (nextIndex != _imDatList.Count)
                    iDat = _imDatList[nextIndex];
            } while (nextIndex != _imDatList.Count);

            UpdateWarningLabel();
        }

        private Dictionary<DayOfWeek, int> getPublicationCycleList()
        {
            var publicationCycleMap=new Dictionary<DayOfWeek, int>();

            if (monCb.Checked) { publicationCycleMap.Add(DayOfWeek.Monday, 0); }
            if (tueCb.Checked) publicationCycleMap.Add(DayOfWeek.Tuesday, 0);
            if (wedCb.Checked) publicationCycleMap.Add(DayOfWeek.Wednesday, 0);
            if (thuCb.Checked) publicationCycleMap.Add(DayOfWeek.Thursday, 0);
            if (friCb.Checked) publicationCycleMap.Add(DayOfWeek.Friday, 0);
            if (satCb.Checked) publicationCycleMap.Add(DayOfWeek.Saturday, 0);
            if (sunCb.Checked) publicationCycleMap.Add(DayOfWeek.Sunday, 0);
            
            var keys = new List<DayOfWeek>(publicationCycleMap.Keys);
            for (int i = 0; i < keys.Count; ++i)
            {
                DayOfWeek next = keys[((i + 1) % keys.Count)];

                int diff = next - keys[i];
                if (diff <= 0) diff += 7;
                publicationCycleMap[keys[i]] = diff;
                //Console.WriteLine("key: " + keys[i] + " diff: " + diff);
            }

            return publicationCycleMap;
        }

        private void ChangeTimeAndIndex(int startIndexInc, int endIndexEx, DateTime date)
        {
            if (startIndexInc == -1) return;
            //if (startIndex == -1) startIndex = 0;
            //if (endIndex == -1) endIndex = _imDatList.Count;
            for (int i = startIndexInc; i < endIndexEx; i++)
            {
                var iDat = _imDatList[i];
                var pBox = FindPBox(iDat);

                iDat.Date = date.ToString(DATE_FORMAT);
                iDat.LocalIndex = i - startIndexInc;

                if (pBox == null) continue;

                var cb = (CheckBox)pBox.Controls[0];
                cb.Text = (iDat.GlobalIndex + 1) + " - " + iDat.Date;
                cb.TextAlign = ContentAlignment.MiddleLeft;
            }
        }

        private void ChangeIssueSpecificSuffix(int startIndexInc, int endIndexEx, String suffix)
        {
            if (startIndexInc == -1) return;
            for (int i = startIndexInc; i < endIndexEx; i++)
            {
                _imDatList[i].IssueSpecificSuffix = suffix;
            }
        }

        private int GetFirstCheckedIDatIndex()
        {
            for (int i=0; i<_imDatList.Count; ++i)
            {
                if (_imDatList[i].IsTitlepage) return i;
            }
            return -1;
        }

        private int GetLastCheckedIDatIndex()
        {
            for (int i = _imDatList.Count-1; i >= 0; --i)
            {
                if (_imDatList[i].IsTitlepage) return i;
            }
            return _imDatList.Count;
        }

        private int GetPrevCheckedIDatIndex(ImageData iDat)
        {
            int index = iDat.GlobalIndex;
            for (int i=index; i>=0; i--)
            {
                if (_imDatList[i].IsTitlepage) return i;
            }
            return -1;
        }

        private int GetNextCheckedIDatIndex(ImageData iDat)
        {
            int index = iDat.GlobalIndex;
            for (int i = index + 1; i < _imDatList.Count; i++)
            {
                if (_imDatList[i].IsTitlepage) return i;
            }
            return _imDatList.Count;
        }

        private List<int> GetCheckedIndices()
        {
            return (from iDat in _imDatList where iDat.IsTitlepage select iDat.GlobalIndex).ToList();
        }

        private bool HasCurrentImage() {
            return (GetCurrentImagePath() != null && !GetCurrentImagePath().Equals(""));
        }

        private void flowLayoutPanel1_MouseEnter(object sender, EventArgs e)
        {
            flowLayoutPanel1.Focus();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog(this) == DialogResult.OK)
            {
                outputFolderTb.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void copyFilesButton_Click(object sender, EventArgs e)
        {
            var outdir = outputFolderTb.Text;

            if (!Directory.Exists(outdir))
            {
                MessageBox.Show(this, "Output directory does not exist!", "Output directory does not exist",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // if moving files: first release loaded images in cache and image window:
            if (moveFilesCb.Checked) {
                Console.WriteLine("Moving files");
                if (_imCacheWorker.IsBusy) {
                    Console.WriteLine("moving files: cancelling imcacher...");
                    _imCacheWorker.CancelAsync();
                    _imCacheCancelledEvent.WaitOne();
                    Console.WriteLine("moving files: done cancelling imcacher");
                }
                ImCacherClear();
                UnloadCurrentImage();
                Console.WriteLine("moving: imcache size (should be 0!): " + _imCache.Count);
            }

            SetIsSaving(true);
            saveWorker.RunWorkerAsync();
        }

        private void SetIsThumbnailing(bool value)
        {
            createThmbButton.Enabled = !value;
            stopThmbButton.Enabled = value;
            chooseInDirButton.Enabled = !value;
            overwriteThmbCb.Enabled = !value;
            thumbnailFilesFormat.Enabled = !value;
        }

        private void SetIsSaving(bool value)
        {
            //this.Enabled = !value;
            splitContainer1.Enabled = !value;
            copyFilesButton.Enabled = !value;
            chooseOutDirButton.Enabled = !value;
            outputFolderTb.Enabled = !value;
            thmbSizeCb.Enabled = !value;
            pubCycleGb.Enabled = !value;
            reloadDirectoryButton.Enabled = !value;

            copyFilesButton.Text = value ? "Copying..." : "Copy files";
        }

        private void saveWorker_Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            if (moveFilesCb.Checked)
                reloadDirectoryButton_Click(this, null);

            SetIsSaving(false);
            var errDiag = new MyErrorDialog();
            errDiag.Text = "Finished copying files";
            errDiag.errorLog.Text = e.Result as string;
            errDiag.Show(this);
        }

        private void saveWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            string outdir = outputFolderTb.Text;
            var inDir = new DirectoryInfo(inputFolderTb.Text);

            string exceptionsLog = "";
            int countSucces = 0;
            int countNoDate = 0;

            var filesNotOverwritten=new List<string>();
            foreach (var iDat in _imDatList)
            {
                
                if (iDat.Date.Equals(DATE_NA_STRING))
                {
                    countNoDate++;
                    continue;
                }

                var dt = DateTime.Parse(iDat.Date);
                //try
                //{
                //    dt = DateTime.Parse(iDat.Date);
                //}
                //catch (Exception ex)
                //{
                //    Console.WriteLine("Cannot parse date for file "+ iDat.Filename + ": " + iDat.Date);
                //    continue;
                //}

                string fileOutdir = outdir + "\\";

                if (createTopLevelCb.Checked)
                    fileOutdir += inDir.Name + "\\";
                if (yearLevelCb.Checked)
                    fileOutdir += dt.Year + "\\";

                string issueFolderName = GetIssueFolderName(iDat);

                // add a suffix if the issue foldername is not unique:
                int nPrevIssuesWithSameFoldername = GetNOfPreviousIssuesWithEqualFoldername(iDat);
                if (nPrevIssuesWithSameFoldername > 0)
                {
                    issueFolderName += "_samedate"+nPrevIssuesWithSameFoldername;
                }

                fileOutdir += issueFolderName;

                string fileOutName = fileOutdir + "\\";
                if (useIssueAsFilePrefixCb.Checked)
                    fileOutName += issueFolderName + "_";

                fileOutName += (iDat.LocalIndex+1).ToString("00000") + Path.GetExtension(iDat.Filename);
                
                try
                {
                    Directory.CreateDirectory(fileOutdir);
                    if (!overwriteCb.Checked && File.Exists(fileOutName))
                    {
                        filesNotOverwritten.Add(fileOutName);
                        continue;
                    }

                    if (!moveFilesCb.Checked)
                    {
                        Console.WriteLine("Copying file " + iDat.Filename + " to " + fileOutName);
                        File.Copy(iDat.Filename, fileOutName, true);
                    }
                    else
                    {
                        Console.WriteLine("Moving file " + iDat.Filename + " to " + fileOutName);
                        
                        if (File.Exists(fileOutName))
                            File.Delete(fileOutName);

                        File.Move(iDat.Filename, fileOutName);
                        // IF FILE STILL DOES NOT EXIST: (I.E. BECAUSE OF FILE LOCK OR WHATEVER), JUST COPY IT:
                        if (!File.Exists(fileOutName))
                        {
                            File.Copy(iDat.Filename, fileOutName, true);
                        }
                    }
                    countSucces++;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Unexpected exception while copying: "+ex.Message);
                    exceptionsLog += "Unexpected exception while copying: " + ex.Message + "\n";

                    Console.WriteLine("stack trace: " + ex.StackTrace);

                    if (moveFilesCb.Checked)
                    {
                        exceptionsLog += "Moving enabled - now trying to copy file instead of moving: "+iDat.Filename+"\n";
                        // IF FILE STILL DOES NOT EXIST: (I.E. BECAUSE OF FILE LOCK OR WHATEVER), JUST COPY IT:
                        if (!File.Exists(fileOutName))
                        {
                            File.Copy(iDat.Filename, fileOutName, true);
                        }
                    }
                }

            }  // end for each file

            // print summary:
            string summary = "";
            if (moveFilesCb.Checked)
                summary += "Nr. of files successfully moved: ";
            else
                summary += "Nr. of files successfully copied: ";
            summary += countSucces + Environment.NewLine;

            summary += "Nr. of files with no date: " + countNoDate + Environment.NewLine;
            if (filesNotOverwritten.Count > 0)
            {
                summary += "--------------------------------------------" + Environment.NewLine;
                summary += "Nr. of existing files not overwritten: " + filesNotOverwritten.Count + ", List:" + Environment.NewLine;
                foreach (var f in filesNotOverwritten)
                    summary += f + Environment.NewLine;
            }
            if (exceptionsLog.Length > 0)
            {
                summary += "--------------------------------------------" + Environment.NewLine;
                summary += "Other errors:" + Environment.NewLine;
                summary += exceptionsLog;
            }
            e.Result = summary;

        }

        private void useInputFolderButton_Click(object sender, EventArgs e)
        {
            outputFolderTb.Text = inputFolderTb.Text;
        }

        private void WriteXML()
        {
            var inDir = inputFolderTb.Text;
            if (!Directory.Exists(inDir))
            {
                MessageBox.Show(this, "No valid input folder!", "No valid input folder!", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }
            var dirinfo = new DirectoryInfo(inDir);

            var settings = new XmlWriterSettings { Indent = true, IndentChars = "\t" };
            var writer = XmlWriter.Create(GetXmlName(), settings);

            writer.WriteStartDocument();
            writer.WriteStartElement("rootDir");
            writer.WriteAttributeString("path", dirinfo.FullName);
            

            //var inFiles = GetInputFiles(inputFolderTb.Text, new string[] {"jpg", "tif", "tiff", "png", ""},
            //                            new string[] {"thumbnails"}, SearchOption.TopDirectoryOnly);

            writer.WriteAttributeString("nImages", "" + _imDatList.Count);
            writer.WriteAttributeString("nIssues", "" + GetNTitlepages());

            foreach (var iDat in _imDatList)
            {
                writer.WriteStartElement("image");
                writer.WriteAttributeString("path", iDat.Filename);
                writer.WriteAttributeString("thumbPath", iDat.ThumbFilename);
                writer.WriteAttributeString("hasThumbnail", ""+File.Exists(GetThmbFileName(iDat.Filename)));

                writer.WriteAttributeString("global_index", "" + iDat.GlobalIndex);
                writer.WriteAttributeString("issue_index", "" + iDat.LocalIndex);
                writer.WriteAttributeString("isTitlepage", "" + iDat.IsTitlepage);
                writer.WriteAttributeString("date", iDat.Date);
                writer.WriteAttributeString("issueSpecificSuffix", iDat.IssueSpecificSuffix);

                writer.WriteEndElement();
            }

            writer.WriteEndElement();
            writer.WriteEndDocument();

            writer.Close();
        }

        public void ReadXML(out List<ImageData> imageDataList)
        {
            var reader = XmlReader.Create(GetXmlName());

            imageDataList = new List<ImageData>();
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    switch (reader.Name)
                    {
                        case "rootDir":
                            break;


                        case "image":
                            reader.MoveToAttribute("path");
                            string fn = reader.Value;
                            reader.MoveToAttribute("thumbPath");
                            string thumb_fn = reader.Value;
                            reader.MoveToAttribute("global_index");
                            int globalIndex = Convert.ToInt32(reader.Value);
                            reader.MoveToAttribute("issue_index");
                            int localIndex = Convert.ToInt32(reader.Value);
                            reader.MoveToAttribute("isTitlepage");
                            bool isTitlepage = Convert.ToBoolean(reader.Value);
                            reader.MoveToAttribute("date");
                            string date = reader.Value;
                            reader.MoveToAttribute("issueSpecificSuffix");
                            string issueSpecificSuffix = reader.Value;
                            var iDat = new ImageData(fn, thumb_fn, globalIndex, localIndex, date, issueSpecificSuffix, isTitlepage);
                            //Console.WriteLine("ReadXML, iDat = "+iDat.ToString());
                            imageDataList.Add(iDat);
                            //Console.WriteLine(iDat.ToString());

                            break;

                    } // end switch
                } // end if element
            } // end read
            reader.Close();
        }

        public void SyncWithXML()
        {
            if (!File.Exists(GetXmlName())) return;

            //throw new NotImplementedException();

            List<ImageData> imageDataList;
            ReadXML(out imageDataList);

            foreach (var iDat in imageDataList)
            {
                // update relevant info from xml:
                _imDatList[iDat.GlobalIndex].Filename = iDat.Filename;
                _imDatList[iDat.GlobalIndex].LocalIndex = iDat.LocalIndex;
                _imDatList[iDat.GlobalIndex].IsTitlepage = iDat.IsTitlepage;
                _imDatList[iDat.GlobalIndex].Date = iDat.Date;
            }
        }

        public string GetXmlName()
        {
            var inDir = inputFolderTb.Text;
            var dirinfo = new DirectoryInfo(inDir);
            var outFn = dirinfo + "\\" + dirinfo.Name + ".xml";
            return outFn;
        }

        //public PictureBox FindPBoxForFile(string f)
        //{
        //    return (from PictureBox pBox in flowLayoutPanel1.Controls let iDat = (ImageData) pBox.Tag where iDat.Filename.Equals(f) select pBox).FirstOrDefault();
        //}

        public PictureBox FindPBox(ImageData iDat)
        {
            int locIndex = GetLocalIndex(iDat.GlobalIndex);
            if (locIndex != -1)
                return flowLayoutPanel1.Controls[locIndex] as PictureBox;

            return null;
        }

        public int GetNTitlepages()
        {
            return _imDatList.Count(iDat => iDat.IsTitlepage);
        }

        public List<ImageData> GetTitlepages()
        {
            return _imDatList.Where(iDat => iDat.IsTitlepage).ToList();
        }

        public bool HasConflictingIssueNames()
        {
            var tps = GetTitlepages();
            foreach (ImageData iDat1 in tps)
            {
                var fn1 = GetIssueFolderName(iDat1);
                foreach (ImageData iDat2 in tps)
                {
                    
                    if (iDat1 == iDat2) continue;
                    var fn2 = GetIssueFolderName(iDat2);
                    if (fn1.Equals(fn2) && !fn1.Contains(DATE_NA_STRING) && !fn2.Contains(DATE_NA_STRING))
                        return true;
                }
            }

            return false;
        }

        public int GetNOfPreviousIssuesWithEqualFoldername(ImageData iDat)
        {
            var currTp = GetCorrespondingTitlepage(iDat);
            if (currTp == null) return 0;
            var currTpFn = GetIssueFolderName(currTp);
            var tps = GetTitlepages();
            return tps.TakeWhile(tp => tp != currTp).Count(tp => GetIssueFolderName(tp).Equals(currTpFn));
        }

        public ImageData GetCorrespondingTitlepage(ImageData iDat)
        {
            int prev = GetPrevCheckedIDatIndex(iDat);
            if (prev == -1) return null;

            return _imDatList[prev];
        }

        public bool HasIssuesWithNoDate()
        {
            var tps = GetTitlepages();
            return tps.Any(iDat1 => iDat1.Date.Equals(DATE_NA_STRING));
        }

        public bool HasIssuesWithUnevenNrOfImages()
        {
            var tps = GetTitlepages();
            return tps.Any(iDat => GetNImagesInIssue(iDat)%2 != 0);
        }

        public string GetIssueFolderName(ImageData iDat)
        {
            string datestr = DATE_NA_STRING;
            if (!iDat.Date.Equals(DATE_NA_STRING))
                datestr = DateTime.Parse(iDat.Date).ToString("yyyyMMdd");

            string issueprefix = string.IsNullOrEmpty(issuePrefixTb.Text) ? "" : issuePrefixTb.Text + "_";
            string issuesuffix = string.IsNullOrEmpty(issueSuffixTb.Text) ? "" : "_" + issueSuffixTb.Text;
            string issuespecsuffix = string.IsNullOrEmpty(iDat.IssueSpecificSuffix) ? "" : "_" + iDat.IssueSpecificSuffix;
            string issueFolderName = issueprefix + datestr + issuesuffix + issuespecsuffix;

            return issueFolderName;
        }

        // takes too long for many files...
        //public int GetNMissingThumbnails()
        //{
        //    return _imDatList.Count(iDat => !File.Exists(GetThmbFileName(iDat.Filename)));
        //}

        private int GetNImagesInIssue(ImageData iDat)
        {
            int next = GetNextCheckedIDatIndex(iDat);
            int prev = GetPrevCheckedIDatIndex(iDat);
            if (prev == -1) prev = 0;

            return (next-prev);
        }

        private void saveXMLButton_Click(object sender, EventArgs e)
        {
            WriteXML();
        }

        private string GetCurrentImagePath()
        {
            if (imView.getImageTag() == null) return null;
            var iDat = imView.getImageTag() as ImageData;

            return iDat.Filename;
        }

        public static int boundValue(int value, int min, int max) {
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }

        private void prevPageButton_Click(object sender, EventArgs e)
        {
            if (_currentPage - 1 >= 0)
            {
                _currentPage--;
                ReloadCurrentPage();
            }
        }

        private void nextPageButton_Click(object sender, EventArgs e)
        {
            if (_currentPage + 1 < GetNPagingPages())
            {
                _currentPage++;
                ReloadCurrentPage();
            }
        }

        private int GetNPagingPages()
        {
            return (int) Math.Ceiling(_imDatList.Count/(double)PAGING_OFFSET);
        }

        private void firstPageButton_Click(object sender, EventArgs e)
        {
            if (_currentPage != 0)
            {
                _currentPage = 0;
                ReloadCurrentPage();
            }
        }
    

        private void lastPageButton_Click(object sender, EventArgs e)
        {
            if (_currentPage != GetNPagingPages()-1)
            {
                _currentPage = GetNPagingPages() - 1;
                ReloadCurrentPage();
            }
        }

        private void reloadPageButton_Click(object sender, EventArgs e)
        {
            ReloadCurrentPage();
        }

        private void currentPageTb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
            {
                try
                {
                    int page = Convert.ToInt32(currentPageTb.Text);
                    if (page < 1 || page > GetNPagingPages()) throw new Exception();
                    _currentPage = page-1;
                    ReloadCurrentPage();
                }
                catch (Exception ex)
                {
                    currentPageTb.Text = Convert.ToString(_currentPage+1);
                }
            }
        }

        private void applySpecSuffixButton_Click(object sender, EventArgs e)
        {
            if (!HasCurrentImage()) return;

            int prevIndex = GetPrevCheckedIDatIndex((ImageData)imView.getImageTag());
            int nextIndex = GetNextCheckedIDatIndex((ImageData)imView.getImageTag());
            ChangeIssueSpecificSuffix(prevIndex, nextIndex, issueSpecSuffixTb.Text);

            UpdateWarningLabel();
        }


    } // end FRTForm
}
