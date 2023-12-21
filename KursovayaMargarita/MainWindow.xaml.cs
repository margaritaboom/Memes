using Microsoft.Win32;
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
using System.IO;
using Newtonsoft.Json;

namespace KursovayaMargarita
{
    public partial class MainWindow : Window
    {
        private MemeManager memeManager;
        public MainWindow()
        {
            InitializeComponent();
            memeManager = new MemeManager();
            memeManager.LoadMemes();
            memeManager.UpdateMemeList(ListBoxMem);
        }
        private void Button_Save_Click(object sender, RoutedEventArgs e)
        {
            memeManager.SaveMeme(TextName, TextURL, cb_select, rb_url, rb_pc);
        }

        private void Button_Delete_Click(object sender, RoutedEventArgs e)
        {
            memeManager.DeleteMeme(ListBoxMem);
        }

        private void Button_Load_Click(object sender, RoutedEventArgs e)
        {
            memeManager.LoadMemes();
            memeManager.UpdateMemeList(ListBoxMem);
        }

        private void ListBoxMem_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            memeManager.ChangeSelectedMeme(ListBoxMem, ImageMem);
        }

        private void ComboBoxCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            memeManager.FilterMemesByCategory(ListBoxMem, ComboBoxCategory);
        }

        private void Button_Search_Click(object sender, RoutedEventArgs e)
        {
            memeManager.SearchMemes(ListBoxMem, TextSearch);
        }
    }
}
