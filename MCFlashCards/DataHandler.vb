Imports System.Text.RegularExpressions

Public Class DataHandler
    Private FileName As String
    Public Cards() As Item
    Public NCards As Integer
    Public Description As String

    Public Sub New()

    End Sub

    'Doesn't properly deal with escaping "
    Public Function SplitCSV(ByVal inputText As String) As String()
        Return Regex.Split(inputText, ",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))")
    End Function

    Public Sub ReadCSV(ByVal FileName As String)

    End Sub

    Public Sub LoadCSVFile(ByVal FName As String)
        Dim afile As FileIO.TextFieldParser = New FileIO.TextFieldParser(FName)
        Dim CurrentRecord As String() ' this array will hold each line of data
        afile.TextFieldType = FileIO.FieldType.Delimited
        afile.Delimiters = New String() {","}
        afile.HasFieldsEnclosedInQuotes = True

        Dim StringData(3) As String

        Description = afile.ReadFields()(0)
        CurrentRecord = Nothing     'avoid the annoying warning about the uninitialized variable.

        ReDim Cards(5000)
        Dim CardNum As Integer
        Dim question As String
        Dim answer As String

        CardNum = 0

        '"Tabbed" format one item per line
        ' parse the actual file
        Do While Not afile.EndOfData
            Try
                CurrentRecord = afile.ReadFields
            Catch ex As FileIO.MalformedLineException
                Stop
            End Try

            'Example test line: """, comma test",", on the next part with """
            'Look for first identifier quotation mark or comma.
            'If Comma first, then take everything up to the comma and consider it as a cell.
            'If Quotation mark first, then read as a quotation mark string.
            'If quotation mark found, check next character, if another quotation mark, consider data as a single quotation mark (Escaped quotation mark)
            'Keep going ignoring commas until an unescaped quotation mark is found.  That is the cell.

            StringData = CurrentRecord

            If (StringData.GetUpperBound(0) >= 0) Then
                question = StringData(0)
            Else
                question = ""
                MsgBox("Error reading file.. continuing anyway..")
                Exit Do
            End If

            If (StringData.GetUpperBound(0) >= 1) Then
                answer = StringData(1)
            Else
                answer = ""
                MsgBox("Error reading file.. continuing anyway..")
                Exit Do
            End If

            Cards(CardNum) = New Item
            'Cards(CardNum).Question = StringData(0)
            'Cards(CardNum).Characters = StringData(1)  '->2
            'Cards(CardNum).Answer = StringData(3)  '->1
            SetCard(CardNum, question, answer)
            CardNum = CardNum + 1
        Loop

        'Use Preserve keyword to retain the data within the resized array:
        NCards = CardNum
        ReDim Preserve Cards(CardNum - 1)
    End Sub

    Public Sub LoadTxtFile(ByVal FName As String)
        Dim IStream As System.IO.StreamReader
        Dim StringData(3) As String
        Dim DataLine As String

        'diag code:
        'NCards = 6
        'ReDim Cards(NCards - 1)
        'For i = 0 To NCards - 1
        '    Cards(i) = New Item
        'Next

        'SetCard(0, "1", "One")
        'SetCard(1, "2", "Two")
        'SetCard(2, "3", "Three")
        'SetCard(3, "4", "Four")
        'SetCard(4, "5", "Five")
        'SetCard(5, "6", "Six")


        'NCards = 0
        'IStream = System.IO.File.OpenText(FName)
        'Description = IStream.ReadLine()
        'DataType = IStream.ReadLine()              'skip this line

        'If InStr(DataType, "tab") Then
        '    '"Tabbed" format one item per line
        '    While Not IStream.EndOfStream
        '        DataLine = IStream.ReadLine()
        '        If DataLine <> "" Then
        '            NCards = NCards + 1                 'determine number of items
        '        End If
        '    End While
        '    IStream.Close()

        'Else
        '    '"normal" format line by line
        '    While Not IStream.EndOfStream
        '        DataLine = ""
        '        For i = 0 To 3
        '            DataLine = IStream.ReadLine()
        '        Next
        '        If DataLine <> "" Then
        '            NCards = NCards + 1                 'determine number of items
        '        End If
        '    End While
        '    IStream.Close()
        'End If

        'Start over:
        IStream = System.IO.File.OpenText(FName)
        Description = IStream.ReadLine()

        ReDim Cards(5000)
        Dim CardNum As Integer
        Dim question As String
        Dim answer As String

        CardNum = 0
        '"Tabbed" format one item per line
        While Not IStream.EndOfStream
            DataLine = IStream.ReadLine()

            If DataLine <> "" Then
                StringData = Split(DataLine, vbTab)

                If (StringData.GetUpperBound(0) >= 0) Then
                    question = StringData(0)
                Else
                    question = ""
                    MsgBox("Error reading file.. continuing anyway..")
                    Exit While
                End If

                If (StringData.GetUpperBound(0) >= 1) Then
                    answer = StringData(1)
                Else
                    answer = ""
                    MsgBox("Error reading file.. continuing anyway..")
                    Exit While
                End If

                Cards(CardNum) = New Item
                'Cards(CardNum).Question = StringData(0)
                'Cards(CardNum).Characters = StringData(1)  '->2
                'Cards(CardNum).Answer = StringData(3)  '->1
                SetCard(CardNum, question, answer)
                CardNum = CardNum + 1
            End If
        End While

        'Use Preserve keyword to retain the data within the resized array:
        NCards = CardNum
        ReDim Preserve Cards(CardNum - 1)
    End Sub

    Public Sub LoadFile(ByVal FName As String)
        If (FName.Contains(".txt")) Then
            LoadTxtFile(FName)
        ElseIf (FName.Contains(".csv")) Then
            LoadCSVFile(FName)
        Else
            MessageBox.Show("Unrecognized file type.")
        End If
    End Sub

    Private Sub SetCard(ByVal Index As Integer, ByVal Ques As String, ByVal Ans As String)
        Cards(Index).Index = Index
        Cards(Index).Question = Ques
        Cards(Index).Answer = Ans
        'Cards(Index).Characters = Chr
    End Sub

End Class
