using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MCFlashCards
{
    public partial class frmMain : Form
    {
        private bool Initialized;
        private string FileName;
        public DataHandler Data;
        public Game myGame;

        private bool LastAnswer;
        public bool PrintQuestions;
        public int PageNumberToPrint;

        public int NumberOfPagestoPrint;
        bool ShowQuestion;

        int FlashCardIndex;
        //Dim FntFC As System.Drawing.Text.PrivateFontCollection
        private static System.Drawing.Text.PrivateFontCollection PFC;

        private static System.Drawing.FontFamily NewFont_FF;
        private int RightMargin;
        private int TopBottomMargin;

        private int CenterGap;
        private bool Loaded = false;

        private Color CardColor;
        private int Streak = 0;
        private int BestStreak = 0;
        private int TotalFirstTryCorrect = 0;
        private int TotalCompleted = 0;
        private int TotalIncorrect = 0;

        private int Attempts = 0;


        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(System.Object sender, System.EventArgs e)
        {
            FileName = Application.StartupPath + "\\test.csv";
            ShowQuestion = true;
            FlashCardIndex = 0;

            //---------------------------------
            //Use the embedded resource font to display the chinese characters:
            //Dim EmbeddedFonts(0) As String
            //EmbeddedFonts(0) = "mingliu.TTF"
            //CharacterLabel.Font = New Font(PFC.Families(0), 15.75)

            //---------------------------------
            //Dim thisExe As System.Reflection.Assembly
            //thisExe = System.Reflection.Assembly.GetExecutingAssembly()
            //Dim file As System.IO.Stream
            //file = thisExe.GetManifestResourceStream("mingliu.TTF")

            //---------------------------------
            //other method:
            //Stream fontStream =
            //this.GetType().Assembly.GetManifestResourceStream( "embedfont.Alphd___.ttf");
            //byte[] fontdata = new byte[fontStream.Length];
            //fontStream.Read(fontdata,0,(int)fontStream.Length) ;
            //fontStream.Close();
            //        unsafe()
            //{
            //fixed(byte * pFontData = fontdata)
            //{
            //pfc.AddMemoryFont((System.IntPtr)pFontData,fontdat a.Length);
            //}
            //}
            //---------------------------------

            //Use the font if it exists.
            //If System.IO.File.Exists(Application.StartupPath & "\mingliu.TTF") Then
            //    CharacterLabel.Font = CreateFont(Application.StartupPath & "\mingliu.TTF", FontStyle.Regular, 15.75, GraphicsUnit.Pixel)
            //End If

            RightMargin = TabControl1.Width - (Card2.Width + Card2.Left) + 10;
            CenterGap = Card2.Left - (Card1.Left + Card1.Width);
            CardColor = Card1.BackColor;

            Loaded = true;

            LastAnswer = false;
            Initialized = true;
            Data = new DataHandler();
            myGame = new Game(Data);
            LoadFile(FileName);
            myGame.NumberOfRndItemSets = Data.NCards / 2;
            StartGame();
        }

        private void OpenButton_Click(System.Object sender, System.EventArgs e)
        {
            //OpenFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*"
            OpenFileDialog1.Filter = "csv files (*.csv)|*.csv|txt files (*.txt)|*.txt|All files (*.*)|*.*";
            OpenFileDialog1.InitialDirectory = Application.StartupPath;
            if (OpenFileDialog1.ShowDialog() == DialogResult.OK)
            {
                LoadFile(OpenFileDialog1.FileName);
            }
            else
            {
                return;
            }
            StartGame();
            LoadFlashCard();
            Streak = 0;
            BestStreak = 0;
            TotalFirstTryCorrect = 0;
            TotalCompleted = 0;
            TotalIncorrect = 0;
            Attempts = 0;
            UpdateStats();
        }

        private void LoadFile(string FName)
        {
            //Check that file exists..
            if (!System.IO.File.Exists(FName))
            {
                return;
            }
            //Set new filename
            FileName = FName;

            //Load file with DataHandler class:
            Data.LoadFile(FileName);

            if (Data.NCards < 4)
            {
                if (Data.Cards[0].NWrongAnswers <= 0)
                {
                    Interaction.MsgBox("Not enough cards to play");
                }
            }
            //Me.Text = "MC Flash Cards - " & System.IO.Path.GetFileNameWithoutExtension(FileName)
            this.Text = "MC Flash Cards 2 - " + Data.Description;
        }

        public void LoadFlashCard()
        {
            if ((ShowQuestion))
            {
                btnFlashCard.Text = Data.Cards[FlashCardIndex].Question;
            }
            else
            {
                btnFlashCard.Text = Data.Cards[FlashCardIndex].Answer;
            }
            lblFlashCardStatus.Text = "Card: " + (FlashCardIndex + 1).ToString() + "/" + Data.NCards.ToString();
        }


        private void StartGame()
        {
            if (Data.NCards < 4)
            {
                if (Data.Cards[0].NWrongAnswers <= 0)
                {
                    //Interaction.MsgBox("Not enough cards to play");
                    return;
                }
            }
            if (myGame.StartGame())
            {
                DisplayCards();

            }
            else
            {
            }
        }

        private void DisplayCards()
        {
            string[] ThisCardSet = new string[4];
            int i = 0;

            if (myGame.GameRunning)
            {

                if (RadioPinYin.Checked)
                {
                    //Start with the initial set of answer cards based on the imported cards
                    ThisCardSet[0] = myGame.CardsToDisplay[0].Answer;
                    ThisCardSet[1] = myGame.CardsToDisplay[1].Answer;
                    ThisCardSet[2] = myGame.CardsToDisplay[2].Answer;
                    ThisCardSet[3] = myGame.CardsToDisplay[3].Answer;

                    //if the answer card has wrong answers listed, use them instead of other cards
                    if ((myGame.AnswerToDisplay.NWrongAnswers > 0))
                    {
                        //RandomizeArray

                        //find the correct answer in the cards
                        int correctAnswerIndex = 0;
                        correctAnswerIndex = -1;
                        for (i = 0; i <= myGame.CardsToDisplay.Length - 1; i++)
                        {
                            if ((myGame.CardsToDisplay[i].Answer == myGame.AnswerToDisplay.Answer & myGame.CardsToDisplay[i].Question == myGame.AnswerToDisplay.Question))
                            {
                                correctAnswerIndex = i;
                                break; // TODO: might not be correct. Was : Exit For
                            }
                        }
                        if ((correctAnswerIndex == -1))
                        {
                            Interaction.MsgBox("No correct answer found.");
                        }

                        //replace answers other than the correct one with Wrong answers from our answer card
                        int WrongAnswerIndex = 0;
                        WrongAnswerIndex = 0;
                        for (i = 0; i <= myGame.CardsToDisplay.Length - 1; i++)
                        {
                            if ((i != correctAnswerIndex))
                            {
                                ThisCardSet[i] = myGame.AnswerToDisplay.WrongAnswers[WrongAnswerIndex];
                                WrongAnswerIndex = WrongAnswerIndex + 1;
                            }
                        }

                    }

                    //randomize the cards before displaying:
                    RandomizeArray(ref ThisCardSet);

                    Card1.Text = ThisCardSet[0];
                    Card2.Text = ThisCardSet[1];
                    Card3.Text = ThisCardSet[2];
                    Card4.Text = ThisCardSet[3];

                    //CharacterLabel.Text = myGame.AnswerToDisplay.Characters
                    TestWordLabel.Text = myGame.AnswerToDisplay.Question;
                }
                else
                {
                    Card1.Text = myGame.CardsToDisplay[0].Question;
                    Card2.Text = myGame.CardsToDisplay[1].Question;
                    Card3.Text = myGame.CardsToDisplay[2].Question;
                    Card4.Text = myGame.CardsToDisplay[3].Question;
                    //CharacterLabel.Text = myGame.AnswerToDisplay.Characters
                    TestWordLabel.Text = myGame.AnswerToDisplay.Answer;
                }

                DataSetLabel.Text = "Data Set: " + (myGame.CurrentItemSet + 1) + "/" + (myGame.NItemSetsInData + myGame.NumberOfRndItemSets);
                Attempts = 0;
            }
        }

        private void RandomizeArray(ref string[] arr)
        {
            int i = 0;
            int j = 0;
            string temp = null;
            for (int count = 1; count <= 10; count++)
            {
                int max_index = arr.Length - 1;
                //Dim rnd As New Random
                for (i = 0; i <= max_index - 1; i++)
                {
                    // Pick an item for position i.
                    j = RandomNumber(i, max_index + 1);

                    // Swap them.
                    temp = arr[i];
                    arr[i] = arr[j];
                    arr[j] = temp;
                }
            }
        }

        //MinNumber is inclusive and MaxNumber is exclusive
        private int RandomNumber(int MinNumber, int MaxNumber)
        {

            //initialize random number generator
            Random r = new Random(System.DateTime.Now.Millisecond);

            //if passed incorrect arguments, swap them
            //can also throw exception or return 0

            if (MinNumber > MaxNumber)
            {
                int t = MinNumber;
                MinNumber = MaxNumber;
                MaxNumber = t;
            }

            return r.Next(MinNumber, MaxNumber);
            //MaxNumber is Exclusive in this call!!

        }


        private void PanelClicked(int PanelNum)
        {

            if (myGame.GameRunning)
            {
                string AnswerClicked = "";
                if ((PanelNum == 1))
                {
                    AnswerClicked = Card1.Text;
                }
                else if ((PanelNum == 2))
                {
                    AnswerClicked = Card2.Text;
                }
                else if ((PanelNum == 3))
                {
                    AnswerClicked = Card3.Text;
                }
                else if ((PanelNum == 4))
                {
                    AnswerClicked = Card4.Text;
                }

                LastAnswer = myGame.CheckAnswer(AnswerClicked);
                ShowPanelResult(LastAnswer, PanelNum);

                if ((LastAnswer))
                {
                    Streak = Streak + 1;
                    if ((Streak > BestStreak))
                    {
                        BestStreak = Streak;
                    }

                    if ((Attempts == 0))
                    {
                        TotalFirstTryCorrect = TotalFirstTryCorrect + 1;
                    }
                    TotalCompleted = TotalCompleted + 1;
                }
                else
                {
                    if ((Attempts == 0))
                    {
                        //Save incorrect answer to csv file
                        //name the file by adding a _incorrect suffix to the filename
                        string ErrorFileNoIncorrect = FileName.Replace("_incorrect.csv", ".csv");
                        var ErrorFile = ErrorFileNoIncorrect.Replace(".csv", "_incorrect.csv");

                        //check if FileName exists
                        if ((!System.IO.File.Exists(ErrorFile)))
                        {
                            //Create a header
                            //extract the name of the file from the full path
                            string FileNameWithoutPath = ErrorFileNoIncorrect.Substring(ErrorFileNoIncorrect.LastIndexOf("\\") + 1);
                            string header = FileNameWithoutPath + " Problematic Questions";

                            //write the header to the file
                            System.IO.FileStream fso1 = new System.IO.FileStream(ErrorFile, System.IO.FileMode.Create, System.IO.FileAccess.Write);
                            System.IO.StreamWriter sw1 = new System.IO.StreamWriter(fso1);
                            sw1.WriteLine(header);
                            sw1.Close();
                            fso1.Close();
                        }

                        //Read the file to check if the line already exists
                        //If it does, then do not write it again
                        string[] lines = System.IO.File.ReadAllLines(ErrorFile);

                        // Check if the question already exists in any line
                        if (lines.Any(line => line.Contains(myGame.AnswerToDisplay.Question + ",")))
                        {
                            // Line already exists, do not write it again
                            //return;
                        }
                        else
                        {
                            System.IO.FileStream fso = new System.IO.FileStream(ErrorFile, System.IO.FileMode.Append, System.IO.FileAccess.Write);
                            System.IO.StreamWriter sw = new System.IO.StreamWriter(fso);

                            string[] IncorrectAnswers = new string[4];
                            string[] CardsDisplayed = new string[4];
                            CardsDisplayed[0] = Card1.Text;
                            CardsDisplayed[1] = Card2.Text;
                            CardsDisplayed[2] = Card3.Text;
                            CardsDisplayed[3] = Card4.Text;

                            //Figure out which are the incorrect answers of those shown.  Check agains myGame.AnswerToDisplay.Answer
                            int j = 0;
                            for (int i = 0; i <= 4 - 1; i++)
                            {
                                if ((CardsDisplayed[i] != myGame.AnswerToDisplay.Answer))
                                {
                                    IncorrectAnswers[j++] = CardsDisplayed[i];
                                }
                            }

                            //sw.WriteLine(Line)
                            //sw.WriteLine("""" + TestWordLabel.Text + """,""" + Card1.Text + """,""" + Card2.Text + """,""" + Card3.Text + """,""" + Card4.Text + """")
                            //sw.WriteLine(myGame.AnswerToDisplay.Question + "," + myGame.AnswerToDisplay.Answer + "," + Card2.Text + "," + Card3.Text + "," + Card4.Text)
                            sw.WriteLine("\"" + myGame.AnswerToDisplay.Question + "\",\"" + myGame.AnswerToDisplay.Answer + "\",\"" + IncorrectAnswers[0] + "\",\"" + IncorrectAnswers[1] + "\",\"" + IncorrectAnswers[2] + "\"");

                            sw.Close();
                            fso.Close();
                        }
                    }

                    Streak = 0;
                    TotalIncorrect = TotalIncorrect + 1;
                    Attempts = Attempts + 1;
                }

                UpdateStats();


                //If myGame.CheckAnswer(PanelNum) Then
                //    LastAnswer = True
                //    MsgBox("Correct.")
                //    If myGame.NextQuestion() Then
                //        DisplayCards()
                //    Else
                //        MsgBox("Done.")
                //    End If

                //Else
                //    LastAnswer = False
                //    MsgBox("Incorrect.")
                //End If
            }
        }

        private void UpdateStats()
        {
            lblBestStreak.Text = BestStreak.ToString();
            lblStreak.Text = Streak.ToString();
            lblTotalCompleted.Text = TotalCompleted.ToString();
            lblTotalIncorrect.Text = TotalIncorrect.ToString();

            //Convert to percentage and round to 0 decimal places
            if (TotalCompleted != 0)
            {
                lblFirstTryCorrect.Text = ((double)TotalFirstTryCorrect * 100.0f / TotalCompleted).ToString("0");
            }
            else
            {
                lblFirstTryCorrect.Text = "100";
            }
        }

        private void ShowPanelResult(bool Correct, int PanelNumber)
        {
            switch (PanelNumber)
            {
                case 1:
                    if (Correct)
                    {
                        Card1.BackColor = Color.Green;
                    }
                    else
                    {
                        Card1.BackColor = Color.Red;
                    }
                    break;
                case 2:
                    if (Correct)
                    {
                        Card2.BackColor = Color.Green;
                    }
                    else
                    {
                        Card2.BackColor = Color.Red;
                    }
                    break;
                case 3:
                    if (Correct)
                    {
                        Card3.BackColor = Color.Green;
                    }
                    else
                    {
                        Card3.BackColor = Color.Red;
                    }
                    break;
                case 4:
                    if (Correct)
                    {
                        Card4.BackColor = Color.Green;
                    }
                    else
                    {
                        Card4.BackColor = Color.Red;
                    }
                    break;
            }
            ShowResultTimer.Enabled = true;
        }

        private void Card1_Click(System.Object sender, System.EventArgs e)
        {
            PanelClicked(1);
        }

        private void Card2_Click(System.Object sender, System.EventArgs e)
        {
            PanelClicked(2);
        }

        private void Card3_Click(System.Object sender, System.EventArgs e)
        {
            PanelClicked(3);
        }

        private void Card4_Click(System.Object sender, System.EventArgs e)
        {
            PanelClicked(4);
        }

        private void ShowResultTimer_Tick(System.Object sender, System.EventArgs e)
        {
            ShowResultTimer.Enabled = false;
            Card1.BackColor = CardColor;
            Card2.BackColor = CardColor;
            Card3.BackColor = CardColor;
            Card4.BackColor = CardColor;
            if (LastAnswer)
            {
                if (myGame.NextQuestion(true))
                {
                    DisplayCards();
                }
                else
                {
                    //save results to csv file
                    string ResultsFileName = Application.StartupPath + "\\results.csv";

                    //lblBestStreak.Text = BestStreak.ToString()
                    //lblStreak.Text = Streak.ToString()
                    //lblTotalCompleted.Text = TotalCompleted.ToString()
                    //lblTotalIncorrect.Text = TotalIncorrect.ToString()

                    //'Convert to percentage and round to 0 decimal places
                    //lblFirstTryCorrect.Text = (TotalFirstTryCorrect / TotalCompleted * 100).ToString("0")

                    string header = null;
                    string result = null;
                    header = "FileName,Time,FirstTryCorrect,BestStreak,Streak,TotalCompleted,TotalIncorrect";
                    result = ResultsFileName + "," + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "," + (TotalFirstTryCorrect / TotalCompleted * 100).ToString("0") + "," + BestStreak.ToString() + "," + Streak.ToString() + "," + TotalCompleted.ToString() + "," + TotalIncorrect.ToString();
                    myGame.SaveResults(ResultsFileName, header, result);

                    MessageBox.Show("Done.");

                    //restart:
                    LoadFile(FileName);
                    StartGame();
                }
            }
        }

        private void RestartButton_Click(System.Object sender, System.EventArgs e)
        {

            // Check if Ctrl and Shift keys are held  
            if (((Control.ModifierKeys & Keys.Control) == Keys.Control) &&
                (Control.ModifierKeys & Keys.Shift) == Keys.Shift)
            {
                NextDataSet.Enabled = true;
                PrevDataSet.Enabled = true;
                return;
            }

            Streak = 0;
            //BestStreak = 0
            TotalFirstTryCorrect = 0;
            TotalIncorrect = 0;
            TotalCompleted = 0;
            FlashCardIndex = 0;
            UpdateStats();


            LoadFile(FileName);
            StartGame();

            Streak = 0;
            //BestStreak = 0
            TotalFirstTryCorrect = 0;
            TotalIncorrect = 0;
            TotalCompleted = 0;
            FlashCardIndex = 0;
            UpdateStats();
        }

        private void RadioPinYin_CheckedChanged(System.Object sender, System.EventArgs e)
        {
            if (Initialized)
            {
                if (myGame.GameRunning)
                {
                    DisplayCards();
                }
            }
        }

        private void RandomCheckBox_CheckedChanged(System.Object sender, System.EventArgs e)
        {
            if (Initialized)
            {
                myGame.AllRandom = RandomCheckBox.Checked;
            }
        }

        /// <summary>
        /// Function to return a new font based on the font file passed to it
        /// </summary>
        /// <param name="name">Path to the new font file</param>
        /// <param name="style">The FontStyle of the new font</param>
        /// <param name="size">T size of the new font</param>
        /// <returns>A new font</returns>
        /// <remarks></remarks>
        private System.Drawing.Font CreateFont(string name, System.Drawing.FontStyle style, float size, System.Drawing.GraphicsUnit unit)
        {
            //Create a new font collection
            PFC = new System.Drawing.Text.PrivateFontCollection();
            //Add the font file to the new font
            //"name" is the qualified path to your font file
            PFC.AddFontFile(name);
            //Retrieve your new font
            NewFont_FF = PFC.Families[0];

            return new System.Drawing.Font(NewFont_FF, size, style, unit);
        }


        private void PrevDataSet_Click(System.Object sender, System.EventArgs e)
        {
            if ((myGame.GameRunning))
            {
                if (myGame.SetItemSet(myGame.CurrentItemSet - 1))
                {
                    DisplayCards();
                }
                else
                {
                    // MsgBox("Done.")  
                }
            }
        }

        private void NextDataSet_Click(System.Object sender, System.EventArgs e)
        {
            if ((myGame.GameRunning))
            {
                if (myGame.SetItemSet(myGame.CurrentItemSet + 1))
                {
                    DisplayCards();
                }
                else
                {
                    //MsgBox("Done.")
                }
            }
        }

        private void btnFlashCard_Click(System.Object sender, System.EventArgs e)
        {
            if ((ShowQuestion))
            {
                ShowQuestion = false;
            }
            else
            {
                ShowQuestion = true;
            }
            LoadFlashCard();
        }

        private void TabControl1_SelectedIndexChanged(System.Object sender, System.EventArgs e)
        {
            if (TabControl1.SelectedIndex == 0)
            {
                TestWordLabel.Visible = true;
            }
            else
            {
                TestWordLabel.Visible = false;
            }
        }

        private void btnPrevFlashCard_Click(System.Object sender, System.EventArgs e)
        {
            ShowQuestion = true;
            if ((FlashCardIndex > 0))
            {
                FlashCardIndex = FlashCardIndex - 1;
            }
            LoadFlashCard();
        }

        private void btnNextFlashCard_Click(System.Object sender, System.EventArgs e)
        {
            ShowQuestion = true;
            if ((FlashCardIndex < Data.NCards - 1))
            {
                FlashCardIndex = FlashCardIndex + 1;
            }
            LoadFlashCard();
        }


        private void btnPrintPreview_Click(System.Object sender, System.EventArgs e)
        {
            return;

            //PrintForm myPrintForm = null;

            //myPrintForm = new PrintForm();

            ////Show printForm modally:
            //myPrintForm.ShowDialog(this);

            //if ((myPrintForm.PrintDoubleSided))
            //{
            //    MessageBox.Show("Double sided printing not presently supported.");
            //}
            //if ((myPrintForm.PrintFronts))
            //{
            //    PrintQuestions = true;
            //    PrintPreviewDialog1.Document = PrintDocument1;
            //    PrintPreviewDialog1.ShowDialog();
            //}
            //if ((myPrintForm.PrintBacks))
            //{
            //    PrintQuestions = false;
            //    PrintPreviewDialog1.Document = PrintDocument1;
            //    PrintPreviewDialog1.ShowDialog();
            //}
            //myPrintForm.Dispose();
        }

        private Bitmap CreateBitmapFromCard(int CardIndex)
        {
            //See ImageCaptionEdit CreateImageWithMetaData() function as a code reference

            int myWidth = 0;
            int myHeight = 0;
            Bitmap myBitMap = null;
            //Dim myImage As Image
            Font FontToUse = null;
            Graphics g = null;
            string CardText = null;

            //Totally arbitrary width and height for now:
            myWidth = 400;
            myHeight = 300;

            myBitMap = new Bitmap(myWidth, myHeight);
            //Make a new bitmap
            g = Graphics.FromImage(myBitMap);
            //The g object will act on the NewPic bitmap
            FontToUse = new Font(FontFamily.GenericSansSerif, 12);

            //paint background white
            g.FillRectangle(Brushes.White, RectangleF.FromLTRB(0, 0, myBitMap.Width, myBitMap.Height));

            //Add text to the image:
            g.DrawRectangle(Pens.Black, Rectangle.FromLTRB(0, 0, myBitMap.Width - 1, myBitMap.Height - 1));
            if ((PrintQuestions))
            {
                CardText = Data.Cards[CardIndex].Question;
            }
            else
            {
                CardText = Data.Cards[CardIndex].Answer;
            }
            //text drawn at top left, but it DOES word wrap:
            //g.DrawString(CardText, FontToUse, Brushes.Black, RectangleF.FromLTRB(0, 0, myBitMap.Width - 3, myBitMap.Height))

            System.Drawing.StringFormat format = null;
            format = new System.Drawing.StringFormat();
            format.LineAlignment = StringAlignment.Center;
            //format.FormatFlags = 
            format.Alignment = StringAlignment.Center;
            g.DrawString(CardText, FontToUse, Brushes.Black, RectangleF.FromLTRB(0, 0, myBitMap.Width - 3, myBitMap.Height), format);

            //no word wrap:
            //TextRenderer.DrawText(g, CardText, FontToUse, Rectangle.FromLTRB(0, 0, myBitMap.Width - 3, myBitMap.Height), Color.Black, TextFormatFlags.HorizontalCenter + TextFormatFlags.VerticalCenter + TextFormatFlags.GlyphOverhangPadding + TextFormatFlags.LeftAndRightPadding)

            //no word wrap again:
            //Dim Size As SizeF
            //Size = g.MeasureString(CardText, FontToUse)
            //g.DrawString(CardText, FontToUse, New SolidBrush(Color.Black), (myBitMap.Width - Size.Width) / 2, 0)

            return myBitMap;
        }

        private void PrintDocument1_PrintPage(System.Object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //Create an image to print:
            Bitmap PageToPrint = null;
            Image CardImage = null;
            Graphics g = null;
            int CardNumberToPrint = 0;
            int Xposition = 0;
            int LeftSide = 0;
            int Rightside = 0;

            LeftSide = 0;
            Rightside = CreateBitmapFromCard(CardNumberToPrint).Width + 20;

            PageToPrint = new Bitmap(840, 1260);
            g = Graphics.FromImage(PageToPrint);

            //Determine Starting card index:
            CardNumberToPrint = (PageNumberToPrint - 1) * 8;

            //Top left corner:
            if ((CardNumberToPrint < Data.NCards))
            {
                CardImage = CreateBitmapFromCard(CardNumberToPrint);
                if ((PrintQuestions))
                {
                    Xposition = LeftSide;
                }
                else
                {
                    Xposition = Rightside;
                }
                g.DrawImage(CardImage, Xposition, 0);
                //x, y
            }

            //Top right corner:
            if ((CardNumberToPrint + 1 < Data.NCards))
            {
                CardImage = CreateBitmapFromCard(CardNumberToPrint + 1);
                if ((PrintQuestions))
                {
                    Xposition = Rightside;
                }
                else
                {
                    Xposition = LeftSide;
                }
                g.DrawImage(CardImage, Xposition, 0);
                //x, y
            }

            //next row left side:
            if ((CardNumberToPrint + 2 < Data.NCards))
            {
                CardImage = CreateBitmapFromCard(CardNumberToPrint + 2);
                if ((PrintQuestions))
                {
                    Xposition = LeftSide;
                }
                else
                {
                    Xposition = Rightside;
                }
                g.DrawImage(CardImage, Xposition, CardImage.Height + 20);
                //x, y
            }

            //next row right side:
            if ((CardNumberToPrint + 3 < Data.NCards))
            {
                CardImage = CreateBitmapFromCard(CardNumberToPrint + 3);
                if ((PrintQuestions))
                {
                    Xposition = Rightside;
                }
                else
                {
                    Xposition = LeftSide;
                }
                g.DrawImage(CardImage, Xposition, CardImage.Height + 20);
                //x, y
            }

            //next row left side:
            if ((CardNumberToPrint + 4 < Data.NCards))
            {
                CardImage = CreateBitmapFromCard(CardNumberToPrint + 4);
                if ((PrintQuestions))
                {
                    Xposition = LeftSide;
                }
                else
                {
                    Xposition = Rightside;
                }
                g.DrawImage(CardImage, Xposition, (CardImage.Height + 20) * 2);
                //x, y
            }

            //next row right side:
            if ((CardNumberToPrint + 5 < Data.NCards))
            {
                CardImage = CreateBitmapFromCard(CardNumberToPrint + 5);
                if ((PrintQuestions))
                {
                    Xposition = Rightside;
                }
                else
                {
                    Xposition = LeftSide;
                }
                g.DrawImage(CardImage, Xposition, (CardImage.Height + 20) * 2);
                //x, y
            }

            //next row left side:
            if ((CardNumberToPrint + 6 < Data.NCards))
            {
                CardImage = CreateBitmapFromCard(CardNumberToPrint + 6);
                if ((PrintQuestions))
                {
                    Xposition = LeftSide;
                }
                else
                {
                    Xposition = Rightside;
                }
                g.DrawImage(CardImage, Xposition, (CardImage.Height + 20) * 3);
                //x, y
            }

            //next row right side:
            if ((CardNumberToPrint + 7 < Data.NCards))
            {
                CardImage = CreateBitmapFromCard(CardNumberToPrint + 7);
                if ((PrintQuestions))
                {
                    Xposition = Rightside;
                }
                else
                {
                    Xposition = LeftSide;
                }
                g.DrawImage(CardImage, Xposition, (CardImage.Height + 20) * 3);
                //x, y
            }


            //        Image theImage = Image.FromFile(filename);

            //W = theImage.Width+SideWidth;
            //H = theImage.Height+HeaderHeight+CaptionHeight;

            //Bitmap NewPic = new Bitmap(W, H);    //Make a new image    
            //Graphics g = Graphics.FromImage(NewPic);  //Get a graphics engine from the new image
            //g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;  //Set the quality of the resize

            ////paint background white
            //g.FillRectangle(Brushes.White, RectangleF.FromLTRB(0, 0, NewPic.Width, NewPic.Height));

            ////draw the image onto the new canvas
            //g.DrawImage(theImage, 0, HeaderHeight, theImage.Width, theImage.Height);  //Resize



            //For now just print one card per page:
            //TODO: Set up CreateBitmapFromCard() to put a border around the image, then
            //Set up the ImageToPrint to actually contain multiple images.
            //ImageToPrint = CreateBitmapFromCard(PageNumberToPrint - 1)

            int Height = 0;
            int width = 0;
            int BorderWidth = 0;
            int BorderHeight = 0;

            //Determine the X, Y coordinate of the top left corner of where to put the image:
            BorderWidth = 30;
            BorderHeight = 30;

            ////fit to page width and height if necessary..
            Height = PageToPrint.Height;
            if ((Height > e.PageBounds.Height - BorderHeight * 2))
            {
                Height = e.PageBounds.Height - BorderHeight * 2;
            }

            width = PageToPrint.Width;
            if ((width > e.PageBounds.Width - BorderWidth * 2))
            {
                width = e.PageBounds.Width - BorderWidth;
            }

            //Set the X, Y location for where to put the image: 
            Rectangle R = default(Rectangle);
            R = new Rectangle(BorderWidth, BorderHeight, width - BorderWidth * 2, Height - BorderHeight * 2);

            ////Here is where we add the image to print to the PrintPageEventArgs:
            e.Graphics.DrawImage(PageToPrint, R);

            //Check if this is the last page to print:
            if ((PageNumberToPrint >= NumberOfPagestoPrint))
            {
                //This is the last page:
                e.HasMorePages = false;
            }
            else
            {
                //Go to the next page:
                PageNumberToPrint = PageNumberToPrint + 1;
                e.HasMorePages = true;
            }
        }

        private void PrintDocument1_BeginPrint(System.Object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            PageNumberToPrint = 1;
            //1 indexed
            //Determine this value based on what is desired to print:
            NumberOfPagestoPrint = Conversion.Int(Data.NCards / 8);
            if ((Data.NCards % 8 != 0))
            {
                NumberOfPagestoPrint = NumberOfPagestoPrint + 1;
            }
        }

        private void frmMain_Resize(System.Object sender, System.EventArgs e)
        {
            if (Loaded)
            {
                Card1.Width = (TabControl1.Width - RightMargin - CenterGap) / 2;
                Card2.Width = (TabControl1.Width - RightMargin - CenterGap) / 2;
                Card3.Width = (TabControl1.Width - RightMargin - CenterGap) / 2;
                Card4.Width = (TabControl1.Width - RightMargin - CenterGap) / 2;

                Card2.Left = Card1.Left + Card1.Width + CenterGap;
                Card4.Left = Card1.Left + Card1.Width + CenterGap;

                Card1.Height = (TabControl1.Height - CenterGap) / 2;
                Card2.Height = (TabControl1.Height - CenterGap) / 2;
                Card3.Height = (TabControl1.Height - CenterGap) / 2;
                Card4.Height = (TabControl1.Height - CenterGap) / 2;

                Card3.Top = Card1.Top + Card1.Height + CenterGap;
                Card4.Top = Card1.Top + Card1.Height + CenterGap;


                //RightMargin()
            }
        }


        private void TabPage1_SizeChanged(System.Object sender, System.EventArgs e)
        {
        }

        private void TabControl1_SizeChanged(System.Object sender, System.EventArgs e)
        {
            TabPage1.Width = TabControl1.Width;
            TabPage1.Height = TabControl1.Height;
        }


        private void Label2_Click(System.Object sender, System.EventArgs e)
        {
        }


        private void btnFlashCard_MouseUp(object sender, MouseEventArgs e)
        {
            //if right mouse button clicked go to the next card
            if (e.Button == MouseButtons.Right)
            {
                ShowQuestion = true;
                if ((FlashCardIndex < Data.NCards - 1))
                {
                    FlashCardIndex = FlashCardIndex + 1;
                }
                LoadFlashCard();
            }
        }

        private void frmMain_Load_2(object sender, EventArgs e)
        {

        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            //frmHelp.Show();
            frmHelp frmHelp = new frmHelp();
            frmHelp.Show();
        }
    }

}

