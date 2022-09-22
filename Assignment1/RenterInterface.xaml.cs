using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Data;
using System.Reflection;

namespace Assignment1
{
    /// <summary>
    /// Interaction logic for Renter.xaml
    /// </summary>
    public partial class RenterInterface : Window
    {
        public Button activeBtn;
        public Button activeIcon;
        public RenterInterface()
        {
            InitializeComponent();

            var bookingfiles = Directory.EnumerateFiles(@"ads\", "*.txt");

            ObservableCollection<Flat> collectionBooking = new ObservableCollection<Flat>();

            foreach (string currentFile in bookingfiles)
            {
                string[] lines = File.ReadAllLines(currentFile);
                Assembly ass = Assembly.GetExecutingAssembly();
                string path = System.IO.Path.GetDirectoryName(ass.Location);
                string imagepath = path + "/" + lines[2];
                collectionBooking.Add(new Flat() { Uid = lines[0], Owner = lines[1], ImageDirectory = imagepath, Area = lines[4], Areaname = lines[5], Blk = lines[6], Street = lines[7], Zipcode = lines[8], Description = lines[9], Noofroom = lines[10] + " room", Type = lines[11], Pax = lines[12]+ " Pax", Price = "$" + lines[13] + " / Day" });
            }

            var rentsfiles = Directory.EnumerateFiles(@"rents\", LoginSession.Email + "*" + ".txt");
            ObservableCollection<YourRent> collectionRents = new ObservableCollection<YourRent>();

            foreach (string currentFile in rentsfiles)
            {
                string[] lines = File.ReadAllLines(currentFile);
                collectionRents.Add(new YourRent() { Uid = lines[0], Owner = lines[1], ImageDirectory = lines[2], Area = lines[4], Areaname = lines[5], Blk = lines[6], Street = lines[7], Zipcode = lines[8], Description = lines[9], Noofroom = lines[10], Type = lines[11],Pax = lines[12], Price = lines[13], Duration = int.Parse(lines[14]), Status = lines[15] });
            }
            nameTxt.Text = LoginSession.Name;
            flatListBox.ItemsSource = collectionBooking;
            rentListView.ItemsSource = collectionRents;
            activeBtn = btnBooking;
            activeIcon = iconBook;
            setActive(btnBooking);
            setIconActive(activeIcon);
            btnShow.IsEnabled = false;


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
        public Flat SelectedFlat { get; set; }

        private void btnSelected_Click(object sender, RoutedEventArgs e)
        {
            pnlRightBooking.Visibility = Visibility.Collapsed;
            selectedFlat.Visibility = Visibility.Visible;

            Uri imageUri = new Uri(SelectedFlat.ImageDirectory, UriKind.Absolute);
            BitmapImage imageBitmap = new BitmapImage(imageUri);
            selectedImage.Source = imageBitmap;
            selectedAddress.Text = "Blk " + SelectedFlat.Blk + " " + SelectedFlat.Street + " Singapore " + SelectedFlat.Zipcode;
            selectedDescription.Text = SelectedFlat.Description;
            selectedPrice.Text = "$" + SelectedFlat.Price;
            selectedRoom.Text = SelectedFlat.Noofroom;
            selectedPax.Text = SelectedFlat.Pax;
            selectedType.Text = SelectedFlat.Type;
            selectedOwner.Text = SelectedFlat.Owner;
            string area = SelectedFlat.Areaname.Replace(' ', '+');
            
            string geocodeURL = "https://maps.googleapis.com/maps/api/geocode/xml?address=" + area;
            string xmlDoc = httpCall(geocodeURL);
            int startPosition = xmlDoc.IndexOf("<location>") + 10;
            int endPosition = xmlDoc.IndexOf("</lng>") - 1;
            int noChars = endPosition - startPosition + 10;
            string coordinates = xmlDoc.Substring(startPosition, noChars);

            int startlat = coordinates.IndexOf("<lat>") + 5;
            int endlat = coordinates.IndexOf("</lat>") - 5;
            int noCharsLat = endlat - startlat + 5;
            string lat = coordinates.Substring(startlat, noCharsLat);

            int startlng = coordinates.IndexOf("<lng>") + 5;
            int endlng = coordinates.IndexOf("</lng>") - 5;
            int noCharsLng = endlng - startlng + 5;
            string lng = coordinates.Substring(startlng, noCharsLng);

            string url = "http://maps.googleapis.com/maps/api/staticmap?zoom=14&size=720x250&markers=color:red|" + lat + "," + lng;

            BitmapImage bmpImage = new BitmapImage();
            bmpImage.BeginInit();
            bmpImage.UriSource = new Uri(url);
            bmpImage.EndInit();

            selectedMap.Source = bmpImage;

        }
        public void setWidth(int width)
        {
            ProfileBorder.Width = width;
            btnBooking.Width = width;
            btnRents.Width = width;
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
            opacityAnimation.Completed += (s, a) => iconBook.Opacity = opacityto;
            opacityAnimation.Completed += (s, a) => iconRent.Opacity = opacityto;
            opacityAnimation.Completed += (s, a) => iconAbout.Opacity = opacityto;
            opacityAnimation.Completed += (s, a) => iconSetting.Opacity = opacityto;

            ProfileBorder.BeginAnimation(Border.WidthProperty, myDoubleAnimation);
            btnRents.BeginAnimation(Button.WidthProperty, myDoubleAnimation);
            btnAbout.BeginAnimation(Button.WidthProperty, myDoubleAnimation);
            btnBooking.BeginAnimation(Button.WidthProperty, myDoubleAnimation);
            btnSetting.BeginAnimation(Button.WidthProperty, myDoubleAnimation);
            iconBook.BeginAnimation(Button.OpacityProperty, opacityAnimation);
            iconRent.BeginAnimation(Button.OpacityProperty, opacityAnimation);
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

        private void btnRents_Click(object sender, RoutedEventArgs e)
        {
            pnlRightBooking.Visibility = Visibility.Collapsed;
            pnlRightRents.Visibility = Visibility.Visible;
            activeBtn = btnRents;
            activeIcon = iconRent;
            setActive(activeBtn);
        }
        public void setActive(Button btn1)
        {
            btnBooking.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFE72939"));
            btnBooking.Foreground = System.Windows.Media.Brushes.White;

            btnRents.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFE72939"));
            btnRents.Foreground = System.Windows.Media.Brushes.White;


            btnAbout.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFE72939"));
            btnAbout.Foreground = System.Windows.Media.Brushes.White;

            btnSetting.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFE72939"));
            btnSetting.Foreground = System.Windows.Media.Brushes.White;

            btn1.Background = System.Windows.Media.Brushes.White;
            btn1.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFE72939"));


        }
        public void setIconActive(Button btn2)
        {

            iconBook.Opacity = 1;
            iconRent.Opacity = 1;
            iconSetting.Opacity = 1;
            iconAbout.Opacity = 1;
            btn2.Opacity = 0.5;
        }

        private void iconBook_Click(object sender, RoutedEventArgs e)
        {

            pnlRightBooking.Visibility = Visibility.Visible;
            pnlRightRents.Visibility = Visibility.Collapsed;
            activeBtn = btnBooking;
            activeIcon = iconBook;
            setIconActive(activeIcon);
            setActive(activeBtn);

        }

        private void iconRent_Click(object sender, RoutedEventArgs e)
        {
            pnlRightBooking.Visibility = Visibility.Collapsed;
            pnlRightRents.Visibility = Visibility.Visible;
            activeBtn = btnRents;
            activeIcon = iconRent;
            setIconActive(activeIcon);
            setActive(activeBtn);
        }

        private void btnBooking_Click(object sender, RoutedEventArgs e)
        {
            pnlRightBooking.Visibility = Visibility.Visible;
            pnlRightRents.Visibility = Visibility.Collapsed;
            activeBtn = btnBooking;
            activeIcon = iconBook;
            setActive(activeBtn);
        }

        private void btnAbout_Click(object sender, RoutedEventArgs e)
        {
            pnlRightBooking.Visibility = Visibility.Collapsed;
            pnlRightRents.Visibility = Visibility.Collapsed;
            activeBtn = btnAbout;
            activeIcon = iconAbout;
            setActive(activeBtn);
        }

        private void iconAbout_Click(object sender, RoutedEventArgs e)
        {
            pnlRightBooking.Visibility = Visibility.Collapsed;
            pnlRightRents.Visibility = Visibility.Collapsed;
            activeBtn = btnAbout;
            activeIcon = iconAbout;
            setIconActive(activeIcon);
            setActive(activeBtn);
        }

        private void btnSetting_Click(object sender, RoutedEventArgs e)
        {
            pnlRightBooking.Visibility = Visibility.Collapsed;
            pnlRightRents.Visibility = Visibility.Collapsed;
            activeBtn = btnSetting;
            activeIcon = iconSetting;
            setActive(activeBtn);
        }

        private void iconSetting_Click(object sender, RoutedEventArgs e)
        {
            pnlRightBooking.Visibility = Visibility.Collapsed;
            pnlRightRents.Visibility = Visibility.Collapsed;
            activeBtn = btnSetting;
            activeIcon = iconSetting;
            setIconActive(activeIcon);
            setActive(activeBtn);
        }
        static string httpCall(string url)
        {
            WebRequest request = WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            reader.Close();
            response.Close();
            return responseFromServer;
        }

        private void selectedBack_Click(object sender, RoutedEventArgs e)
        {
            selectedFlat.Visibility = Visibility.Collapsed;
            pnlRightBooking.Visibility = Visibility.Visible;
        }

        private void flatListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (flatListBox.SelectedIndex != -1)
            {
                btnShow.IsEnabled = true;
            }
        }

        private void selectedRent_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
