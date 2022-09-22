using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Animation;
using System.IO;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Reflection;

namespace Assignment1
{
    /// <summary>
    /// Interaction logic for Renter.xaml
    /// </summary>
    /// 
    public partial class OwnerInterface : Window
    {
        public Button activeBtn;
        public Button activeIcon;
        public OwnerInterface()
        {
            InitializeComponent();
            nameTxt.Text = LoginSession.Name;

            activeBtn = btnAds;
            activeIcon = iconAds;
            setActive(activeBtn);
            setIconActive(activeIcon);

            var files = Directory.EnumerateFiles(@"ads\", LoginSession.Email + "*.txt");
            ObservableCollection<Flat> collection = new ObservableCollection<Flat>();

            foreach (string currentFile in files)
            {

                string[] lines = File.ReadAllLines(currentFile);
                Assembly ass = Assembly.GetExecutingAssembly();
                string path = System.IO.Path.GetDirectoryName(ass.Location);
                string imagepath = path + "/" + lines[2];
                collection.Add(new Flat() { Uid = lines[0], Owner = lines[1], ImageDirectory = imagepath, Area = lines[4], Areaname = lines[5], Blk = lines[6], Street = lines[7], Zipcode = lines[8], Description = lines[9], Noofroom = lines[10], Type = lines[11], Pax = lines[12], Price = "$"+lines[13]+" / Day" });
                
            }

            flatListBox.ItemsSource = collection;
            btnSubmit.IsEnabled = false;

        }
        public class Flat
        {
            public string Uid { get; set; }
            public string Owner { get; set; }
            public string ImageDirectory { get; set; }
            public string Area { get; set; }
            public string Areaname { get; set; }
            public string Blk { get; set; }
            public string Street { get; set; }
            public string Zipcode { get; set; }
            public string Description { get; set; }
            public string Noofroom { get; set; }
            public string Type { get; set; }
            public string Pax { get; set; }
            public string Price { get; set; }
        }



        private void btnLeftMenuHide_Click(object sender, RoutedEventArgs e)
        {
            ShowHideMenu("sbHideLeftMenu", btnLeftMenuHide, btnLeftMenuShow, pnlLeftMenu);


            animateMenu(285, 235, 0, 1, 1, activeIcon, 0, 0.5);
            setActive(activeBtn);
        }

        private void btnLeftMenuShow_Click(object sender, RoutedEventArgs e)
        {
            ShowHideMenu("sbShowLeftMenu", btnLeftMenuHide, btnLeftMenuShow, pnlLeftMenu);

            animateMenu(235, 285, 1, 0, .5, activeIcon, 0.5, 0);
            setActive(activeBtn);
        }
        private void ShowHideMenu(string Storyboard, Button btnHide, Button btnShow, StackPanel pnl)
        {
            Storyboard sb = Resources[Storyboard] as Storyboard;
            sb.Begin(pnl);

            if (Storyboard.Contains("Show"))
            {
                btnHide.Visibility = System.Windows.Visibility.Visible;
                btnShow.Visibility = System.Windows.Visibility.Collapsed;
                setWidth(285);
            }
            else if (Storyboard.Contains("Hide"))
            {
                btnHide.Visibility = System.Windows.Visibility.Collapsed;
                btnShow.Visibility = System.Windows.Visibility.Visible;
                setWidth(235);
            }
        }
        private void ShowHideMenu(string Storyboard, Button btnHide, Button btnShow, Grid pnl)
        {
            Storyboard sb = Resources[Storyboard] as Storyboard;
            sb.Begin(pnl);

            if (Storyboard.Contains("Show"))
            {
                btnHide.Visibility = System.Windows.Visibility.Visible;
                btnShow.Visibility = System.Windows.Visibility.Collapsed;
                setWidth(285);
            }
            else if (Storyboard.Contains("Hide"))
            {
                btnHide.Visibility = System.Windows.Visibility.Collapsed;
                btnShow.Visibility = System.Windows.Visibility.Visible;
                setWidth(235);
            }
        }
        public void setActive(Button btn1)
        {
            btnAds.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFE72939"));
            btnAds.Foreground = System.Windows.Media.Brushes.White;

            btnYourFlat.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFE72939"));
            btnYourFlat.Foreground = System.Windows.Media.Brushes.White;


            btnAbout.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFE72939"));
            btnAbout.Foreground = System.Windows.Media.Brushes.White;

            btnSetting.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFE72939"));
            btnSetting.Foreground = System.Windows.Media.Brushes.White;

            btn1.Background = System.Windows.Media.Brushes.White;
            btn1.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFE72939"));


        }
        public void setIconActive(Button btn2)
        {

            iconAds.Opacity = 1;
            iconYourFlat.Opacity = 1;
            iconSetting.Opacity = 1;
            iconAbout.Opacity = 1;
            btn2.Opacity = 0.5;
        }

        public void setWidth(int width)
        {
            ProfileBorder.Width = width;
            btnAds.Width = width;
            btnYourFlat.Width = width;
            btnAbout.Width = width;
            btnSetting.Width = width;
        }
        public void animateMenu(int from, int to, int opacityfrom, int opacityto, double opacityseconds, Button activeIcon, double iconOpacityFrom, double iconOpacityTo)
        {
            DoubleAnimation myDoubleAnimation = new DoubleAnimation();
            myDoubleAnimation.From = from;
            myDoubleAnimation.To = to;
            myDoubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(1));

            DoubleAnimation opacityAnimation = new DoubleAnimation();
            opacityAnimation.From = opacityfrom;
            opacityAnimation.To = opacityto;
            opacityAnimation.Duration = new Duration(TimeSpan.FromSeconds(opacityseconds));
            opacityAnimation.FillBehavior = FillBehavior.Stop;
            opacityAnimation.Completed += (s, a) => iconAds.Opacity = opacityto;
            opacityAnimation.Completed += (s, a) => iconYourFlat.Opacity = opacityto;
            opacityAnimation.Completed += (s, a) => iconAbout.Opacity = opacityto;
            opacityAnimation.Completed += (s, a) => iconSetting.Opacity = opacityto;

            ProfileBorder.BeginAnimation(Border.WidthProperty, myDoubleAnimation);
            btnYourFlat.BeginAnimation(Button.WidthProperty, myDoubleAnimation);
            btnAbout.BeginAnimation(Button.WidthProperty, myDoubleAnimation);
            btnAds.BeginAnimation(Button.WidthProperty, myDoubleAnimation);
            btnSetting.BeginAnimation(Button.WidthProperty, myDoubleAnimation);
            iconAds.BeginAnimation(Button.OpacityProperty, opacityAnimation);
            iconYourFlat.BeginAnimation(Button.OpacityProperty, opacityAnimation);
            iconAbout.BeginAnimation(Button.OpacityProperty, opacityAnimation);
            iconSetting.BeginAnimation(Button.OpacityProperty, opacityAnimation);

            animateOpacity(activeIcon, iconOpacityFrom, iconOpacityTo, opacityseconds);
        }
        public void animateOpacity(Button opacity, double iconOpacityFrom, double iconOpacityTo, double opacityseconds)
        {

            DoubleAnimation activeAnimation = new DoubleAnimation();
            activeAnimation.From = iconOpacityFrom;
            activeAnimation.To = iconOpacityTo;
            activeAnimation.Duration = new Duration(TimeSpan.FromSeconds(opacityseconds));
            activeAnimation.FillBehavior = FillBehavior.Stop;
            activeAnimation.Completed += (s, a) => opacity.Opacity = iconOpacityTo;

            opacity.BeginAnimation(Button.OpacityProperty, activeAnimation);
        }
        private void streetTxt_LostFocus(object sender, RoutedEventArgs e)
        {
            string street = streetTxt.Text;
            if (string.IsNullOrEmpty(street))
            {
                streetError.Visibility = Visibility.Visible;
                streetExc.Visibility = Visibility.Visible;
                btnSubmit.IsEnabled = false;
            }
            else if (!checkValidation())
            {
                btnSubmit.IsEnabled = false;
            }
            else
            {
                streetError.Visibility = Visibility.Collapsed;
                streetExc.Visibility = Visibility.Collapsed;
                btnSubmit.IsEnabled = true;

            }
        }
        private void blkTxt_LostFocus(object sender, RoutedEventArgs e)
        {
            string blk = blkTxt.Text;
            if (string.IsNullOrEmpty(blk))
            {
                blkError.Visibility = Visibility.Visible;
                blkExc.Visibility = Visibility.Visible;
                btnSubmit.IsEnabled = false;
            }
            else if (!checkValidation())
            {
                btnSubmit.IsEnabled = false;
            }
            else
            {
                blkError.Visibility = Visibility.Collapsed;
                blkExc.Visibility = Visibility.Collapsed;
                btnSubmit.IsEnabled = true;

            }
        }
        private void zipCodeTxt_LostFocus(object sender, RoutedEventArgs e)
        {

            string zip = zipCodeTxt.Text;
            int zipcode;
            if (string.IsNullOrEmpty(zip) || !int.TryParse(zip, out zipcode))
            {
                zipCodeError.Visibility = Visibility.Visible;
                zipCodeExc.Visibility = Visibility.Visible;
                btnSubmit.IsEnabled = false;
            }
            else if (!checkValidation())
            {
                btnSubmit.IsEnabled = false;
            }
            else 
            {
                zipCodeError.Visibility = Visibility.Collapsed;
                zipCodeExc.Visibility = Visibility.Collapsed;
                btnSubmit.IsEnabled = true;

            }
        }

