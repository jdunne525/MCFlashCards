namespace MCFlashCards
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            PrintPreviewDialog1 = new PrintPreviewDialog();
            PrintDocument1 = new System.Drawing.Printing.PrintDocument();
            OpenFileDialog1 = new OpenFileDialog();
            ShowResultTimer = new System.Windows.Forms.Timer(components);
            RestartButton = new Button();
            RandomCheckBox = new CheckBox();
            OpenButton = new Button();
            ApplicationLabel = new Label();
            btnPrintPreview = new Button();
            TabPage2 = new TabPage();
            lblFlashCardStatus = new Label();
            btnNextFlashCard = new Button();
            btnPrevFlashCard = new Button();
            btnFlashCard = new Button();
            TabPage1 = new TabPage();
            RadioEnglish = new RadioButton();
            RadioPinYin = new RadioButton();
            DataSetLabel = new Label();
            NextDataSet = new Button();
            PrevDataSet = new Button();
            Card4 = new Button();
            Card3 = new Button();
            Card2 = new Button();
            Card1 = new Button();
            TabControl1 = new TabControl();
            TestWordLabel = new Label();
            Label3 = new Label();
            Label1 = new Label();
            lblStreak = new Label();
            lblBestStreak = new Label();
            Label2 = new Label();
            Label4 = new Label();
            lblTotalIncorrect = new Label();
            Label6 = new Label();
            lblTotalCompleted = new Label();
            Label8 = new Label();
            lblFirstTryCorrect = new Label();
            Label5 = new Label();
            TabPage2.SuspendLayout();
            TabPage1.SuspendLayout();
            TabControl1.SuspendLayout();
            SuspendLayout();
            // 
            // PrintPreviewDialog1
            // 
            PrintPreviewDialog1.AutoScrollMargin = new Size(0, 0);
            PrintPreviewDialog1.AutoScrollMinSize = new Size(0, 0);
            PrintPreviewDialog1.ClientSize = new Size(400, 300);
            PrintPreviewDialog1.Enabled = true;
            PrintPreviewDialog1.Icon = (Icon)resources.GetObject("PrintPreviewDialog1.Icon");
            PrintPreviewDialog1.Name = "PrintPreviewDialog1";
            PrintPreviewDialog1.Visible = false;
            // 
            // OpenFileDialog1
            // 
            OpenFileDialog1.FileName = "OpenFileDialog1";
            // 
            // ShowResultTimer
            // 
            ShowResultTimer.Interval = 500;
            ShowResultTimer.Tick += ShowResultTimer_Tick;
            // 
            // RestartButton
            // 
            RestartButton.Location = new Point(838, 51);
            RestartButton.Margin = new Padding(4, 3, 4, 3);
            RestartButton.Name = "RestartButton";
            RestartButton.Size = new Size(74, 28);
            RestartButton.TabIndex = 26;
            RestartButton.Text = "Restart";
            RestartButton.UseVisualStyleBackColor = true;
            RestartButton.Click += RestartButton_Click;
            // 
            // RandomCheckBox
            // 
            RandomCheckBox.AutoSize = true;
            RandomCheckBox.Location = new Point(777, 234);
            RandomCheckBox.Margin = new Padding(4, 3, 4, 3);
            RandomCheckBox.Name = "RandomCheckBox";
            RandomCheckBox.Size = new Size(88, 19);
            RandomCheckBox.TabIndex = 29;
            RandomCheckBox.Text = "All Random";
            RandomCheckBox.UseVisualStyleBackColor = true;
            // 
            // OpenButton
            // 
            OpenButton.Location = new Point(838, 16);
            OpenButton.Margin = new Padding(4, 3, 4, 3);
            OpenButton.Name = "OpenButton";
            OpenButton.Size = new Size(74, 28);
            OpenButton.TabIndex = 25;
            OpenButton.Text = "Open";
            OpenButton.UseVisualStyleBackColor = true;
            OpenButton.Click += OpenButton_Click;
            // 
            // ApplicationLabel
            // 
            ApplicationLabel.Font = new Font("Lucida Fax", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ApplicationLabel.Location = new Point(2, 2);
            ApplicationLabel.Margin = new Padding(4, 0, 4, 0);
            ApplicationLabel.Name = "ApplicationLabel";
            ApplicationLabel.Size = new Size(751, 62);
            ApplicationLabel.TabIndex = 28;
            ApplicationLabel.Text = "Math Facts";
            ApplicationLabel.TextAlign = ContentAlignment.MiddleCenter;
            ApplicationLabel.Visible = false;
            // 
            // btnPrintPreview
            // 
            btnPrintPreview.Location = new Point(737, 51);
            btnPrintPreview.Margin = new Padding(4, 3, 4, 3);
            btnPrintPreview.Name = "btnPrintPreview";
            btnPrintPreview.Size = new Size(93, 28);
            btnPrintPreview.TabIndex = 31;
            btnPrintPreview.Text = "Print Preview";
            btnPrintPreview.UseVisualStyleBackColor = true;
            btnPrintPreview.Click += btnPrintPreview_Click;
            // 
            // TabPage2
            // 
            TabPage2.Controls.Add(lblFlashCardStatus);
            TabPage2.Controls.Add(btnNextFlashCard);
            TabPage2.Controls.Add(btnPrevFlashCard);
            TabPage2.Controls.Add(btnFlashCard);
            TabPage2.Location = new Point(4, 24);
            TabPage2.Margin = new Padding(4, 3, 4, 3);
            TabPage2.Name = "TabPage2";
            TabPage2.Padding = new Padding(4, 3, 4, 3);
            TabPage2.Size = new Size(901, 605);
            TabPage2.TabIndex = 1;
            TabPage2.Text = "FlashCards";
            TabPage2.UseVisualStyleBackColor = true;
            // 
            // lblFlashCardStatus
            // 
            lblFlashCardStatus.AutoSize = true;
            lblFlashCardStatus.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblFlashCardStatus.Location = new Point(762, 38);
            lblFlashCardStatus.Margin = new Padding(4, 0, 4, 0);
            lblFlashCardStatus.Name = "lblFlashCardStatus";
            lblFlashCardStatus.Size = new Size(51, 17);
            lblFlashCardStatus.TabIndex = 21;
            lblFlashCardStatus.Text = "Label1";
            // 
            // btnNextFlashCard
            // 
            btnNextFlashCard.Location = new Point(814, 7);
            btnNextFlashCard.Margin = new Padding(4, 3, 4, 3);
            btnNextFlashCard.Name = "btnNextFlashCard";
            btnNextFlashCard.Size = new Size(42, 28);
            btnNextFlashCard.TabIndex = 19;
            btnNextFlashCard.Text = ">";
            btnNextFlashCard.UseVisualStyleBackColor = true;
            btnNextFlashCard.Click += btnNextFlashCard_Click;
            // 
            // btnPrevFlashCard
            // 
            btnPrevFlashCard.Location = new Point(765, 7);
            btnPrevFlashCard.Margin = new Padding(4, 3, 4, 3);
            btnPrevFlashCard.Name = "btnPrevFlashCard";
            btnPrevFlashCard.Size = new Size(42, 28);
            btnPrevFlashCard.TabIndex = 20;
            btnPrevFlashCard.Text = "<";
            btnPrevFlashCard.UseVisualStyleBackColor = true;
            btnPrevFlashCard.Click += btnPrevFlashCard_Click;
            // 
            // btnFlashCard
            // 
            btnFlashCard.BackColor = Color.LightGray;
            btnFlashCard.FlatStyle = FlatStyle.Flat;
            btnFlashCard.Font = new Font("Microsoft Sans Serif", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnFlashCard.Location = new Point(7, 7);
            btnFlashCard.Margin = new Padding(4, 3, 4, 3);
            btnFlashCard.Name = "btnFlashCard";
            btnFlashCard.Size = new Size(751, 585);
            btnFlashCard.TabIndex = 13;
            btnFlashCard.Text = "Button1";
            btnFlashCard.UseVisualStyleBackColor = false;
            // 
            // TabPage1
            // 
            TabPage1.BackColor = Color.FromArgb(255, 224, 192);
            TabPage1.Controls.Add(RadioEnglish);
            TabPage1.Controls.Add(RadioPinYin);
            TabPage1.Controls.Add(DataSetLabel);
            TabPage1.Controls.Add(NextDataSet);
            TabPage1.Controls.Add(PrevDataSet);
            TabPage1.Controls.Add(Card4);
            TabPage1.Controls.Add(Card3);
            TabPage1.Controls.Add(Card2);
            TabPage1.Controls.Add(Card1);
            TabPage1.Location = new Point(4, 24);
            TabPage1.Margin = new Padding(4, 3, 4, 3);
            TabPage1.Name = "TabPage1";
            TabPage1.Padding = new Padding(4, 3, 4, 3);
            TabPage1.Size = new Size(901, 605);
            TabPage1.TabIndex = 0;
            TabPage1.Text = "Multiple Choice Cards";
            // 
            // RadioEnglish
            // 
            RadioEnglish.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            RadioEnglish.AutoSize = true;
            RadioEnglish.Location = new Point(768, 103);
            RadioEnglish.Margin = new Padding(4, 3, 4, 3);
            RadioEnglish.Name = "RadioEnglish";
            RadioEnglish.Size = new Size(123, 19);
            RadioEnglish.TabIndex = 20;
            RadioEnglish.Text = "Answer - Question";
            RadioEnglish.UseVisualStyleBackColor = true;
            // 
            // RadioPinYin
            // 
            RadioPinYin.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            RadioPinYin.AutoSize = true;
            RadioPinYin.Checked = true;
            RadioPinYin.Location = new Point(768, 76);
            RadioPinYin.Margin = new Padding(4, 3, 4, 3);
            RadioPinYin.Name = "RadioPinYin";
            RadioPinYin.Size = new Size(123, 19);
            RadioPinYin.TabIndex = 19;
            RadioPinYin.TabStop = true;
            RadioPinYin.Text = "Question - Answer";
            RadioPinYin.UseVisualStyleBackColor = true;
            // 
            // DataSetLabel
            // 
            DataSetLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            DataSetLabel.AutoSize = true;
            DataSetLabel.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            DataSetLabel.Location = new Point(757, 38);
            DataSetLabel.Margin = new Padding(4, 0, 4, 0);
            DataSetLabel.Name = "DataSetLabel";
            DataSetLabel.Size = new Size(51, 17);
            DataSetLabel.TabIndex = 18;
            DataSetLabel.Text = "Label1";
            // 
            // NextDataSet
            // 
            NextDataSet.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            NextDataSet.Enabled = false;
            NextDataSet.Location = new Point(810, 7);
            NextDataSet.Margin = new Padding(4, 3, 4, 3);
            NextDataSet.Name = "NextDataSet";
            NextDataSet.Size = new Size(42, 28);
            NextDataSet.TabIndex = 16;
            NextDataSet.Text = ">";
            NextDataSet.UseVisualStyleBackColor = true;
            NextDataSet.Click += NextDataSet_Click;
            // 
            // PrevDataSet
            // 
            PrevDataSet.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            PrevDataSet.Enabled = false;
            PrevDataSet.Location = new Point(761, 7);
            PrevDataSet.Margin = new Padding(4, 3, 4, 3);
            PrevDataSet.Name = "PrevDataSet";
            PrevDataSet.Size = new Size(42, 28);
            PrevDataSet.TabIndex = 17;
            PrevDataSet.Text = "<";
            PrevDataSet.UseVisualStyleBackColor = true;
            PrevDataSet.Click += PrevDataSet_Click;
            // 
            // Card4
            // 
            Card4.BackColor = Color.FromArgb(255, 255, 192);
            Card4.FlatStyle = FlatStyle.Flat;
            Card4.Font = new Font("Microsoft Sans Serif", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Card4.Location = new Point(384, 303);
            Card4.Margin = new Padding(4, 3, 4, 3);
            Card4.Name = "Card4";
            Card4.Size = new Size(370, 290);
            Card4.TabIndex = 15;
            Card4.Text = "Button4";
            Card4.UseVisualStyleBackColor = false;
            Card4.Click += Card4_Click;
            // 
            // Card3
            // 
            Card3.BackColor = Color.FromArgb(255, 255, 192);
            Card3.FlatStyle = FlatStyle.Flat;
            Card3.Font = new Font("Microsoft Sans Serif", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Card3.Location = new Point(7, 303);
            Card3.Margin = new Padding(4, 3, 4, 3);
            Card3.Name = "Card3";
            Card3.Size = new Size(370, 290);
            Card3.TabIndex = 14;
            Card3.Text = "Button3";
            Card3.UseVisualStyleBackColor = false;
            Card3.Click += Card3_Click;
            // 
            // Card2
            // 
            Card2.BackColor = Color.FromArgb(255, 255, 192);
            Card2.FlatStyle = FlatStyle.Flat;
            Card2.Font = new Font("Microsoft Sans Serif", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Card2.Location = new Point(384, 7);
            Card2.Margin = new Padding(4, 3, 4, 3);
            Card2.Name = "Card2";
            Card2.Size = new Size(370, 290);
            Card2.TabIndex = 13;
            Card2.Text = "Button2";
            Card2.UseVisualStyleBackColor = false;
            Card2.Click += Card2_Click;
            // 
            // Card1
            // 
            Card1.BackColor = Color.FromArgb(255, 255, 192);
            Card1.FlatStyle = FlatStyle.Flat;
            Card1.Font = new Font("Microsoft Sans Serif", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Card1.Location = new Point(7, 7);
            Card1.Margin = new Padding(4, 3, 4, 3);
            Card1.Name = "Card1";
            Card1.Size = new Size(370, 290);
            Card1.TabIndex = 12;
            Card1.Text = "Button1";
            Card1.UseVisualStyleBackColor = false;
            Card1.Click += Card1_Click;
            // 
            // TabControl1
            // 
            TabControl1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            TabControl1.Controls.Add(TabPage1);
            TabControl1.Controls.Add(TabPage2);
            TabControl1.Location = new Point(2, 175);
            TabControl1.Margin = new Padding(4, 3, 4, 3);
            TabControl1.Name = "TabControl1";
            TabControl1.SelectedIndex = 0;
            TabControl1.Size = new Size(909, 633);
            TabControl1.TabIndex = 30;
            // 
            // TestWordLabel
            // 
            TestWordLabel.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TestWordLabel.Location = new Point(2, 91);
            TestWordLabel.Margin = new Padding(4, 0, 4, 0);
            TestWordLabel.Name = "TestWordLabel";
            TestWordLabel.Size = new Size(751, 62);
            TestWordLabel.TabIndex = 27;
            TestWordLabel.Text = "TestWordLabel";
            TestWordLabel.TextAlign = ContentAlignment.BottomCenter;
            // 
            // Label3
            // 
            Label3.AutoSize = true;
            Label3.Font = new Font("Microsoft Sans Serif", 111.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label3.ForeColor = Color.FromArgb(192, 0, 0);
            Label3.Location = new Point(588, -77);
            Label3.Margin = new Padding(4, 0, 4, 0);
            Label3.Name = "Label3";
            Label3.Size = new Size(122, 169);
            Label3.TabIndex = 33;
            Label3.Text = "-";
            Label3.Visible = false;
            // 
            // Label1
            // 
            Label1.AutoSize = true;
            Label1.Location = new Point(840, 83);
            Label1.Margin = new Padding(4, 0, 4, 0);
            Label1.Name = "Label1";
            Label1.Size = new Size(42, 15);
            Label1.TabIndex = 34;
            Label1.Text = "Streak:";
            // 
            // lblStreak
            // 
            lblStreak.AutoSize = true;
            lblStreak.Location = new Point(896, 83);
            lblStreak.Margin = new Padding(4, 0, 4, 0);
            lblStreak.Name = "lblStreak";
            lblStreak.Size = new Size(13, 15);
            lblStreak.TabIndex = 35;
            lblStreak.Text = "0";
            // 
            // lblBestStreak
            // 
            lblBestStreak.AutoSize = true;
            lblBestStreak.Location = new Point(896, 98);
            lblBestStreak.Margin = new Padding(4, 0, 4, 0);
            lblBestStreak.Name = "lblBestStreak";
            lblBestStreak.Size = new Size(13, 15);
            lblBestStreak.TabIndex = 36;
            lblBestStreak.Text = "0";
            // 
            // Label2
            // 
            Label2.AutoSize = true;
            Label2.Font = new Font("Microsoft Sans Serif", 72F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label2.ForeColor = Color.FromArgb(0, 192, 0);
            Label2.Location = new Point(63, -31);
            Label2.Margin = new Padding(4, 0, 4, 0);
            Label2.Name = "Label2";
            Label2.Size = new Size(101, 108);
            Label2.TabIndex = 32;
            Label2.Text = "+";
            Label2.Visible = false;
            // 
            // Label4
            // 
            Label4.AutoSize = true;
            Label4.Location = new Point(812, 98);
            Label4.Margin = new Padding(4, 0, 4, 0);
            Label4.Name = "Label4";
            Label4.Size = new Size(67, 15);
            Label4.TabIndex = 39;
            Label4.Text = "Best Streak:";
            // 
            // lblTotalIncorrect
            // 
            lblTotalIncorrect.AutoSize = true;
            lblTotalIncorrect.Location = new Point(890, 130);
            lblTotalIncorrect.Margin = new Padding(4, 0, 4, 0);
            lblTotalIncorrect.Name = "lblTotalIncorrect";
            lblTotalIncorrect.Size = new Size(13, 15);
            lblTotalIncorrect.TabIndex = 37;
            lblTotalIncorrect.Text = "0";
            // 
            // Label6
            // 
            Label6.AutoSize = true;
            Label6.Location = new Point(791, 130);
            Label6.Margin = new Padding(4, 0, 4, 0);
            Label6.Name = "Label6";
            Label6.Size = new Size(85, 15);
            Label6.TabIndex = 40;
            Label6.Text = "Total Incorrect:";
            // 
            // lblTotalCompleted
            // 
            lblTotalCompleted.AutoSize = true;
            lblTotalCompleted.Location = new Point(890, 145);
            lblTotalCompleted.Margin = new Padding(4, 0, 4, 0);
            lblTotalCompleted.Name = "lblTotalCompleted";
            lblTotalCompleted.Size = new Size(13, 15);
            lblTotalCompleted.TabIndex = 38;
            lblTotalCompleted.Text = "0";
            // 
            // Label8
            // 
            Label8.AutoSize = true;
            Label8.Location = new Point(791, 145);
            Label8.Margin = new Padding(4, 0, 4, 0);
            Label8.Name = "Label8";
            Label8.Size = new Size(97, 15);
            Label8.TabIndex = 41;
            Label8.Text = "Total Completed:";
            // 
            // lblFirstTryCorrect
            // 
            lblFirstTryCorrect.AutoSize = true;
            lblFirstTryCorrect.Location = new Point(890, 115);
            lblFirstTryCorrect.Margin = new Padding(4, 0, 4, 0);
            lblFirstTryCorrect.Name = "lblFirstTryCorrect";
            lblFirstTryCorrect.Size = new Size(13, 15);
            lblFirstTryCorrect.TabIndex = 42;
            lblFirstTryCorrect.Text = "0";
            // 
            // Label5
            // 
            Label5.AutoSize = true;
            Label5.Location = new Point(774, 115);
            Label5.Margin = new Padding(4, 0, 4, 0);
            Label5.Name = "Label5";
            Label5.Size = new Size(102, 15);
            Label5.TabIndex = 43;
            Label5.Text = "First Try Correct %";
            // 
            // frmMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(919, 812);
            Controls.Add(Label5);
            Controls.Add(lblFirstTryCorrect);
            Controls.Add(Label8);
            Controls.Add(lblTotalCompleted);
            Controls.Add(Label6);
            Controls.Add(lblTotalIncorrect);
            Controls.Add(Label4);
            Controls.Add(lblBestStreak);
            Controls.Add(lblStreak);
            Controls.Add(Label1);
            Controls.Add(Label3);
            Controls.Add(Label2);
            Controls.Add(btnPrintPreview);
            Controls.Add(ApplicationLabel);
            Controls.Add(OpenButton);
            Controls.Add(TestWordLabel);
            Controls.Add(TabControl1);
            Controls.Add(RandomCheckBox);
            Controls.Add(RestartButton);
            Margin = new Padding(4, 3, 4, 3);
            Name = "frmMain";
            Text = "frmMain2";
            Load += frmMain_Load;
            TabPage2.ResumeLayout(false);
            TabPage2.PerformLayout();
            TabPage1.ResumeLayout(false);
            TabPage1.PerformLayout();
            TabControl1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        internal System.Windows.Forms.PrintPreviewDialog PrintPreviewDialog1;
        internal System.Drawing.Printing.PrintDocument PrintDocument1;
        internal System.Windows.Forms.OpenFileDialog OpenFileDialog1;
        internal System.Windows.Forms.Timer ShowResultTimer;
        internal System.Windows.Forms.Button RestartButton;
        internal System.Windows.Forms.CheckBox RandomCheckBox;
        internal System.Windows.Forms.Button OpenButton;
        internal System.Windows.Forms.Label ApplicationLabel;
        internal System.Windows.Forms.Button btnPrintPreview;
        internal System.Windows.Forms.TabPage TabPage2;
        internal System.Windows.Forms.Label lblFlashCardStatus;
        internal System.Windows.Forms.Button btnNextFlashCard;
        internal System.Windows.Forms.Button btnPrevFlashCard;
        internal System.Windows.Forms.Button btnFlashCard;
        internal System.Windows.Forms.TabPage TabPage1;
        internal System.Windows.Forms.RadioButton RadioEnglish;
        internal System.Windows.Forms.RadioButton RadioPinYin;
        internal System.Windows.Forms.Label DataSetLabel;
        internal System.Windows.Forms.Button NextDataSet;
        internal System.Windows.Forms.Button PrevDataSet;
        internal System.Windows.Forms.Button Card4;
        internal System.Windows.Forms.Button Card3;
        internal System.Windows.Forms.Button Card2;
        internal System.Windows.Forms.Button Card1;
        internal System.Windows.Forms.TabControl TabControl1;
        internal System.Windows.Forms.Label TestWordLabel;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Label lblStreak;
        internal System.Windows.Forms.Label lblBestStreak;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.Label lblTotalIncorrect;
        internal System.Windows.Forms.Label Label6;
        internal System.Windows.Forms.Label lblTotalCompleted;
        internal System.Windows.Forms.Label Label8;
        internal System.Windows.Forms.Label lblFirstTryCorrect;
        internal System.Windows.Forms.Label Label5;
    }
}