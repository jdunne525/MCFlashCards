'.net framework 3.5 required      http://www.microsoft.com/downloads/details.aspx?FamilyID=333325FD-AE52-4E35-B531-508D977D32A6&displaylang=en

Public Class frmMain
    Private Initialized As Boolean
    Private FileName As String
    Public Data As DataHandler
    Public myGame As Game
    Private LastAnswer As Boolean

    Public PrintQuestions As Integer
    Public PageNumberToPrint As Integer
    Public NumberOfPagestoPrint As Integer

    Dim ShowQuestion As Boolean
    Dim FlashCardIndex As Integer

    'Dim FntFC As System.Drawing.Text.PrivateFontCollection
    Private Shared PFC As System.Drawing.Text.PrivateFontCollection
    Private Shared NewFont_FF As Drawing.FontFamily

    Private RightMargin As Integer
    Private TopBottomMargin As Integer
    Private CenterGap As Integer

    Private Loaded As Boolean = False
    Private CardColor As Color

    Private Streak As Integer = 0
    Private BestStreak As Integer = 0
    Private TotalCorrect As Integer = 0
    Private TotalIncorrect As Integer = 0


    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        FileName = Application.StartupPath & "\test.csv"
        ShowQuestion = True
        FlashCardIndex = 0

        '---------------------------------
        'Use the embedded resource font to display the chinese characters:
        'Dim EmbeddedFonts(0) As String
        'EmbeddedFonts(0) = "mingliu.TTF"
        'CharacterLabel.Font = New Font(PFC.Families(0), 15.75)

        '---------------------------------
        'Dim thisExe As System.Reflection.Assembly
        'thisExe = System.Reflection.Assembly.GetExecutingAssembly()
        'Dim file As System.IO.Stream
        'file = thisExe.GetManifestResourceStream("mingliu.TTF")

        '---------------------------------
        'other method:
        'Stream fontStream =
        'this.GetType().Assembly.GetManifestResourceStream( "embedfont.Alphd___.ttf");
        'byte[] fontdata = new byte[fontStream.Length];
        'fontStream.Read(fontdata,0,(int)fontStream.Length) ;
        'fontStream.Close();
        '        unsafe()
        '{
        'fixed(byte * pFontData = fontdata)
        '{
        'pfc.AddMemoryFont((System.IntPtr)pFontData,fontdat a.Length);
        '}
        '}
        '---------------------------------

        'Use the font if it exists.
        'If System.IO.File.Exists(Application.StartupPath & "\mingliu.TTF") Then
        '    CharacterLabel.Font = CreateFont(Application.StartupPath & "\mingliu.TTF", FontStyle.Regular, 15.75, GraphicsUnit.Pixel)
        'End If

        RightMargin = TabControl1.Width - (Card2.Width + Card2.Left) + 10
        CenterGap = Card2.Left - (Card1.Left + Card1.Width)
        CardColor = Card1.BackColor

        Loaded = True

        LastAnswer = False
        Initialized = True
        Data = New DataHandler
        myGame = New Game(Data)
        LoadFile(FileName)
        myGame.NumberOfRndItemSets = Data.NCards / 2
        StartGame()
    End Sub

    Private Sub OpenButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenButton.Click
        'OpenFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*"
        OpenFileDialog1.Filter = "csv files (*.csv)|*.csv|txt files (*.txt)|*.txt|All files (*.*)|*.*"
        OpenFileDialog1.InitialDirectory = Application.StartupPath
        If OpenFileDialog1.ShowDialog() Then
            LoadFile(OpenFileDialog1.FileName)
        End If
        StartGame()
        LoadFlashCard()
        Streak = 0
        BestStreak = 0
        TotalCorrect = 0
        TotalIncorrect = 0
        UpdateStats()
    End Sub

    Private Sub LoadFile(ByVal FName As String)
        'Check that file exists..
        If Not System.IO.File.Exists(FName) Then
            Exit Sub
        End If
        'Set new filename
        FileName = FName

        'Load file with DataHandler class:
        Data.LoadFile(FileName)
        If Data.NCards < 4 Then
            MsgBox("Not enough cards to play")
        End If
        'Me.Text = "MC Flash Cards - " & System.IO.Path.GetFileNameWithoutExtension(FileName)
        Me.Text = "MC Flash Cards - " & Data.Description
    End Sub

    Public Sub LoadFlashCard()
        If (ShowQuestion) Then
            btnFlashCard.Text = Data.Cards(FlashCardIndex).Question
        Else
            btnFlashCard.Text = Data.Cards(FlashCardIndex).Answer
        End If
        lblFlashCardStatus.Text = "Card: " + (FlashCardIndex + 1).ToString() + "/" + Data.NCards.ToString()
    End Sub


    Private Sub StartGame()
        If Data.NCards < 4 Then
            Return
        End If
        If myGame.StartGame() Then
            DisplayCards()
        Else

        End If
    End Sub

    Private Sub DisplayCards()
        If myGame.GameRunning Then
            If RadioPinYin.Checked Then
                Card1.Text = myGame.CardsToDisplay(0).Answer
                Card2.Text = myGame.CardsToDisplay(1).Answer
                Card3.Text = myGame.CardsToDisplay(2).Answer
                Card4.Text = myGame.CardsToDisplay(3).Answer
                'CharacterLabel.Text = myGame.AnswerToDisplay.Characters
                TestWordLabel.Text = myGame.AnswerToDisplay.Question
            Else
                Card1.Text = myGame.CardsToDisplay(0).Question
                Card2.Text = myGame.CardsToDisplay(1).Question
                Card3.Text = myGame.CardsToDisplay(2).Question
                Card4.Text = myGame.CardsToDisplay(3).Question
                'CharacterLabel.Text = myGame.AnswerToDisplay.Characters
                TestWordLabel.Text = myGame.AnswerToDisplay.Answer
            End If

            DataSetLabel.Text = "Data Set: " & myGame.CurrentItemSet + 1 & "/" & myGame.NItemSetsInData + myGame.NumberOfRndItemSets
        End If
    End Sub

    Private Sub PanelClicked(ByVal PanelNum As Integer)
        If myGame.GameRunning Then

            LastAnswer = myGame.CheckAnswer(PanelNum)
            ShowPanelResult(LastAnswer, PanelNum)

            If (LastAnswer) Then
                Streak = Streak + 1
                If (Streak > BestStreak) Then
                    BestStreak = Streak
                End If

                TotalCorrect = TotalCorrect + 1
            Else
                Streak = 0
                TotalIncorrect = TotalIncorrect + 1
            End If

            UpdateStats()


            'If myGame.CheckAnswer(PanelNum) Then
            '    LastAnswer = True
            '    MsgBox("Correct.")
            '    If myGame.NextQuestion() Then
            '        DisplayCards()
            '    Else
            '        MsgBox("Done.")
            '    End If

            'Else
            '    LastAnswer = False
            '    MsgBox("Incorrect.")
            'End If
        End If
    End Sub

    Private Sub UpdateStats()
        lblBestStreak.Text = BestStreak.ToString()
        lblStreak.Text = Streak.ToString()
        lblTotalCorrect.Text = TotalCorrect.ToString()
        lblTotalIncorrect.Text = TotalIncorrect.ToString()
    End Sub

    Private Sub ShowPanelResult(ByVal Correct As Boolean, ByVal PanelNumber As Integer)
        Select Case PanelNumber
            Case 1
                If Correct Then
                    Card1.BackColor = Color.Green
                Else
                    Card1.BackColor = Color.Red
                End If
            Case 2
                If Correct Then
                    Card2.BackColor = Color.Green
                Else
                    Card2.BackColor = Color.Red
                End If
            Case 3
                If Correct Then
                    Card3.BackColor = Color.Green
                Else
                    Card3.BackColor = Color.Red
                End If
            Case 4
                If Correct Then
                    Card4.BackColor = Color.Green
                Else
                    Card4.BackColor = Color.Red
                End If
        End Select
        ShowResultTimer.Enabled = True
    End Sub

    Private Sub Card1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Card1.Click
        PanelClicked(1)
    End Sub

    Private Sub Card2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Card2.Click
        PanelClicked(2)
    End Sub

    Private Sub Card3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Card3.Click
        PanelClicked(3)
    End Sub

    Private Sub Card4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Card4.Click
        PanelClicked(4)
    End Sub

    Private Sub ShowResultTimer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShowResultTimer.Tick
        ShowResultTimer.Enabled = False
        Card1.BackColor = CardColor
        Card2.BackColor = CardColor
        Card3.BackColor = CardColor
        Card4.BackColor = CardColor
        If LastAnswer Then
            If myGame.NextQuestion(True) Then
                DisplayCards()
            Else
                MsgBox("Done.")
                'restart:
                LoadFile(FileName)
                StartGame()
            End If
        End If
    End Sub

    Private Sub RestartButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RestartButton.Click
        LoadFile(FileName)
        StartGame()

        Streak = 0
        'BestStreak = 0
        TotalCorrect = 0
        TotalIncorrect = 0
        UpdateStats()
    End Sub

    Private Sub RadioPinYin_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioPinYin.Click
        If Initialized Then
            If myGame.GameRunning Then
                DisplayCards()
            End If
        End If
    End Sub

    Private Sub RandomCheckBox_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RandomCheckBox.CheckedChanged
        If Initialized Then
            myGame.AllRandom = RandomCheckBox.Checked
        End If
    End Sub

    ''' <summary>
    ''' Function to return a new font based on the font file passed to it
    ''' </summary>
    ''' <param name="name">Path to the new font file</param>
    ''' <param name="style">The FontStyle of the new font</param>
    ''' <param name="size">T size of the new font</param>
    ''' <returns>A new font</returns>
    ''' <remarks></remarks>
    Private Function CreateFont(ByVal name As String, ByVal style As Drawing.FontStyle, ByVal size As Single, ByVal unit As Drawing.GraphicsUnit) As Drawing.Font
        'Create a new font collection
        PFC = New Drawing.Text.PrivateFontCollection
        'Add the font file to the new font
        '"name" is the qualified path to your font file
        PFC.AddFontFile(name)
        'Retrieve your new font
        NewFont_FF = PFC.Families(0)

        Return New Drawing.Font(NewFont_FF, size, style, unit)
    End Function


    Private Sub PrevDataSet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrevDataSet.Click
        If (myGame.GameRunning) Then
            If myGame.SetItemSet(myGame.CurrentItemSet - 1) Then
                DisplayCards()
            Else
                'MsgBox("Done.")
            End If
        End If
    End Sub

    Private Sub NextDataSet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NextDataSet.Click
        If (myGame.GameRunning) Then
            If myGame.SetItemSet(myGame.CurrentItemSet + 1) Then
                DisplayCards()
            Else
                'MsgBox("Done.")
            End If
        End If
    End Sub

    Private Sub btnFlashCard_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFlashCard.Click
        If (ShowQuestion) Then
            ShowQuestion = False
        Else
            ShowQuestion = True
        End If
        LoadFlashCard()
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
        If TabControl1.SelectedIndex = 0 Then
            TestWordLabel.Visible = True
        Else
            TestWordLabel.Visible = False
        End If
    End Sub

    Private Sub btnPrevFlashCard_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrevFlashCard.Click
        ShowQuestion = True
        If (FlashCardIndex > 0) Then
            FlashCardIndex = FlashCardIndex - 1
        End If
        LoadFlashCard()
    End Sub

    Private Sub btnNextFlashCard_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNextFlashCard.Click
        ShowQuestion = True
        If (FlashCardIndex < Data.NCards - 1) Then
            FlashCardIndex = FlashCardIndex + 1
        End If
        LoadFlashCard()
    End Sub

    Private Sub btnPrintPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrintPreview.Click

        Dim myPrintForm As PrintForm

        myPrintForm = New PrintForm()

        'Show printForm modally:
        myPrintForm.ShowDialog(Me)

        If (myPrintForm.PrintDoubleSided) Then
            MessageBox.Show("Double sided printing not presently supported.")
        End If
        If (myPrintForm.PrintFronts) Then
            PrintQuestions = True
            PrintPreviewDialog1.Document = PrintDocument1
            PrintPreviewDialog1.ShowDialog()
        End If
        If (myPrintForm.PrintBacks) Then
            PrintQuestions = False
            PrintPreviewDialog1.Document = PrintDocument1
            PrintPreviewDialog1.ShowDialog()
        End If
        myPrintForm.Dispose()
    End Sub

    Private Function CreateBitmapFromCard(ByVal CardIndex As Integer) As Bitmap
        'See ImageCaptionEdit CreateImageWithMetaData() function as a code reference

        Dim myWidth As Integer
        Dim myHeight As Integer
        Dim myBitMap As Bitmap
        'Dim myImage As Image
        Dim FontToUse As Font
        Dim g As Graphics
        Dim CardText As String

        'Totally arbitrary width and height for now:
        myWidth = 400
        myHeight = 300

        myBitMap = New Bitmap(myWidth, myHeight)       'Make a new bitmap
        g = Graphics.FromImage(myBitMap)              'The g object will act on the NewPic bitmap
        FontToUse = New Font(FontFamily.GenericSansSerif, 12)

        'paint background white
        g.FillRectangle(Brushes.White, RectangleF.FromLTRB(0, 0, myBitMap.Width, myBitMap.Height))

        'Add text to the image:
        g.DrawRectangle(Pens.Black, Rectangle.FromLTRB(0, 0, myBitMap.Width - 1, myBitMap.Height - 1))
        If (PrintQuestions) Then
            CardText = Data.Cards(CardIndex).Question
        Else
            CardText = Data.Cards(CardIndex).Answer
        End If
        'text drawn at top left, but it DOES word wrap:
        'g.DrawString(CardText, FontToUse, Brushes.Black, RectangleF.FromLTRB(0, 0, myBitMap.Width - 3, myBitMap.Height))

        Dim format As System.Drawing.StringFormat
        format = New System.Drawing.StringFormat
        format.LineAlignment = StringAlignment.Center
        'format.FormatFlags = 
        format.Alignment = StringAlignment.Center
        g.DrawString(CardText, FontToUse, Brushes.Black, RectangleF.FromLTRB(0, 0, myBitMap.Width - 3, myBitMap.Height), format)

        'no word wrap:
        'TextRenderer.DrawText(g, CardText, FontToUse, Rectangle.FromLTRB(0, 0, myBitMap.Width - 3, myBitMap.Height), Color.Black, TextFormatFlags.HorizontalCenter + TextFormatFlags.VerticalCenter + TextFormatFlags.GlyphOverhangPadding + TextFormatFlags.LeftAndRightPadding)

        'no word wrap again:
        'Dim Size As SizeF
        'Size = g.MeasureString(CardText, FontToUse)
        'g.DrawString(CardText, FontToUse, New SolidBrush(Color.Black), (myBitMap.Width - Size.Width) / 2, 0)

        Return myBitMap
    End Function

    Private Sub PrintDocument1_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        'Create an image to print:
        Dim PageToPrint As Bitmap
        Dim CardImage As Image
        Dim g As Graphics
        Dim CardNumberToPrint As Integer
        Dim Xposition As Integer
        Dim LeftSide As Integer
        Dim Rightside As Integer

        LeftSide = 0
        Rightside = CreateBitmapFromCard(CardNumberToPrint).Width + 20

        PageToPrint = New Bitmap(840, 1260)
        g = Graphics.FromImage(PageToPrint)

        'Determine Starting card index:
        CardNumberToPrint = (PageNumberToPrint - 1) * 8

        'Top left corner:
        If (CardNumberToPrint < Data.NCards) Then
            CardImage = CreateBitmapFromCard(CardNumberToPrint)
            If (PrintQuestions) Then
                Xposition = LeftSide
            Else
                Xposition = Rightside
            End If
            g.DrawImage(CardImage, Xposition, 0)        'x, y
        End If

        'Top right corner:
        If (CardNumberToPrint + 1 < Data.NCards) Then
            CardImage = CreateBitmapFromCard(CardNumberToPrint + 1)
            If (PrintQuestions) Then
                Xposition = Rightside
            Else
                Xposition = LeftSide
            End If
            g.DrawImage(CardImage, Xposition, 0)        'x, y
        End If

        'next row left side:
        If (CardNumberToPrint + 2 < Data.NCards) Then
            CardImage = CreateBitmapFromCard(CardNumberToPrint + 2)
            If (PrintQuestions) Then
                Xposition = LeftSide
            Else
                Xposition = Rightside
            End If
            g.DrawImage(CardImage, Xposition, CardImage.Height + 20)        'x, y
        End If

        'next row right side:
        If (CardNumberToPrint + 3 < Data.NCards) Then
            CardImage = CreateBitmapFromCard(CardNumberToPrint + 3)
            If (PrintQuestions) Then
                Xposition = Rightside
            Else
                Xposition = LeftSide
            End If
            g.DrawImage(CardImage, Xposition, CardImage.Height + 20)        'x, y
        End If

        'next row left side:
        If (CardNumberToPrint + 4 < Data.NCards) Then
            CardImage = CreateBitmapFromCard(CardNumberToPrint + 4)
            If (PrintQuestions) Then
                Xposition = LeftSide
            Else
                Xposition = Rightside
            End If
            g.DrawImage(CardImage, Xposition, (CardImage.Height + 20) * 2)        'x, y
        End If

        'next row right side:
        If (CardNumberToPrint + 5 < Data.NCards) Then
            CardImage = CreateBitmapFromCard(CardNumberToPrint + 5)
            If (PrintQuestions) Then
                Xposition = Rightside
            Else
                Xposition = LeftSide
            End If
            g.DrawImage(CardImage, Xposition, (CardImage.Height + 20) * 2)        'x, y
        End If

        'next row left side:
        If (CardNumberToPrint + 6 < Data.NCards) Then
            CardImage = CreateBitmapFromCard(CardNumberToPrint + 6)
            If (PrintQuestions) Then
                Xposition = LeftSide
            Else
                Xposition = Rightside
            End If
            g.DrawImage(CardImage, Xposition, (CardImage.Height + 20) * 3)        'x, y
        End If

        'next row right side:
        If (CardNumberToPrint + 7 < Data.NCards) Then
            CardImage = CreateBitmapFromCard(CardNumberToPrint + 7)
            If (PrintQuestions) Then
                Xposition = Rightside
            Else
                Xposition = LeftSide
            End If
            g.DrawImage(CardImage, Xposition, (CardImage.Height + 20) * 3)        'x, y
        End If


        '        Image theImage = Image.FromFile(filename);

        'W = theImage.Width+SideWidth;
        'H = theImage.Height+HeaderHeight+CaptionHeight;

        'Bitmap NewPic = new Bitmap(W, H);    //Make a new image    
        'Graphics g = Graphics.FromImage(NewPic);  //Get a graphics engine from the new image
        'g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;  //Set the quality of the resize

        '//paint background white
        'g.FillRectangle(Brushes.White, RectangleF.FromLTRB(0, 0, NewPic.Width, NewPic.Height));

        '//draw the image onto the new canvas
        'g.DrawImage(theImage, 0, HeaderHeight, theImage.Width, theImage.Height);  //Resize



        'For now just print one card per page:
        'TODO: Set up CreateBitmapFromCard() to put a border around the image, then
        'Set up the ImageToPrint to actually contain multiple images.
        'ImageToPrint = CreateBitmapFromCard(PageNumberToPrint - 1)

        Dim Height As Integer
        Dim width As Integer
        Dim BorderWidth As Integer
        Dim BorderHeight As Integer

        'Determine the X, Y coordinate of the top left corner of where to put the image:
        BorderWidth = 30
        BorderHeight = 30

        '//fit to page width and height if necessary..
        Height = PageToPrint.Height
        If (Height > e.PageBounds.Height - BorderHeight * 2) Then
            Height = e.PageBounds.Height - BorderHeight * 2
        End If

        width = PageToPrint.Width
        If (width > e.PageBounds.Width - BorderWidth * 2) Then
            width = e.PageBounds.Width - BorderWidth
        End If

        'Set the X, Y location for where to put the image: 
        Dim R As Rectangle
        R = New Rectangle(BorderWidth, BorderHeight, width - BorderWidth * 2, Height - BorderHeight * 2)

        '//Here is where we add the image to print to the PrintPageEventArgs:
        e.Graphics.DrawImage(PageToPrint, R)

        'Check if this is the last page to print:
        If (PageNumberToPrint >= NumberOfPagestoPrint) Then
            'This is the last page:
            e.HasMorePages = False
        Else
            'Go to the next page:
            PageNumberToPrint = PageNumberToPrint + 1
            e.HasMorePages = True
        End If
    End Sub

    Private Sub PrintDocument1_BeginPrint(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles PrintDocument1.BeginPrint
        PageNumberToPrint = 1   '1 indexed
        'Determine this value based on what is desired to print:
        NumberOfPagestoPrint = Int(Data.NCards / 8)
        If (Data.NCards Mod 8 <> 0) Then
            NumberOfPagestoPrint = NumberOfPagestoPrint + 1
        End If
    End Sub

    Private Sub frmMain_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        If Loaded Then
            Card1.Width = (TabControl1.Width - RightMargin - CenterGap) / 2
            Card2.Width = (TabControl1.Width - RightMargin - CenterGap) / 2
            Card3.Width = (TabControl1.Width - RightMargin - CenterGap) / 2
            Card4.Width = (TabControl1.Width - RightMargin - CenterGap) / 2

            Card2.Left = Card1.Left + Card1.Width + CenterGap
            Card4.Left = Card1.Left + Card1.Width + CenterGap

            Card1.Height = (TabControl1.Height - CenterGap) / 2
            Card2.Height = (TabControl1.Height - CenterGap) / 2
            Card3.Height = (TabControl1.Height - CenterGap) / 2
            Card4.Height = (TabControl1.Height - CenterGap) / 2

            Card3.Top = Card1.Top + Card1.Height + CenterGap
            Card4.Top = Card1.Top + Card1.Height + CenterGap


            'RightMargin()
        End If
    End Sub

    Private Sub TabPage1_SizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabPage1.SizeChanged

    End Sub

    Private Sub TabControl1_SizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabControl1.SizeChanged
        TabPage1.Width = TabControl1.Width
        TabPage1.Height = TabControl1.Height
    End Sub

    Private Sub Label2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label2.Click

    End Sub
End Class