        private void areaName_LostFocus(object sender, RoutedEventArgs e)
        {
            string areaName = areaNameTxt.Text;
            if (string.IsNullOrEmpty(areaName))
            {
                areaNameError.Visibility = Visibility.Visible;
                areaNameExc.Visibility = Visibility.Visible;
                btnSubmit.IsEnabled = false;
            }
            else if (!checkValidation())
            {
                btnSubmit.IsEnabled = false;
            }
            else
            {
                areaNameError.Visibility = Visibility.Hidden;
                areaNameExc.Visibility = Visibility.Hidden;
                btnSubmit.IsEnabled = true;
            }
        }

        private void noOfRoom_LostFocus(object sender, RoutedEventArgs e)
        {
            string noOfRoom = noOfRoomTxt.Text;
            int number;
            if (string.IsNullOrEmpty(noOfRoom) || !int.TryParse(noOfRoom,out number))
            {
                noOfRoomError.Visibility = Visibility.Visible;
                noOfRoomExc.Visibility = Visibility.Visible;
                btnSubmit.IsEnabled = false;
            }
            else
            {
                noOfRoomError.Visibility = Visibility.Hidden;
                noOfRoomExc.Visibility = Visibility.Hidden;
                btnSubmit.IsEnabled = true;
            }
        }

