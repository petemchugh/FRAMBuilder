
Public Class form_ViewCodes

    Dim j As Integer 'counter variable for odd/even select/unselect button
    Dim BonusLength As Boolean = False
    Dim OOB As Boolean = False


    Public Sub ViewCodes_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        pb_Loading.Visible = False
        grd_CodeView.DataSource = Nothing
        lbl_queryrunning.Text = ""
        'BonusLength = False
        'OOB = False
        'Just to make sure it's never stuffed with goodies that don't belong there.
        'i.e., you need to reload if you come back here after selecting/verifying
        If dsRecoveries.Tables.Count > 0 Then
            If dsRecoveries.Tables(0).Rows.Count > 0 Then
               'MessageBox.Show("Hiya!")
                dsRecoveries.Tables.Clear()
            End If
        End If
        dsWireTagCodedisplay.Clear() 'Also, start with an empty CWT release dataset
        dtWeights.Clear() 'Clean slate for weights table too

            Me.dom_BroodYearChoose.Items.Add("Everything in database")
            Me.dom_BroodYearChoose.Items.Add("Base Yrs/Stks/Codes Only (2005-2009)")
            Me.dom_BroodYearChoose.Items.Add("OOB Codes")
            'Me.dom_BroodYearChoose.Items.Add("2002")
            'Me.dom_BroodYearChoose.Items.Add("2003")
            'Me.dom_BroodYearChoose.Items.Add("2004")
            'Me.dom_BroodYearChoose.Items.Add("2005")
            'Me.dom_BroodYearChoose.Items.Add("2006")
            'Me.dom_BroodYearChoose.Items.Add("2007")
            'Me.dom_BroodYearChoose.Items.Add("2008")
            'Me.dom_BroodYearChoose.Items.Add("2009")
            Me.dom_BroodYearChoose.Text = "Choose Tag Subset"
            Me.dom_BroodYearChoose.Items.Sort()


        lbl_CWTdb2.Text = CWTdatabasename 'specify db name on form

        'Specify the tool tip hover over help bubbles
        Dim SelectUnselectTip As New ToolTip
        Dim stringy As String
        stringy = "Click for an all stocks/codes run" & vbCr & "Otherwise choose custom list"
        SelectUnselectTip.SetToolTip(btn_SelectUnselect, stringy)



    End Sub


    Private Sub dom_BroodYearChoose_SelectedItemChanged(sender As System.Object, e As System.EventArgs) Handles dom_BroodYearChoose.SelectedItemChanged
        ChooseBY = dom_BroodYearChoose.Text
    End Sub



    Private Sub btn_LoadCodes_Click(sender As System.Object, e As System.EventArgs) Handles btn_LoadCodes.Click
        '-------------------------------------------------------------
         'This one fills the data grid view with recovery information
        '-------------------------------------------------------------


        '*******************************************************************
        'Wipe data grid if it's been loaded once and a new BY is added
        'Delete the added check box column before heading back if it's been created.
        If dsWireTagCodedisplay.Tables.Count > 0 Then
            If dsWireTagCodedisplay.Tables(0).Columns.Count < grd_CodeView.Columns.Count Then
                dsWireTagCodedisplay.Clear()
                grd_CodeView.Columns.Remove("chk")
            End If
        End If
        grd_CodeView.DataSource = Nothing
        dsWireTagCodedisplay.Clear() 'Also, start with an empty CWT release dataset
        '*******************************************************************


        'Define local variables needed to access and import the data contained in a table
        Dim CWTdb As New OleDb.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & CWTdatabasename)
        Dim sql As String       'SQL Query text string
        Dim oledbAdapter As OleDb.OleDbDataAdapter

        'Pretty it up for viewing and less cross-eyed selection
        Dim objAlternatingCellStyle As New DataGridViewCellStyle()
        objAlternatingCellStyle.BackColor = Color.WhiteSmoke
        grd_CodeView.AlternatingRowsDefaultCellStyle = objAlternatingCellStyle
        'grd_CodeView.AutoResizeColumns()
        'grd_CodeView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells


        'Define the data selection here
        'sql = "SELECT WireTagCode.*, RelLoc.release_location_name " & _
        '   "FROM WireTagCode INNER JOIN RelLoc ON WireTagCode.ReleaseSite = RelLoc.release_location_code " & _
        '   "WHERE (((WireTagCode.BroodYear)=2005)) " & _
        '   "ORDER BY WireTagCode.BroodYear"
            Me.dom_BroodYearChoose.Items.Add("Everything in database")
            Me.dom_BroodYearChoose.Items.Add("Base Yrs/Stks/Codes Only (2005-2009)")
            Me.dom_BroodYearChoose.Items.Add("OOB Codes")
            If ChooseBY = "Base Yrs/Stks/Codes Only (2005-2009)" Then
                ChooseBY = "Between 2005 And 2009"
            ElseIf ChooseBY = "Everything in database" Then
                ChooseBY = "Between 1971 And 2015"
            ElseIf ChooseBY = "OOB Codes" Then
                OOB = True
                ChooseBY = "Between 1971 And 2015"
            End If


        If OOB = False Then
            sql = _
              "SELECT FRAM_Stocks.FRAM_StockID, FRAM_Stocks.Stock, WireTagCode.BroodYear, FRAM_Stocks.FRAM_StockLongName, " & _
                 "FRAM_Stocks.Description, WireTagCode.TagCode, WireTagCode.Run, RelLoc.release_location_name, WireTagCode.FirstReleaseDate, WireTagCode.CWTMark1, " & _
                 "WireTagCode.CWTMark1Count, WireTagCode.CWTMark2, WireTagCode.CWTMark2Count " & _
              "FROM FRAM_Stocks INNER JOIN (WireTagCode INNER JOIN RelLoc ON WireTagCode.ReleaseSite = RelLoc.release_location_code) " & _
              "ON FRAM_Stocks.FineStock = WireTagCode.Stock " & _
              "WHERE (((WireTagCode.BroodYear)" & ChooseBY & ") AND ((WireTagCode.CWTMark1)='5000' OR (WireTagCode.CWTMark1)='5500' OR (WireTagCode.CWTMark1)='5205') AND ((FRAM_Stocks.Include)=True)) " & _
              "ORDER BY FRAM_Stocks.FRAM_StockID, WireTagCode.BroodYear, FRAM_Stocks.FRAM_StockLongName, WireTagCode.TagCode"
        ElseIf OOB = True Then
            sql = _
              "SELECT FRAM_Stocks.FRAM_StockID, FRAM_Stocks.Stock, WireTagCode.BroodYear, FRAM_Stocks.FRAM_StockLongName, " & _
                 "FRAM_Stocks.Description, WireTagCode.TagCode, WireTagCode.Run, RelLoc.release_location_name, WireTagCode.FirstReleaseDate, WireTagCode.CWTMark1, " & _
                 "WireTagCode.CWTMark1Count, WireTagCode.CWTMark2, WireTagCode.CWTMark2Count " & _
              "FROM FRAM_Stocks INNER JOIN (WireTagCode INNER JOIN RelLoc ON WireTagCode.ReleaseSite = RelLoc.release_location_code) " & _
              "ON FRAM_Stocks.FineStock = WireTagCode.Stock " & _
              "WHERE (((WireTagCode.BroodYear)" & ChooseBY & ") AND ((WireTagCode.FRAM_OOB) = TRUE) AND ((WireTagCode.CWTMark1)='5000' OR (WireTagCode.CWTMark1)='0000' OR (WireTagCode.CWTMark1)='0500' OR (WireTagCode.CWTMark1)='5500' OR (WireTagCode.CWTMark1)='5205') AND ((FRAM_Stocks.Include)=True)) " & _
              "ORDER BY FRAM_Stocks.FRAM_StockID, WireTagCode.BroodYear, FRAM_Stocks.FRAM_StockLongName, WireTagCode.TagCode"
              ' The Elwha or Sac only filter version...
              ' "WHERE (((WireTagCode.BroodYear)" & ChooseBY & ") AND ((FRAM_Stocks.FineStock) = 'SAC') AND ((WireTagCode.CWTMark1)='5000' OR (WireTagCode.CWTMark1)='0000' OR (WireTagCode.CWTMark1)='0500' OR (WireTagCode.CWTMark1)='5500' OR (WireTagCode.CWTMark1)='5205') AND ((FRAM_Stocks.Include)=True)) " & _
              ' "ORDER BY FRAM_Stocks.FRAM_StockID, WireTagCode.BroodYear, FRAM_Stocks.FRAM_StockLongName, WireTagCode.TagCode"


        End If


        'Read in the data and fill the VB DataSet
        Try
            CWTdb.Open()
            oledbAdapter = New OleDb.OleDbDataAdapter(sql, CWTdb)
            oledbAdapter.Fill(dsWireTagCodedisplay, "WireTagCode")
            oledbAdapter.Dispose()
            CWTdb.Close()

        Catch ex As Exception
            MsgBox("Can not open connection to FRAM_Stock and WireTagCode tables! ")
        End Try

        'Now populate the grid for data viewing
        grd_CodeView.AutoGenerateColumns = True
        grd_CodeView.DataSource = dsWireTagCodedisplay
        grd_CodeView.DataMember = "WireTagCode"
        'grd_CodeView.Columns().SortMode = DataGridViewColumnSortMode.NotSortable

        'Add the checkbox variable once and once only.
        If dsWireTagCodedisplay.Tables(0).Columns.Count = grd_CodeView.Columns.Count Then
            grd_CodeView.Columns.Insert(0, chk)
            chk.HeaderText = "Include?"
            chk.Name = "chk"
            'grd_CodeView.Columns(dsWireTagCodedisplay.Tables(0).Columns.Count + 1).DefaultCellStyle.ForeColor = Color.HotPink
        End If
        'Fix to correct sorting issues...
       For sss As Integer = 0 To grd_CodeView.Columns.Count - 1
              grd_CodeView.Columns(sss).SortMode = DataGridViewColumnSortMode.NotSortable
              If sss > 0 Then
                grd_CodeView.Columns(sss).ReadOnly = True
              End If
       Next sss

    End Sub


    Public Sub btn_SelectUnselect_Click(sender As System.Object, e As System.EventArgs) Handles btn_SelectUnselect.Click
        '-------------------------------------------------------------
         'This one provides select/unselect all capabilities
        '-------------------------------------------------------------
        grd_CodeView.CurrentCell = grd_CodeView.Rows(0).Cells(0)

        'Need to select brood year and display first.
        If grd_CodeView.Rows.Count = 0 Then
            MessageBox.Show("You must choose a brood and load tag info first (above).")
            Exit Sub
        End If

        'Button for selecting or unselecting all, for an all-stocks run
        Dim i As Integer
        Dim R As Integer
        Dim C As Integer

        R = grd_CodeView.Rows.Count - 1
        C = 0 'Move to zero if it's on left
        j = j + 1   'Start it on 1 (True)
        For i = 0 To R - 1
            grd_CodeView.CurrentCell = grd_CodeView(C, i)
            If j Mod 2 = 0 Then
                grd_CodeView.CurrentCell.Value = False
            ElseIf j Mod 2 = 1 Then
                grd_CodeView.CurrentCell.Value = True
            End If
        Next i
        grd_CodeView.CurrentCell = grd_CodeView.Item(0, 0) 'To restore home position after selecting all
        'DataGridView1.FirstDisplayedCell = DataGridView1.Item(0, DataGridView1.RowCount - 1)

    End Sub


    Private Sub btnReturn1_Click(sender As System.Object, e As System.EventArgs) Handles btn_Return1.Click

        '-------------------------------------------------------------
         'Exit without doing anything, go back to start
        '-------------------------------------------------------------

        'Take me back to the main menu, now.
        Me.Visible = False
        form_WelcomeAndConnect.Visible = True


        'Delete the added check box column before heading back if it's been created.
        If dsWireTagCodedisplay.Tables.Count > 0 
            If dsWireTagCodedisplay.Tables(0).Columns.Count < grd_CodeView.Columns.Count Then
                dsWireTagCodedisplay.Clear()
                grd_CodeView.Columns.Remove("chk")
            End If
        End If

    End Sub


   Public Sub btn_LoadTags_Click(sender As System.Object, e As System.EventArgs) Handles btn_LoadTags.Click
        '-------------------------------------------------------------
         'This one populates a list with the chosen (checked) CWT codes
         'And creates a data set of recoveries for processing/mapping
         'which can then be viewed in the verify recoveries form
         'the main meet of this subroutine is done in the background by
         'BackgroundWorker1()
        '-------------------------------------------------------------

        'xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        'In case an error brought you back here, make doubly sure you don't have any junk in your tables
        If dsRecoveries.Tables.Count > 0 Then
            If dsRecoveries.Tables(0).Rows.Count > 0 Then
               'MessageBox.Show("Hiya!")
                dsRecoveries.Tables.Clear()
            End If
        End If
        dtWeights.Clear() 'Clean slate for weights table too
        'xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx

    grd_CodeView.CurrentCell = grd_CodeView.Rows(0).Cells(0)
    lbl_queryrunning.Text = "Recoveries query running, please wait..."



      'Now construct the list of tag codes that will be used to select recoveries.
        'Indexing variables needed for scanning the dataset and populating local arrays

        Dim i As Integer 'Indexing variable for counting the number of rows in the CWT dataset
        Dim k As Integer 'Indexing variable for counting the number of rows with Include = True
        Dim R As Integer 'Rows in the DataGridView
        Dim C As Integer 'Columns in the DataGridView

        'Initialize these clunky indexing variables
        numCodes = 0
        k = 0

        'Columns and rows in the full dataset (selected and unselected) - Count them
        R = grd_CodeView.Rows.Count - 1
        C = 0 'grd_CodeView.Columns.Count - 1 'formerly set up this way with checkbox at right

        'Step for counting the number of codes that were selected
        For i = 0 To R - 1
            If IsDBNull(grd_CodeView(C, i).Value) = False And grd_CodeView(C, i).Value = True Then
                numCodes = numCodes + 1
            End If
        Next i

        If numCodes = 0 Then
            MessageBox.Show("You didn't select any tag codes." & vbCr & _
                                "Select again or exit application.")
            Exit Sub
        End If

        btn_SelectUnselect.Enabled = False
        btn_Return1.Enabled = False
        btn_LoadTags.Enabled = False
        btn_LoadCodes.Enabled = False
        pb_Loading.Visible = True
        Me.Cursor = Cursors.WaitCursor

        'Redim these arrays based on the selection
        ReDim CWTStock(numCodes - 1)
        ReDim CWTcode(numCodes - 1) 'This is really the only one that's needed for recovery selection
        ReDim CWTbrood(numCodes - 1)
        ReDim CWTruntime(numCodes - 1)
        ReDim FRAMStockName(numCodes - 1)
        ReDim FRAMStockNum(numCodes - 1)

        'Now, scan and store the tag details
        For i = 0 To R - 1
            grd_CodeView.CurrentCell = grd_CodeView(C, i)
            If grd_CodeView.CurrentCell.Value = True Then
                'grd_CodeView.CurrentCell = grd_CodeView(0, i) '^^^^^^^^^^^^^^^May need to renumber if columns change
                'CWTStock(k) = grd_CodeView.CurrentCell.Value
                CWTStock(k) = dsWireTagCodedisplay.Tables(0).Select()(i)("Stock") 'More general coding to deal with the potential for CAS changes
                'grd_CodeView.CurrentCell = grd_CodeView(2, i) '^^^^^^^^^^^^^^^May need to renumber if columns change
                'CWTcode(k) = grd_CodeView.CurrentCell.Value
                CWTcode(k) = dsWireTagCodedisplay.Tables(0).Select()(i)("TagCode") 'More general coding to deal with the potential for CAS changes
                'grd_CodeView.CurrentCell = grd_CodeView(1, i) '^^^^^^^^^^^^^^^May need to renumber if columns change
                'CWTbrood(k) = grd_CodeView.CurrentCell.Value
                CWTbrood(k) = dsWireTagCodedisplay.Tables(0).Select()(i)("BroodYear") 'More general coding to deal with the potential for CAS changes
                If IsDBNull(dsWireTagCodedisplay.Tables(0).Select()(i)("Run")) = False Then
                    CWTruntime(k) = dsWireTagCodedisplay.Tables(0).Select()(i)("Run") 'More general coding to deal with the potential for CAS changes
                Else
                    CWTruntime(k) = 0
                End If
                FRAMStockName(k) = dsWireTagCodedisplay.Tables(0).Select()(i)("FRAM_StockLongName") 'Full name of stock
                FRAMStockNum(k) = dsWireTagCodedisplay.Tables(0).Select()(i)("FRAM_StockID")  'FRAM stock number
                'Debug.Print(CWTStock(k) & " And " & CWTcode(k) & " Brood Year " & CWTbrood(k))
                k = k + 1

            End If

        Next i


        '    '************************************************************************************
        '    'Also, populate a datatable with weights for merging across BYs later on down the line
        '    'First - make string of selected codes for the weights table query
        'If (form_OutputOptions.ck_bnBroodWt.Checked = True Or form_OutputOptions.ck_winBYwt.Checked = True) Then


        '    Dim codestring As String 'Text for SQL Query of Weights table
        '    Dim Sql As String
        '    Dim CWTdb As New OleDb.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & CWTdatabasename)
        '    Dim oledbAdapter As OleDb.OleDbDataAdapter

        '    codestring = "(("
        '    If CWTcode.Length = 1 Then
        '        codestring = codestring & "(FRAM_Weights.CWTcode)= """ & CWTcode(0) & """))"
        '    Else 'CWTcode.Length = 2 Then
        '        For i = 0 To CWTcode.Length - 2
        '            codestring = codestring & "(FRAM_Weights.CWTcode)= """ & CWTcode(i) & """ OR "
        '        Next
        '        codestring = codestring & "(FRAM_Weights.CWTcode)= """ & CWTcode(CWTcode.Length - 1) & """))"
        '    End If

        '    Sql = "SELECT * FROM(FRAM_Weights)" 'WHERE " & codestring

        '    'Read in the data and fill the VB DataSet
        '    Try
        '        CWTdb.Open()
        '        oledbAdapter = New OleDb.OleDbDataAdapter(Sql, CWTdb)
        '        oledbAdapter.Fill(dtWeights)
        '        oledbAdapter.Dispose()
        '        CWTdb.Close()

        '    Catch ex As Exception
        '        MsgBox("Can not open connection to FRAM_BYwts table! ")
        '    End Try
        '    '************************************************************************************
        'End If

        'Now, run the recovery query in the background, but don't proceed to the VerifyTags form until it's done...
        Call BackgroundWorker1.RunWorkerAsync()

        Do While BackgroundWorker1.IsBusy = True
            Application.DoEvents()
        Loop

    btn_SelectUnselect.Enabled = True
    btn_Return1.Enabled = True
    btn_LoadTags.Enabled = True
    btn_LoadCodes.Enabled = True

   Me.Cursor = Cursors.Default
   pb_Loading.Visible = False
   lbl_queryrunning.Text = ""

   Me.Visible = False
   VerifyTags.ShowDialog() 'Now move to the tag viewer form for a quick view/selection
   VerifyTags.TopMost = True
   End Sub

    Public Sub BackgroundWorker1_DoWork(sender As System.Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        '-------------------------------------------------------------
         'This backgroundworker actually does the query for the chosen
         'set of CWT codes
        '-------------------------------------------------------------


      'Define variables needed to access and import the data contained in a table
        Dim CWTdb As New OleDb.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & CWTdatabasename)
        Dim sqlCodes As String
        Dim sql As String       'SQL Query text string
        Dim oledbAdapter As OleDb.OleDbDataAdapter
        Dim i As Integer

        If CWTcode.Length = 1 Then 'One tag case
            sqlCodes = "(("
            sqlCodes = sqlCodes & "(CWDBRecovery.TagCode)= """ & CWTcode(0) & """))"
            'Build the query string for extracting from the recovery database
            sql = "SELECT * FROM CWDBRecovery WHERE " & sqlCodes
            'Read in the data and fill the VB DataSet
            Try
                CWTdb.Open()
                oledbAdapter = New OleDb.OleDbDataAdapter(sql, CWTdb)
                oledbAdapter.Fill(dsRecoveries, "CWDBRecovery") 'Creates the local dataset of chosen recoveries
                oledbAdapter.Dispose()
                CWTdb.Close()
            Catch ex As Exception
                MsgBox("Can not open connection ! ")
            End Try
        ElseIf CWTcode.Length >= 99 Then 'Length limits on query conditions, must break into 99 object parcels
            Dim sets As Integer = Math.Round(CWTcode.Length / 99)
            Dim counter As Integer = 0
            Dim countess As Integer = 0
            For l = 0 To sets
                sqlCodes = "(("
                If (((l + 1) * 99) - 1) <= (CWTcode.Length - 1) Then counter = ((l + 1) * 99) - 1 Else counter = CWTcode.Length - 1
                For i = countess To counter - 1
                    sqlCodes = sqlCodes & "(CWDBRecovery.TagCode)= """ & CWTcode(i) & """ OR "
                Next
                sqlCodes = sqlCodes & "(CWDBRecovery.TagCode)= """ & CWTcode(counter) & """))"
                countess = (l + 1) * 99

                'Build the query string for extracting from the recovery database
                sql = "SELECT * FROM CWDBRecovery WHERE " & sqlCodes
                'Read in the data and fill the VB DataSet
                Try
                    CWTdb.Open()
                    oledbAdapter = New OleDb.OleDbDataAdapter(sql, CWTdb)
                    oledbAdapter.Fill(dsRecoveries, "CWDBRecovery") 'Creates the local dataset of chosen recoveries
                    oledbAdapter.Dispose()
                    CWTdb.Close()
                Catch ex As Exception
                    MsgBox("Can not open connection ! ")
                End Try
            Next
        Else 'between 1 and 99 records...
            sqlCodes = "(("
            For i = 0 To CWTcode.Length - 2
                sqlCodes = sqlCodes & "(CWDBRecovery.TagCode)= """ & CWTcode(i) & """ OR "
            Next
            sqlCodes = sqlCodes & "(CWDBRecovery.TagCode)= """ & CWTcode(CWTcode.Length - 1) & """))"

            'Build the query string for extracting from the recovery database
            sql = "SELECT * FROM CWDBRecovery WHERE " & sqlCodes
            'Read in the data and fill the VB DataSet
            Try
                CWTdb.Open()
                oledbAdapter = New OleDb.OleDbDataAdapter(sql, CWTdb)
                oledbAdapter.Fill(dsRecoveries, "CWDBRecovery") 'Creates the local dataset of chosen recoveries
                oledbAdapter.Dispose()
                CWTdb.Close()

            Catch ex As Exception
                MsgBox("Can not open connection ! ")
            End Try
        End If

        'Debug.Print(sqlCodes)

    End Sub


   Private Sub btn_BypassView_Click(sender As System.Object, e As System.EventArgs) Handles btn_BypassView.Click


    'THIS SUBROUTINE IS DISUSED. MUST ADD IN CODE TO  HANDLE TAG LIST >100 IF REACTIVATING



        'xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        'In case an error brought you back here, make doubly sure you don't have any junk in your tables
        If dsRecoveries.Tables.Count > 0 Then
            If dsRecoveries.Tables(0).Rows.Count > 0 Then
               'MessageBox.Show("Hiya!")
                dsRecoveries.Tables.Clear()
            End If
        End If
        dtWeights.Clear() 'Clean slate for weights table too
        'xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx

        '-------------------------------------------------------------
         'This achieves the same ends as btn_LoadTags() and BackgroundWorker()
         'withouth having to go to the verify tags screen
        '-------------------------------------------------------------
        grd_CodeView.CurrentCell = grd_CodeView.Rows(0).Cells(0)
        lbl_queryrunning.Text = "Recoveries query running, please wait..."

      'Now construct the list of tag codes that will be used to select recoveries.
      'Indexing variables needed for scanning the dataset and populating local arrays

      Dim i As Integer 'Indexing variable for counting the number of rows in the CWT dataset
      Dim k As Integer 'Indexing variable for counting the number of rows wtih Include = True
      Dim R As Integer 'Rows in the DataGridView
      Dim C As Integer 'Columns in the DataGridView


      'Initialize these clunky indexing variables
      numCodes = 0
      k = 0

      'Columns and rows in the full dataset (selected and unselected) - Count them
      R = grd_CodeView.Rows.Count - 1
      C = 0 'grd_CodeView.Columns.Count - 1 'formerly set up this way with checkbox at right

      'Step for counting the number of codes that were selected
      For i = 0 To R - 1
         If IsDBNull(grd_CodeView(C, i).Value) = False And grd_CodeView(C, i).Value = True Then
            numCodes = numCodes + 1
         End If
      Next i

      If numCodes = 0 Then
         MessageBox.Show("You didn't select any tag codes." & vbCr & _
                             "Select again or exit application.")
         Exit Sub
      End If
      pb_Loading.Visible = True
      Me.Cursor = Cursors.WaitCursor

      'Redim these arrays based on the selection
      ReDim CWTStock(numCodes - 1)
      ReDim CWTcode(numCodes - 1) 'This is really the only one that's needed for recovery selection
      ReDim CWTbrood(numCodes - 1)
      ReDim CWTruntime(numCodes - 1)
      ReDim FRAMStockName(numCodes - 1)
      ReDim FRAMStockNum(numCodes - 1)

      'Now, scan and store the tag details
      For i = 0 To R - 1

         Application.DoEvents()

         grd_CodeView.CurrentCell = grd_CodeView(C, i)
         If grd_CodeView.CurrentCell.Value = True Then
            'grd_CodeView.CurrentCell = grd_CodeView(0, i) '^^^^^^^^^^^^^^^May need to renumber if columns change
            'CWTStock(k) = grd_CodeView.CurrentCell.Value
            CWTStock(k) = dsWireTagCodedisplay.Tables(0).Select()(i)("Stock") 'More general coding to deal with the potential for CAS changes
            'grd_CodeView.CurrentCell = grd_CodeView(2, i) '^^^^^^^^^^^^^^^May need to renumber if columns change
            'CWTcode(k) = grd_CodeView.CurrentCell.Value
            CWTcode(k) = dsWireTagCodedisplay.Tables(0).Select()(i)("TagCode") 'More general coding to deal with the potential for CAS changes
            'grd_CodeView.CurrentCell = grd_CodeView(1, i) '^^^^^^^^^^^^^^^May need to renumber if columns change
            'CWTbrood(k) = grd_CodeView.CurrentCell.Value
            CWTbrood(k) = dsWireTagCodedisplay.Tables(0).Select()(i)("BroodYear") 'More general coding to deal with the potential for CAS changes
            If IsDBNull(dsWireTagCodedisplay.Tables(0).Select()(i)("Run")) = False Then
               CWTruntime(k) = dsWireTagCodedisplay.Tables(0).Select()(i)("Run") 'More general coding to deal with the potential for CAS changes
            Else
               CWTruntime(k) = 0
            End If
            FRAMStockName(k) = dsWireTagCodedisplay.Tables(0).Select()(i)("FRAM_StockLongName") 'Full name of stock
            FRAMStockNum(k) = dsWireTagCodedisplay.Tables(0).Select()(i)("FRAM_StockID")  'FRAM stock number
            'Debug.Print(CWTStock(k) & " And " & CWTcode(k) & " Brood Year " & CWTbrood(k))
            k = k + 1

         End If
      Next i


      Dim codestring As String 'Text for SQL Query of Weights table
      Dim Sql As String
      Dim CWTdb As New OleDb.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & CWTdatabasename)
      Dim oledbAdapter As OleDb.OleDbDataAdapter


        If (form_OutputOptions.ck_bnBroodWt.Checked = True Or form_OutputOptions.ck_winBYwt.Checked = True) Then

              '************************************************************************************
              'Also, populate a datatable with weights for merging across BYs later on down the line
              'First - make string of selected codes for the weights table query


              codestring = "(("
              If CWTcode.Length = 1 Then
                  codestring = codestring & "(FRAM_Weights.CWTcode)= """ & CWTcode(0) & """))"
              Else 'CWTcode.Length = 2 Then
                  For i = 0 To CWTcode.Length - 2
                      codestring = codestring & "(FRAM_Weights.CWTcode)= """ & CWTcode(i) & """ OR "
                  Next
                  codestring = codestring & "(FRAM_Weights.CWTcode)= """ & CWTcode(CWTcode.Length - 1) & """))"
              End If
              Sql = "SELECT * FROM(FRAM_Weights)" 'WHERE " & codestring

              'Read in the data and fill the VB DataSet
              Try
                  CWTdb.Open()
                  oledbAdapter = New OleDb.OleDbDataAdapter(Sql, CWTdb)
                  oledbAdapter.Fill(dtWeights)
                  oledbAdapter.Dispose()
                  CWTdb.Close()
               Catch ex As Exception
                  MsgBox("Can not open connection to FRAM_BYwts table! ")
               End Try
        End If

      '************************************************************************************

      '************************************************************************************
      'Now get CWT recovery details for selected codes; recycle db access variables from weights query
      Dim sqlCodes As String
      sqlCodes = "(("
      If CWTcode.Length = 1 Then
         sqlCodes = sqlCodes & "(CWDBRecovery.TagCode)= """ & CWTcode(0) & """))"
      Else 'CWTcode.Length = 2 Then
         For i = 0 To CWTcode.Length - 2
            sqlCodes = sqlCodes & "(CWDBRecovery.TagCode)= """ & CWTcode(i) & """ OR "
         Next
         sqlCodes = sqlCodes & "(CWDBRecovery.TagCode)= """ & CWTcode(CWTcode.Length - 1) & """))"
      End If
      Sql = "SELECT * FROM CWDBRecovery WHERE " & sqlCodes

      'Read in the data and fill the VB DataSet
      Try
         CWTdb.Open()
         oledbAdapter = New OleDb.OleDbDataAdapter(Sql, CWTdb)
         oledbAdapter.Fill(dsRecoveries, "CWDBRecovery") 'Creates the local dataset of chosen recoveries
         oledbAdapter.Dispose()
         CWTdb.Close()

      Catch ex As Exception
         MsgBox("Can not open connection to CWDBRecovery table! ")
      End Try
    '************************************************************************************

      Me.Cursor = Cursors.Default
      pb_Loading.Visible = False

      Me.Close()
      dsWireTagCodedisplay.Clear() 'Dump the CWT releases dataset
      grd_CodeView.Columns.Remove("chk")
      form_WelcomeAndConnect.Visible = True

End Sub




End Class


