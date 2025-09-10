using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using UtfUnknown;

namespace Codirovka
{
    public partial class Form1 : Form
    {
        private string filePath = "";
        public Form1()
        {
            InitializeComponent();
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            comboTarget.Items.AddRange(new string[] { "CP866", "CP1251", "MacCyrillic", "CP65001" });
            comboTarget.SelectedIndex = 3; //utf8 standart
        }

        private Encoding DetectEncoding(string path)
        {
            try
            {
                var result = CharsetDetector.DetectFromFile(path);
                if (result.Detected != null && result.Detected.Confidence > 0.5f) 
                {
                    return result.Detected.Encoding ?? Encoding.UTF8;  
                }
                return Encoding.UTF8;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка определения: {ex.Message}");
                return Encoding.UTF8;
            }
        }
        private void buttonSelect_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Текстовые файлы|*.txt|Все файлы|*.*";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                filePath = dialog.FileName;
                textFilePath.Text = filePath;
                Encoding detected = DetectEncoding(filePath);
                string detectedName = detected.EncodingName ?? "Кодировка неизвестна";
                //MessageBox.Show($"Обнаружена кодировка: {detectedName}, CodePage: {detected.CodePage}");
                if (detected.CodePage == 10007) detectedName = "MacCyrillic";
                labelDetect.Text = $"Обнаружена кодировка: {detectedName}";
                labelStatus.Text = "Ожидание конвертации";
            }
        }

        private void buttonConvert_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(filePath) || labelDetect.Text.Contains("Кодировка неизвестна"))
            {
                MessageBox.Show("Выберите файл для определения исходной кодировки");
                return;
            }
            if (comboTarget.SelectedItem == null)
            {
                MessageBox.Show("Выберите новую кодировку");
                return;
            }
            Encoding sourceEnc = DetectEncoding(filePath);
            string encodingName = comboTarget.SelectedItem.ToString();
            Encoding newEnc = Encoding.UTF8;
            try
            {
                switch (encodingName)
                {
                    case "CP866":
                        newEnc = Encoding.GetEncoding(866);
                        break;
                    case "CP1251":
                        newEnc = Encoding.GetEncoding(1251);
                        break;
                    case "MacCyrillic":
                        newEnc = Encoding.GetEncoding(10007);
                        break;
                    case "CP65001":
                        newEnc = Encoding.UTF8;
                        break;
                    default:
                        newEnc = Encoding.UTF8;
                        MessageBox.Show($"Неизвестная целевая кодировка: {encodingName}. Используем UTF-8.");
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка конвертации: {ex.Message}");
                return;
            }
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Текстовые файлы|*.txt";
            saveDialog.FileName = Path.GetFileNameWithoutExtension(filePath) + "_" + encodingName + ".txt";
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string content = File.ReadAllText(filePath, sourceEnc);
                    File.WriteAllText(saveDialog.FileName, content, newEnc);
                    labelStatus.Text = $"Конвертировано в {encodingName} Сохранено: {saveDialog.FileName}";
                    MessageBox.Show("Готово");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка конвертации: {ex.Message}");
                }
            }
        }
    }
}