using IBANDesktopApp.IBAN;
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

namespace IBANDesktopApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IIbanValidator validator;
        private string selectedFilePath;

        public MainWindow()
        {
            validator = new IbanValidator();

            InitializeComponent();
        }

        private void checkFileButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.selectedFilePath))
            {
                this.fileResultTextBlock.Text = "Nepasirinktas tinkamas failas.";
                return;
            }
            var readFile = new StreamReader(selectedFilePath);
            string line = "";
            StringBuilder stringBuilder = new StringBuilder();
            while ((line = readFile.ReadLine()) != null)
            {
                if (validator.ValidateIban(line))
                    stringBuilder.AppendLine(line + ";true");
                else
                    stringBuilder.AppendLine(line + ";false");
            }
            readFile.Close();

            var writePath = System.IO.Path.GetDirectoryName(selectedFilePath)
                + "\\" + System.IO.Path.GetFileNameWithoutExtension(selectedFilePath) + ".out";

            StreamWriter writeFile = new System.IO.StreamWriter(writePath);
            writeFile.WriteLine(stringBuilder.ToString());
            this.fileResultTextBlock.Text = "Rezultatai įrašyti į " + writePath;
            writeFile.Close();
        }

        private void openFileDialogButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.DefaultExt = "*.*";
            fileDialog.Multiselect = false;
            var result = fileDialog.ShowDialog();
            if (result == false || result == null)
                return;

            this.selectedFilePath = fileDialog.FileName;
            this.fileResultTextBlock.Text = "Pasirinktas failas: " + selectedFilePath;
        }

        private void checkTextButton_Click(object sender, RoutedEventArgs e)
        {
            var iban = this.ibanInput.Text;
            if (string.IsNullOrWhiteSpace(iban))
            {
                this.resultTextBlock.Text = "IBAN nėra įvestas.";
                return;
            }
            if (validator.ValidateIban(iban))
                this.resultTextBlock.Text = "IBAN: " + iban + " yra teisingas.";
            else
                this.resultTextBlock.Text = "IBAN: " + iban + " yra neteisingas.";
        }
    }
}
