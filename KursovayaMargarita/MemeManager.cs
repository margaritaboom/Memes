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
    public class MemeManager
    {
        private List<Meme> memes = new List<Meme>();

        public void LoadMemes()
        {
            try
            {
                var json = File.ReadAllText("memes.json");
                memes = JsonConvert.DeserializeObject<List<Meme>>(json);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке мемов: {ex.Message}");
            }
        }

        public void SaveMemes()
        {
            try
            {
                var json = JsonConvert.SerializeObject(memes);
                File.WriteAllText("memes.json", json);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении мемов: {ex.Message}");
            }
        }

        public void UpdateMemeList(ListBox ListBoxMem)
        {
            ListBoxMem.Items.Clear();
            foreach (var meme in memes)
            {
                ListBoxMem.Items.Add(meme.Title);
            }
        }

        public void SaveMeme(TextBox TextName, TextBox TextURL, ComboBox cb_select, RadioButton rb_url, RadioButton rb_pc)
        {
            var meme = new Meme
            {
                Title = TextName.Text,
                Category = cb_select.Text,
            };
            if (rb_url.IsChecked == true)
            {
                meme.ImagePath = TextURL.Text;
            }
            else if (rb_pc.IsChecked == true)
            {
                var openFileDialog = new OpenFileDialog();
                if (openFileDialog.ShowDialog() == true)
                {
                    meme.ImagePath = openFileDialog.FileName;
                }
            }
            memes.Add(meme);
            SaveMemes();
        }

        public void DeleteMeme(ListBox ListBoxMem)
        {
            var selectedMeme = memes.FirstOrDefault(meme => meme.Title == (string)ListBoxMem.SelectedItem);
            if (selectedMeme != null)
            {
                memes.Remove(selectedMeme);
                UpdateMemeList(ListBoxMem);
            }
            SaveMemes();
        }

        public void ChangeSelectedMeme(ListBox ListBoxMem, Image ImageMem)
        {
            var selectedMeme = memes.FirstOrDefault(meme => meme.Title == (string)ListBoxMem.SelectedItem);
            if (selectedMeme != null)
            {
                ImageMem.Source = new BitmapImage(new Uri(selectedMeme.ImagePath));
            }
        }

        public void FilterMemesByCategory(ListBox ListBoxMem, ComboBox ComboBoxCategory)
        {
            var category = (ComboBoxCategory.SelectedItem as ComboBoxItem).Content.ToString();
            var filteredMemes = memes.Where(meme => meme.Category == category).ToList();
            ListBoxMem.Items.Clear();
            foreach (var meme in filteredMemes)
            {
                ListBoxMem.Items.Add(meme.Title);
            }
        }
        public void SearchMemes(ListBox ListBoxMem, TextBox TextSearch)
        {
            var searchText = TextSearch.Text.ToLower();
            var filteredMemes = memes.Where(meme => meme.Title.ToLower().Contains(searchText)).ToList();
            ListBoxMem.Items.Clear();
            foreach (var meme in filteredMemes)
            {
                ListBoxMem.Items.Add(meme.Title);
            }
        }
    }
}
