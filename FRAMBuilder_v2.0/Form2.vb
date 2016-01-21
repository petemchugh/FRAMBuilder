Public Class ViewCodes

    Private Sub DataGridView1_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdCodeView.CellContentClick

    End Sub

    Private Sub btn_LoadCodes_Click(sender As System.Object, e As System.EventArgs) Handles btn_LoadCodes.Click

        'Define variables needed to access and import the data contained in a table
        Dim CWTdb As New OleDb.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & CWTdatabasename)
        Dim sql As String       'SQL Query text string
        Dim oledbAdapter As OleDb.OleDbDataAdapter
        Dim ds As New DataSet   'object type in VB that handles data well
        Dim i As Integer

        'Define the data selection here
        sql = "SELECT * FROM WireTagCode"

        'Read in the data and fill the VB DataSet
        'Try
        CWTdb.Open()
        oledbAdapter = New OleDb.OleDbDataAdapter(sql, CWTdb)
        oledbAdapter.Fill(ds)
        oledbAdapter.Dispose()
        CWTdb.Close()

        'For i = 0 To ds.Tables(0).Rows.Count - 1
        'MsgBox(ds.Tables(0).Rows(i).Item(0) & "  --  " & ds.Tables(0).Rows(i).Item(1))
        'Next
        'Catch ex As Exception
        '    MsgBox("Can not open connection ! ")
        'End Try

        'Now populate the grid for data viewing
        grdCodeView.AutoGenerateColumns = True
        grdCodeView.DataSource = ds
        grdCodeView.DataMember = "WireTagCode"

    End Sub
End Class