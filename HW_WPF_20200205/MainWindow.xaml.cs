using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Xml;

namespace HW_WPF_20200205
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnGetInfo_Click(object sender, RoutedEventArgs e)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("https://habrahabr.ru/rss/interesting/");

            XmlElement xmlRoot = xmlDoc.DocumentElement;

            int countNews = 0;

            foreach (XmlNode xmlNode in xmlRoot)
                foreach (XmlNode xmlNodeChild in xmlNode)
                {
                    if (xmlNodeChild.Name == "title") labelTitle.Content = xmlNodeChild.InnerText;
                    if (xmlNodeChild.Name == "description") labelDescription.Content = xmlNodeChild.InnerText;
                    if (xmlNodeChild.Name == "managingEditor") labelManagingEditor.Content = xmlNodeChild.InnerText;
                    if (xmlNodeChild.Name == "generator") labelGenerator.Content = xmlNodeChild.InnerText;
                    if (xmlNodeChild.Name == "pubDate") labelPubDate.Content = xmlNodeChild.InnerText;
                    if (xmlNodeChild.Name == "item") countNews++;
                }

            labelCountNews.Content = countNews;
        }
    }
}
