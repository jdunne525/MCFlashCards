Public Class Game
    Public GameRunning As Boolean
    Public AnswerToDisplay As Item
    Public CardsToDisplay(3) As Item

    Public CardsDisplayed(3) As Item
    Public CurrentItemSet As Integer
    Public NumberOfRndItemSets As Integer
    Public AllRandom As Boolean
    Public NItemSetsInData As Integer

    Private CardsInPlay(3) As Item
    Private CurrentItem As Integer
    Private Data As DataHandler
    Private LastCardShown As Integer

    Public Sub New(ByVal DHandler As DataHandler)
        Data = DHandler
        GameRunning = False
        NumberOfRndItemSets = 2
        For i = 0 To 3
            CardsToDisplay(i) = New Item()
        Next
    End Sub

    'Returns True if Game started.
    Public Function StartGame() As Boolean
        GameRunning = False
        If Data.NCards < 4 Then
            Return False        'Not enough cards to play.
        End If

        NItemSetsInData = (Data.NCards - 4) / 2 + 1         'This works because NItemSetsInData MUST be > 4

        LastCardShown = 3       'we're about to show cards 0 through 4:
        For i = 0 To 3
            CardsInPlay(i) = Data.Cards(i)      'load first 4 cards

            CardsToDisplay(i) = CardsInPlay(i)  'Load up display cards
        Next

        RandomizeArray(CardsToDisplay)          'Randomize the cards to display
        RandomizeArray(CardsInPlay)             'Randomize the order in which to Request them
        CurrentItemSet = 0
        CurrentItem = 0
        AnswerToDisplay = CardsInPlay(CurrentItem)
        GameRunning = True
        Return True
    End Function

    Public Function NextQuestion(ByVal AutoIncrement As Boolean)
        If CurrentItem < 3 Then
            RandomizeArray(CardsToDisplay)          'Randomize the cards to display
            CurrentItem = CurrentItem + 1           'go to the next question
            AnswerToDisplay = CardsInPlay(CurrentItem)
        Else

            'If LastCardShown + 2 >= Data.NCards Then
            '    'We're out of new cards.
            '    Return False
            'End If

            If AutoIncrement Then
                CurrentItemSet = CurrentItemSet + 1
            End If

            If CurrentItemSet >= NItemSetsInData + NumberOfRndItemSets Then
                CurrentItemSet = NItemSetsInData + NumberOfRndItemSets - 1
                GameRunning = False
                Return False        'Done.
            End If

            Dim NumOfRndCards As Integer

            LastCardShown = 3 + CurrentItemSet * 2
            If LastCardShown >= Data.NCards Then
                LastCardShown = Data.NCards - 1
            End If

            If (CurrentItemSet = 0) Then
                NumOfRndCards = 4
            Else
                NumOfRndCards = 2
            End If

            If Data.NCards - LastCardShown - 1 < 2 Then
                NumOfRndCards = 4 - (Data.NCards - LastCardShown - 1)
                If NumOfRndCards > 4 Then
                    NumOfRndCards = 4
                End If
            End If
            If AllRandom Then
                NumOfRndCards = 4
                LastCardShown = Data.NCards
            End If

            Dim RndCards(NumOfRndCards - 1) As Item

            ''Grab 2 random cards from the previous set:
            'Dim rnd As New Random
            ''Get 2 random cards which are not the same from the previous data sets:
            'RndCard1 = RandomNumber(0, LastCardShown)            'pick one of the previous cards
            'Do
            '    RndCard2 = rnd.Next(0, LastCardShown)
            '    If RndCard1 <> RndCard2 Then
            '        Exit Do                                 'loop until we have 2 distinct cards
            '    End If
            'Loop

            GetRandomCards(NumOfRndCards, LastCardShown, RndCards, Data.Cards)  'Get 2 random cards

            'Load 4 new cards:
            For i = 1 To NumOfRndCards
                CardsInPlay(i - 1) = RndCards(i - 1)            'put the random cards into the set
            Next

            For i = NumOfRndCards To 3
                CardsInPlay(i) = Data.Cards(LastCardShown + 1)  'put the Next cards into the set
                LastCardShown = LastCardShown + 1
            Next

            For i = 0 To 3
                CardsToDisplay(i) = CardsInPlay(i)              'Load up display cards
            Next

            RandomizeArray(CardsToDisplay)                      'Randomize the cards to display
            RandomizeArray(CardsInPlay)             'Randomize the order in which to Request them

            CurrentItem = 0
            AnswerToDisplay = CardsToDisplay(CurrentItem)
        End If

        Dim tempval As String
        For i = 0 To 3
            tempval = CardsInPlay(i).Answer
        Next

        Return True
    End Function

    Private Sub GetRandomCards(ByVal Number As Integer, ByVal LastItem As Integer, ByRef RndCards() As Item, ByRef DataSet() As Item)
        'Dim rnd As New Random
        Dim NewCard As Integer
        Dim Cards(Number - 1) As Integer
        Dim Attempts As Integer

        If Number = 0 Then Return

        For i = 0 To Number - 1
            Cards(i) = -1           'initialize with nonzero values so we can put 0 wherever we want
        Next

        Cards(0) = RandomNumber(0, LastItem + 1)            'pick one of the previous cards
        For i = 1 To Number - 1
            Attempts = 0
            Do
                Attempts = Attempts + 1
                NewCard = RandomNumber(0, LastItem)
                For j = 0 To i - 1
                    If Attempts >= 50 Then
                        'possible infinite loop.  Just give up.
                        Cards(i) = NewCard
                        Exit Do
                    End If

                    If NewCard = Cards(j) Then
                        Continue Do                 'try again.  It's a dupe
                    End If
                Next
                Cards(i) = NewCard                  'The New card is good, add it to our Cards list
                Exit Do
            Loop
        Next

        For i = 0 To Number - 1
            RndCards(i) = DataSet(Cards(i))        'copy the random cards from the full dataset to the Random card set.
        Next

    End Sub

    Public Function SetItemSet(ByVal NewItemSet As Integer)
        Dim NItemSetsInData As Integer
        NItemSetsInData = (Data.NCards - 4) / 2 + 1         'This works because NItemSetsInData MUST be > 4

        If (NewItemSet < 0) Then NewItemSet = 0


        CurrentItemSet = NewItemSet

        If CurrentItemSet >= NItemSetsInData + NumberOfRndItemSets Then
            CurrentItemSet = NItemSetsInData + NumberOfRndItemSets - 1          '-2 so that when we go to the NextQuestion
        End If
        CurrentItem = 3
        Return NextQuestion(False)
    End Function

    Public Function CheckAnswer(ByVal AnswerClicked As String)
        'If CardsToDisplay(Index - 1).Index = AnswerToDisplay.Index Then
        'Check that the answer is correct, not just that it's the exact matching card we were looking for:
        If AnswerClicked = AnswerToDisplay.Answer Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub RandomizeArray(ByVal items() As Item)
        For count = 1 To 10
            Dim max_index As Integer = items.Length - 1
            'Dim rnd As New Random
            For i As Integer = 0 To max_index - 1
                ' Pick an item for position i.
                Dim j As Integer = RandomNumber(i, max_index + 1)

                ' Swap them.
                Dim temp As Item = items(i)
                items(i) = items(j)
                items(j) = temp
            Next i
        Next
    End Sub
    Private Sub RandomizeArray(ByVal items() As Integer)
        For count = 1 To 10
            Dim max_index As Integer = items.Length - 1
            'Dim rnd As New Random
            For i As Integer = 0 To max_index - 1
                ' Pick an item for position i.
                Dim j As Integer = RandomNumber(i, max_index + 1)

                ' Swap them.
                Dim temp As Integer = items(i)
                items(i) = items(j)
                items(j) = temp
            Next i
        Next
    End Sub

    'MinNumber is inclusive and MaxNumber is exclusive
    Public Function RandomNumber(ByVal MaxNumber As Integer, _
        Optional ByVal MinNumber As Integer = 0) As Integer

        'initialize random number generator
        Dim r As New Random(System.DateTime.Now.Millisecond)

        'if passed incorrect arguments, swap them
        'can also throw exception or return 0

        If MinNumber > MaxNumber Then
            Dim t As Integer = MinNumber
            MinNumber = MaxNumber
            MaxNumber = t
        End If

        Return r.Next(MinNumber, MaxNumber)       'MaxNumber is Exclusive in this call!!

    End Function

    Public Sub SaveResults(filename As String, header As String, Line As String)
        'Save results to csv file
        'Append to existing file
        'check if the file exists and if not, create it and append the header

        If Not System.IO.File.Exists(filename) Then
            Dim fso1 As New System.IO.FileStream(filename, System.IO.FileMode.Create, System.IO.FileAccess.Write)
            Dim sw1 As New System.IO.StreamWriter(fso1)
            sw1.WriteLine(header)
            sw1.Close()
            fso1.Close()
        End If

        Dim fso As New System.IO.FileStream(filename, System.IO.FileMode.Append, System.IO.FileAccess.Write)
        Dim sw As New System.IO.StreamWriter(fso)

        sw.WriteLine(Line)
        sw.Close()
        fso.Close()


    End Sub

End Class
