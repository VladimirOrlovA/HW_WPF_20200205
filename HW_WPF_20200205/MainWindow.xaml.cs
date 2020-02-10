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
using System.Xml.Linq;

namespace HW_WPF_20200205
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Item> items = new List<Item>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnGetInfo_Click(object sender, RoutedEventArgs e)
        {
            labelException.Foreground = Brushes.LimeGreen;
            labelException.Content = "Считали успешно";

            XmlDocument xmlDoc = new XmlDocument();

            try
            {
                xmlDoc.Load("https://habrahabr.ru/rss/interesting/");
            }
            catch (Exception eXml)
            {
                labelException.Foreground = Brushes.Red;
                string errMes = eXml.ToString();
                labelException.Content = errMes.Substring(errMes.IndexOf(':') + 1); ;
                return;
            }

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

                    if (xmlNodeChild.Name == "item")
                    {
                        countNews++;
                        Item item = new Item();

                        foreach (XmlNode xmlItem in xmlNodeChild)
                        {
                            if (xmlItem.Name == "title") item.title = xmlItem.InnerText;
                            if (xmlItem.Name == "link") item.link = xmlItem.InnerText;
                            if (xmlItem.Name == "description") item.description = xmlItem.InnerText;
                            if (xmlItem.Name == "pubDate") item.pubDate = xmlItem.InnerText;
                        }

                        items.Add(item);
                    }
                }

            labelCountNews.Content = countNews;
        }

        private void CreateXML_Click(object sender, RoutedEventArgs e)
        {
            labelExceptionCreateXML.Foreground = Brushes.Lime;
            labelExceptionCreateXML.Content = "Считываем...";

            XDocument xDoc = new XDocument();
            XElement xItems = new XElement("items");

            foreach (Item itemObj in items)
            {
                XElement item = new XElement("item");
                XElement title = new XElement("title", itemObj.title);
                XElement link = new XElement("link", itemObj.link);
                XElement description = new XElement("description", itemObj.description);
                XElement pubDate = new XElement("pupDate", itemObj.pubDate);

                item.Add(title);
                item.Add(link);
                item.Add(description);
                item.Add(pubDate);

                xItems.Add(item);
            }

            xDoc.Add(xItems);

            try
            {
                xDoc.Save(tbPathAndFileName.Text);
            }
            catch (Exception eCreateXML)
            {
                labelExceptionCreateXML.Foreground = Brushes.Red;
                string errMes = eCreateXML.ToString();
                labelExceptionCreateXML.Content = errMes.Substring(errMes.IndexOf(':') + 1);
                return;
            }

            labelExceptionCreateXML.Foreground = Brushes.LimeGreen;
            labelExceptionCreateXML.Content = "Файл успешно записан.";
        }
    }
}