        private void pax_LostFocus(object sender, RoutedEventArgs e)
        {
            string pax = paxTxt.Text;
            int numberpax;
            if (string.IsNullOrEmpty(pax) || !int.TryParse(pax, out numberpax))
            {
                paxError.Visibility = Visibility.Visible;
                paxExc.Visibility = Visibility.Visible;
                btnSubmit.IsEnabled = false;
            }
            else
            {
                paxError.Visibility = Visibility.Hidden;
                paxExc.Visibility = Visibility.Hidden;
                btnSubmit.IsEnabled = true;
            }
        }

        private void price_LostFocus(object sender, RoutedEventArgs e)
        {
            string price = priceTxt.Text;
            int pricetag;
            if (string.IsNullOrEmpty(price) || !int.TryParse(price, out pricetag))
            {
                priceError.Visibility = Visibility.Visible;
                priceExc.Visibility = Visibility.Visible;
                btnSubmit.IsEnabled = false;
            }
            else
            {
                priceError.Visibility = Visibility.Hidden;
                priceExc.Visibility = Visibility.Hidden;
                btnSubmit.IsEnabled = true;
            }
        }

        private void desc_LostFocus(object sender, RoutedEventArgs e)
        {
            string desc = descTxt.Text;
            if (string.IsNullOrEmpty(desc))
            {
                descError.Visibility = Visibility.Visible;
                descExc.Visibility = Visibility.Visible;
                btnSubmit.IsEnabled = false;
            }
            else
            {
                descError.Visibility = Visibility.Hidden;
                descExc.Visibility = Visibility.Hidden;
                btnSubmit.IsEnabled = true;
            }
        }


        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            string street = streetTxt.Text;
            string blk = blkTxt.Text;
            string zipcode = zipCodeTxt.Text;
            string desc = descTxt.Text;
            string area = comboBoxArea.SelectedItem.ToString();
            string type = comboBoxType.SelectedItem.ToString();
            string areaName = areaNameTxt.Text;
            string noOfRoom = noOfRoomTxt.Text;
            string pax = paxTxt.Text;
            double price = double.Parse(priceTxt.Text);
            string uid = generateID();


