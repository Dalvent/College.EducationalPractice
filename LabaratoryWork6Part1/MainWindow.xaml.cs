using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace LabaratoryWork6Part1
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

        private void openFile1_Click(object sender, RoutedEventArgs e) =>
            wordsTextBox1.Text = GetFileStringWithDialog();
        private void openFile2_Click(object sender, RoutedEventArgs e) =>
            wordsTextBox2.Text = GetFileStringWithDialog();
        public static string OnlyContainsWords(string string1, string string2)
        {
            var string1Words = SplitByWord(string1);
            var string2Words = SplitByWord(string2).Select(s => s.ToLower()).ToArray();
            var builder = new StringBuilder();
            foreach (var string1Word in string1Words)
            {
                if (string2Words.Contains(string1Word.ToLower()))
                {
                    builder.Append(string1Word + " ");
                }
            }
            return builder.ToString().TrimEnd();
        }

        private static string[] SplitByWord(string value) => value.Split(' ')
            .Select(s => RemoveSymbols(s, new char[] { ',', '.', '!', '?', '(', ')', '[', ']', '{', '}' })).ToArray();

        private static string RemoveSymbols(string value, params char[] symbols)
        {
            string result = value;
            foreach (var symbol in symbols)
            {
                result = result.Replace(symbol.ToString(), string.Empty);
            }
            return result;
        }

        private void showOnlyContains_Click(object sender, RoutedEventArgs e)
        {
            contatinsWordsTextBox.Text = OnlyContainsWords(wordsTextBox1.Text, wordsTextBox2.Text);
        }

        private string GetFileStringWithDialog()
        {
            var dialog = new OpenFileDialog();
            if(dialog.ShowDialog() == true)
            {
                return File.ReadAllText(dialog.FileName);
            }
            return null;
        }

        private void searchFile1_Click(object sender, RoutedEventArgs e)
        {
            var findedWords = GetAllContatinsWords(wordsTextBox1.Text, searchBox.Text);
            var messageBuilder = new StringBuilder();
            messageBuilder.Append("Найденные слова:\n");
            foreach (var findedWord in findedWords)
            {
                messageBuilder.Append($"{findedWord};\n");
            }
            MessageBox.Show(messageBuilder.ToString());
        }

        private IEnumerable<string> GetAllContatinsWords(string str, string value)
        {
            foreach (var word in SplitByWord(str))
            {
                if(word.ToLower().Contains(value.ToLower()))
                {
                    yield return word;
                }
            }
        }
    }
}
