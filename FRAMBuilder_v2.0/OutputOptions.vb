Imports System.Data.OleDb
Imports System.Data
Imports System.IO
Imports System.Windows
Imports System.Text
Imports System.IO.File
Imports System.Math
Imports System.Threading


Public Class form_OutputOptions

    Private Sub OutputOptions_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        lbl_CWTdb3.Text = CWTdatabasename


        Dim tip_MapWrite As New ToolTip
        Dim string4 As String
        string4 = "Generate mapped output files and tag summaries" & vbCr & "and monitor mapping/writing progress."
        tip_MapWrite.SetToolTip(btn_CreateOut, string4)

    End Sub

    Private Sub btnReturn1_Click(sender As System.Object, e As System.EventArgs) Handles btn_Return1.Click

        Me.Visible = False
        form_WelcomeAndConnect.Visible = True

    End Sub

    Private Sub btn_SetDir_Click(sender As System.Object, e As System.EventArgs) Handles btn_SetDir.Click

        'Code for specifying the directory for writing XXXXXX.CWT and XXXXXXbad.txt files
        Dim FileChoose As New FolderBrowserDialog
        FileChoose.Description = "Choose Output Directory"
        FileChoose.SelectedPath = "C:\"
        'fpath = "C:\"   'Default directory
        'fpath = "C:\Users\mchugpam\Desktop\AA TestingJunk\" 'Set this for faster running and testing
        FileChoose.RootFolder = Environment.SpecialFolder.Desktop
        If FileChoose.ShowDialog() = Windows.Forms.DialogResult.OK Then
            fpath = FileChoose.SelectedPath
        End If

        'Debug.Print(fpath)

    End Sub

    Private Sub btn_CreateOut_Click(sender As System.Object, e As System.EventArgs) Handles btn_CreateOut.Click


        'Uncomment this if you desire to create/revert to the old *.CWT text file world...
        '' '' ''Junk for speedy testing...
        ' '' ''If IsNothing(fpath) = True Then
        ' '' ''    'fpath = "C:\Users\mchugpam\Desktop\AA TestingJunk" 'Set this for faster running and testing
        ' '' ''    'Need error handler
        ' '' ''    MessageBox.Show("You haven't set a file path for writing files" & vbCrLf _
        ' '' ''    & "Click 'Set Output Directory' and Re-run")
        ' '' ''    Exit Sub
        ' '' ''End If


        '************************************************************************************
        'Also, populate a datatable with weights for merging across BYs later on down the line
        'First - make string of selected codes for the weights table query
        If (ck_bnBroodWt.Checked = True Or ck_winBYwt.Checked = True) Then


            Dim codestring As String 'Text for SQL Query of Weights table
            Dim Sql As String
            Dim CWTdb As New OleDb.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & CWTdatabasename)
            Dim oledbAdapter As OleDb.OleDbDataAdapter

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


     '----------------------------------------------------------------
     'Code that invokes the weighting review/edit screen
      wtresult = False 'boolean for returning to output options screen
      If ck_winBYwt.Checked = True Then
        WeightSpecs.ShowDialog()
        WeightSpecs.BringToFront()
        'User canceled in the weight view/edit/confirm screen, return to OutputOptions
        If wtresult = True Then
            Exit Sub
        End If
        'Otherwise, move forward with weights specified by user
      End If
     '----------------------------------------------------------------


      Me.Cursor = Cursors.WaitCursor
      form_StatusAndSummary.pb_statussumm.Visible = True
      form_StatusAndSummary.lbl_StatusUpd.Text = "Recovery mapping and file writing underway, please wait..."
      form_StatusAndSummary.btn_Return1.Enabled = False


        'If you haven't selected anything, it won't go forward...
        'If dsRecoveries.Tables.Count = 0 Then
        '    MessageBox.Show("You need to select codes and import recoveries before specifying options")
        '    Exit Sub
        'End If
        Me.Visible = False
        form_StatusAndSummary.Visible = True

        If (ck_LengthOnly.Checked = True Or ck_LengthToo.Checked = True) Then
          Dim Sql As String
          Dim CWTdb As New OleDb.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & CWTdatabasename)
          Dim oledbAdapter As OleDb.OleDbDataAdapter

          'Read in the data and fill the VB DataSet
          Try
              CWTdb.Open()
                'Sql = "SELECT * FROM(FRAM_SizeLimits)" 'WHERE " & codestring
                'oledbAdapter = New OleDb.OleDbDataAdapter(Sql, CWTdb)
                'oledbAdapter.Fill(dtSizeLimits)
                'oledbAdapter.Dispose()
              Sql = "SELECT * FROM(MarkType)" 'WHERE " & codestring
              oledbAdapter = New OleDb.OleDbDataAdapter(Sql, CWTdb)
              oledbAdapter.Fill(dtMarkType)
              oledbAdapter.Dispose()
              CWTdb.Close()
          Catch ex As Exception
              MsgBox("Can not open connection to FRAM_BYwts table! ")
          End Try
        End If


        'This is a bit slow and clunky, will bog down some. 
        Call BGworker_output.RunWorkerAsync()

        Do While BGworker_output.IsBusy = True
            Application.DoEvents()
        Loop

        'Lastly, wipe the recoveries dataset clean so additional codes can be prepped in same session
        If dsRecoveries.Tables.Count > 0 Then
            If dsRecoveries.Tables(0).Rows.Count > 0 Then
               dsRecoveries.Tables.Clear()
            End If
        End If

        Me.Cursor = Cursors.Default
        form_StatusAndSummary.lbl_StatusUpd.Text = "Processing complete."
        form_StatusAndSummary.pb_statussumm.Visible = False
        form_StatusAndSummary.btn_Return1.Enabled = True

    End Sub



   Public Sub CombineCodes()

        'dsWireTagCodemerge.Clear() 'Make sure this is clean and clear...
        dtCWTout.Tables(0).Rows.Clear() 'Clear the local data table and re-use it for supacode

        'The goal of this subroutine is to create a supercode and associated files for all selected release groups within a BY and stock,
        ' and/or production type (Yr/Fing)
        'It allows for either no weighting (i.e., self weighting by release size, default or flag = 1 in db table)
        ' or user-specified weighting (flag = 2 in db table).

        '________________________________________________________________________________________________________________________________
        'Step 1, query again the WireTagCode Table to find out whether or not there is any merging to be done
        'It will be much easier to test whether or not there's anything to merge within the set of codes selected, 
        'and if so to identify those codes/stocks
        Dim CWTdb As New OleDb.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & CWTdatabasename)
        'Dim sqlCodes As String
        Dim sql As String       'SQL Query text string
        Dim oledbAdapter As OleDb.OleDbDataAdapter
        Dim i As Integer
        Dim DBwtWithinBYwarning As Boolean = False

        ' ''If numCodes = 1 Then 'One tag case
        ' ''    sqlCodes = "(("
        ' ''    sqlCodes = sqlCodes & "(WireTagCode.TagCode)= """ & CWTcode(i) & """ ))"
        ' ''    'Build the query string for extracting from the recovery database
        ' ''    sql = "SELECT * FROM WireTagCode WHERE " & sqlCodes
        ' ''    'Read in the data and fill the VB DataSet
        ' ''    Try
        ' ''        CWTdb.Open()
        ' ''        oledbAdapter = New OleDb.OleDbDataAdapter(sql, CWTdb)
        ' ''        oledbAdapter.Fill(dsWireTagCodemerge, "WireTagCode") 'Creates the local dataset of chosen recoveries
        ' ''        oledbAdapter.Dispose()
        ' ''        CWTdb.Close()
        ' ''    Catch ex As Exception
        ' ''        MsgBox("Can not open connection! ")
        ' ''    End Try
        ' ''ElseIf numCodes >= 99 Then 'Length limits on query conditions, must break into 99 object parcels
        ' ''    Dim sets As Integer = Math.Round(numCodes / 99)
        ' ''    Dim counter As Integer = 0
        ' ''    Dim countess As Integer = 0
        ' ''    For l = 0 To sets
        ' ''        sqlCodes = "(("
        ' ''        If (((l + 1) * 99) - 1) <= (numCodes - 1) Then counter = ((l + 1) * 99) - 1 Else counter = numCodes - 1
        ' ''        For i = countess To counter - 1
        ' ''            sqlCodes = sqlCodes & "(WireTagCode.TagCode)= """ & CWTcode(i) & """ OR "
        ' ''        Next
        ' ''        sqlCodes = sqlCodes & " (WireTagCode.TagCode)= """ & CWTcode(counter) & """))"
        ' ''        countess = (l + 1) * 99
        ' ''    'Build the query string for extracting from the recovery database
        ' ''    sql = "SELECT * FROM WireTagCode WHERE " & sqlCodes
        ' ''    'Read in the data and fill the VB DataSet
        ' ''    Try
        ' ''        CWTdb.Open()
        ' ''        oledbAdapter = New OleDb.OleDbDataAdapter(sql, CWTdb)
        ' ''        oledbAdapter.Fill(dsWireTagCodemerge, "WireTagCode") 'Creates the local dataset of chosen recoveries
        ' ''        oledbAdapter.Dispose()
        ' ''        CWTdb.Close()
        ' ''    Catch ex As Exception
        ' ''        MsgBox("Can not open connection! ")
        ' ''    End Try
        ' ''    Next
        ' ''Else 'between 1 and 99 records...
        ' ''    sqlCodes = "(("
        ' ''    For i = 0 To numCodes - 2
        ' ''        sqlCodes = sqlCodes & "(WireTagCode.TagCode)= """ & CWTcode(i) & """ OR "
        ' ''    Next
        ' ''    sqlCodes = sqlCodes & " (WireTagCode.TagCode)= """ & CWTcode(CWTcode.Length - 1) & """))"
        ' ''    'Debug.Print(sqlCodes)
        ' ''    'Build the query string for extracting from the recovery database
        ' ''    sql = "SELECT * FROM WireTagCode WHERE " & sqlCodes
        ' ''    'Read in the data and fill the VB DataSet
        ' ''    Try
        ' ''        CWTdb.Open()
        ' ''        oledbAdapter = New OleDb.OleDbDataAdapter(sql, CWTdb)
        ' ''        oledbAdapter.Fill(dsWireTagCodemerge, "WireTagCode") 'Creates the local dataset of chosen recoveries
        ' ''        oledbAdapter.Dispose()
        ' ''        CWTdb.Close()
        ' ''    Catch ex As Exception
        ' ''        MsgBox("Can not open connection! ")
        ' ''    End Try
        ' ''End If

        ' ''sqlCodes = "(("
        'sqlCodes = ""


        'If numCodes = 1 Then
        '    sqlCodes = sqlCodes & "'TagCode'= '" & CWTcode(i) & "'"
        '    'Debug.Print(sqlCodes)
        'Else
        '    For i = 0 To numCodes - 2
        '        sqlCodes = sqlCodes & "'TagCode'= '" & CWTcode(i) & "' OR "
        '    Next
        '    sqlCodes = sqlCodes & " 'TagCode'= '" & CWTcode(CWTcode.Length - 1) & "'" '))"
        '    'Debug.Print(sqlCodes)
        'End If

        Dim Lim As Integer = dsWireTagCodemerge.Tables(0).Rows.Count - 1
        Dim Delete As Boolean
        For i = 0 To Lim 'Step -1
            Delete = True
            For j = 0 To numCodes - 1
                    If dsWireTagCodemerge.Tables(0)(i)("TagCode") = CWTcode(j) Then
                        Delete = False
                    End If
            Next
            If Delete = True Then
                dsWireTagCodemerge.Tables(0)(i).Delete()
            End If
        Next

        'dsWireTagCodemerge.GetDataSetSchema(dsWireTagCodedisplay)
        ''Build the query string for extracting from the recovery database
        'sql = "SELECT * FROM WireTagCode WHERE " & sqlCodes

        ''Read in the data and fill the VB DataSet
        'Try
        '    CWTdb.Open()
        '    oledbAdapter = New OleDb.OleDbDataAdapter(sql, CWTdb)
        '    oledbAdapter.Fill(dsWireTagCodemerge, "WireTagCode") 'Creates the local dataset of chosen recoveries
        '    oledbAdapter.Dispose()
        '    CWTdb.Close()

        'Catch ex As Exception
        '    MsgBox("Can not open connection! ")
        'End Try

        Dim Table As DataTable
        Dim FiltString As String
        Dim Dupes As New List(Of String)
        Table = dsWireTagCodemerge.Tables("WireTagCode")
        dsWireTagCodemerge.Clear() 'Now we can release the dataset...
        'Table = dsWireTagCodedisplay.Tables("WireTagCode").Select(

        'Determine which stocks in the selection have >1 tag codes for a particular BY
        For i = 0 To numCodes - 1
            FiltString = "[Stock] = '" & CWTStock(i) & "' AND [BroodYear] = " & CWTbrood(i)
            Dupes.Add(CWTStock(i) & "-" & CWTbrood(i) & "-" & Table.Compute("Count(TagCode)", FiltString))
            'Debug.Print(Dupes(i))
        Next
        'Debug.Print(Dupes.ToString)
        Dim Unique As IEnumerable(Of String) = Dupes.Distinct 'This is a list of what stocks/brood years have multiple tag codes associated with them
        'Debug.Print(Unique.ToString)
        'END STEP 1______________________________________________________________________________________________________________________

        '________________________________________________________________________________________________________________________________
        'Step 2, Now query the the FRAM-mapped table (FRAM_star_CWT) database table to make a summation of recoveries over all codes
        'for a given stock-age-timestep-fishery-runID combination

        Dim SupaStocks As Integer = Unique.Count 'This tells us how many superstocks there are
        If Dupes.Count >= 1 Then 'No restrictions here now due to need for within and between BY functionality...
            For i = 0 To SupaStocks - 1
                'Extract the Stock & BY details from FRAM_star_CWT for creation of new supercodefile
                Dim stkq As String = Microsoft.VisualBasic.Left(Unique(i).ToString, 3) 'Stock code
                Dim byq As Integer = Microsoft.VisualBasic.Mid(Unique(i).ToString, 5, 4) 'Brood year
                Dim CWToutwrtsupa As New StringBuilder() 'Stringbuilder for composite superstock CWT summary file
                'Dim stringcheese As String
                Dim dtCWTsupa As New DataSet

                'Connect to the database and fill a local dataset with the individual values
                sql = "SELECT FRAM_star_CWT.StockNum, FRAM_star_CWT.StockAbbrev, FRAM_star_CWT.StockName, FRAM_star_CWT.CWTCode, FRAM_star_CWT.RunTiming, FRAM_star_CWT.BroodYear, FRAM_star_CWT.Age," & _
                "FRAM_star_CWT.FisheryNum, FRAM_star_CWT.FisheryName, FRAM_star_CWT.TStepNum, FRAM_star_CWT.Notes, FRAM_star_CWT.TStepName, FRAM_star_CWT.EstdRecs, FRAM_star_CWT.nRecs" & _
                " FROM(FRAM_star_CWT) " & _
                " WHERE(((FRAM_star_CWT.RunID) = " & runID & ") And ((FRAM_star_CWT.StockAbbrev) = """ & stkq & """) And ((FRAM_star_CWT.BroodYear) = " & byq & "))"

                ''Connect to the database and fill a local dataset with the summed values
                'sql = "SELECT FRAM_star_CWT.StockNum, FRAM_star_CWT.StockAbbrev, FRAM_star_CWT.StockName, FRAM_star_CWT.RunTiming, FRAM_star_CWT.BroodYear, FRAM_star_CWT.Age," & _
                '"FRAM_star_CWT.FisheryNum, FRAM_star_CWT.FisheryName, FRAM_star_CWT.TStepNum, FRAM_star_CWT.Notes, FRAM_star_CWT.TStepName, Sum(FRAM_star_CWT.EstdRecs) AS SumOfEstdRecs, Sum(FRAM_star_CWT.nRecs)" & _
                '"AS SumOfnRecs " & _
                '"FROM(FRAM_star_CWT) " & _
                '"WHERE(((FRAM_star_CWT.RunID) = " & runID & ")) " & _
                '"GROUP BY FRAM_star_CWT.StockNum, FRAM_star_CWT.StockAbbrev, FRAM_star_CWT.StockName, FRAM_star_CWT.RunTiming, FRAM_star_CWT.BroodYear, FRAM_star_CWT.Age,  FRAM_star_CWT.Notes, FRAM_star_CWT.FisheryNum," & _
                '"FRAM_star_CWT.FisheryName, FRAM_star_CWT.TStepNum, FRAM_star_CWT.TStepName " & _
                '"HAVING(((FRAM_star_CWT.StockAbbrev) = """ & stkq & """) And ((FRAM_star_CWT.BroodYear) = " & byq & "))"

                Try
                    CWTdb.Open()
                    oledbAdapter = New OleDb.OleDbDataAdapter(sql, CWTdb)
                    oledbAdapter.Fill(dtCWTsupa, "FRAM_star_CWT")
                    oledbAdapter.Dispose()
                    CWTdb.Close()

                Catch ex As Exception
                    MsgBox("Can not open connection! ")
                End Try

                '-------------Step 2a-----------------
                'figure out the list of codes over which the within-year weighting application will occur and then create a list
                Dim cwtlist(1) As String
                Dim cwttemp As String
                Dim numtags As Integer
                Dim l As Integer
                Dim wty As Double
                Dim wtflag As Integer
                'Dim wtflagNote As String

                cwtlist(0) = dtCWTsupa.Tables(0)(0)("CWTCode") 'set the first one to the first value seen in the table
                l = 0
                For k = 1 To dtCWTsupa.Tables(0).Rows.Count - 1
                    cwttemp = dtCWTsupa.Tables(0)(k)("CWTCode")
                    If cwttemp <> cwtlist(l) Then
                        l += 1          'Final l+1 will also be length of stock list
                        Array.Resize(cwtlist, l + 1)
                        cwtlist(l) = dtCWTsupa.Tables(0)(k)("CWTCode")
                    End If
                Next
                numtags = l
                '----------------------------------------

                '-------------Step 2b-----------------
                'Inflate/deflate each stock/code's recoveries according to weights; default = 1.0 (no wt)
                Dim supaCode As String = "X" & Microsoft.VisualBasic.Right(byq, 2) & stkq
                Dim dtWINBYmergesubset As DataTable
                dtWINBYmergesubset = dtCWTsupa.Tables(0).Clone()
                Dim drWINBYmergesubset() As DataRow
                'Now do the within-year weighting math
                For l = 0 To numtags

                    If ck_winBYwt.Checked = False Then 'default = 1
                        wty = 1
                    Else
                        If (ck_winBYwt.Checked = True And dtWeights.Rows.Count = 0) Then
                            If DBwtWithinBYwarning = False Then
                                MessageBox.Show("You specified 'use within-brood DB weighting rules' but your DB is missing the FRAM_Weights table." & vbCrLf & "Unweigted within-BY merging (default) will occur.")
                                DBwtWithinBYwarning = True
                            End If
                            wty = 1 'no weighting or flagging provided for this stock/code, default method = 1 (no wting)
                            wtflag = 1
                            GoTo Here
                        End If

                        Dim exp As String
                        exp = "CWTcode = '" & cwtlist(l) & "'"
                        If (dtWeights.Select(exp).Count = 0) Then
                        'If (IsDBNull(dtWeights.Select(exp)) = True Or IsNothing(dtWeights.Select(exp)) = True) Then
                            wty = 1 'no weighting or flagging provided for this stock/code, default method = 1 (no wting)
                            wtflag = 1
                        ElseIf dtWeights.Select(exp)(0)("wnBYmeth") = 1 Then
                            wty = 1 'no weighting flag specified, default to 1.
                            wtflag = 1
                        Else
                            wty = dtWeights.Select(exp)(0)("wnBYwt") 'if 2 it's user specified, grab the weight from table
                            wtflag = 2
                        End If
                    End If
Here:
                    drWINBYmergesubset = dtCWTsupa.Tables(0).Select("CWTCode = '" & cwtlist(l) & "'")
                    For m = 0 To drWINBYmergesubset.Count - 1
                        drWINBYmergesubset(m)("EstdRecs") = drWINBYmergesubset(m)("EstdRecs") * wty
                        drWINBYmergesubset(m)("CWTCode") = supaCode
                        dtWINBYmergesubset.ImportRow(drWINBYmergesubset(m))
                    Next

                Next
                '----------------------------------------

                '-------------Step 2c-----------------
                'Sum within BY across codes and put in output datatable for updating
                Dim snum As Integer = dtCWTsupa.Tables(0)(0)("StockNum")
                Dim sname As String = dtCWTsupa.Tables(0)(0)("StockName")
                Dim rnum As Integer = dtCWTsupa.Tables(0)(0)("RunTiming")
                Dim bnum As Integer = dtCWTsupa.Tables(0)(0)("BroodYear")

                For f = 1 To FishNum
                    For t = 1 To TSNum
                        For a = MinAge To MaxAge
                            Dim bysum As Double
                            Dim byobs As Double
                            Dim criteria As String
                            criteria = "FisheryNum = " & f & " AND Age = " & a & " AND TStepNum = " & t
                            If IsDBNull(dtWINBYmergesubset.Compute("Sum(EstdRecs)", criteria)) = False Then
                                    bysum = dtWINBYmergesubset.Compute("Sum(EstdRecs)", criteria)
                                    byobs = dtWINBYmergesubset.Compute("Sum(nRecs)", criteria)
                                    Dim fname As String = dtWINBYmergesubset.Select("FisheryNum = " & f)(0)("FisheryName")
                                    Dim tname As String = dtWINBYmergesubset.Select("TStepNum = " & t)(0)("TStepName")
                                    dtCWTout.Tables("FRAMstarCWT").Rows.Add( _
                                        snum, stkq, sname, supaCode, rnum, _
                                        bnum, a, f, fname, t, _
                                        tname, bysum, byobs, DateTime.Now, runID, "Within BY merged")
                            End If
                        Next
                    Next
                Next

                'xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
                'DISABLED / DYSFUNCTIONAL OLD CWT *.txt WRITING CAPABILITIES
                'IF USE OF THIS IS DESIRED, NEED TO DISSECT dtCWTout.Tables(0) into code-by-code pieces...
                '-------------Step 2d-----------------
                'Create new *.CWT text file and add supacode to database
                'The supercode CWT Code format is currently "Xyynnn" where X is X, yy is brood year e.g. 05, and nnn is the stock code e.g. GAD 
                'Can reformat as desired
                'Header for CWT file

                'If wtflag = 2 Then
                '    wtflagNote = "User-specified within-BY weighting used, refer to Fb database for values associated with each code"
                'Else
                '    wtflagNote = "Unweighted within-BY merging of codes (default)"
                'End If

                'stringcheese = "*********************************************************************************************************" & vbCrLf & _
                '"***********************************FRAMBuilder 2.0 Mapping Outputs***************************************" & vbCrLf & _
                '"********************** " & "Created On " & Date.Now & " RunID " & runID & " *******************************" & vbCrLf & _
                '"*********************************************************************************************************" & vbCrLf & vbCrLf & _
                '"### " & wtflagNote & vbCrLf & vbCrLf & _
                '"Stock" & "," & "Code" & "," & "Brood" & "," & "Age" & "," & "Fishery" & "," & "TStep" & "," & "Estd Recs" & "," & "TStep" & "," & "Fishery" & "," & "TagsInHand" & "," & "Notes" 'Header for CWT file
                ''"Stock# " & dtCWTsupa.Tables(0)(j)("StockNum").ToString & " " & dtCWTsupa.Tables(0)(j)("StockName") & vbCrLf & vbCrLf & _
                'CWToutwrtsupa.AppendLine(stringcheese)
                ''Add rows of sums over each fishery-age-TS combo...
                'For j = 0 To dtCWTout.Tables(0).Rows.Count - 1
                '    Dim dat As String = dtCWTout.Tables(0)(j)("StockAbbrev").ToString
                '    stringcheese = dtCWTout.Tables(0)(j)("StockAbbrev") & "," & supaCode & "," & dtCWTout.Tables(0)(j)("BroodYear").ToString & "," & _
                '        dtCWTout.Tables(0)(j)("Age").ToString & "," & dtCWTout.Tables(0)(j)("FisheryNum").ToString & "," & _
                '        dtCWTout.Tables(0)(j)("TStepNum").ToString & "," & dtCWTout.Tables(0)(j)("EstdRecs").ToString & "," & _
                '        dtCWTout.Tables(0)(j)("TStepName").ToString & "," & dtCWTout.Tables(0)(j)("FisheryName").ToString & "," & _
                '        dtCWTout.Tables(0)(j)("nRecs").ToString & "," & dtCWTout.Tables(0)(j)("Notes")
                '    CWToutwrtsupa.AppendLine(stringcheese)
                '    'Dim Note As String
                '    'If IsDBNull(dtCWTsupa.Tables(0)(j)("Notes")) = True Then
                '    '    Note = ""
                '    'Elsessv
                '    '    Note = dtCWTsupa.Tables(0)(j)("Notes")
                '    'End If
                '    ''Populate the datatable for transfering to database
                '    'dtCWTout.Tables("FRAMstarCWT").Rows.Add( _
                '    '    dtCWTsupa.Tables(0)(j)("StockNum"), dtCWTsupa.Tables(0)(j)("StockAbbrev"), dtCWTsupa.Tables(0)(j)("StockName"), supaCode, dtCWTsupa.Tables(0)(j)("RunTiming"), _
                '    '    dtCWTsupa.Tables(0)(j)("BroodYear"), dtCWTsupa.Tables(0)(j)("Age"), dtCWTsupa.Tables(0)(j)("FisheryNum"), dtCWTsupa.Tables(0)(j)("FisheryName"), dtCWTsupa.Tables(0)(j)("TStepNum"), _
                '    '    dtCWTsupa.Tables(0)(j)("TStepName"), dtCWTsupa.Tables(0)(j)("SumOfEstdRecs"), dtCWTsupa.Tables(0)(j)("SumOfnRecs"), DateTime.Now, runID, Note)
                '    Using outfile As New StreamWriter(fpath & "\" & supaCode.ToString & ".CWT")
                '        outfile.Write(CWToutwrtsupa.ToString)
                '    End Using
                '    '-------------------------------------------------------------------------------------
                'Next
                ''----------------------------------------
                'xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx


            Next

                'Finally, Add the new records to the database table--this will not overwrite old records for the same code/stock if there are any.
                CWTdb.Open()
                sql = "SELECT * FROM FRAM_star_CWT"
                oledbAdapter = New OleDb.OleDbDataAdapter(sql, CWTdb)
                Dim oleCommander As New OleDb.OleDbCommandBuilder(oledbAdapter)
                oledbAdapter.Update(dtCWTout.Tables(0))
                CWTdb.Close()
                '----------------------------------------


        End If


    End Sub

    Public Sub BGworker_output_DoWork(sender As System.Object, e As System.ComponentModel.DoWorkEventArgs) Handles BGworker_output.DoWork



        '##############################################################################################
        'The guts of Fb 2.0 -- where individual recoveries are evaluated, mapped, and enumerated on a FRAM model fishery basis
        '##############################################################################################

        'Create a data table from the dataset for easier querying
        Dim code, stk, FRAMname As String 'Local variables for tidier subset selection, file writing, etc...
        Dim brood, runtiming, FRAMnum As Integer 'local variables for computing age (brood = broodyear, runtiming = run-timing group)
        Dim cwtsubset() As DataRow  'Subset for a given CWT code
        Dim FishMatch() As DataRow 'Row from FRAM_Fishery LookupTable
        Dim fram_match, agecalc, tscalc As Integer 'Variables for indexing the summation across a code
        'Dim cols As Integer = dsRecoveries.Tables("CWDBRecovery").Columns.Count 'Probably don't need anylonger, keep as example
        Dim fishname, stringier, stringiest As String
        runID = Round(Now().ToOADate, 4)

        Dim CWTdb As New OleDb.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & CWTdatabasename)
        Dim sql As String       'SQL Query text string
        Dim oledbAdapter As OleDb.OleDbDataAdapter
        dtLengthOut.Clear()
        dtCWTout.Tables(0).Rows.Clear() 'Clear the local data table of existing rows, clean start
        dtProcessOut.Tables(0).Rows.Clear()

        'A field that will be necessary for creating a master FRAM-to-RMIS lookup table.
        'dsRecoveries.Tables("CWDBRecovery").Columns.Add("finalFmap", GetType(Integer))

        '******************Evaluate all tag codes selected for tabulation/summarization
        For i = 0 To numCodes - 1

            'Collapse these stringbuilders into a single one once things settle out...
            'Dim CWToutwrt As New StringBuilder() 'Stringbuilder for main CWT summary file
            'Dim CWTbad As New StringBuilder() 'Stringbuilder for reject (unassigned) CWT recoveries
            'Dim CWTlog As New StringBuilder() 'Stringbuilder for reject (unassigned) CWT recoveries
            Dim note As String

            'CWToutwrt.Clear() 'Wipe out local summation table
            ReDim CWTout(MaxAge - MinAge + 1, FishNum, TSNum) 'This is the array for tallying the goods for the CWT file
            ReDim CWToutN(MaxAge - MinAge + 1, FishNum, TSNum) 'This is the array for tallying the goods for the CWT file

            'Dim streak As String    'string variable for updating label

            code = CWTcode(i)   'Prefix for files
            stk = CWTStock(i)   'Stock code for transfering to dataset
            brood = CWTbrood(i) 'brood  year for age calculations
            runtiming = CWTruntime(i) 'Run timing group for forcing escapements to time 3 as needed
            FRAMname = FRAMStockName(i) 'local variable for full name
            FRAMnum = FRAMStockNum(i) 'local variable for stock num

            'First, get the subset of recoveries associated with the first code in the list
            cwtsubset = dsRecoveries.Tables("CWDBRecovery").Select("TagCode = '" & code.ToString & "'")

            'Dim min As Integer = cwtsubset.GetLowerBound(0)
            'Dim max As Integer = cwtsubset.GetUpperBound(0)
            'Console.Write(min.ToString & "   " & max.ToString & vbCrLf)

            ''Console.WriteLine(cwtsubset) what's the selection look like?
            'For l = cwtsubset.GetLowerBound(0) To cwtsubset.GetUpperBound(0)
            '    Debug.Print(String.Join(",", cwtsubset(l).ItemArray.Select(Function(s) s.ToString).ToArray)) 'Shorthand notation for writing a single row to file
            'Next

            '******************Evaluate individual recoveries on a one-by-one basis, map them, and tabulate totals
            For l = cwtsubset.GetLowerBound(0) To cwtsubset.GetUpperBound(0)
                Dim RecId As String
                RecId = cwtsubset(l)("RecoveryId").ToString

                If RecId = "1998232" Then
                    RecId = RecId
                End If

                Dim AgeFlag As Boolean = False 'Flags for warning that age in length file is fudged to meet FRAM restrictions
                Dim TSFlag As Boolean = False 'Flags for warning that TS in length file is fudged to meet FRAM restrictions
                'Dim DetailString As String 'String for passing info to LengthWrite Sub

                stringier = ""
                stringiest = ""

                'Criteria for writing records to reject file
                'Will need to think of additional critera for writing to rejects file
                If IsDBNull(cwtsubset(l)("Fishery")) = True Then    'No CAS fishery specified for this record, can't match with a FRAM fishery

                    stringier = String.Join(",", cwtsubset(l).ItemArray.Select(Function(s) s.ToString).ToArray) 'Shorthand notation for writing a single row to file
                    'CWTbad.AppendLine(stringier)
                    'CWTlog.AppendLine("Record Rejected:   " & stringier)
                    cwtsubset(l)("finalFmap") = -1000 'flag for not mapped
                    'Write to DB log
                    note = "Record Rejected:  " & stringier
                    dtProcessOut.Tables(0).Rows.Add(runID, stk, code, RecId, note)

                Else


                    'Begin to process and map recovery
                    FishMatch = dsFRAMlkup.Tables("FRAM_Fishery").Select("CAS_Fishery = " & cwtsubset(l)("Fishery").ToString) 'Assigns the CAS fishery value for the recovery record to local variable

                    'If match found...
                    If IsDBNull(FishMatch(0)("FRAM_FishID_Mapped")) = False Then


                        '****###--Special Mapping Rules--###**********************************************************************
                        'The goal of this code is to further split the CAS-to-FRAM mappings to address the one-to-many CAS-to-FRAM relations
                        'See the OneNote documentation for additional details.


                        'Figure out what FRAM fishery the CAS Fishery associated with record l corresponds to
                        fram_match = FishMatch(0)("FRAM_FishID_Mapped") 'Grabs the associated FRAM Fishery Number
                        cwtsubset(l)("finalFmap") = fram_match 'pop this here first.

                        'Some additional rules for reassignment given one-to-many FRAM to CAS relationship

                        'First Rule: Splitting Troll fisheries into Treaty/Non-treaty Components, based on fishery type 10/15
                        If fram_match = 16 And cwtsubset(l)("CWDBFishery") = 15 Then 'If A3:4:4B troll and treaty troll code then split
                            fram_match = 17
                            cwtsubset(l)("finalFmap") = fram_match 'Rewire accordingly
                        ElseIf fram_match = 20 And cwtsubset(l)("CWDBFishery") = 15 Then 'If A2 troll and treaty troll code then split
                            fram_match = 21
                            cwtsubset(l)("finalFmap") = fram_match 'Rewire accordingly
                        End If

                        'Second Rule: Splitting CAS WASH COASTAL RIVERS NET (3035) into FRAM Grays, Willapa, and N WA Coast components
                        'NOTE: Still need to split Grays Hbr into T/NT equivalents
                        If cwtsubset(l)("Fishery") = 3035 Then
                            If cwtsubset(l)("RecoverySite").ToString.Contains("3M21902") = True Then
                        fram_match = 25  'Assign to Willapa, corrected to 25 from 23 5/5/2014
                                cwtsubset(l)("finalFmap") = fram_match 'Rewire accordingly
                            ElseIf cwtsubset(l)("RecoverySite").ToString.Contains("3M21802") = True Then
                        fram_match = 23 'Assign to NT Grays Harbor Net, corrected to 23 from 24 5/5/2014
                                cwtsubset(l)("finalFmap") = fram_match 'Rewire accordingly
                            Else
                                fram_match = 19 'Assign to N WA Coastal Net
                                cwtsubset(l)("finalFmap") = fram_match 'Rewire accordingly
                                'Also flag it in the log file so that the 'other to N WA Coast' can be verified by user
                                stringier = String.Join(",", cwtsubset(l).ItemArray.Select(Function(s) s.ToString).ToArray) 'Shorthand notation for writing a single row to file
                                'CWTlog.AppendLine("Verify rec mapped to (default) N WA Coastal Net (#19),   " & stringier)
                                'Write to DB log
                                note = "Verify rec mapped to (default) N WA Coastal Net (#19),   " & stringier
                                dtProcessOut.Tables(0).Rows.Add(runID, stk, code, RecId, note)
                            End If
                        End If

                        'Third Rule: Splitting CAS GEO ST SPORT (2015) into FRAM N & S GS SPORT and JDF SPORT; ALSO, Get JOHN STR SPORT OUT OF BC OUTSIDE SPT
                        Dim TrimmedTo As String
                        If cwtsubset(l)("Fishery") = 2015 Then
                            'TrimmedTo = Microsoft.VisualBasic.Right(Microsoft.VisualBasic.Left(cwtsubset(l)("RecoverySite").ToString, 9), 3)
                            TrimmedTo = Microsoft.VisualBasic.Right(Microsoft.VisualBasic.Left(cwtsubset(l)("RecoverySite").ToString, 9), 3) 'NEW way, due to new Canadian Location Codes, June 2015
                            If TrimmedTo = "013" Or TrimmedTo = "014" Or TrimmedTo = "015" Or TrimmedTo = "016" Then 'Canadian Stat Areas 13-16
                                fram_match = 13  'Assign to North Georgia Strait Sport
                                cwtsubset(l)("finalFmap") = fram_match 'Rewire accordingly
                            ElseIf (TrimmedTo = "017 " Or TrimmedTo = "018" Or TrimmedTo = "19A" Or TrimmedTo = "028" Or TrimmedTo = "029" Or cwtsubset(l)("RecoverySite").ToString.Contains("19A") Or TrimmedTo.Contains("17")) Then 'Canadian Stat Areas Canadian Stat Areas 17-18, 19A, 28-29
                                fram_match = 14  'Assign to South Georgia Strait Sport
                                cwtsubset(l)("finalFmap") = fram_match 'Rewire accordingly
                            ElseIf TrimmedTo = "19B" Or TrimmedTo = "020" Or cwtsubset(l)("RecoverySite").ToString.Contains("19B") Then
                                fram_match = 15 'Assign to BC JDF Sport
                                cwtsubset(l)("finalFmap") = fram_match 'Rewire accordingly
                            Else
                                'Leave it in the default original mapping and write note to log to verify
                                stringier = String.Join(",", cwtsubset(l).ItemArray.Select(Function(s) s.ToString).ToArray) 'Shorthand notation for writing a single row to file
                                'CWTlog.AppendLine("Verify rec mapped to (default) N Georgia St Spt (#15),   " & stringier)
                                note = "Verify rec mapped to (default) N Georgia St Spt (#15),   " & stringier
                                dtProcessOut.Tables(0).Rows.Add(runID, stk, code, RecId, note)
                            End If
                        ElseIf cwtsubset(l)("Fishery") = 2016 Then 'If BC Outside Sport in Area 12, Make it into N GS SPORT
                            TrimmedTo = Microsoft.VisualBasic.Right(Microsoft.VisualBasic.Left(cwtsubset(l)("RecoverySite").ToString, 9), 3)
                            If TrimmedTo = "12 " Then
                                fram_match = 13  'Assign to North Georgia Strait Sport
                                cwtsubset(l)("finalFmap") = fram_match 'Rewire accordingly
                            End If
                        End If

                        'Fourth Rule: Splitting CAS WA SPS NET (3013) into FRAM Area 13A (T+NT) and SPS Net (13, 13D-K) Fisheries
                        'Also deals with Chambers 13C Catch by shifting it to FW Net (consistent with TAMM treatment of fishery and actual
                        'Fishery layout (i.e., 13C is east of RR trestle at mouth of stream, anyone caught there is probably staying there, thus a stray)
                        'NOTE: Still need to split all fisheries into T/NT equivalents
                        If cwtsubset(l)("Fishery") = 3013 Then
                            If cwtsubset(l)("RecoverySite").ToString.Contains("C") = True Then
                                fram_match = 73  'Assign 13C to Freshwater Net
                                cwtsubset(l)("finalFmap") = fram_match 'Rewire accordingly
                            ElseIf cwtsubset(l)("RecoverySite").ToString.Contains("A") = True Then
                                fram_match = 70 'Assign All 13A Catch to NT (will split via other means later)
                                cwtsubset(l)("finalFmap") = fram_match 'Rewire accordingly
                                'NOTE: Still need to split into T/NT equivalents
                            Else
                                fram_match = 68 'Assign everything else NT SPS Net (i.e., Gen 13, 13B, 13D-K)
                                cwtsubset(l)("finalFmap") = fram_match 'Rewire accordingly
                                'Also if it's not 13D or 13F (only locs fished in 2007-11 FY), flag it in the log file so that the 'SPS Net' assignment can be inspected by user
                                If cwtsubset(l)("RecoverySite").ToString.Contains("D") = False Or cwtsubset(l)("RecoverySite").ToString.Contains("F") = False Then
                                    cwtsubset(l)("finalFmap") = fram_match 'Rewire accordingly
                                    stringier = String.Join(",", cwtsubset(l).ItemArray.Select(Function(s) s.ToString).ToArray) 'Shorthand notation for writing a single row to file
                                    'CWTlog.AppendLine("Verify rec mapped to (default) SPS Net (#68),   " & stringier)
                                    note = "Verify rec mapped to (default) SPS Net (#68),   " & stringier
                                    dtProcessOut.Tables(0).Rows.Add(runID, stk, code, RecId, note)
                                End If
                            End If
                        End If

                        'Fifth Rule: Splitting CAS WA PS AREAS 10 AND 11 NET Into FRAM 10A Tr, 10E Tr, and 10:11 Tr/NT Net fisheries
                        'NOTE: Still need to split 10:11 Net into Tr/NT equivalents
                        If cwtsubset(l)("Fishery") = 3012 Then
                            If cwtsubset(l)("RecoverySite").ToString.Contains("10  A") = True Then
                                fram_match = 61  'Assign To Treaty 10A
                                cwtsubset(l)("finalFmap") = fram_match 'Rewire accordingly
                            ElseIf cwtsubset(l)("RecoverySite").ToString.Contains("10  E") = True Then
                                fram_match = 63 'Assign To Treaty 10E
                                cwtsubset(l)("finalFmap") = fram_match 'Rewire accordingly
                                'NOTE: Still need to split into T/NT equivalents
                            Else
                                fram_match = 58 'Assign everything else NT 10:11 Net, but also write to log for verification
                                cwtsubset(l)("finalFmap") = fram_match 'Rewire accordingly
                                stringier = String.Join(",", cwtsubset(l).ItemArray.Select(Function(s) s.ToString).ToArray) 'Shorthand notation for writing a single row to file
                                'CWTlog.AppendLine("Verify rec mapped to (default) 10:11 Net (#58),   " & stringier)
                                note = "Verify rec mapped to (default) 10:11 Net (#58),   " & stringier
                                dtProcessOut.Tables(0).Rows.Add(runID, stk, code, RecId, note)
                            End If
                        End If

                        'Sixth Rule: Dealing with 10A (EBay) and 10E (Stinklair) vs. Area 10 during summer period
                        'Note: this rule has possible imperfections due to 10A, 10E, 10 miscoding on RMIS, although
                        'the fraction of erroneous recoveries is likely minimal.
                        If cwtsubset(l)("Fishery") = 3025 And (Month(cwtsubset(l)("RecoveryDate")) >= 7 And Month(cwtsubset(l)("RecoveryDate")) <= 9) Then
                            If cwtsubset(l)("RecoverySite").ToString.Contains("872155" Or "872585") = True Then
                                fram_match = 60  'Assign To Elliott Bay
                                cwtsubset(l)("finalFmap") = fram_match 'Rewire accordingly
                            ElseIf cwtsubset(l)("RecoverySite").ToString.Contains("872477" Or "872286" Or "872408") = True Then
                                fram_match = 62 'Assign To Sinclair Inlet
                                cwtsubset(l)("finalFmap") = fram_match 'Rewire accordingly
                            Else
                                fram_match = 56 'Assign everything else General Area 10
                                cwtsubset(l)("finalFmap") = fram_match 'Rewire accordingly
                            End If
                        End If

                        'Seventh Rule: Moving 8D Sport out of 8-1/8-2 Sport during Tulalip Bay management
                        If cwtsubset(l)("Fishery") = 3023 And (Month(cwtsubset(l)("RecoveryDate")) >= 6 And Month(cwtsubset(l)("RecoveryDate")) <= 9) Then
                            fram_match = 48 'Move recoveries into 8-D if they were an 8-2 Rec during months of Jun-Sept (8-2 closed, 8D open)
                            cwtsubset(l)("finalFmap") = fram_match 'Rewire accordingly
                            'But also write to log for inspection
                            stringier = String.Join(",", cwtsubset(l).ItemArray.Select(Function(s) s.ToString).ToArray) 'Shorthand notation for writing a single row to file
                            'CWTlog.AppendLine("Verify rec mapped to 8D Sport (#48) from 8-1/8-2 Sport,   " & stringier)
                                note = "Verify rec mapped to 8D Sport (#48) from 8-1/8-2 Sport,   " & stringier
                                dtProcessOut.Tables(0).Rows.Add(runID, stk, code, RecId, note)
                        End If

                        'Eighth Rule: Moving 8D Net Out of 8A Net
                        'NOTE: Still need to split 8A/8D Net into Tr/NT equivalents
                        If cwtsubset(l)("Fishery") = 3011 And cwtsubset(l)("RecoverySite").ToString.Contains("8  D") = True Then
                            fram_match = 51 'Move Recoveries to NT 8D and deal with T/NT splitting later
                            cwtsubset(l)("finalFmap") = fram_match 'Rewire accordingly
                            'But also write to log for inspection
                            stringier = String.Join(",", cwtsubset(l).ItemArray.Select(Function(s) s.ToString).ToArray) 'Shorthand notation for writing a single row to file
                            'CWTlog.AppendLine("Verify rec mapped to 8D Commercial (#51) from 8A,   " & stringier)
                                note = "Verify rec mapped to 8D Commercial (#51) from 8A,   " & stringier
                                dtProcessOut.Tables(0).Rows.Add(runID, stk, code, RecId, note)
                        End If

                        'Ninth Rule: Separating KMZ From California Troll so that it can be merged with OR KMZ Troll into single FRAM KMZ Troll Fishery
                        If cwtsubset(l)("Fishery") = 3034 And cwtsubset(l)("RecoverySite").ToString.Contains("OBHJ") = True Then
                            'At present the only troll recovery code discernable as within KMZ is Oregon Border-Humboldt Jetty, eval as needed
                            fram_match = 32 'Move Recoveries to KMZ Troll, leave others in Cali Sport
                            cwtsubset(l)("finalFmap") = fram_match 'Rewire accordingly
                            'But also write to log for inspection
                            stringier = String.Join(",", cwtsubset(l).ItemArray.Select(Function(s) s.ToString).ToArray) 'Shorthand notation for writing a single row to file
                            'CWTlog.AppendLine("Verify rec mapped to KMZ Troll (#32) from CA Troll,   " & stringier)
                                note = "Verify rec mapped to KMZ Troll (#32) from CA Troll,   " & stringier
                                dtProcessOut.Tables(0).Rows.Add(runID, stk, code, RecId, note)
                        End If

                        'Tenth Rule: Separating KMZ From California Sport so that it can be merged with OR KMZ Sport into single FRAM KMZ Sport Fishery
                        If cwtsubset(l)("Fishery") = 3043 And cwtsubset(l)("RecoverySite").ToString.Contains("BGCB") = True Then
                            'At present the only sport recovery code discernable is Big Lagoon to Centerville Beach, eval as needed
                            fram_match = 33 'Move Recoveries to KMZ Sport, leave others in Cali Sport
                            cwtsubset(l)("finalFmap") = fram_match 'Rewire accordingly
                            'But also write to log for inspection
                            stringier = String.Join(",", cwtsubset(l).ItemArray.Select(Function(s) s.ToString).ToArray) 'Shorthand notation for writing a single row to file
                            'CWTlog.AppendLine("Verify rec mapped to KMZ Sport (#33) from CA Sport,   " & stringier)
                                note = "Verify rec mapped to KMZ Sport (#33) from CA Sport,   " & stringier
                                dtProcessOut.Tables(0).Rows.Add(runID, stk, code, RecId, note)
                        End If

                        'Eleventh Rule: Moving 12H (Hoodsport) to FW Net, consistent with the TAMM treatment of the fishery
                        If cwtsubset(l)("Fishery") = 3014 And cwtsubset(l)("RecoverySite").ToString.Contains("12  H") = True Then
                            'At present the only sport recovery code discernable is Big Lagoon to Centerville Beach, eval as needed
                            cwtsubset(l)("finalFmap") = fram_match 'Rewire accordingly
                            fram_match = 73 'Move Recoveries to KMZ Sport, leave others in Cali Sport
                            'But also write to log for inspection
                            stringier = String.Join(",", cwtsubset(l).ItemArray.Select(Function(s) s.ToString).ToArray) 'Shorthand notation for writing a single row to file
                            'CWTlog.AppendLine("Verify rec mapped to Freshwater Net (#73) from 12 H Net,   " & stringier)
                                note = "Verify rec mapped to Freshwater Net (#73) from 12 H Net,   " & stringier
                                dtProcessOut.Tables(0).Rows.Add(runID, stk, code, RecId, note)
                        End If

                        'Twelfth Rule: moving incorrectly mapped Area 1 Troll recoveries into the correct fishery
                        If (cwtsubset(l)("Fishery") = 3030 And (cwtsubset(l)("RecoverySite").ToString = "5M2220202O0202  10" Or cwtsubset(l)("RecoverySite").ToString = "5M2221002O1002  10")) Then
                            fram_match = 26 'Move Recoveries to KMZ Sport, leave others in Cali Sport
                            cwtsubset(l)("finalFmap") = fram_match 'Rewire accordingly
                            stringier = String.Join(",", cwtsubset(l).ItemArray.Select(Function(s) s.ToString).ToArray) 'Shorthand notation for writing a single row to file
                            'CWTlog.AppendLine("Verify rec mapped to Freshwater Net (#73) from 12 H Net,   " & stringier)
                                note = "OR Area 3 Troll Rec moved to Area 1 Troll,   " & stringier
                                dtProcessOut.Tables(0).Rows.Add(runID, stk, code, RecId, note)
                        End If

                        'Thirteenth Rule: Separating Bellingham Bay Net Fishery Recoveries into their respective Treaty/Non-treaty components
                        'Note that this requires some pre-processing of data and manual manipulation of recoveries within the CAS database in order for it to work
                        'Specifically, you need to (1) add a field called 'Tr_NT' to the 'CWDBRecovery' table, if it hasn't been done already
                        '(2) populate it with 'Tr' and 'NT' values based on the Gear Codes which aren't in CAS (you must pair with raw RMIS recovery data)
                        'and (3) duplicate within the database any 'mixed' (indistinguishable Tr and NT recs) recoveries and 
                        'and assign a fraction to each NT/Tr fisher types (e.g., using catch proportions for that run year). It's a kludge, but gets it done
                        If (cwtsubset(l)("Fishery") = 3008) Then
                            If cwtsubset(l)("Tr_NT").ToString = "Tr" Then
                                fram_match = 40 'Assign to Treaty Bham Bay
                                cwtsubset(l)("finalFmap") = fram_match 'Rewire accordingly
                            ElseIf cwtsubset(l)("Tr_NT").ToString = "NT" Then
                                fram_match = 39 'Assign to Non-Treaty Bham Bay
                                cwtsubset(l)("finalFmap") = fram_match 'Rewire accordingly
                            End If
                        End If



                        '!!!! Angelika temporary fix, treat all FW recs (FW net & sport, esc., B10, Col R net as escapement
                        If (fram_match = 28 Or fram_match = 29 Or fram_match = 72 Or fram_match = 73 Or fram_match = 74 Or fram_match = 75) Then
                            fram_match = 74
                            cwtsubset(l)("finalFmap") = fram_match 'Rewire accordingly
                        End If



                        '****###--End Special Mapping Rules--###******************************************************************

                        ''A little diddy to break on certain recs to test the decrementing, shoving of recs, ageing, etc.
                        'If RecId = "H3926" Then
                        '    Dim Word = "Word"
                        'End If

                        '***Time Step Assignment**********************************************************************
                        'This includes a step for fudging su/fa escapement recoveries occuring outside of TS3 into TS3
                        'will need to update for Col R Summers and Others (?)
                        Dim tscalcFL As Integer 'For R file, real age to store.
                        tscalc = Month(cwtsubset(l)("RecoveryDate"))
                        If tscalc <= 4 Or tscalc >= 10 Then
                            tscalc = 1
                        ElseIf tscalc = 5 Or tscalc = 6 Then
                            tscalc = 2
                        Else
                            tscalc = 3
                        End If
                        tscalcFL = tscalc

                        'Shift timestep 1 or 2 escapement, fwnet, Col R net, Buoy 10, and fw sport recoveries into timestep 2 or 3 as needed
                        'For TS 1 recoveries and for stocks allowed to mature in timesteps other than 3
                        If ((fram_match >= 72 And fram_match <= 75) Or fram_match = 29 Or fram_match = 28) And (tscalc = 1 Or tscalc = 2) Then

                            'Maturation is allowed in Times 2 and 3 for North Puget Sound Spring/Summer Stocks, and Col R Summers
                            If ((FRAMnum >= 3 And FRAMnum <= 12) Or (FRAMnum = 45 Or FRAMnum = 46)) Then
                                If tscalc = 1 Then 'Let 'em be in TS2/TS3 if that's where they fell out, but shift otherwise
                                    If (Month(cwtsubset(l)("RecoveryDate")) >= 10 Or Month(cwtsubset(l)("RecoveryDate")) < 2) Then 'Oct+ Recs assume to be from TS3
                                        stringiest = "Sp/Su Run FW Recovery in TS " & tscalc & " for rec ID# " & cwtsubset(l)("RecoveryId").ToString & " Assigned to TS 3"
                                        'CWTlog.AppendLine(stringiest)
                                        note = stringiest
                                        dtProcessOut.Tables(0).Rows.Add(runID, stk, code, RecId, note)
                                        tscalc = 3 'shifted to TS3
                                        TSFlag = True 'For output file and to trigger alternative age calcs below
                                    Else
                                        stringiest = "Sp/Su Run FW Recovery in TS " & tscalc & " for rec ID# " & cwtsubset(l)("RecoveryId").ToString & " Forced to TS 2"
                                        'CWTlog.AppendLine(stringiest)
                                        note = stringiest
                                        dtProcessOut.Tables(0).Rows.Add(runID, stk, code, RecId, note)
                                        tscalc = 2 'Assume the rare Jan-Apr recovery is mature in new year during May-Jun TS
                                        TSFlag = True 'For output file and to trigger alternative age calcs below
                                    End If
                                End If
                            ElseIf (tscalc = 2 And (FRAMnum < 50 Or FRAMnum > 52)) Then 'Do this for other stocks 'cept Willy/CKL
                                    stringiest = "Su/Fa Run FW Recovery in TS " & tscalc & " for rec ID# " & cwtsubset(l)("RecoveryId").ToString & " Forced to TS 3"
                                    'CWTlog.AppendLine(stringiest)
                                    note = stringiest
                                    dtProcessOut.Tables(0).Rows.Add(runID, stk, code, RecId, note)
                                    tscalc = 3
                                    TSFlag = True 'For output file and to trigger alternative age calcs below
                            ElseIf (tscalc = 1 And (FRAMnum < 50 Or FRAMnum > 52)) Then 'Do this for other stocks 'cept Willy/CKL
                                    stringiest = "Su/Fa Run FW Recovery in TS " & tscalc & " for rec ID# " & cwtsubset(l)("RecoveryId").ToString & " Forced to TS 3"
                                    'CWTlog.AppendLine(stringiest)
                                    note = stringiest
                                    dtProcessOut.Tables(0).Rows.Add(runID, stk, code, RecId, note)
                                    tscalc = 3
                                    TSFlag = True 'For output file and to trigger alternative age calcs below

                            End If
                        End If


                        '***Time Step Assignment**********************************************************************


                        '***Age Calculations**********************************************************************
                        'First, determine whether or not the recovery occurred on or after Oct 1, age accordingly
                        Dim agecalcFL As Integer 'For R file, real age to store.
                        agecalc = cwtsubset(l)("RunYear") - brood
                        agecalcFL = agecalc 'Keep this as calendar year age for VBGF output file

                        If Month(cwtsubset(l)("RecoveryDate")) >= 10 Then
                            If TSFlag = False Then 'You don't get to have a birthday if you're a S/F stock and have been shoved to maturation time step
                                agecalc = cwtsubset(l)("RunYear") - brood + 1
                            End If
                        'ElseIf Month(cwtsubset(l)("RecoveryDate")) <= 4 Then
                        End If

                        'Special Cowlitz and Willamette ageing & FW TS assignment
                        If (FRAMnum = 50 Or FRAMnum = 52) Then
                            agecalc = agecalc - 1 'First, decrement by 1 year to get OA on same page
                            'Now, if it's a FW rec, make sure it's aged properly
                            If ((fram_match >= 72 And fram_match <= 75) Or fram_match = 29 Or fram_match = 28) Then
                                agecalc = Year(cwtsubset(l)("RecoveryDate")) - brood - 1 'Age computed from CY b/n Jan and Dec is good FW convention
                                tscalc = 1 'All fw recs of CKLs and Willies should be assigned to TS1 for maturation purposes    
                                stringiest = "Will/CKL FW Recovery ID " & cwtsubset(l)("RecoveryId").ToString & " Computed as Age " & agecalc.ToString & " placed into TS " & tscalc.ToString
                                note = stringiest
                                dtProcessOut.Tables(0).Rows.Add(runID, stk, code, RecId, note)
                            End If
                        End If

                        'Second, determine whether or not the computed ages exceed the 2 to 5 limits of FRAM model stock age limits (2-5)
                        If agecalc < MinAge Then
                            stringiest = "Recovery ID " & cwtsubset(l)("RecoveryId").ToString & " Computed as Age " & agecalc.ToString & " Forced to " & MinAge.ToString
                            agecalc = MinAge
                            AgeFlag = True
                            'CWTlog.AppendLine(stringiest)
                                note = stringiest
                                dtProcessOut.Tables(0).Rows.Add(runID, stk, code, RecId, note)
                        End If
                        If agecalc > MaxAge Then
                            stringiest = "Recovery ID " & cwtsubset(l)("RecoveryId").ToString & " Computed as Age " & agecalc.ToString & " Forced to " & MaxAge.ToString
                            agecalc = MaxAge
                            AgeFlag = True
                            'CWTlog.AppendLine(stringiest)
                                note = stringiest
                                dtProcessOut.Tables(0).Rows.Add(runID, stk, code, RecId, note)
                        End If
                        '***Age Calculations**********************************************************************



                        '%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                        'ADDITIONAL SPECIAL MAPPING/INVETORYING RULES FOR ASSESSING POSSIBILITY OF NEW FISHERIES
                        If ck_draftfish.Checked = True Then

                            '1.  New Breakouts for Tr/NT Troll in 3 and 4
                            If cwtsubset(l)("Fishery") = 3003 Then
                                If cwtsubset(l)("CWDBFishery") = 15 Then
                                    CWTout(agecalc - 2, 77 - 1, tscalc - 1) += cwtsubset(l)("AdjustedEstimatedNumber")
                                    CWToutN(agecalc - 2, 77 - 1, tscalc - 1) += 1
                                Else
                                    CWTout(agecalc - 2, 76 - 1, tscalc - 1) += cwtsubset(l)("AdjustedEstimatedNumber")
                                    CWToutN(agecalc - 2, 76 - 1, tscalc - 1) += 1
                                End If
                            End If
                            If cwtsubset(l)("Fishery") = 3004 Then
                                If cwtsubset(l)("CWDBFishery") = 15 Then
                                    CWTout(agecalc - 2, 79 - 1, tscalc - 1) += cwtsubset(l)("AdjustedEstimatedNumber")
                                    CWToutN(agecalc - 2, 79 - 1, tscalc - 1) += 1
                                Else
                                    CWTout(agecalc - 2, 78 - 1, tscalc - 1) += cwtsubset(l)("AdjustedEstimatedNumber")
                                    CWToutN(agecalc - 2, 78 - 1, tscalc - 1) += 1
                                End If
                            End If

                            '2.  New ISBM (Inside) and AABM (QCI) Components of NBC Sport
                            If cwtsubset(l)("Fishery") = 2016 Then
                                If cwtsubset(l)("RecoverySite").ToString.Contains("3MN25003") = True _
                                    Or cwtsubset(l)("RecoverySite").ToString.Contains("3MN25004") = True _
                                    Or cwtsubset(l)("RecoverySite").ToString.Contains("3MN25005") = True Then
                                    CWTout(agecalc - 2, 81 - 1, tscalc - 1) += cwtsubset(l)("AdjustedEstimatedNumber")
                                    CWToutN(agecalc - 2, 81 - 1, tscalc - 1) += 1
                                Else
                                    CWTout(agecalc - 2, 80 - 1, tscalc - 1) += cwtsubset(l)("AdjustedEstimatedNumber")
                                    CWToutN(agecalc - 2, 80 - 1, tscalc - 1) += 1
                                End If
                            End If
                        End If
                        '%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

                        '*****************************************************************************************
                        ' Begin code for processing length data and filtering out sublegal recoveries
                        '*****************************************************************************************
                        Dim SublegExclude As Boolean = False 'Variable to identify sublegal recoveries that should be excluded

                        If ((fram_match < 74) And (IsDBNull(cwtsubset(l)("LengthCode")) = False)) Then
                            'Some preparations for things to chuck into the length file subroutine
                            If (cwtsubset(l)("LengthCode") = 0 Or cwtsubset(l)("LengthCode") = 1 Or cwtsubset(l)("LengthCode") = 3) Then
                                ''Pass info to length subroutine so that it can be stored..
                                'DetailString = "INSERT INTO FRAM_GrowthData ([RecoveryId],[RunID],[StockNum],[StockAbbrev],[StockName],[CWTCode],[RunTiming],[RunYear],[BroodYear],[Age],[FisheryNum],[FisheryName],[TStepNum],[TStepName],[RecoverySite],[RecoveryDate],[SizeLimit],[ForkLength],[Convert],[MeasMeth],[AgeFlag],[TSFlag],[Comments]) VALUES ("
                                'fishname = dsFRAMlist.Tables(0).Select()(fram_match - 1)("FisheryName").ToString
                                'DetailString = DetailString & "'" & RecId & "', " & runID & ", " & FRAMnum & ", '" & stk & "', '" & FRAMname & "', '"
                                'DetailString = DetailString & code & "', " & runtiming & ", " & cwtsubset(l)("RunYear") & ", " & brood & ", "
                                'DetailString = DetailString & agecalc & ", " & fram_match & ", '" & fishname & "', "
                                'DetailString = DetailString & tscalc & ", '" & TSNames(tscalc - 1) & "', '" & cwtsubset(l)("RecoverySite") & "', "
                                'DetailString = DetailString & "#" & cwtsubset(l)("RecoveryDate") & "#"

                                Dim FL As Double
                                Dim FLwarn, MeasMeth As String
                                Dim FLconv As Boolean = False 'Booleans indicating conversion occurred
                                Dim Limit, Length As Double 'Actual size limit in the fishery/time step when/where the CWT was recovered
                                Dim expy As String = "(FishingYear = 2010) AND (FisheryID = " & fram_match & ") AND (TimeStep = " & tscalc & ")"
                                Dim LengthMeth As Integer = cwtsubset(l)("LengthCode")
                                Dim agedays As Integer
                                Dim reldate As DateTime
                                Dim agemonths As Integer
                                Dim mark, markrel, fishtype As String
                                Limit = 0
                                If fram_match < 73 Then
                                    Limit = dtSizeLimits.Select(expy)(0)("MinimumSize")
                                End If
                                Length = cwtsubset(l)("Length")
                                MeasMeth = ""
                                If IsNothing(Limit) = True Then
                                    Limit = 0 'Default 0 mm limit in absence of a limit
                                End If
                                'Now do some calculations...
                                If LengthMeth = 0 Then 'Fork Length, use it as is
                                    FL = Length
                                    MeasMeth = "FL"
                                End If
                                If LengthMeth = 1 Then 'Mid-Eye to Fork (SEAK/ADFG primarily), convert to FL
                                    FL = 1.101 * Length - 15.878 'MEF to SNF conversion from Pahlke ADFG Report 89-02
                                    FLconv = True
                                    MeasMeth = "MEF"
                                End If
                                If LengthMeth = 3 Then 'Total length, convert to FL
                                    If Length < 720 Then
                                        FL = 0.957 * Length - 0.979 'Conrad & Gutman (1996) piecewise regression conversions of TL to FL
                                    Else
                                        FL = 0.969 * Length - 1.442
                                    End If
                                    FLconv = True
                                    MeasMeth = "TL"
                                End If

                                FLwarn = ""
                                'Add some screening warnings
                                If FL > 1300 Or FL < 100 Then
                                    FLwarn = "*** Unusually large or small"
                                End If
                                If FL < Limit Then
                                    FLwarn = FLwarn & "*** FL lower than limit"
                                End If
                                If FL < (Limit - 18) And (fram_match < 30 Or fram_match > 35) Then
                                    SublegExclude = True
                                End If

                                If ck_LengthOnly.Checked = True Or ck_LengthToo.Checked = True Then
                                    reldate = dsWireTagCodemerge.Tables(0).Select("TagCode = '" & code & "'")(0)("FirstReleaseDate")
                                    agedays = (cwtsubset(l)("RecoveryDate") - dsWireTagCodemerge.Tables(0).Select("TagCode = '" & code & "'")(0)("FirstReleaseDate")).Days
                                    Dim myDateTime2 As New DateTime(dsWireTagCodemerge.Tables(0).Select("TagCode = '" & code & "'")(0)("BroodYear"), 10, 1)
                                    agemonths = DateDiff(DateInterval.Month, myDateTime2, cwtsubset(l)("RecoveryDate"))

                                    mark = dtMarkType.Select("Code =" & cwtsubset(l)("RecordedMark"))(0)("Description")
                                    markrel = dsWireTagCodemerge.Tables(0).Select("TagCode = '" & code & "'")(0)("CWTMark1")
                                    markrel = dtMarkType.Select("Code =" & markrel)(0)("Description")
                                    fishtype = "Preterm"
                                    If dsFRAMlist.Tables(0).Select("FishID = " & fram_match)(0)(tscalcFL + 2) = True Then
                                        fishtype = "Term"
                                    End If

                                    dtLengthOut.Tables(0).Rows.Add(RecId, runID, FRAMnum, stk, FRAMname, code, runtiming, cwtsubset(l)("RunYear"), brood, agecalcFL, fram_match, fishname, tscalcFL, _
                                        TSNames(tscalcFL - 1), cwtsubset(l)("RecoverySite"), cwtsubset(l)("RecoveryDate"), Limit, FL, _
                                        reldate, agedays, agemonths, mark, markrel, fishtype, FLconv, MeasMeth, AgeFlag, TSFlag, FLwarn)
                                End If
                            End If
                        End If


                        'Console.Write("Age = " & agecalc & " Fish = " & fram_match & " Time step = " & tscalc & " CWT out N = " & cwtsubset(l)("AdjustedEstimatedNumber").ToString & vbCrLf)

                        '*****************************************************************************************
                        'End code for processing length data and filtering out sublegal recoveries
                        '*****************************************************************************************


                        '*****************************************************************************************
                        'Begin storage and summation
                        '*****************************************************************************************
                        'Given the fishery, age, TS combination, store the associated estimated value of recoveries
                        If SublegExclude = False Then
                            CWTout(agecalc - 2, fram_match - 1, tscalc - 1) += cwtsubset(l)("AdjustedEstimatedNumber")

                            'Track number of records to approximate tags in hand
                            If cwtsubset(l)("AdjustedEstimatedNumber") > 0 Then
                                CWToutN(agecalc - 2, fram_match - 1, tscalc - 1) += 1
                            End If
                        Else 'If SublegExclude = True...
                            stringier = ""
                            stringier = String.Join(",", cwtsubset(l).ItemArray.Select(Function(s) s.ToString).ToArray) 'Shorthand notation for writing a single row to file
                            'CWTbad.AppendLine(stringier)
                            'CWTlog.AppendLine("Record Rejected:   " & stringier)

                            note = "Record Rejected - Sublegal Recovery:  " & stringier
                            dtProcessOut.Tables(0).Rows.Add(runID, stk, code, RecId, note)
                        End If

                        '*****************************************************************************************
                        'End storage and summation
                        '*****************************************************************************************

                    Else 'If no match found...

                        stringier = ""
                        stringier = String.Join(",", cwtsubset(l).ItemArray.Select(Function(s) s.ToString).ToArray) 'Shorthand notation for writing a single row to file
                        'CWTbad.AppendLine(stringier)
                        'CWTlog.AppendLine("Record Rejected:   " & stringier)

                        note = "Record Rejected:  " & stringier
                        dtProcessOut.Tables(0).Rows.Add(runID, stk, code, RecId, note)

                    End If
                End If
SkipRecord:     'Place to go to after printing reject record to move on to next recovery
            Next 'On to the next recovery for tag code i
            '********************************************************************************************************


            ''********************************************************************************************************
            ''Now do a wee work up of missing FW Sport Recoveries
            'If IsDBNull(dsSpecialCodes.Tables(0).Select("TagCode = " & code)) = False Then 'The tag is in the list
            '    'Now get the necessary multipliers if it's on the menu for FW sport adjustment
            '    If dsSpecialCodes.Tables(0).Select("TagCode = " & code)(0)("Rule") = 1 Then
            '        Dim stocky As String = dsSpecialCodes.Tables(0).Select("TagCode = " & code)(0)("Stock")
            '        Dim mult2 As Double = dsFWSportRatios.Tables(0).Select("(Stock = '" & stocky & "') AND (RunYear = " & brood + 2 & ")")(0)("Ratio")
            '        Dim mult3 As Double = dsFWSportRatios.Tables(0).Select("(Stock = '" & stocky & "') AND (RunYear = " & brood + 3 & ")")(0)("Ratio")
            '        Dim mult4 As Double = dsFWSportRatios.Tables(0).Select("(Stock = '" & stocky & "') AND (RunYear = " & brood + 4 & ")")(0)("Ratio")
            '        Dim mult5 As Double = dsFWSportRatios.Tables(0).Select("(Stock = '" & stocky & "') AND (RunYear = " & brood + 5 & ")")(0)("Ratio")
            '    End If
            'End If
            ''********************************************************************************************************






            If ck_LengthOnly.Checked = False Then 'Don't do this stuff if you only want a length file prep run.

                '******************Write mapping results, rejects, and processing logs to file
                'Write Reject file
                'Using outfile As New StreamWriter(fpath & "\" & code.ToString & "rejects.rjc")
                '    outfile.Write(CWTbad.ToString)
                'End Using

                ''Write Processing Log file
                'Using outfile As New StreamWriter(fpath & "\" & code.ToString & "proclog.txt")
                '    outfile.Write(CWTlog.ToString)
                'End Using

                'Reformat mapped recoveries for *.CWT file creation -- CSV at present, reformat as needed
                'stringier = "Stock " & "Code " & "Brood " & "Age " & "Fishery " & "TStep " & "Estd Recs " & "TStep " & "Fishery " 'Header for CWT file
                'stringier = "*********************************************************************************************************" & vbCrLf & _
                '    "***********************************FRAMBuilder 2.0 Mapping Outputs***************************************" & vbCrLf & _
                '    "********************** " & "Created On " & Date.Now & " RunID " & runID & " *******************************" & vbCrLf & _
                '    "*********************************************************************************************************" & vbCrLf & vbCrLf & _
                '    "Stock# " & FRAMnum.ToString & " " & FRAMname & vbCrLf & vbCrLf & _
                '    "Stock" & "," & "Code" & "," & "Brood" & "," & "Age" & "," & "Fishery" & "," & "TStep" & "," & "Estd Recs" & "," & "TStep" & "," & "Fishery" & "," & "TagsInHand" & "," & "Notes" 'Header for CWT file
                'CWToutwrt.AppendLine(stringier)
                For a = 0 To MaxAge - MinAge
                    For k = 0 To TSNum - 1 'j = 0 To FishNum - 1 
                        For j = 0 To FishNum - 1 'k = 0 To TSNum - 1

                            'Populate a stringbuilder variable for *.CWT file
                            fishname = dsFRAMlist.Tables(0).Select()(j)("FisheryName").ToString
                            'Console.WriteLine(fishname)
                            'stringier = stk & " " & code & " " & brood.ToString & " " & (a + 1).ToString & _
                            '    " " & (j + 1).ToString & " " & (k + 1).ToString & " " & Round(CWTout(a, j, k), 2) & _
                            '    " " & TSNames(k) & " " & fishname
                            If CWTout(a, j, k) > 0 Then 'Only write non-zero items to file/db...
                                If j >= 75 Then
                                    'stringier = stk & "," & code & "," & brood.ToString & "," & (a + 2).ToString & _
                                    '    "," & (j + 1).ToString & "," & (k + 1).ToString & "," & Round(CWTout(a, j, k), 2) & _
                                    '    "," & TSNames(k) & "," & fishname & "," & CWToutN(a, j, k) & "," & dsFRAMlist.Tables(0).Select()(j)("Comments").ToString
                                    'CWToutwrt.AppendLine(stringier)
                                    ''Populate a DataTable for adding to database
                                    dtCWTout.Tables(0).Rows.Add(FRAMnum, stk, FRAMname, code, runtiming, brood, a + 2, j + 1, fishname, k + 1, TSNames(k), _
                                                      CWTout(a, j, k), CWToutN(a, j, k), DateTime.Now, runID, dsFRAMlist.Tables(0).Select()(j)("Comments").ToString)

                                ElseIf j < 75 Then
                                    'stringier = stk & "," & code & "," & brood.ToString & "," & (a + 2).ToString & _
                                    '    "," & (j + 1).ToString & "," & (k + 1).ToString & "," & Round(CWTout(a, j, k), 2) & _
                                    '    "," & TSNames(k) & "," & fishname & "," & CWToutN(a, j, k) & "," & dsFRAMlist.Tables(0).Select()(j)("Comments").ToString
                                    'CWToutwrt.AppendLine(stringier)
                                    ''Populate a DataTable for adding to database
                                    dtCWTout.Tables(0).Rows.Add(FRAMnum, stk, FRAMname, code, runtiming, brood, a + 2, j + 1, fishname, k + 1, TSNames(k), _
                                                      CWTout(a, j, k), CWToutN(a, j, k), DateTime.Now, runID, dsFRAMlist.Tables(0).Select()(j)("Comments").ToString)
                                End If

                            End If
                        Next
                    Next
                Next


                '**************WRITING *.CWT file type to text file and Access database table*********************************************

                '-----------First the text file-------------------------------------------------------
                'If *.CWT file option is checked, create the text file
                If ck_CWTfile.Checked = True Then 'Optional to write it to file
                    'Using outfile As New StreamWriter(fpath & "\" & code.ToString & ".CWT")
                        'outfile.Write(CWToutwrt.ToString)
                    'End Using
                End If
                '-------------------------------------------------------------------------------------

                '********************************************************************************************************
            End If

        Next 'On to the next tag code

        '-----------Now the database update---------------------------------------------------
        'If db write option is checked, Write mapped results to new or existing database table
        If ck_database.Checked = True Then 'Default is to write it to db
            'Define local variables needed to access and import the data contained in a table
            'Dim CWTdb As New OleDb.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & CWTdatabasename)
            'Dim sql As String       'SQL Query text string
            'Dim oledbAdapter As OleDb.OleDbDataAdapter

''...Uncomment if starting from scratch. Assume DB has this table...
'            'First check if the CAS database already has the table...if not, will do make table; if so, will do append query
'            CWTdb.Open()
'            Dim restrictions(3) As String
'            Dim DoesTableExist As Boolean
'            restrictions(2) = "FRAM_star_CWT"
'            Dim dbTbl As DataTable = CWTdb.GetSchema("Tables", restrictions)
'            If dbTbl.Rows.Count = 0 Then
'                'Table does not exist
'                DoesTableExist = False
'            Else
'                'Table exists
'                DoesTableExist = True
'            End If
'            dbTbl.Dispose()
'            CWTdb.Close()

'            'If it doesn't have it, create it.
'            If DoesTableExist = False Then
'                sql = "CREATE TABLE FRAM_star_CWT (StockNum INTEGER,StockAbbrev VARCHAR(255),StockName VARCHAR(255),CWTCode VARCHAR(255),RunTiming INTEGER," & _
'                    "BroodYear INTEGER,Age INTEGER,FisheryNum INTEGER,FisheryName VARCHAR(255),TStepNum INTEGER,TStepName VARCHAR(255),EstdRecs DOUBLE," & _
'                    "nRecs INTEGER,DateCreated DATETIME, RunID DOUBLE, Notes VARCHAR(255))"
'                'Now connect to the database and make the table...
'                'create a command
'                Dim my_Command As New OleDbCommand(sql, CWTdb)
'                CWTdb.Open()
'                'command execute
'                my_Command.ExecuteNonQuery()
'                CWTdb.Close()
'            End If
''...Uncomment if starting from scratch. Assume DB has this table...

            'Now, Add the new records to the table--will not overwrite the old records.
            CWTdb.Open()
                sql = "SELECT * FROM FRAM_star_CWT"
                oledbAdapter = New OleDb.OleDbDataAdapter(sql, CWTdb)
                Dim oleCommander As New OleDb.OleDbCommandBuilder(oledbAdapter)
                oledbAdapter.Update(dtCWTout.Tables(0))
                sql = "SELECT * FROM FRAM_ProcessLog"
                oledbAdapter = New OleDb.OleDbDataAdapter(sql, CWTdb)
                Dim oleCommander2 As New OleDb.OleDbCommandBuilder(oledbAdapter)
                oledbAdapter.Update(dtProcessOut.Tables(0))
                If (ck_LengthOnly.Checked = True Or ck_LengthToo.Checked = True) Then
                    'Now, Add the new records to the table--will not overwrite the old records.
                        sql = "SELECT * FROM FRAM_GrowthData"
                        oledbAdapter = New OleDb.OleDbDataAdapter(sql, CWTdb)
                        Dim oleCommander1 As New OleDb.OleDbCommandBuilder(oledbAdapter)
                        oledbAdapter.Update(dtLengthOut.Tables(0))
                End If
            CWTdb.Close()




            ''Now, Add the new records to the table--will not overwrite the old records.
            'CWTdb.Open()
            'sql = "SELECT * FROM FRAM_ProcessLog"
            'oledbAdapter = New OleDb.OleDbDataAdapter(sql, CWTdb)
            'Dim oleCommander2 As New OleDb.OleDbCommandBuilder(oledbAdapter)
            'oledbAdapter.Update(dtProcessOut.Tables(0))
            'CWTdb.Close()

            '-------------------------------------------------------------------------------------
        End If


        If ck_LengthOnly.Checked = False Then
                If ck_mergeCodes.Checked = True Then
                    Call CombineCodes()
                End If
                If ck_mergeBtwnBY.Checked = True Then
                    Call MergeBYs()
                End If
                If ck_BigTable.Checked = True Then
                    Call WriteMasterXWalktoDB(dsRecoveries.Tables("CWDBRecovery"))
                End If
                If ck_CWTAll.Checked = True Then
                    Call CWTAll()
                End If

        End If


    End Sub

    Public Sub SpecialRules()

        'At some point figure out how to isolate the special mapping rules, would be nice.

    End Sub




    'Private Sub btn_CatSam_Click(sender As System.Object, e As System.EventArgs) Handles btn_CatSam.Click

    '    'This subroutine tried to make the most of RMIS catch sample data; it was abandoned due to clear
    '    'evidence that RMIS catch data aren't necessarily the best and brightest for said purposes.


    '    'Step 1: connect to the database and load a dataset with catch sample data

    '    Dim CWTdb As New OleDb.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & CWTdatabasename)
    '    Dim sqlcat As String       'SQL Query text string
    '    Dim oledbAdapter As OleDb.OleDbDataAdapter

    '    'Even though it won't open yet, set the settings for less cross-eyed selection
    '    Dim objAlternatingCellStyle As New DataGridViewCellStyle()
    '    objAlternatingCellStyle.BackColor = Color.WhiteSmoke
    '    form_CatchSampleViewer.grd_CatView.AlternatingRowsDefaultCellStyle = objAlternatingCellStyle
    '    form_CatchSampleViewer.grd_CatView.AutoResizeColumns()
    '    form_CatchSampleViewer.grd_CatView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells


    '    'Define the data selection here
    '    'sqlcat = "SELECT DISTINCT FRAM_CatSamDat.format_version, [FRAM_CatSamDat].[reporting_agency] & ' - ' & [FRAM_CatSamDat].[catch_sample_id] & ' - ' & [catch_year] AS sampID, CWDBRecovery.Fishery, FRAM_Fishery.CAS_Fishery, FRAM_Fishery.FRAM_FishID_Mapped, FRAM_Fishery.FRAM_FisheryName_Mapped, FRAM_CatSamDat.record_code, " & _
    '    '"  FRAM_CatSamDat.reporting_agency, FRAM_CatSamDat.submission_date, FRAM_CatSamDat.catch_sample_id, FRAM_CatSamDat.catch_year, FRAM_CatSamDat.sampling_agency, FRAM_CatSamDat.species, FRAM_CatSamDat.period, FRAM_CatSamDat.period_type, FRAM_CatSamDat.first_period, FRAM_CatSamDat.fishery, FRAM_CatSamDat.last_period, " & _
    '    '"  FRAM_CatSamDat.adclip_selective_fishery, FRAM_CatSamDat.catch_location_code, FRAM_CatSamDat.estimation_level, FRAM_CatSamDat.detection_method, FRAM_CatSamDat.sample_type, FRAM_CatSamDat.sampled_maturity, FRAM_CatSamDat.sampled_length_range, FRAM_CatSamDat.sampled_run, FRAM_CatSamDat.sampled_mark,  " & _
    '    '"  FRAM_CatSamDat.sampled_sex, FRAM_CatSamDat.escapement_estimation_method, FRAM_CatSamDat.number_caught, FRAM_CatSamDat.number_estimated, FRAM_CatSamDat.number_sampled, FRAM_CatSamDat.number_recovered_decoded, FRAM_CatSamDat.number_recovered_lost_cwts, FRAM_CatSamDat.number_recovered_no_cwts, " & _
    '    '"  FRAM_CatSamDat.number_recovered_unreadable, FRAM_CatSamDat.number_recovered_not_processed, FRAM_CatSamDat.number_recovered_unresolved, FRAM_CatSamDat.mr_1st_partition_size, FRAM_CatSamDat.number_recovered_pseudotags, FRAM_CatSamDat.mr_1st_sample_known_ad_status, FRAM_CatSamDat.mr_1st_sample_size, " & _
    '    '"  FRAM_CatSamDat.mr_1st_sample_obs_adclips, FRAM_CatSamDat.mr_2nd_partition_size, FRAM_CatSamDat.mr_2nd_sample_known_ad_status, FRAM_CatSamDat.mr_2nd_sample_size, FRAM_CatSamDat.mark_rate, FRAM_CatSamDat.mr_2nd_sample_obs_adclips, FRAM_CatSamDat.awareness_factor, FRAM_CatSamDat.sport_mark_inc_sampl_obs_adclips, " & _
    '    '"  FRAM_CatSamDat.sport_mark_incidence_sampl_size, FRAM_CatSamDat.catch_location_name, FRAM_CatSamDat.ID, FRAM_CatSamDat.record_origin " & _
    '    '"FROM FRAM_Fishery INNER JOIN " & _
    '    '"  (CWDBRecovery INNER JOIN " & _
    '    '"      FRAM_CatSamDat ON (CWDBRecovery.CatchSampleId = FRAM_CatSamDat.catch_sample_id) AND (CWDBRecovery.RunYear = FRAM_CatSamDat.catch_year) AND (CWDBRecovery.Agency = FRAM_CatSamDat.reporting_agency)) ON FRAM_Fishery.CAS_Fishery = CWDBRecovery.Fishery " & _
    '    '"WHERE (((FRAM_CatSamDat.catch_year) Between 2000 And 2011)) " & _
    '    '"ORDER BY FRAM_Fishery.FRAM_FishID_Mapped, FRAM_CatSamDat.catch_year, FRAM_CatSamDat.sampling_agency"

    '    'Alternative query structure--includes average date for all recoveries associated with catch sample; more efficient than using various periods/levels in catch ID
    '    sqlcat = "SELECT FRAM_CatSamDat.format_version, [FRAM_CatSamDat].[reporting_agency] & ' - ' & [FRAM_CatSamDat].[catch_sample_id] & ' - ' & [catch_year] " & _
    '    "AS sampID, CWDBRecovery.Fishery, FRAM_Fishery.FRAM_FishID_Mapped, FRAM_Fishery.FRAM_FisheryName_Mapped, FRAM_CatSamDat.record_code, FRAM_CatSamDat.reporting_agency, " & _
    '    " Min(CWDBRecovery.RecoveryDate) AS MinOfRecoveryDate, Format(Round(Avg([CWDBRecovery].[RecoveryDate]),0),'m/d/yyyy') AS RecDate, Max(CWDBRecovery.RecoveryDate) " & _
    '    " AS MaxOfRecoveryDate, FRAM_CatSamDat.catch_sample_id, FRAM_CatSamDat.catch_year, FRAM_CatSamDat.sampling_agency, FRAM_CatSamDat.species, FRAM_CatSamDat.period, " & _
    '    " FRAM_CatSamDat.period_type, FRAM_CatSamDat.first_period, FRAM_CatSamDat.fishery, FRAM_CatSamDat.last_period, FRAM_CatSamDat.adclip_selective_fishery, FRAM_CatSamDat.catch_location_code, " & _
    '    " FRAM_CatSamDat.estimation_level, FRAM_CatSamDat.detection_method, FRAM_CatSamDat.sample_type, FRAM_CatSamDat.sampled_maturity, FRAM_CatSamDat.sampled_length_range, FRAM_CatSamDat.sampled_run, " & _
    '    " FRAM_CatSamDat.sampled_mark, FRAM_CatSamDat.sampled_sex, FRAM_CatSamDat.escapement_estimation_method, FRAM_CatSamDat.number_caught, FRAM_CatSamDat.number_estimated, " & _
    '    " FRAM_CatSamDat.number_sampled, FRAM_CatSamDat.number_recovered_decoded, FRAM_CatSamDat.number_recovered_lost_cwts, FRAM_CatSamDat.number_recovered_no_cwts, FRAM_CatSamDat.number_recovered_unreadable, " & _
    '    " FRAM_CatSamDat.number_recovered_not_processed, FRAM_CatSamDat.number_recovered_unresolved, FRAM_CatSamDat.mr_1st_partition_size, FRAM_CatSamDat.number_recovered_pseudotags, " & _
    '    " FRAM_CatSamDat.mr_1st_sample_known_ad_status, FRAM_CatSamDat.mr_1st_sample_size, FRAM_CatSamDat.mr_1st_sample_obs_adclips, FRAM_CatSamDat.mr_2nd_partition_size, " & _
    '    " FRAM_CatSamDat.mr_2nd_sample_known_ad_status, FRAM_CatSamDat.mr_2nd_sample_size, FRAM_CatSamDat.mark_rate, FRAM_CatSamDat.mr_2nd_sample_obs_adclips, FRAM_CatSamDat.awareness_factor, " & _
    '    " FRAM_CatSamDat.sport_mark_inc_sampl_obs_adclips, FRAM_CatSamDat.sport_mark_incidence_sampl_size, FRAM_CatSamDat.catch_location_name, FRAM_CatSamDat.ID, FRAM_CatSamDat.record_origin " & _
    '    " FROM FRAM_Fishery INNER JOIN (CWDBRecovery INNER JOIN FRAM_CatSamDat ON (CWDBRecovery.CatchSampleId = FRAM_CatSamDat.catch_sample_id) AND (CWDBRecovery.RunYear = FRAM_CatSamDat.catch_year) " & _
    '    " AND (CWDBRecovery.Agency = FRAM_CatSamDat.reporting_agency)) ON FRAM_Fishery.CAS_Fishery = CWDBRecovery.Fishery " & _
    '    " GROUP BY FRAM_CatSamDat.format_version, [FRAM_CatSamDat].[reporting_agency] & ' - ' & [FRAM_CatSamDat].[catch_sample_id] & ' - ' & [catch_year], CWDBRecovery.Fishery, FRAM_Fishery.FRAM_FishID_Mapped, " & _
    '    " FRAM_Fishery.FRAM_FisheryName_Mapped, FRAM_CatSamDat.record_code, FRAM_CatSamDat.reporting_agency, FRAM_CatSamDat.catch_sample_id, FRAM_CatSamDat.catch_year, FRAM_CatSamDat.sampling_agency, " & _
    '    " FRAM_CatSamDat.species, FRAM_CatSamDat.period, FRAM_CatSamDat.period_type, FRAM_CatSamDat.first_period, FRAM_CatSamDat.fishery, FRAM_CatSamDat.last_period, FRAM_CatSamDat.adclip_selective_fishery, " & _
    '    " FRAM_CatSamDat.catch_location_code, FRAM_CatSamDat.estimation_level, FRAM_CatSamDat.detection_method, FRAM_CatSamDat.sample_type, FRAM_CatSamDat.sampled_maturity, FRAM_CatSamDat.sampled_length_range, " & _
    '    " FRAM_CatSamDat.sampled_run, FRAM_CatSamDat.sampled_mark, FRAM_CatSamDat.sampled_sex, FRAM_CatSamDat.escapement_estimation_method, FRAM_CatSamDat.number_caught, FRAM_CatSamDat.number_estimated, FRAM_CatSamDat.number_sampled, " & _
    '    " FRAM_CatSamDat.number_recovered_decoded, FRAM_CatSamDat.number_recovered_lost_cwts, FRAM_CatSamDat.number_recovered_no_cwts, FRAM_CatSamDat.number_recovered_unreadable, FRAM_CatSamDat.number_recovered_not_processed, " & _
    '    " FRAM_CatSamDat.number_recovered_unresolved, FRAM_CatSamDat.mr_1st_partition_size, FRAM_CatSamDat.number_recovered_pseudotags, FRAM_CatSamDat.mr_1st_sample_known_ad_status, FRAM_CatSamDat.mr_1st_sample_size, " & _
    '    " FRAM_CatSamDat.mr_1st_sample_obs_adclips, FRAM_CatSamDat.mr_2nd_partition_size, FRAM_CatSamDat.mr_2nd_sample_known_ad_status, FRAM_CatSamDat.mr_2nd_sample_size, FRAM_CatSamDat.mark_rate, " & _
    '    " FRAM_CatSamDat.mr_2nd_sample_obs_adclips, FRAM_CatSamDat.awareness_factor, FRAM_CatSamDat.sport_mark_inc_sampl_obs_adclips, FRAM_CatSamDat.sport_mark_incidence_sampl_size, FRAM_CatSamDat.catch_location_name, " & _
    '    " FRAM_CatSamDat.ID, FRAM_CatSamDat.record_origin " & _
    '    "HAVING (((FRAM_CatSamDat.catch_year) Between 2000 And 2011)) " & _
    '    "ORDER BY FRAM_CatSamDat.catch_year, FRAM_CatSamDat.sampling_agency"

    '    'Read in the data and fill the VB DataSet
    '    Try
    '        CWTdb.Open()
    '        oledbAdapter = New OleDb.OleDbDataAdapter(sqlcat, CWTdb)
    '        oledbAdapter.Fill(dsCatSampDat, "CatchData")
    '        oledbAdapter.Dispose()
    '        CWTdb.Close()

    '    Catch ex As Exception
    '        MsgBox("Can not open connection! ")
    '    End Try


    '    'Now do the ugly part of processing the data.
    '    Call CatchProc()


    '    'Now load the catch sample data viewer...
    '    Me.Visible = False
    '    form_CatchSampleViewer.ShowDialog()

    'End Sub

'    Public Sub CatchProc()

'        'Get the score on what came through the tubes for the datatable...
'        Dim CatTab As New DataTable
'        CatTab = dsCatSampDat.Tables("CatchData")

'        'Create the table and get the specs for dimensioning
'        Dim yearbeg, yearend, nrowsFish, nrowsSamp, curFish As Integer
'        yearbeg = CatTab.Compute("Min(catch_year)", "") 'computes something for the entire column, no filter required
'        yearend = CatTab.Compute("Max(catch_year)", "") ' ditto
'        nrowsFish = dsFRAMlist.Tables("FRAM_FishList").Rows.Count
'        nrowsSamp = CatTab.Rows.Count

'        'Now, let's get down to business...
'        CatTab.Columns.Add("ReMap", GetType(Integer)) 'New field for final mapping value
'        CatTab.Columns.Add("timestep", GetType(Integer)) 'Field for time step assignment

'        'Create a separate DataTable for storing the catches for the new fisheries
'        'Have to add these at the end to avoid an infinite processing loop.
'        Dim TempAdditions As DataTable
'        TempAdditions = CatTab.Clone()

'        For i = 0 To nrowsSamp - 1

'            '****###--Special Mapping Rules -- Mirrors what's done for tags--###********************************************************
'            'The goal of this code is to further map catch sample data to FRAM fishery to address the one-to-many CAS-to-FRAM relations
'            'See the OneNote documentation for additional details.

'            If IsDBNull(CatTab(i)("FRAM_FishID_Mapped")) = True Then
'                CatTab(i)("ReMap") = -1000 ' Junk value for warning/review
'                GoTo Skedaddle

'            Else
'                'Figure out what FRAM fishery the CAS Fishery associated with record l corresponds to
'                curFish = CatTab(i)("FRAM_FishID_Mapped") 'Grabs the associated FRAM Fishery Number
'                CatTab(i)("ReMap") = curFish 'Default mapping location -- will be replaced based on rules below...

'                'Some additional rules for reassignment given one-to-many FRAM to CAS relationship

'                'First Rule: Splitting Troll fisheries into Treaty/Non-treaty Components, based on fishery type 10/15
'                If curFish = 16 And CatTab(i)("FRAM_CatSamDat.fishery") = 15 Then 'If A3:4:4B troll and treaty troll code then split
'                    CatTab(i)("ReMap") = 17
'                ElseIf curFish = 20 And CatTab(i)("FRAM_CatSamDat.fishery") = 15 Then 'If A2 troll and treaty troll code then split
'                    CatTab(i)("ReMap") = 21
'                End If

'                'Second Rule: Splitting CAS WASH COASTAL RIVERS NET (3035) into FRAM Grays, Willapa, and N WA Coast components
'                'NOTE: Still need to split Grays Hbr into T/NT equivalents
'                If CatTab(i)("CWDBRecovery.fishery") = 3035 Then
'                    If CatTab(i)("catch_location_code").ToString.Contains("3M21902") = True Then
'                        CatTab(i)("ReMap") = 23  'Assign to Willapa
'                    ElseIf CatTab(i)("catch_location_code").ToString.Contains("3M21802") = True Then
'                        CatTab(i)("ReMap") = 24 'Assign to NT Grays Harbor Net
'                    Else
'                        CatTab(i)("ReMap") = 19 'Assign to N WA Coastal Net
'                    End If
'                End If

'                'Third Rule: Splitting CAS GEO ST SPORT (2015) into FRAM N & S GS SPORT and JDF SPORT
'                If CatTab(i)("CWDBRecovery.fishery") = 2015 Then
'                    Dim TrimmedTo As String
'                    TrimmedTo = Microsoft.VisualBasic.Right(Microsoft.VisualBasic.Left(CatTab(i)("catch_location_code").ToString, 9), 3)
'                    If TrimmedTo = "13 " Or TrimmedTo = "14 " Or TrimmedTo = "15 " Or TrimmedTo = "16 " Then 'Canadian Stat Areas 13-16
'                        CatTab(i)("ReMap") = 13  'Assign to North Georgia Strait Sport
'                    ElseIf TrimmedTo = "17 " Or TrimmedTo = "18 " Or TrimmedTo = "19A" Or TrimmedTo = "28 " Or TrimmedTo = "29 " Or CatTab(i)("catch_location_code").ToString.Contains("19A") Then 'Canadian Stat Areas Canadian Stat Areas 17-18, 19A, 28-29
'                        CatTab(i)("ReMap") = 14  'Assign to South Georgia Strait Sport
'                    ElseIf TrimmedTo = "19B" Or TrimmedTo = "20 " Or CatTab(i)("catch_location_code").ToString.Contains("19B") Then
'                        CatTab(i)("ReMap") = 15 'Assign to BC JDF Sport
'                    End If
'                End If

'                'Fourth Rule: Splitting CAS WA SPS NET (3013) into FRAM Area 13A (T+NT) and SPS Net (13, 13D-K) Fisheries
'                'Also deals with Chambers 13C Catch by shifting it to FW Net (consistent with TAMM treatment of fishery and actual
'                'Fishery layout (i.e., 13C is east of RR trestle at mouth of stream, anyone caught there is probably staying there, thus a stray)
'                'NOTE: Still need to split all fisheries into T/NT equivalents
'                If CatTab(i)("CWDBRecovery.fishery") = 3013 Then
'                    If CatTab(i)("catch_location_code").ToString.Contains("C") = True Then
'                        CatTab(i)("ReMap") = 73  'Assign 13C to Freshwater Net
'                    ElseIf CatTab(i)("catch_location_code").ToString.Contains("A") = True Then
'                        CatTab(i)("ReMap") = 70 'Assign All 13A Catch to NT (will split via other means later)
'                        'NOTE: Still need to split into T/NT equivalents
'                    Else
'                        CatTab(i)("ReMap") = 68 'Assign everything else NT SPS Net (i.e., Gen 13, 13B, 13D-K)
'                        'Also if it's not 13D or 13F (only locs fished in 2007-11 FY), flag it in the log file so that the 'SPS Net' assignment can be inspected by user
'                    End If
'                End If

'                'Fifth Rule: Splitting CAS WA PS AREAS 10 AND 11 NET Into FRAM 10A Tr, 10E Tr, and 10:11 Tr/NT Net fisheries
'                'NOTE: Still need to split 10:11 Net into Tr/NT equivalents
'                If CatTab(i)("CWDBRecovery.fishery") = 3012 Then
'                    If CatTab(i)("catch_location_code").ToString.Contains("10  A") = True Then
'                        CatTab(i)("ReMap") = 61  'Assign To Treaty 10A
'                    ElseIf CatTab(i)("catch_location_code").ToString.Contains("10  E") = True Then
'                        CatTab(i)("ReMap") = 63 'Assign To Treaty 10E
'                        'NOTE: Still need to split into T/NT equivalents
'                    Else
'                        CatTab(i)("ReMap") = 58 'Assign everything else NT 10:11 Net, but also write to log for verification
'                    End If
'                End If

'                'Sixth Rule: Dealing with 10A (EBay) and 10E (Stinklair) vs. Area 10 during summer period
'                'Note: this rule has possible imperfections due to 10A, 10E, 10 miscoding on RMIS, although
'                'the fraction of erroneous recoveries is likely minimal.
'                If CatTab(i)("CWDBRecovery.fishery") = 3025 And (Month(CatTab(i)("RecDate")) >= 7 And Month(CatTab(i)("RecDate")) <= 9) Then
'                    If CatTab(i)("catch_location_code").ToString.Contains("872155" Or "872585") = True Then
'                        CatTab(i)("ReMap") = 60  'Assign To Elliott Bay
'                    ElseIf CatTab(i)("catch_location_code").ToString.Contains("872477" Or "872286" Or "872408") = True Then
'                        CatTab(i)("ReMap") = 62 'Assign To Sinclair Inlet
'                    Else
'                        CatTab(i)("ReMap") = 56 'Assign everything else General Area 10
'                    End If
'                End If

'                'Seventh Rule: Moving 8D Sport out of 8-1/8-2 Sport during Tulalip Bay management
'                If CatTab(i)("CWDBRecovery.fishery") = 3023 And (Month(CatTab(i)("RecDate")) >= 6 And Month(CatTab(i)("RecDate")) <= 9) Then
'                    CatTab(i)("ReMap") = 48 'Move recoveries into 8-D if they were an 8-2 Rec during months of Jun-Sept (8-2 closed, 8D open)
'                End If

'                'Eighth Rule: Moving 8D Net Out of 8A Net
'                'NOTE: Still need to split 8A/8D Net into Tr/NT equivalents
'                If CatTab(i)("CWDBRecovery.fishery") = 3011 And CatTab(i)("catch_location_code").ToString.Contains("8  D") = True Then
'                    CatTab(i)("ReMap") = 51 'Move Recoveries to NT 8D and deal with T/NT splitting later
'                End If

'                'Ninth Rule: Separating KMZ From California Troll so that it can be merged with OR KMZ Troll into single FRAM KMZ Troll Fishery
'                If CatTab(i)("CWDBRecovery.fishery") = 3034 And CatTab(i)("catch_location_code").ToString.Contains("OBHJ") = True Then
'                    'At present the only troll recovery code discernable as within KMZ is Oregon Border-Humboldt Jetty, eval as needed
'                    CatTab(i)("ReMap") = 32 'Move Recoveries to KMZ Troll, leave others in Cali Sport
'                End If

'                'Tenth Rule: Separating KMZ From California Sport so that it can be merged with OR KMZ Sport into single FRAM KMZ Sport Fishery
'                If CatTab(i)("CWDBRecovery.fishery") = 3043 And CatTab(i)("catch_location_code").ToString.Contains("BGCB") = True Then
'                    'At present the only sport recovery code discernable is Big Lagoon to Centerville Beach, eval as needed
'                    CatTab(i)("ReMap") = 33 'Move Recoveries to KMZ Sport, leave others in Cali Sport
'                    'But also write to log for inspection
'                End If

'                'Eleventh Rule: Moving 12H (Hoodsport) to FW Net, consistent with the TAMM treatment of the fishery
'                If CatTab(i)("CWDBRecovery.fishery") = 3014 And CatTab(i)("catch_location_code").ToString.Contains("12  H") = True Then
'                    'At present the only sport recovery code discernable is Big Lagoon to Centerville Beach, eval as needed
'                    CatTab(i)("ReMap") = 73 'Move Recoveries to KMZ Sport, leave others in Cali Sport
'                End If
'                '****###--End Special Mapping Rules--###******************************************************************

'                '***Time Step Assignment*********************************************************************
'                'For future use, not sure yet how to do this...how to combine 1 vs. 4 vs. 2 and 3
'                Dim TS As Integer
'                TS = Month(CatTab(i)("RecDate"))
'                If TS > 9 Then
'                    TS = 4
'                ElseIf TS > 5 Then
'                    TS = 3
'                ElseIf TS > 4 Then
'                    TS = 2
'                Else
'                    TS = 1
'                End If
'                CatTab(i)("timestep") = TS
'                '***Time Step Assignment**********************************************************************


'                Dim InsertMeHere = CatTab.NewRow()
'                ''%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
'                ''ADDITIONAL SPECIAL MAPPING/INVENTORYING RULES FOR ASSESSING POSSIBILITY OF NEW FISHERIES
'                'If ck_draftfish.Checked = True Then
'                'Create a duplicate row for the subset we're slicing into...

'                '1.  New Breakouts for Tr/NT Troll in 3 and 4
'                If CatTab(i)("CWDBRecovery.fishery") = 3003 Then
'                    InsertMeHere.ItemArray = CatTab.Rows(i).ItemArray
'                    If InsertMeHere("FRAM_CatSamDat.fishery") = 15 Then 'If A3:4:4B troll and treaty troll code then split
'                        InsertMeHere("ReMap") = 77
'                    Else
'                        InsertMeHere("ReMap") = 76
'                    End If
'                    CatTab.Rows.Add(InsertMeHere)
'                End If

'                If CatTab(i)("CWDBRecovery.fishery") = 3004 Then
'                    InsertMeHere.ItemArray = CatTab.Rows(i).ItemArray
'                    If InsertMeHere("FRAM_CatSamDat.fishery") = 15 Then 'If A3:4:4B troll and treaty troll code then split
'                        InsertMeHere("ReMap") = 79
'                    Else
'                        InsertMeHere("ReMap") = 78
'                    End If
'                    CatTab.Rows.Add(InsertMeHere)
'                End If


'                '2.  New ISBM (Inside) and AABM (QCI) Components of NBC Sport
'                If CatTab(i)("CWDBRecovery.fishery") = 2016 Then
'                    InsertMeHere.ItemArray = CatTab.Rows(i).ItemArray
'                    If CatTab(i)("catch_location_code").ToString.Contains("3MN25003") = True _
'                        Or CatTab(i)("catch_location_code").ToString.Contains("3MN25004") = True _
'                        Or CatTab(i)("catch_location_code").ToString.Contains("3MN25005") = True Then
'                        InsertMeHere("ReMap") = 81
'                    Else
'                        InsertMeHere("ReMap") = 80
'                    End If
'                    CatTab.Rows.Add(InsertMeHere)
'                End If

'                '%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
'            End If
'Skedaddle:

'        Next

'        'Now add the duplicate rows for the special new fisheries
'        'Debug.Print("*** " & CatTab.Rows.Count.ToString & vbCrLf)

'        CatTab.Merge(TempAdditions)

'        'Debug.Print("*** " & CatTab.Rows.Count.ToString & vbCrLf)

'        'And Delete the junk stuff
'        Dim RowsTOdelete As DataRow()
'        RowsTOdelete = CatTab.Select("ReMap = -1000")
'        For Each dr As DataRow In RowsTOdelete
'            CatTab.Rows.Remove(dr)
'        Next
'        'Debug.Print("*** " & CatTab.Rows.Count.ToString & vbCrLf)

'        ''A wee bit of debugging code for comparing Excel Pivot to what occurs herein.
'        'Dim FileChoose As New FolderBrowserDialog
'        'FileChoose.Description = "Choose Output Directory"
'        'FileChoose.SelectedPath = "C:\"
'        'FileChoose.RootFolder = Environment.SpecialFolder.Desktop
'        'If FileChoose.ShowDialog() = Windows.Forms.DialogResult.OK Then
'        '    fpath = FileChoose.SelectedPath
'        'End If
'        ''Code to export to a spreadsheet a catch sample table...
'        'Module1.ExportToExcel("OutAll", fpath, CatTab)

'        'Now that the array has been populated, let's do a pivot on this guy!
'        'Goal, to sum catch estimates for each FRAM fishery outlined above by year (for entire catch year).

'        'Add the fishery ID and fishery name columns
'        dtPivCat.Columns.Add("FishID", GetType(Integer))
'        dtPivCat.Columns.Add("FishName", GetType(String))
'        'Add columns for each yearly total
'        For i = yearbeg To yearend
'            dtPivCat.Columns.Add(i.ToString, GetType(Double))
'        Next
'        'Add rows for fisheries
'        For i = 0 To dsFRAMlist.Tables(0).Rows.Count - 1
'            dtPivCat.Rows.Add()
'        Next

'        For i = 0 To dsFRAMlist.Tables(0).Rows.Count - 1
'            For yrcount = yearbeg To yearend
'                Dim FiltString As String = "catch_year = " & yrcount & " AND ReMap = " & i + 1
'                Dim NCaught As Double
'                dtPivCat(i)("FishID") = i + 1 'Add Name and ID to pivot table
'                dtPivCat(i)("FishName") = dsFRAMlist.Tables(0).Rows(i)("FisheryName")

'                'Now sum
'                If IsDBNull(CatTab.Compute("Sum(number_caught)", FiltString)) Then
'                    NCaught = 0
'                Else
'                    NCaught = CatTab.Compute("Sum(number_caught)", FiltString)
'                End If

'                'Store resut in cell
'                dtPivCat(i)(yrcount.ToString) = NCaught 'computes something for the entire column, no filter required

'            Next
'        Next
'    End Sub

    Public Sub WriteMasterXWalktoDB(ByVal DatTab As DataTable)

        Dim objConnection As New OleDb.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & CWTdatabasename)
        Dim sql As String = "SELECT * FROM CWDBRecovery "
        Dim objdataadapter As New OleDbDataAdapter(sql, objConnection)
        Dim builder As OleDbCommandBuilder = New OleDbCommandBuilder(objdataadapter)

        objConnection.Open()

        builder.GetUpdateCommand()
        builder.GetUpdateCommand()

        objdataadapter.Update(dsRecoveries, "CWDBRecovery")
        objConnection.Close()
        objdataadapter = Nothing
        objConnection = Nothing


    End Sub

   Public Sub MergeBYs()

    'This subroutine merges BY files into a single base period CWT file based on weights specified by user, no weights, or 
    'based on weights computed internally

        '________________________________________________________________________________________________________________________________
        'some database connection variables
        Dim CWTdb As New OleDb.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & CWTdatabasename)
        Dim sql As String       'SQL Query text string
        Dim oledbAdapter As OleDb.OleDbDataAdapter
        Dim i, j, k, nstks, nbys As Integer
        Dim stk As String
        Dim stklist As New DataTable
        Dim stklisty(1) As String
        Dim DBwtBYwarning As Boolean = False
        dtCWTout.Tables(0).Rows.Clear() 'Clear the local data table and re-use it for supacode
        '________________________________________________________________________________________________________________________________

        '________________________________________________________________________________________________________________________________
        'Step 1: get a list of stocks that were included in the run
        'Build the query string for extracting from the recovery database
        sql = "SELECT * FROM FRAM_star_CWT WHERE (((FRAM_star_CWT.RunID)= " & runID & ")) ORDER BY FRAM_star_CWT.StockAbbrev"
        stklist.Clear() 'start with a clean slate
        'Read in the data and fill the VB DataSet
        Try
            CWTdb.Open()
            oledbAdapter = New OleDb.OleDbDataAdapter(sql, CWTdb)
            oledbAdapter.Fill(stklist) 'Creates the local dataset of chosen recoveries
            oledbAdapter.Dispose()
            CWTdb.Close()

        Catch ex As Exception
            MsgBox("Can not open connection! ")
        End Try

        'Create stock list
        stklisty(0) = stklist(0)("StockAbbrev") 'set the first one to the first value seen in the table
        j = 0
        For i = 1 To stklist.Rows.Count - 1
            stk = stklist(i)("StockAbbrev")
            If stk <> stklisty(j) Then
                j += 1          'Final j+1 will also be length of stock list
                Array.Resize(stklisty, j + 1)
                stklisty(j) = stklist(i)("StockAbbrev")
            End If
        Next
        nstks = j

        'stklisty(0) = stklist(0)("StockName") 'set the first one to the first value seen in the table
        'j = 0
        'For i = 1 To stklist.Rows.Count - 1
        '    stk = stklist(i)("StockName")
        '    If stk <> stklisty(j) Then
        '        j += 1          'Final j+1 will also be length of stock list
        '        Array.Resize(stklisty, j + 1)
        '        stklisty(j) = stklist(i)("StockName")
        '    End If
        'Next
        'nstks = j

        '________________________________________________________________________________________________________________________________


        '________________________________________________________________________________________________________________________________
        'Step 2: Now use the list to merge on a stock-by-stock basis
        For i = 0 To nstks

            Dim dsBYmerge As New DataSet  'Tertiary WireTageCode Table dataset for merging across BYs within a stock.
            'Dim CWTAllOut As NewDataSet 'Table for formatting CWTAllOutt to send to the database
            Dim dsBYmergesubset() As DataRow  'Subset for a given CWT code
            Dim BYmergeRule As Integer

            Dim bylisty(1), bywts(1) As Double

            stk = stklisty(i)
            'sql = "SELECT * FROM FRAM_star_CWT"
            sql = "SELECT * FROM FRAM_star_CWT WHERE (((FRAM_star_CWT.CWTCode) Like ""%" & stk & "%"") AND ((FRAM_star_CWT.RunID)= " & runID & "))" 'ORDER BY FRAM_star_CWT.BroodYear"
            'Read in the data and fill the VB DataSet
            Dim oledbAdapter1 As New OleDb.OleDbDataAdapter
            Try
                CWTdb.Open()
                oledbAdapter = New OleDb.OleDbDataAdapter(sql, CWTdb)
                oledbAdapter.Fill(dsBYmerge, "FRAM_star_CWT") 'for stock i --all BYs
                oledbAdapter.Dispose()
                CWTdb.Close()
            Catch ex As Exception
                MsgBox("Can not open connection to query FRAM_star_CWT for BY merge! ")
            End Try

            'Determine what BYs are included for the stock, i.e., create a list
            bylisty(0) = dsBYmerge.Tables(0)(0)("BroodYear")
            j = 0
            For k = 1 To dsBYmerge.Tables(0).Rows.Count - 1
                Dim Testy As Boolean = False
                For mm = 0 To (bylisty.Length - 1)
                    If dsBYmerge.Tables(0)(k)("BroodYear") = bylisty(mm) Then
                        Testy = True
                    End If
                Next
                If Testy = False Then
                    j += 1          'Final j+1 will also be length of stock list
                    Array.Resize(bylisty, j + 1)
                    bylisty(j) = dsBYmerge.Tables(0)(k)("BroodYear")
                End If
            Next
            nbys = j

            ReDim bywts(nbys)


            '-------------------------------------------------------
            'Step 2a: get merge rules and weights where appropriate
            'NOTE: this construct requires that all codes and BYs included in the DB are correctly flagged and identically wtd within BYs
            BYmergeRule = 2 '(default, recoveries [all BYs are equal] weighted merging)
            If (ck_bnBroodWt.Checked = True And dtWeights.Rows.Count = 0) Then
                If DBwtBYwarning = False Then
                    MessageBox.Show("You specified 'use between-brood DB weighting rules' but your DB is missing the FRAM_Weights table." & vbCrLf & "Recoveries-wtd between-BY merging (default) will occur")
                    DBwtBYwarning = True
                End If
                GoTo MoveOn
            End If
            If ck_bnBroodWt.Checked = True Then 'Get rules and weights from the db
                    Dim exp As String
                    Dim z As Integer
                For z = 0 To nbys
                    exp = "(Stock = '" & stk & "') AND (BroodYear = " & bylisty(z) & ")"

                    Try
                        If ((IsDBNull(dtWeights.Select(exp)) = True) Or (dtWeights.Select(exp)(0)("bnBYmeth") = 2)) Then
                            BYmergeRule = 2 'Stays 2
                            GoTo MoveOn
                        ElseIf dtWeights.Select(exp)(0)("bnBYmeth") = 1 Then 'no weights
                            bywts(z) = 1
                            BYmergeRule = 1
                        ElseIf dtWeights.Select(exp)(0)("bnBYmeth") = 3 Then
                            bywts(z) = dtWeights.Select(exp)(0)("bnBYwt")
                            BYmergeRule = 3
                        End If
                    Catch ex As Exception
                        GoTo MoveOn
                    End Try

                Next
            End If
MoveOn:
            If BYmergeRule = 2 Then 'Compute recoveries-weighted weights
                Dim max As Double
                Dim recs As Double
                'get total recs per BY and compute max
                recs = dsBYmerge.Tables(0).Compute("Sum(EstdRecs)", "BroodYear = " & bylisty(0) & "AND FisheryNum < 76")
                max = recs
                bywts(0) = recs
                For k = 1 To nbys
                    recs = dsBYmerge.Tables(0).Compute("Sum(EstdRecs)", "BroodYear = " & bylisty(k) & "AND FisheryNum < 76")
                    If recs > max Then max = recs
                    bywts(k) = recs
                Next
                'then compute weights as wi = max(r1,r2,...,rn)/ri, i.e., scale everything up to largest brood
                For k = 0 To nbys
                    bywts(k) = max / bywts(k)
                Next
            End If
            '-------------------------------------------------------

            '-------------------------------------------------------
            'Step 2b: Now modify recs within broods according to weights unless unwtd merging
            If (BYmergeRule = 2 Or BYmergeRule = 3) Then
                For k = 0 To nbys
                    dsBYmergesubset = dsBYmerge.Tables(0).Select("BroodYear = " & bylisty(k))
                    For d = 0 To dsBYmergesubset.Length - 1
                        dsBYmergesubset(d)("EstdRecs") = dsBYmergesubset(d)("EstdRecs") * bywts(k)
                        dsBYmergesubset(d)("CWTCode") = "AB." & dsBYmergesubset(d)("CWTCode")
                    Next
                Next
            End If
            '-------------------------------------------------------

            '-------------------------------------------------------
            'Step 2c: Format output and commit it to the database

            'Need a few more names for ALL by file (names, timing, etc.)
            Dim snum As Integer = dsBYmerge.Tables(0)(0)("StockNum")
            Dim sname As String = dsBYmerge.Tables(0)(0)("StockName")
            Dim rnum As Integer = dsBYmerge.Tables(0)(0)("RunTiming")
            Dim suffix As String = "rc" 'string to denote type of weighting
            If BYmergeRule = 3 Then
                suffix = "us"
            ElseIf BYmergeRule = 1 Then
                suffix = "no"
            End If


            Dim supaCode As String = "AB" & suffix & "." & stk
            For f = 1 To FishNum
                For t = 1 To TSNum
                    For a = MinAge To MaxAge
                        Dim bysum As Double
                        Dim byobs As Double
                        Dim criteria As String
                        criteria = "FisheryNum = " & f & " AND Age = " & a & " AND TStepNum = " & t
                        If IsDBNull(dsBYmerge.Tables(0).Compute("Sum(EstdRecs)", criteria)) = False Then
                                bysum = dsBYmerge.Tables(0).Compute("Sum(EstdRecs)", criteria)
                                byobs = dsBYmerge.Tables(0).Compute("Sum(nRecs)", criteria)
                                Dim fname As String = dsBYmerge.Tables(0).Select("FisheryNum = " & f)(0)("FisheryName")
                                Dim tname As String = dsBYmerge.Tables(0).Select("TStepNum = " & t)(0)("TStepName")
                                dtCWTout.Tables("FRAMstarCWT").Rows.Add( _
                                    snum, stk, sname, supaCode, rnum, _
                                    "9999", a, f, fname, t, _
                                    tname, bysum, byobs, DateTime.Now, runID, "merged")
                            'dtCWTout.Tables("FRAMstarCWT").Rows.Add( _
                            '    dtCWTsupa.Tables(0)(j)("StockNum"), dtCWTsupa.Tables(0)(j)("StockAbbrev"), dtCWTsupa.Tables(0)(j)("StockName"), supaCode, dtCWTsupa.Tables(0)(j)("RunTiming"), _
                            '    dtCWTsupa.Tables(0)(j)("BroodYear"), dtCWTsupa.Tables(0)(j)("Age"), dtCWTsupa.Tables(0)(j)("FisheryNum"), dtCWTsupa.Tables(0)(j)("FisheryName"), dtCWTsupa.Tables(0)(j)("TStepNum"), _
                            '    dtCWTsupa.Tables(0)(j)("TStepName"), dtCWTsupa.Tables(0)(j)("SumOfEstdRecs"), dtCWTsupa.Tables(0)(j)("SumOfnRecs"), DateTime.Now, runID, Note)
                        End If
                    Next
                Next
            Next

        Next




       'FINALLY, Add the new records to the database table--this will not overwrite old records for the same code/stock if there are any.
       CWTdb.Open()
       sql = "SELECT * FROM FRAM_star_CWT"
       oledbAdapter = New OleDb.OleDbDataAdapter(sql, CWTdb)
       Dim oleCommander As New OleDb.OleDbCommandBuilder(oledbAdapter)
       oledbAdapter.Update(dtCWTout.Tables(0))
       CWTdb.Close()


        '-------------------------------------------------------
        '________________________________________________________________________________________________________________________________

    End Sub

    Public Sub CWTAll()
        'some database connection variables
        Dim CWTdb As New OleDb.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & CWTdatabasename)
        Dim sql As String       'SQL Query text string
        Dim oleCon As OleDbCommand
        'Dim oledbAdapter As OleDb.OleDbDataAdapter
        'Dim dtCWTall As New DataSet

        '    'Fill datatable with values that are going to CWTAll...
        '    CWTdb.Open()
        '      'sql = " SELECT FRAM_star_CWT.RunID AS ID, [FRAM_star_CWT].[StockNum]/2 AS StockID, FRAM_star_CWT.Age, FRAM_star_CWT.FisheryNum, FRAM_star_CWT.TStepNum, FRAM_star_CWT.EstdRecs, Null AS Adjusted" & _
        '      'sql = " SELECT FRAM_star_CWT.RunID AS ID, 1 AS BasePeriodID, 1 AS Species, [FRAM_star_CWT].[StockNum]/2 AS StockID, FRAM_star_CWT.Age, FRAM_star_CWT.FisheryNum, FRAM_star_CWT.TStepNum, FRAM_star_CWT.EstdRecs, Null AS Adjusted" & _
        '      '  " FROM FRAM_star_CWT" & _
        '      '  " WHERE (((FRAM_star_CWT.RunID)= " & runID & ") AND ((FRAM_star_CWT.CWTCode) Like '*AB*'));"
        '      sql = "SELECT * FROM FRAM_star_CWT WHERE (((RunID)= " & runID & ") AND ((CWTCode) Like '%AB%'))"
        '      oledbAdapter = New OleDb.OleDbDataAdapter(sql, CWTdb)
        '      oledbAdapter.Fill(dtCWTall)
        '    CWTdb.Close()



        sql = _
                " INSERT INTO [FRAM-OUT_CWTAll] ( [ID], BasePeriodID, Species, StockID, Age, FisheryID, TimeStep, Catch, Adjusted )" & _
                " SELECT FRAM_star_CWT.RunID AS ID, 1 AS BasePeriodID, 1 AS Species, [FRAM_star_CWT].[StockNum]/2 AS StockID, FRAM_star_CWT.Age, FRAM_star_CWT.FisheryNum, FRAM_star_CWT.TStepNum, FRAM_star_CWT.EstdRecs, Null AS Adjusted" & _
                " FROM FRAM_star_CWT" & _
                " WHERE (((FRAM_star_CWT.RunID)= " & runID & ") AND ((FRAM_star_CWT.CWTCode) Like '%AB%'));"
        '" VALUES (" & runID & ",1,1,1,1,1,1,1,1)"
        CWTdb.Open()
        oleCon = New OleDbCommand(sql, CWTdb)
        oleCon.ExecuteNonQuery()
        oleCon.Dispose()
        CWTdb.Close()



    End Sub


Sub insertTodb(ByVal stk As String, ByVal code As String, ByVal runID As String, ByVal note As String, ByVal RecId As String)
    If ck_LengthOnly.Checked = False Then 'Having this restriction placed here is a little sloppy, but it's less code to maintain
        dtProcessOut.Tables(0).Rows.Add(runID, stk, code, RecId, note)
        'Dim sqlnote As String
        'sqlnote = "INSERT INTO FRAM_ProcessLog (runID, Stock, Code, RecoveryId, Comments) VALUES (" & runID & ", '" & stk & "', '" & code & "', '" & RecId & "', '" & note & "')"
        'Dim CWTdb As New OleDb.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & CWTdatabasename)
        'CWTdb.Open()
        'Dim my_Command1 As New OleDbCommand(sqlnote, CWTdb)
        ''command execute
        'my_Command1.ExecuteNonQuery()
        'CWTdb.Close()
    End If
End Sub

''Function LengthPrep(ByVal Details As String, ByVal Length As Double, ByVal LengthMeth As Integer, ByVal AgeConv As Boolean, ByVal TSConv As Boolean, Yr As Integer, TS As Integer, Fishery As Integer)

''    Dim FL As Double
''    Dim FLwarn, MeasMeth As String
''    Dim FLconv As Boolean = False 'Booleans indicating conversion occurred
''    Dim Limit As Double 'Actual size limit in the fishery/time step when/where the CWT was recovered
''    Dim CWTdb As New OleDb.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & CWTdatabasename)
''    Dim LimitGrab As String

''    'First, get the size limit for the fisher/time step from Angelika's table (FRAM_SizeLimits)
''    LimitGrab = "SELECT FRAM_SizeLimits.MinimumSize " & _
''                "FROM(FRAM_SizeLimits) " & _
''                "WHERE (((FRAM_SizeLimits.FishingYear)=" & Yr & ") AND ((FRAM_SizeLimits.FisheryID)=" & Fishery & ") AND ((FRAM_SizeLimits.TimeStep)=" & TS & "));"
''    CWTdb.Open()
''    Dim my_Command1 As New OleDbCommand(LimitGrab, CWTdb)
''    'Command(execute)
''    Limit = my_Command1.ExecuteScalar()
''    CWTdb.Close()

''    If IsNothing(Limit) = True Then
''        Limit = 100 'Default 100 mm limit in absence of a limit
''    End If


''    MeasMeth = ""
''    'Now do some calculations...
''    If LengthMeth = 0 Then 'Fork Length, use it as is
''        FL = Length
''        MeasMeth = "FL"
''    End If

''    If LengthMeth = 1 Then 'Mid-Eye to Fork (SEAK/ADFG primarily), convert to FL
''        FL = 1.101 * Length - 15.878 'MEF to SNF conversion from Pahlke ADFG Report 89-02
''        FLconv = True
''        MeasMeth = "MEF"
''    End If

''    If LengthMeth = 3 Then 'Total length, convert to FL
''        If Length < 720 Then
''            FL = 0.957 * Length - 0.979 'Conrad & Gutman (1996) piecewise regression conversions of TL to FL
''        Else
''            FL = 0.969 * Length - 1.442
''        End If
''        FLconv = True
''        MeasMeth = "TL"
''    End If

''    FLwarn = ""
''    'Add some screening warnings
''    If FL > 1300 Or FL < 100 Then
''        FLwarn = "*** Unusually large or small"
''    End If

''    If FL < Limit Then
''        FLwarn = FLwarn & "*** FL lower than limit"
''    End If

''    'Now finish the update string and then commit it to the database.
''    'Details = ""
''    'Details = Details & ", " & Limit & ", " & FL & ", " & FLconv & ", '" & MeasMeth & "', " & AgeConv & _
''     '        ", " & TSConv & ", '" & FLwarn & "')"

' ''Details = "INSERT INTO FRAM_GrowthData ([RecoveryId],[RunID],[StockNum],[StockAbbrev],[StockName],[CWTCode],[RunTiming],[RunYear],[BroodYear],[Age],[FisheryNum],[FisheryName],[TStepNum],[TStepName],[RecoverySite],[RecoveryDate],[SizeLimit],[ForkLength],[Convert],[ConvMeth],[AgeFlag],[TSFlag],[Comments]) VALUES ('1884913',41806.6082,2,'SAM','Nooksack/Samish Fall','633369',3,2007,2005,3,74,'Stray Escapement',3,'Jul-Sept','3F10107  030017 H',#10/17/2007#,100,70,False,'FL',False,True,'')"

' ''    CWTdb.Open()
' ''    Dim my_Command2 As New OleDbCommand(Details, CWTdb)
' ''    'command execute
' ''    my_Command2.ExecuteNonQuery()
' ''    CWTdb.Close()
''Details = Limit & ", " & FL & ", " & FLconv & ", '" & MeasMeth & "', " & AgeConv & _
''             ", " & TSConv & ", '" & FLwarn & "'"

''Return Details
' ''Return FL
' ''Return FLconv
' ''Return MeasMeth
' ''Return AgeConv
' ''Return TSConv
' ''Return FLwarn


''End Function



End Class