            StreamWriter streamwriter = new StreamWriter(@"ads\" + LoginSession.Email + uid + ".txt");

            Grid r = new Grid();
            r.Background = new ImageBrush(imgPhoto.Source);


            System.Windows.Size sz = new System.Windows.Size(imgPhoto.Source.Width, imgPhoto.Source.Height);
            r.Measure(sz);
            r.Arrange(new Rect(sz));

            RenderTargetBitmap rtb = new RenderTargetBitmap((int)imgPhoto.Source.Width, (int)imgPhoto.Source.Height, 96d, 96d, PixelFormats.Default);
            rtb.Render(r);

            BmpBitmapEncoder encoder = new BmpBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(rtb));

            FileStream fs = File.Open(@"ads\" + LoginSession.Email + uid + ".jpg", FileMode.Create);
            encoder.Save(fs);
            fs.Close();
            streamwriter.WriteLine(uid);
            streamwriter.WriteLine(LoginSession.Email);
            streamwriter.WriteLine(@"ads\" + LoginSession.Email + uid + ".jpg");

            streamwriter.WriteLine("From: " + fromDatePicker + " To: " + toDatePicker);

            streamwriter.WriteLine(area);
            streamwriter.WriteLine(areaName);
            streamwriter.WriteLine(blk);
            streamwriter.WriteLine(street);
            streamwriter.WriteLine(zipcode);
            streamwriter.WriteLine(desc);
            streamwriter.WriteLine(noOfRoom);
            streamwriter.WriteLine(type);
            streamwriter.WriteLine(pax);
            streamwriter.WriteLine(price);
            streamwriter.Close();

            MessageBox.Show("Your Ads have been published successfully !", "Success", MessageBoxButton.OK,MessageBoxImage.Asterisk);


            imgPhoto.Source = null;
            fromDatePicker.Text = String.Empty;
            toDatePicker.Text = String.Empty;
            streetTxt.Text = String.Empty;
            blkTxt.Text = String.Empty;
            zipCodeTxt.Text = String.Empty;
            descTxt.Text = String.Empty;
            areaNameTxt.Text = String.Empty;
            noOfRoomTxt.Text = String.Empty;
            paxTxt.Text = String.Empty;
            priceTxt.Text = String.Empty;


            var files = Directory.EnumerateFiles(@"ads\", LoginSession.Email + "*.txt");
            ObservableCollection<Flat> collection = new ObservableCollection<Flat>();

            foreach (string currentFile in files)
            {

                string[] lines = File.ReadAllLines(currentFile);
                Assembly ass = Assembly.GetExecutingAssembly();
                string path = System.IO.Path.GetDirectoryName(ass.Location);
                string imagepath = path + "/" + lines[2];
                collection.Add(new Flat() { Uid = lines[0], Owner = lines[1], ImageDirectory = imagepath, Area = lines[4], Areaname = lines[5], Blk = lines[6], Street = lines[7], Zipcode = lines[8], Description = lines[9], Noofroom = lines[10], Type = lines[11], Pax = lines[12], Price = String.Format("{0:C} / Day", Convert.ToDecimal(lines[13])) });

            }

            flatListBox.ItemsSource = collection;


        }



        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                imgPhoto.Source = new BitmapImage(new Uri(op.FileName));
            }
        }

        private void areaBox_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> data = new List<string>();
            data.Add("North");
            data.Add("South");
            data.Add("East");
            data.Add("West");
            var areabox = sender as ComboBox;
            areabox.ItemsSource = data;
            areabox.SelectedIndex = 0;
        }


