Public Class frmOption

    Private Sub butContinuousSlope_Click(sender As System.Object, e As System.EventArgs) Handles butContinuousSlope.Click
        frmMain.Show()
    End Sub
    Private Sub butSeparateSlope_Click(sender As System.Object, e As System.EventArgs) Handles butSeparateSlope.Click
        frmSeparateSlopesInput.Show()
    End Sub
    Private Sub frmOption_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        MDI.mnuItems.Enabled = True
    End Sub
End Class