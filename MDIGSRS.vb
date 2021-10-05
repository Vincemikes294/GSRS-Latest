Public Class MDIGSRS
    Dim Group_Number As String
    Dim a As Integer
    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

    End Sub
    Private Sub ContinuousSlopeToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs)
        frmMain.GroupContinuousSlope.Enabled = True
        frmMain.GroupSeparateSlope.Enabled = True
        frmMain.butsReset.PerformClick()
        frmMain.GroupSeparateSlope.Enabled = False
    End Sub
    Private Sub SeparateSlopeToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs)
        frmMain.GroupSeparateSlope.Enabled = True
        frmMain.GroupContinuousSlope.Enabled = True
        frmMain.butReset.PerformClick()
        frmMain.GroupContinuousSlope.Enabled = False
        Group_Number = "1"
        frmMain.txtsGroupNumber.Text = Group_Number
        a = frmMain.txtsGroupNumber.Text
    End Sub
    Private Sub LogOutToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs)
        frmMain.Close()
        frmLogin.MdiParent = Me
        frmLogin.Show()
        frmLogin.txtusername.Text = ""
        frmLogin.txtpassword.Text = ""
    End Sub
    Private Sub mnuItems_ItemClicked(sender As System.Object, e As System.Windows.Forms.ToolStripItemClickedEventArgs)

    End Sub
    Private Sub FileToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs)

    End Sub

    Private Sub MDIGSRS_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        frmLogin.Close()
    End Sub
End Class