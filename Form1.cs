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

        private string DetectEncoding(string path)
        {
            try
            {
                var result = CharsetDetector.DetectFromFile(path);
                if (result.Detected != null && result.Detected.Confidence > 0.5f) 
                {
                    return result.Detected.EncodingName ?? "UTF-8";  
                }
                return "��������� ����������";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"������ �����������: {ex.Message}");
                return "UTF-8";
            }
        }
        private void buttonSelect_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "��������� �����|*.txt|��� �����|*.*";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                filePath = dialog.FileName;
                textFilePath.Text = filePath;
                string detected = DetectEncoding(filePath);
                labelDetect.Text = $"���������� ���������: {detected}";
                labelStatus.Text = "�������� �����������";
            }
        }

        private void buttonConvert_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(filePath) || labelDetect.Text.Contains("��������� ����������"))
            {
                MessageBox.Show("�������� ���� ��� ����������� �������� ���������");
                return;
            }
            if (comboTarget.SelectedItem == null)
            {
                MessageBox.Show("�������� ����� ���������");
                return;
            }
            string detectedName = labelDetect.Text.Replace("���������� ���������: ", "");
            string encodingName = comboTarget.SelectedItem.ToString();
            MessageBox.Show($"DetectedName: {detectedName}, EncodingName: {encodingName}");
            int codePage;
            if (detectedName == "��������� ����������")  // �������� �������� �� ����������� ��������� (ToLower() ������)
            {
                codePage = 65001;  // Fallback UTF-8
            }
            else
            {
                switch (detectedName)
                {
                    case "ibm866":
                    case "cp866":
                    case "866":
                        codePage = 866;
                        break;
                    case "windows-1251":
                    case "cp1251":
                    case "1251":
                        codePage = 1251;
                        break;
                    case "x-mac-cyrillic":
                    case "maccyrillic":
                    case "10007":
                        codePage = 10007;
                        break;
                    case "utf-8":
                        codePage = 65001;
                        break;
                    default:
                        codePage = 65001;
                        MessageBox.Show($"����������� ��� ���������: {detectedName}. ���������� UTF-8 (65001).");
                        break;
                }
            }
                Encoding sourceEnc;
            try
            {
                sourceEnc = Encoding.GetEncoding(codePage);
            }
            catch (Exception ex)
            {
                MessageBox.Show("�� ������� �������� �������� ���������. ���������� UTF-8.");
                sourceEnc = Encoding.UTF8;
            }

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
                        MessageBox.Show($"����������� ������� ���������: {encodingName}. ���������� UTF-8.");
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"������ �����������: {ex.Message}");
                return;
            }
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "��������� �����|*.txt";
            saveDialog.FileName = Path.GetFileNameWithoutExtension(filePath) + "_" + encodingName + ".txt";
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string content = File.ReadAllText(filePath, sourceEnc);
                    File.WriteAllText(saveDialog.FileName, content, newEnc);
                    labelStatus.Text = $"�������������� � {encodingName} ���������: {saveDialog.FileName}";
                    MessageBox.Show("������");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"������ �����������: {ex.Message}");
                }
            }
        }
    }
}