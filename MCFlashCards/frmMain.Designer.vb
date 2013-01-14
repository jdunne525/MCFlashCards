<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.OpenButton = New System.Windows.Forms.Button
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.TestWordLabel = New System.Windows.Forms.Label
        Me.ShowResultTimer = New System.Windows.Forms.Timer(Me.components)
        Me.RestartButton = New System.Windows.Forms.Button
        Me.RandomCheckBox = New System.Windows.Forms.CheckBox
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.RadioEnglish = New System.Windows.Forms.RadioButton
        Me.RadioPinYin = New System.Windows.Forms.RadioButton
        Me.DataSetLabel = New System.Windows.Forms.Label
        Me.NextDataSet = New System.Windows.Forms.Button
        Me.PrevDataSet = New System.Windows.Forms.Button
        Me.Card4 = New System.Windows.Forms.Button
        Me.Card3 = New System.Windows.Forms.Button
        Me.Card2 = New System.Windows.Forms.Button
        Me.Card1 = New System.Windows.Forms.Button
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.lblFlashCardStatus = New System.Windows.Forms.Label
        Me.btnNextFlashCard = New System.Windows.Forms.Button
        Me.btnPrevFlashCard = New System.Windows.Forms.Button
        Me.btnFlashCard = New System.Windows.Forms.Button
        Me.PrintPreviewDialog1 = New System.Windows.Forms.PrintPreviewDialog
        Me.PrintDocument1 = New System.Drawing.Printing.PrintDocument
        Me.btnPrintPreview = New System.Windows.Forms.Button
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.SuspendLayout()
        '
        'OpenButton
        '
        Me.OpenButton.Location = New System.Drawing.Point(717, 12)
        Me.OpenButton.Name = "OpenButton"
        Me.OpenButton.Size = New System.Drawing.Size(63, 24)
        Me.OpenButton.TabIndex = 2
        Me.OpenButton.Text = "Open"
        Me.OpenButton.UseVisualStyleBackColor = True
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'TestWordLabel
        '
        Me.TestWordLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TestWordLabel.Location = New System.Drawing.Point(1, 12)
        Me.TestWordLabel.Name = "TestWordLabel"
        Me.TestWordLabel.Size = New System.Drawing.Size(644, 54)
        Me.TestWordLabel.TabIndex = 3
        Me.TestWordLabel.Text = "TestWordLabel"
        '
        'ShowResultTimer
        '
        Me.ShowResultTimer.Interval = 500
        '
        'RestartButton
        '
        Me.RestartButton.Location = New System.Drawing.Point(717, 42)
        Me.RestartButton.Name = "RestartButton"
        Me.RestartButton.Size = New System.Drawing.Size(63, 24)
        Me.RestartButton.TabIndex = 2
        Me.RestartButton.Text = "Restart"
        Me.RestartButton.UseVisualStyleBackColor = True
        '
        'RandomCheckBox
        '
        Me.RandomCheckBox.AutoSize = True
        Me.RandomCheckBox.Location = New System.Drawing.Point(665, 201)
        Me.RandomCheckBox.Name = "RandomCheckBox"
        Me.RandomCheckBox.Size = New System.Drawing.Size(80, 17)
        Me.RandomCheckBox.TabIndex = 15
        Me.RandomCheckBox.Text = "All Random"
        Me.RandomCheckBox.UseVisualStyleBackColor = True
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(5, 72)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(779, 546)
        Me.TabControl1.TabIndex = 16
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.RadioEnglish)
        Me.TabPage1.Controls.Add(Me.RadioPinYin)
        Me.TabPage1.Controls.Add(Me.DataSetLabel)
        Me.TabPage1.Controls.Add(Me.NextDataSet)
        Me.TabPage1.Controls.Add(Me.PrevDataSet)
        Me.TabPage1.Controls.Add(Me.Card4)
        Me.TabPage1.Controls.Add(Me.Card3)
        Me.TabPage1.Controls.Add(Me.Card2)
        Me.TabPage1.Controls.Add(Me.Card1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(771, 520)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Multiple Choice Cards"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'RadioEnglish
        '
        Me.RadioEnglish.AutoSize = True
        Me.RadioEnglish.Location = New System.Drawing.Point(652, 89)
        Me.RadioEnglish.Name = "RadioEnglish"
        Me.RadioEnglish.Size = New System.Drawing.Size(111, 17)
        Me.RadioEnglish.TabIndex = 20
        Me.RadioEnglish.Text = "Answer - Question"
        Me.RadioEnglish.UseVisualStyleBackColor = True
        '
        'RadioPinYin
        '
        Me.RadioPinYin.AutoSize = True
        Me.RadioPinYin.Checked = True
        Me.RadioPinYin.Location = New System.Drawing.Point(652, 66)
        Me.RadioPinYin.Name = "RadioPinYin"
        Me.RadioPinYin.Size = New System.Drawing.Size(111, 17)
        Me.RadioPinYin.TabIndex = 19
        Me.RadioPinYin.TabStop = True
        Me.RadioPinYin.Text = "Question - Answer"
        Me.RadioPinYin.UseVisualStyleBackColor = True
        '
        'DataSetLabel
        '
        Me.DataSetLabel.AutoSize = True
        Me.DataSetLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DataSetLabel.Location = New System.Drawing.Point(649, 33)
        Me.DataSetLabel.Name = "DataSetLabel"
        Me.DataSetLabel.Size = New System.Drawing.Size(51, 17)
        Me.DataSetLabel.TabIndex = 18
        Me.DataSetLabel.Text = "Label1"
        '
        'NextDataSet
        '
        Me.NextDataSet.Location = New System.Drawing.Point(694, 6)
        Me.NextDataSet.Name = "NextDataSet"
        Me.NextDataSet.Size = New System.Drawing.Size(36, 24)
        Me.NextDataSet.TabIndex = 16
        Me.NextDataSet.Text = ">"
        Me.NextDataSet.UseVisualStyleBackColor = True
        '
        'PrevDataSet
        '
        Me.PrevDataSet.Location = New System.Drawing.Point(652, 6)
        Me.PrevDataSet.Name = "PrevDataSet"
        Me.PrevDataSet.Size = New System.Drawing.Size(36, 24)
        Me.PrevDataSet.TabIndex = 17
        Me.PrevDataSet.Text = "<"
        Me.PrevDataSet.UseVisualStyleBackColor = True
        '
        'Card4
        '
        Me.Card4.BackColor = System.Drawing.Color.LightGray
        Me.Card4.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Card4.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Card4.Location = New System.Drawing.Point(329, 263)
        Me.Card4.Name = "Card4"
        Me.Card4.Size = New System.Drawing.Size(317, 251)
        Me.Card4.TabIndex = 15
        Me.Card4.Text = "Button4"
        Me.Card4.UseVisualStyleBackColor = False
        '
        'Card3
        '
        Me.Card3.BackColor = System.Drawing.Color.LightGray
        Me.Card3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Card3.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Card3.Location = New System.Drawing.Point(6, 263)
        Me.Card3.Name = "Card3"
        Me.Card3.Size = New System.Drawing.Size(317, 251)
        Me.Card3.TabIndex = 14
        Me.Card3.Text = "Button3"
        Me.Card3.UseVisualStyleBackColor = False
        '
        'Card2
        '
        Me.Card2.BackColor = System.Drawing.Color.LightGray
        Me.Card2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Card2.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Card2.Location = New System.Drawing.Point(329, 6)
        Me.Card2.Name = "Card2"
        Me.Card2.Size = New System.Drawing.Size(317, 251)
        Me.Card2.TabIndex = 13
        Me.Card2.Text = "Button2"
        Me.Card2.UseVisualStyleBackColor = False
        '
        'Card1
        '
        Me.Card1.BackColor = System.Drawing.Color.LightGray
        Me.Card1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Card1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Card1.Location = New System.Drawing.Point(6, 6)
        Me.Card1.Name = "Card1"
        Me.Card1.Size = New System.Drawing.Size(317, 251)
        Me.Card1.TabIndex = 12
        Me.Card1.Text = "Button1"
        Me.Card1.UseVisualStyleBackColor = False
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.lblFlashCardStatus)
        Me.TabPage2.Controls.Add(Me.btnNextFlashCard)
        Me.TabPage2.Controls.Add(Me.btnPrevFlashCard)
        Me.TabPage2.Controls.Add(Me.btnFlashCard)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(771, 520)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "FlashCards"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'lblFlashCardStatus
        '
        Me.lblFlashCardStatus.AutoSize = True
        Me.lblFlashCardStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFlashCardStatus.Location = New System.Drawing.Point(653, 33)
        Me.lblFlashCardStatus.Name = "lblFlashCardStatus"
        Me.lblFlashCardStatus.Size = New System.Drawing.Size(51, 17)
        Me.lblFlashCardStatus.TabIndex = 21
        Me.lblFlashCardStatus.Text = "Label1"
        '
        'btnNextFlashCard
        '
        Me.btnNextFlashCard.Location = New System.Drawing.Point(698, 6)
        Me.btnNextFlashCard.Name = "btnNextFlashCard"
        Me.btnNextFlashCard.Size = New System.Drawing.Size(36, 24)
        Me.btnNextFlashCard.TabIndex = 19
        Me.btnNextFlashCard.Text = ">"
        Me.btnNextFlashCard.UseVisualStyleBackColor = True
        '
        'btnPrevFlashCard
        '
        Me.btnPrevFlashCard.Location = New System.Drawing.Point(656, 6)
        Me.btnPrevFlashCard.Name = "btnPrevFlashCard"
        Me.btnPrevFlashCard.Size = New System.Drawing.Size(36, 24)
        Me.btnPrevFlashCard.TabIndex = 20
        Me.btnPrevFlashCard.Text = "<"
        Me.btnPrevFlashCard.UseVisualStyleBackColor = True
        '
        'btnFlashCard
        '
        Me.btnFlashCard.BackColor = System.Drawing.Color.LightGray
        Me.btnFlashCard.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnFlashCard.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnFlashCard.Location = New System.Drawing.Point(6, 6)
        Me.btnFlashCard.Name = "btnFlashCard"
        Me.btnFlashCard.Size = New System.Drawing.Size(644, 507)
        Me.btnFlashCard.TabIndex = 13
        Me.btnFlashCard.Text = "Button1"
        Me.btnFlashCard.UseVisualStyleBackColor = False
        '
        'PrintPreviewDialog1
        '
        Me.PrintPreviewDialog1.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialog1.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialog1.ClientSize = New System.Drawing.Size(400, 300)
        Me.PrintPreviewDialog1.Enabled = True
        Me.PrintPreviewDialog1.Icon = CType(resources.GetObject("PrintPreviewDialog1.Icon"), System.Drawing.Icon)
        Me.PrintPreviewDialog1.Name = "PrintPreviewDialog1"
        Me.PrintPreviewDialog1.Visible = False
        '
        'PrintDocument1
        '
        '
        'btnPrintPreview
        '
        Me.btnPrintPreview.Location = New System.Drawing.Point(631, 42)
        Me.btnPrintPreview.Name = "btnPrintPreview"
        Me.btnPrintPreview.Size = New System.Drawing.Size(80, 24)
        Me.btnPrintPreview.TabIndex = 17
        Me.btnPrintPreview.Text = "Print Preview"
        Me.btnPrintPreview.UseVisualStyleBackColor = True
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(788, 619)
        Me.Controls.Add(Me.btnPrintPreview)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.RandomCheckBox)
        Me.Controls.Add(Me.TestWordLabel)
        Me.Controls.Add(Me.RestartButton)
        Me.Controls.Add(Me.OpenButton)
        Me.Name = "frmMain"
        Me.Text = "MC Flash Cards"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents OpenButton As System.Windows.Forms.Button
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents TestWordLabel As System.Windows.Forms.Label
    Friend WithEvents ShowResultTimer As System.Windows.Forms.Timer
    Friend WithEvents RestartButton As System.Windows.Forms.Button
    Friend WithEvents RandomCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents RadioEnglish As System.Windows.Forms.RadioButton
    Friend WithEvents RadioPinYin As System.Windows.Forms.RadioButton
    Friend WithEvents DataSetLabel As System.Windows.Forms.Label
    Friend WithEvents NextDataSet As System.Windows.Forms.Button
    Friend WithEvents PrevDataSet As System.Windows.Forms.Button
    Friend WithEvents Card4 As System.Windows.Forms.Button
    Friend WithEvents Card3 As System.Windows.Forms.Button
    Friend WithEvents Card2 As System.Windows.Forms.Button
    Friend WithEvents Card1 As System.Windows.Forms.Button
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents lblFlashCardStatus As System.Windows.Forms.Label
    Friend WithEvents btnNextFlashCard As System.Windows.Forms.Button
    Friend WithEvents btnPrevFlashCard As System.Windows.Forms.Button
    Friend WithEvents btnFlashCard As System.Windows.Forms.Button
    Friend WithEvents PrintPreviewDialog1 As System.Windows.Forms.PrintPreviewDialog
    Friend WithEvents PrintDocument1 As System.Drawing.Printing.PrintDocument
    Friend WithEvents btnPrintPreview As System.Windows.Forms.Button

End Class
