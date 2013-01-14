Public Class PrintForm

    Public PrintFronts As Boolean
    Public PrintBacks As Boolean
    Public PrintDoubleSided As Boolean

    Private Sub PrintForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        PrintFronts = False
        PrintBacks = False
        PrintDoubleSided = False
        Label2.Text = "Pages are printed in 2 stages: " + vbCr + _
            "First Click Print ""Fronts,"" " + vbCr + _
            " then put those pages back into the printer and click Print ""Backs"""
    End Sub

    Private Sub btnPrintFronts_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrintFronts.Click
        PrintFronts = True
        Me.Hide()
    End Sub

    Private Sub btnPrintBacks_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrintBacks.Click
        PrintBacks = True
        Me.Hide()
    End Sub

    Private Sub btnDoubleSided_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDoubleSided.Click
        PrintDoubleSided = True
        Me.Hide()
    End Sub

    Private Sub PrintForm_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        Me.Hide()
    End Sub
End Class