        private void areaBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedarea = sender as ComboBox;
            string area = selectedarea.SelectedItem as string;

        }

        private void typeBox_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> data = new List<string>();
            data.Add("HDB");
            data.Add("Condo");
            data.Add("Landed");
            var typebox = sender as ComboBox;
            typebox.ItemsSource = data;
            typebox.SelectedIndex = 0;
        }


        private void typeBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedtype = sender as ComboBox;
            string type = selectedtype.SelectedItem as string;
        }

        private void btnAds_Click(object sender, RoutedEventArgs e)
        {
            pnlRightAds.Visibility = Visibility.Visible;
            pnlRightFlats.Visibility = Visibility.Collapsed;
            pnlRightAbout.Visibility = Visibility.Collapsed;

            activeBtn = btnAds;
            activeIcon = iconAds;
            setActive(activeBtn);

        }

        private void btnYourFlat_Click(object sender, RoutedEventArgs e)
        {
            pnlRightAds.Visibility = Visibility.Collapsed;
            pnlRightFlats.Visibility = Visibility.Visible;
            pnlRightAbout.Visibility = Visibility.Collapsed;

            activeBtn = btnYourFlat;
            activeIcon = iconYourFlat;
            setActive(activeBtn);

        }

        private void btnAbout_Click(object sender, RoutedEventArgs e)
        {
            pnlRightAds.Visibility = Visibility.Collapsed;
            pnlRightFlats.Visibility = Visibility.Collapsed;
            pnlRightAbout.Visibility = Visibility.Visible;

            activeBtn = btnAbout;
            activeIcon = iconAbout;
            setActive(activeBtn);

        }

        private void iconAds_Click(object sender, RoutedEventArgs e)
        {
            pnlRightAds.Visibility = Visibility.Visible;
            pnlRightFlats.Visibility = Visibility.Collapsed;
            pnlRightAbout.Visibility = Visibility.Collapsed;

            activeBtn = btnAds;
            activeIcon = iconAds;
            setIconActive(activeIcon);
            setActive(activeBtn);

        }

        private void iconYourFlat_Click(object sender, RoutedEventArgs e)
        {
            pnlRightAds.Visibility = Visibility.Collapsed;
            pnlRightFlats.Visibility = Visibility.Visible;
            pnlRightAbout.Visibility = Visibility.Visible;

            activeBtn = btnYourFlat;
            activeIcon = iconYourFlat;
            setIconActive(activeIcon);
            setActive(activeBtn);

        }

        private void iconAbout_Click(object sender, RoutedEventArgs e)
        {
            pnlRightAds.Visibility = Visibility.Collapsed;
            pnlRightFlats.Visibility = Visibility.Collapsed;
            pnlRightAbout.Visibility = Visibility.Visible;

            activeBtn = btnAbout;
            activeIcon = iconAbout;
            setIconActive(activeIcon);
            setActive(activeBtn);

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            var button = sender as Button;
            if (button != null)
            {
                var task = button.DataContext as Flat;

                ((ObservableCollection<Flat>)flatListBox.ItemsSource).Remove(task);
                MessageBox.Show("Your listed flat has been successfully deleted");
                System.IO.File.Delete(@"ads\" + LoginSession.Email + ".txt");
            }
            else
            {
                return;
            }

        }
        public bool checkValidation()
        {
            string fromDate = fromDatePicker.Text;
            string toDate = toDatePicker.Text;
            string street = streetTxt.Text;
            string blk = blkTxt.Text;
            string zip = zipCodeTxt.Text;
            string areaName = areaNameTxt.Text;
            string noOfRoom = noOfRoomTxt.Text;
            string desc = descTxt.Text;
            string pax = paxTxt.Text;
            string price = priceTxt.Text;
            bool check;
            if (string.IsNullOrEmpty(fromDate) && string.IsNullOrEmpty(toDate) && string.IsNullOrEmpty(street) && string.IsNullOrEmpty(blk) && string.IsNullOrEmpty(zip) && string.IsNullOrEmpty(areaName) && string.IsNullOrEmpty(noOfRoom) && string.IsNullOrEmpty(desc) && string.IsNullOrEmpty(pax) && string.IsNullOrEmpty(price))
            {
                check = false;
            }
            else
            {
                check = true;
            }

            return check;
        }
        public bool checkAddress()
        {
            string street = streetTxt.Text;
            string blk = blkTxt.Text;
            string zip = zipCodeTxt.Text;
            bool check;
            if(string.IsNullOrEmpty(street) && string.IsNullOrEmpty(blk) && string.IsNullOrEmpty(zip))
            {
                check = false;
            }
            else
            {
                check = true;
            }
            return check;
        }
        public string generateID()
        {
            return Guid.NewGuid().ToString("N");
        }
        
    }
}