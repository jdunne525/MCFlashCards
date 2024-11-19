Public Class Item
    Public Question As String
    Public Answer As String
    Public WrongAnswers(3) As String
    Public Index As Integer
    Public NWrongAnswers As Integer

    Public Sub New()
        Question = ""
        Answer = ""
        'clear WrongAnswers:
        For i = 0 To 3
            WrongAnswers(i) = ""
        Next
    End Sub
End Class
