using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace YearInProgressWPF.ViewModels
{
    public class MainViewModel:ViewModelBase
    {

        public enum ProgressType
        {
            Day,
            Year,
            Life
        };

        String _dayInProgressImagePath = @"";
        public string DayInProgressImagePath
        {
            get => _dayInProgressImagePath;
            set => SetValueIfChanged(() => DayInProgressImagePath, () => _dayInProgressImagePath, value);
        }   

        TimeSpan _updateInterval = TimeSpan.FromHours(1);
        public TimeSpan UpdateInterval
        {
            get => _updateInterval; 
            set => SetValueIfChanged(() => UpdateInterval, () => _updateInterval, value);
        }

        System.Drawing.Color _background = System.Drawing.Color.Red;
        public System.Drawing.Color Background
        {
            get => _background;
            set => SetValueIfChanged(() => Background, () => _background, value);
        }

        System.Drawing.Color _foreground = System.Drawing.Color.White;
        public System.Drawing.Color Foreground
        {
            get => _foreground;
            set => SetValueIfChanged(() => Foreground, () => _foreground, value);
        }

        System.Drawing.Color _textColor = System.Drawing.Color.White;
        public System.Drawing.Color TextColor
        {
            get => _textColor;
            set => SetValueIfChanged(() => TextColor, () => _textColor, value);
        }

        String outputPath = @"c:\temp\"; // @"c:\Program Files\Logitech\DirectOutput\Fip6.jpg";
        public String OutputPath
        {
            get => outputPath;
            set => SetValueIfChanged(() => OutputPath, () => outputPath, value);
        }

        int _width = 320;
        public int Width
        {
            get => _width;
            set => SetValueIfChanged(() => Width, () => _width, value);
        }

        int _height = 240;
       public int Height
        {
            get => _height;
            set => SetValueIfChanged(() => Height, () => _height, value);
        }

        BitmapImage _dayInProgressImage;
        public BitmapImage DayInProgressImage
        {
            get => _dayInProgressImage;
            set => SetValueIfChanged(() => DayInProgressImage, () => _dayInProgressImage, value);
        }

        BitmapImage _yearInProgressImage;
        public BitmapImage YearInProgressImage
        {
            get => _yearInProgressImage;
            set => SetValueIfChanged(() => YearInProgressImage, () => _yearInProgressImage, value);
        }


        BitmapImage _lifeInProgressImage;
        public BitmapImage LifeInProgressImage
        {
            get => _lifeInProgressImage;
            set => SetValueIfChanged(() => YearInProgressImage, () => _lifeInProgressImage, value);
        }





        void GenerateImage(ProgressType progressType, String fileName, System.Windows.Media.Color foregroundColor, System.Windows.Media.Color backgroundColor, System.Windows.Media.Color textColor)
        {            
            var renderTargetBitmap = new RenderTargetBitmap(Width, Height, 96, 96, PixelFormats.Pbgra32);

            DrawingVisual visual = new DrawingVisual();
            var context = visual.RenderOpen();
            {
                //System.Windows.Media.Color backgroundColor = System.Windows.Media.Color.FromArgb(Background.A, Background.R, Background.G, Background.B);
                var backgroundBrush = new SolidColorBrush(backgroundColor);
                var backgroundPen = new Pen(backgroundBrush, 0);
                context.DrawRectangle(backgroundBrush, backgroundPen, new Rect(0, 0, Width, Height));

                //System.Windows.Media.Color textColor = System.Windows.Media.Color.FromArgb(TextColor.A, TextColor.R, TextColor.G, TextColor.B);
                var textBrush = new SolidColorBrush(textColor);
                var textPen = new Pen(textBrush, 0);
                FormattedText text;
                if (progressType == ProgressType.Day)
                {
                    text = new FormattedText("Day  in Progress", System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Segoe UI"), 42, textBrush);
                    context.DrawText(text, new Point(16, 20));
                } else if (progressType == ProgressType.Year)
                {
                    text = new FormattedText("Year in Progress", System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Segoe UI"), 42, textBrush);
                    context.DrawText(text, new Point(20, 20));
                }
                else if (progressType == ProgressType.Life)
                {
                    text = new FormattedText("Life in Progress", System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Segoe UI"), 42, textBrush);
                    context.DrawText(text, new Point(20, 20));
                }




                    var now = DateTime.Now;
                float progress = 0;
                if(progressType == ProgressType.Year)
                {
                    progress = (float)now.DayOfYear / (DateTime.IsLeapYear(now.Year) ? 366.0f : 365.0f);
                } else if (progressType == ProgressType.Day)
                {
                    progress = (float)now.TimeOfDay.TotalHours/24.0f;
                }
                else if (progressType == ProgressType.Life)
                {
                    var birthDate = LifeInProgressSettings.Default.BirthDate;
                    var lifeSpan = now - birthDate;
                    progress = Math.Min(1.0f, (float)lifeSpan.TotalDays / (LifeInProgressSettings.Default.AnticipatedLifeSpan * 365.25f));
                    
                }

                text = new FormattedText($"{(progress * 100).ToString("0.0")} %", System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Segoe UI"), 32, textBrush);
                context.DrawText(text, new Point(130, 80));

                //System.Windows.Media.Color foregroundColor = System.Windows.Media.Color.FromArgb(Foreground.A, Foreground.R, Foreground.G, Foreground.B);
                var foregroundBrush = new SolidColorBrush(foregroundColor);
                var foregroundPen = new Pen(foregroundBrush, 0);
                context.DrawRectangle(foregroundBrush, foregroundPen, new Rect(18, 148, 1 * ((float)Width - 40f+4f), 44));
                context.DrawRectangle(backgroundBrush, backgroundPen, new Rect(20, 150, 1 * ((float)Width - 40f), 40));
                context.DrawRectangle(foregroundBrush, foregroundPen, new Rect(22, 152, progress * ((float)Width-44f), 36));


                
            }

            context.Close();
            renderTargetBitmap.Render(visual);
            
            using (var outputStream = new FileStream(fileName, FileMode.Create))
            {
                BitmapEncoder encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(renderTargetBitmap));
                encoder.Save(outputStream);
            }
        }

        void UpdateDayInProgress()
        {
            var fg = DayInProgressSettings.Default.ForegroundColor;
            var bg = DayInProgressSettings.Default.BackgroundColor;
            var tc = DayInProgressSettings.Default.TextColor;

            String filePath;
            DoOnMain(() =>
            {
                DayInProgressImage = null;
                GenerateImage(ProgressType.Day, filePath = Path.Combine(OutputPath, "Fip6.jpg"), System.Windows.Media.Color.FromArgb(fg.A, fg.R, fg.G, fg.B), System.Windows.Media.Color.FromArgb(bg.A, bg.R, bg.G, bg.B), System.Windows.Media.Color.FromArgb(tc.A, tc.R, tc.G, tc.B));
                DayInProgressImagePath = filePath;
                BitmapImage bi = new BitmapImage();
                
                bi.BeginInit();
                //bi.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                bi.CacheOption = BitmapCacheOption.OnLoad;
                var fileBytes = File.ReadAllBytes(filePath);
                MemoryStream ms = new MemoryStream(fileBytes);
                bi.StreamSource = ms;
                bi.EndInit();
                //bi.Freeze();
                DayInProgressImage = bi;
                OnPropertyChanged(nameof(DayInProgressImagePath));
            });
        }

        void UpdateYearInProgress()
        {
            var fg = YearInProgressSettings.Default.ForegroundColor;
            var bg = YearInProgressSettings.Default.BackgroundColor;
            var tc = YearInProgressSettings.Default.TextColor;
            DoOnMain(() =>
            {
                string filePath;
                GenerateImage(ProgressType.Year, filePath = Path.Combine(OutputPath, "Fip7.jpg"), System.Windows.Media.Color.FromArgb(fg.A, fg.R, fg.G, fg.B), System.Windows.Media.Color.FromArgb(bg.A, bg.R, bg.G, bg.B), System.Windows.Media.Color.FromArgb(tc.A, tc.R, tc.G, tc.B));
                BitmapImage bi = new BitmapImage();
                bi.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                var fileBytes = File.ReadAllBytes(filePath);
                MemoryStream ms = new MemoryStream(fileBytes);
                bi.BeginInit();
                bi.StreamSource = ms;
                bi.EndInit();
                bi.Freeze();
                YearInProgressImage = bi;

            });
        }

        void UpdateLifeInProgress()
        {
            var fg = LifeInProgressSettings.Default.ForegroundColor;
            var bg = LifeInProgressSettings.Default.BackgroundColor;
            var tc = LifeInProgressSettings.Default.TextColor;
            DoOnMain(() =>
            {
                string filePath;
                GenerateImage(ProgressType.Life, filePath = Path.Combine(OutputPath, "Fip8.jpg"), System.Windows.Media.Color.FromArgb(fg.A, fg.R, fg.G, fg.B), System.Windows.Media.Color.FromArgb(bg.A, bg.R, bg.G, bg.B), System.Windows.Media.Color.FromArgb(tc.A, tc.R, tc.G, tc.B));
                BitmapImage bi = new BitmapImage();
                bi.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                bi.BeginInit();
                bi.StreamSource = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                bi.EndInit();
                bi.Freeze();
                LifeInProgressImage = bi;
            });
        }


        String GetDirectOutputPath()
        {
            using (RegistryKey hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
            {
                using (RegistryKey key = hklm.OpenSubKey(@"SOFTWARE\Saitek\DirectOutput"))
                {
                    if (key != null)
                    {
                        Object o = key.GetValue("DirectOutput_Saitek");
                        if (o != null)
                        {
                            return o.ToString();
                        }
                    }
                }
            }
            return null;
        }

        DispatcherTimer _dayTimer, _yearTimer,_lifeTimer;

        public MainViewModel()
        {

            var path = GetDirectOutputPath();
            if (!String.IsNullOrEmpty(path))
            {
                OutputPath = (new FileInfo(path)).DirectoryName;
            }

            //Delete old files
            FileInfo fip6 = new FileInfo(outputPath + "\\Fip6.jpg");
            FileInfo fip7 = new FileInfo(outputPath + "\\Fip7.jpg");
            FileInfo fip8 = new FileInfo(outputPath + "\\Fip8.jpg");
            FileInfo[] fipList = { fip6, fip7, fip8 };
            foreach (FileInfo fip in fipList)
            {
                if (fip.Exists)
                {
                    fip.Delete();
                }
            }


            var dayRefreshInterval = DayInProgressSettings.Default.RefreshInterval;
            if(dayRefreshInterval.TotalMinutes> 0)
            {
                _dayTimer = new DispatcherTimer();
                _dayTimer.Interval= dayRefreshInterval;
                _dayTimer.Tick += (s, e) => UpdateDayInProgress();
                _dayTimer.Start();
                UpdateDayInProgress();
            }

            var yearRefreshInterval = YearInProgressSettings.Default.RefreshInterval;
            if(yearRefreshInterval.TotalMinutes> 0)
            {
                _yearTimer = new DispatcherTimer();                
                _yearTimer.Interval= yearRefreshInterval;
                _yearTimer.Tick += (s, e) => UpdateYearInProgress();
                _yearTimer.Start();
                UpdateYearInProgress();
            }

            var lifeRefreshInterval = LifeInProgressSettings.Default.RefreshInterval;
            if(lifeRefreshInterval.TotalMinutes> 0)
            {
                _lifeTimer = new DispatcherTimer();                
                _lifeTimer.Interval= lifeRefreshInterval;
                _lifeTimer.Tick += (s, e) => UpdateLifeInProgress();
                _lifeTimer.Start();
                UpdateLifeInProgress();
            }
            

        }
    }
}
