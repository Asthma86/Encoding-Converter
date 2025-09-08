namespace Codirovka
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            textFilePath = new TextBox();
            labelDetect = new Label();
            comboTarget = new ComboBox();
            labelStatus = new Label();
            buttonSelect = new Button();
            buttonConvert = new Button();
            SuspendLayout();
            // 
            // textFilePath
            // 
            textFilePath.Location = new Point(48, 77);
            textFilePath.Name = "textFilePath";
            textFilePath.Size = new Size(275, 23);
            textFilePath.TabIndex = 0;
            // 
            // labelDetect
            // 
            labelDetect.AutoSize = true;
            labelDetect.Location = new Point(47, 110);
            labelDetect.Name = "labelDetect";
            labelDetect.Size = new Size(124, 15);
            labelDetect.TabIndex = 1;
            labelDetect.Text = "Исходная кодировка:";
            // 
            // comboTarget
            // 
            comboTarget.FormattingEnabled = true;
            comboTarget.Location = new Point(48, 137);
            comboTarget.Name = "comboTarget";
            comboTarget.Size = new Size(275, 23);
            comboTarget.TabIndex = 2;
            // 
            // labelStatus
            // 
            labelStatus.AutoSize = true;
            labelStatus.Location = new Point(48, 173);
            labelStatus.Name = "labelStatus";
            labelStatus.Size = new Size(138, 15);
            labelStatus.TabIndex = 3;
            labelStatus.Text = "Ожидание конвертации";
            // 
            // buttonSelect
            // 
            buttonSelect.Location = new Point(382, 77);
            buttonSelect.Name = "buttonSelect";
            buttonSelect.Size = new Size(125, 23);
            buttonSelect.TabIndex = 4;
            buttonSelect.Text = "Выбрать файл";
            buttonSelect.UseVisualStyleBackColor = true;
            buttonSelect.Click += buttonSelect_Click;
            // 
            // buttonConvert
            // 
            buttonConvert.Location = new Point(382, 136);
            buttonConvert.Name = "buttonConvert";
            buttonConvert.Size = new Size(125, 23);
            buttonConvert.TabIndex = 5;
            buttonConvert.Text = "Конвертировать";
            buttonConvert.UseVisualStyleBackColor = true;
            buttonConvert.Click += buttonConvert_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(buttonConvert);
            Controls.Add(buttonSelect);
            Controls.Add(labelStatus);
            Controls.Add(comboTarget);
            Controls.Add(labelDetect);
            Controls.Add(textFilePath);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textFilePath;
        private Label labelDetect;
        private ComboBox comboTarget;
        private Label labelStatus;
        private Button buttonSelect;
        private Button buttonConvert;
    }
}