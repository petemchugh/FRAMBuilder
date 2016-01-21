Imports System.Data.OleDb
Imports System.Data
Imports System.IO
Imports System.Windows
Imports System.Text
Imports System.IO.File
Imports System.Math




Public Class form_WelcomeAndConnect

    Private Sub ConnectDB_Click(sender As System.Object, e As System.EventArgs) Handles btn_ConnectDB.Click

        '-----------------------------------------------------------------------------
        'Code for establishing connections between FRAM Builder and an Access Database
        '-----------------------------------------------------------------------------
        Dim OpenDatabase As New OpenFileDialog()

        CWTdatabasename = ""
        OpenDatabase.Filter = "DataBase Files (*.mdb)|*.mdb|All files (*.*)|*.*"
        OpenDatabase.FilterIndex = 1
        OpenDatabase.RestoreDirectory = True
        If OpenDatabase.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            Try
                CWTdatabasename = OpenDatabase.FileName
                CWTshortname = My.Computer.FileSystem.GetFileInfo(CWTdatabasename).Name
                CWTdatabasepath = My.Computer.FileSystem.GetFileInfo(CWTdatabasename).DirectoryName
            Catch Ex As Exception
                MessageBox.Show("Cannot read file from disk. Original error: " & Ex.Message)
            End Try
        End If

        If CWTdatabasename.Length > 50 Then
            lbl_CWTdb.Text = CWTshortname
        Else
            lbl_CWTdb.Text = CWTdatabasename
        End If
        If CWTdatabasename = "" Then Exit Sub
        '-----------------------------------------------------------------------------



        '-----------------------------------------------------------------------------
        'Load necessary lookup table(s) now as well
        '-----------------------------------------------------------------------------
        Dim CWTdb As New OleDb.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & CWTdatabasename)
        Dim sql As String       'SQL Query text string
        Dim oledbAdapter As OleDb.OleDbDataAdapter


        sql = "SELECT * FROM FRAM_Fishery"

        Try
            CWTdb.Open()
            oledbAdapter = New OleDb.OleDbDataAdapter(sql, CWTdb)
            oledbAdapter.Fill(dsFRAMlkup, "FRAM_Fishery")
            oledbAdapter.Dispose()
            CWTdb.Close()

        Catch ex As Exception
            MsgBox("Failed to load FRAM_Fishery lookup table!" & vbCr & "Verify that your database contains this table and try again.")
        End Try

        sql = "SELECT * FROM FRAM_FishList"

        Try
            CWTdb.Open()
            oledbAdapter = New OleDb.OleDbDataAdapter(sql, CWTdb)
            oledbAdapter.Fill(dsFRAMlist, "FRAM_FishList")
            oledbAdapter.Dispose()
            CWTdb.Close()

        Catch ex As Exception
            MsgBox("Failed to load FRAM_Fishery lookup table!" & vbCr & "Verify that your database contains this table and try again.")
        End Try


        sql = "SELECT * FROM FRAM_code_rules"

        Try
            CWTdb.Open()
            oledbAdapter = New OleDb.OleDbDataAdapter(sql, CWTdb)
            oledbAdapter.Fill(dsSpecialCodes, "FRAM_code_rules")
            oledbAdapter.Dispose()
            CWTdb.Close()

        Catch ex As Exception
            MsgBox("Failed to load Special Code List!" & vbCr & "Verify that your database contains this table and try again.")
        End Try

        sql = "SELECT * FROM FRAM_fwspt_Ratios"

        Try
            CWTdb.Open()
            oledbAdapter = New OleDb.OleDbDataAdapter(sql, CWTdb)
            oledbAdapter.Fill(dsFWSportRatios, "FRAM_fwspt_Ratios")
            oledbAdapter.Dispose()
            CWTdb.Close()

        Catch ex As Exception
            MsgBox("Failed to load PS FW Sport Adjustments!" & vbCr & "Verify that your database contains this table and try again.")
        End Try


        '-----------------------------------------------------------------------------


        '-----------------------------------------------------------------------------
        'Define the datatable for tabulation of mapped CWT recoveries
        '-----------------------------------------------------------------------------
        If dtCWTout.Tables.Count = 0 Then
            dtCWTout.Tables.Add("FRAMstarCWT")
        End If
        If dtCWTout.Tables("FRAMstarCWT").Columns.Count = 0 Then 'Only add columns if you haven't already done so
            dtCWTout.Tables("FRAMstarCWT").Columns.Add("StockNum", GetType(Integer))
            dtCWTout.Tables("FRAMstarCWT").Columns.Add("StockAbbrev", GetType(String))
            dtCWTout.Tables("FRAMstarCWT").Columns.Add("StockName", GetType(String))
            dtCWTout.Tables("FRAMstarCWT").Columns.Add("CWTCode", GetType(String))
            dtCWTout.Tables("FRAMstarCWT").Columns.Add("RunTiming", GetType(Integer))
            dtCWTout.Tables("FRAMstarCWT").Columns.Add("BroodYear", GetType(Integer))
            dtCWTout.Tables("FRAMstarCWT").Columns.Add("Age", GetType(Integer))
            dtCWTout.Tables("FRAMstarCWT").Columns.Add("FisheryNum", GetType(Integer))
            dtCWTout.Tables("FRAMstarCWT").Columns.Add("FisheryName", GetType(String))
            dtCWTout.Tables("FRAMstarCWT").Columns.Add("TStepNum", GetType(Integer))
            dtCWTout.Tables("FRAMstarCWT").Columns.Add("TStepName", GetType(String))
            dtCWTout.Tables("FRAMstarCWT").Columns.Add("EstdRecs", GetType(Double))
            dtCWTout.Tables("FRAMstarCWT").Columns.Add("nRecs", GetType(Integer))
            dtCWTout.Tables("FRAMstarCWT").Columns.Add("DateCreated", GetType(Date))
            dtCWTout.Tables("FRAMstarCWT").Columns.Add("RunID", GetType(Double))
            dtCWTout.Tables("FRAMstarCWT").Columns.Add("Notes", GetType(String))
        End If
        '-----------------------------------------------------------------------------



        '-----------------------------------------------------------------------------
        'Define the datatable for tabulation of processed length data
        '-----------------------------------------------------------------------------
        If dtLengthOut.Tables.Count = 0 Then
            dtLengthOut.Tables.Add("LengthOut")
        End If
        If dtLengthOut.Tables("LengthOut").Columns.Count = 0 Then 'Only add columns if you haven't already done so
            dtLengthOut.Tables("LengthOut").Columns.Add("RecoveryId", GetType(String))
            dtLengthOut.Tables("LengthOut").Columns.Add("RunID", GetType(Double))
            dtLengthOut.Tables("LengthOut").Columns.Add("StockNum", GetType(Integer))
            dtLengthOut.Tables("LengthOut").Columns.Add("StockAbbrev", GetType(String))
            dtLengthOut.Tables("LengthOut").Columns.Add("StockName", GetType(String))
            dtLengthOut.Tables("LengthOut").Columns.Add("CWTCode", GetType(String))
            dtLengthOut.Tables("LengthOut").Columns.Add("RunTiming", GetType(Integer))
            dtLengthOut.Tables("LengthOut").Columns.Add("RunYear", GetType(Integer))
            dtLengthOut.Tables("LengthOut").Columns.Add("BroodYear", GetType(Integer))
            dtLengthOut.Tables("LengthOut").Columns.Add("age", GetType(Integer))
            dtLengthOut.Tables("LengthOut").Columns.Add("FisheryNum", GetType(Integer))
            dtLengthOut.Tables("LengthOut").Columns.Add("FisheryName", GetType(String))
            dtLengthOut.Tables("LengthOut").Columns.Add("TStep", GetType(Integer))
            dtLengthOut.Tables("LengthOut").Columns.Add("TStepName", GetType(String))
            dtLengthOut.Tables("LengthOut").Columns.Add("RecoverySite", GetType(String))
            dtLengthOut.Tables("LengthOut").Columns.Add("RecoveryDate", GetType(Date))
            dtLengthOut.Tables("LengthOut").Columns.Add("SizeLimit", GetType(Double))
            dtLengthOut.Tables("LengthOut").Columns.Add("ForkLength", GetType(Double))
            dtLengthOut.Tables("LengthOut").Columns.Add("RelDate", GetType(Date))
            dtLengthOut.Tables("LengthOut").Columns.Add("DaysSinceRelease", GetType(Integer))
            dtLengthOut.Tables("LengthOut").Columns.Add("AgeMonths", GetType(Double))
            dtLengthOut.Tables("LengthOut").Columns.Add("MarkObs", GetType(String))
            dtLengthOut.Tables("LengthOut").Columns.Add("MarkRel", GetType(String))
            dtLengthOut.Tables("LengthOut").Columns.Add("FisheryType", GetType(String))
            dtLengthOut.Tables("LengthOut").Columns.Add("Converted", GetType(Boolean))
            dtLengthOut.Tables("LengthOut").Columns.Add("MeasMeth", GetType(String))
            dtLengthOut.Tables("LengthOut").Columns.Add("AgeFlag", GetType(Boolean))
            dtLengthOut.Tables("LengthOut").Columns.Add("TSFlag", GetType(Boolean))
            dtLengthOut.Tables("LengthOut").Columns.Add("Comments", GetType(String))
        End If
        '-----------------------------------------------------------------------------


        '-----------------------------------------------------------------------------
        'Define the datatable for tabulation of processed length data
        '-----------------------------------------------------------------------------
        If dtProcessOut.Tables.Count = 0 Then
            dtProcessOut.Tables.Add("ProcOut")
        End If
        If dtProcessOut.Tables("ProcOut").Columns.Count = 0 Then 'Only add columns if you haven't already done so
            dtProcessOut.Tables("ProcOut").Columns.Add("RunID", GetType(String))
            dtProcessOut.Tables("ProcOut").Columns.Add("Stock", GetType(String))
            dtProcessOut.Tables("ProcOut").Columns.Add("Code", GetType(Double))
            dtProcessOut.Tables("ProcOut").Columns.Add("RecoveryId", GetType(String))
            dtProcessOut.Tables("ProcOut").Columns.Add("Comments", GetType(String))
        End If
        '-----------------------------------------------------------------------------





    End Sub

    Private Sub btn_viewCodes_Click(sender As System.Object, e As System.EventArgs) Handles btn_viewCodes.Click
         If CWTdatabasename = "" Then
            MessageBox.Show("You must select a database before proceeding.")
            Exit Sub
         End If

        'Prevents re-entry if you don't want to start over.
        If dsRecoveries.Tables.Count > 0 Then
                    Dim result = MessageBox.Show(Me, "You've already selected codes. Are you sure you want to start over?", "Warning", MessageBoxButtons.YesNo)
                    If result = DialogResult.No Then
                        Exit Sub
                    End If
        End If

         Me.Visible = False
        form_ViewCodes.ShowDialog()
    End Sub

    Private Sub btnExit_Click(sender As System.Object, e As System.EventArgs) Handles btn_Exit.Click

        Me.Close()

    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs)

        Me.Visible = False
        form_StatusAndSummary.ShowDialog()

    End Sub

    Private Sub btnOptions_Click(sender As System.Object, e As System.EventArgs) Handles btn_Options.Click
         If CWTdatabasename = "" Then
            MessageBox.Show("You must select a database and codes before proceeding.")
            Exit Sub
         End If

         If dsRecoveries.Tables.Count = 0 Then
            MessageBox.Show("You must select codes before proceeding.")
            Exit Sub
         End If

         Me.Visible = False
        form_OutputOptions.ShowDialog()
    End Sub

    Private Sub form_WelcomeAndConnect_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        'Initialize some important dimensioning variables
        Call Specifications()

        'Now define the text for a bunch of hover-over bubbles
        Dim tip_ChooseDB As New ToolTip
        Dim string1 As String
        string1 = "Define the connection for the FRAM calibration CWT database." & vbCr & _
                "If you're uncertain, it should contain the following tables:" & vbCr & _
                    "  XXXX" & vbCr & _
                    "  XXXX" & vbCr & _
                    "  XXXX" & vbCr & _
                    "  XXXX"

        tip_ChooseDB.SetToolTip(btn_ConnectDB, string1)

        Dim tip_MakeCodeSelection As New ToolTip
        Dim string2 As String
        string2 = "View available CWTs and select the stocks, broods, and codes" & vbCr & "for use in calibration."
        tip_MakeCodeSelection.SetToolTip(btn_viewCodes, string2)

        Dim tip_SpecifyFilePrepOpts As New ToolTip
        Dim string3 As String
        string3 = "Set mapping and compiling options, as well as output summary specs." & vbCr & "Please review to ensure default settings are appropriate!"
        tip_SpecifyFilePrepOpts.SetToolTip(btn_Options, string3)

    End Sub


    Public Sub Specifications()

        'This subroutine is a catch all of sorts, a tidier place for defining a bunch of important dimensioning variables
        FishNum = 85 'See commented out list for FishNum details below, this includes some new components.
        MaxAge = 5
        MinAge = 2
        TSNum = 3
        ReDim CWTout(MaxAge - MinAge, FishNum, TSNum)

        'Names of FRAM time steps -- modify here if necessary.
        ReDim TSNames(TSNum)
        TSNames(0) = "Oct-Apr"
        TSNames(1) = "May-Jun"
        TSNames(2) = "Jul-Sept"


        'FisheryName(FishID)
        'SE Alaska Sport	1
        'SE Alaska Net	2
        'SE Alaska Sport	3
        'BC No/Cent Net	4
        'BC WCVI Net	5
        'BC Georgia Strait Net	6
        'BC JDF Net	7
        'BC Outside Sport	8
        'BC No/Cent Troll	9
        'BC WCVI Troll	10
        'BC WCVI Sport	11
        'BC Georgia Strait Troll	12
        'BC N Georgia Strait Sport	13
        'BC S Georgia Strait Sport	14
        'BC JDF Sport	15
        'NT Area 3:4:4B Troll	16
        'Tr Area 3:4:4B Troll	17
        'NT Area 3:4 Sport	18
        'No Wash. Coastal Net	19
        'NT Area 2 Troll	20
        'Tr Area 2 Troll	21
        'NT Area 2 Sport	22
        'NrT G. Harbor Net	23
        'T G. Harbor Net	24
        'Willapa Bay Net	25
        'Area 1 Troll	26
        'Area 1 Sport	27
        'Columbia River Net	28
        'Buoy 10 Sport	29
        'Central OR Troll	30
        'Central OR Sport	31
        'KMZ Troll	32
        'KMZ Sport	33
        'So Calif. Troll	34
        'So Calif. Sport	35
        'NT Area 7 Sport	36
        'NT Area 6A:7:7A Net	37
        'Tr Area 6A:7:7A Net	38
        'NT Area 7B-7D Net	39
        'Tr Area 7B-7D Net	40
        'Tr JDF Troll	41
        'NT Area 5 Sport	42
        'NT JDF Net	43
        'Tr JDF Net	44
        'NT Area 8-1 Sport	45
        'NT Skagit Net	46
        'Tr Skagit Net	47
        'NT Area 8D Sport	48
        'NT St/Snohomish Net	49
        'Tr St/Snohomish Net	50
        'NT Tulalip Bay Net	51
        'Tr Tulalip Bay Net	52
        'NT Area 9 Sport	53
        'NT Area 6 Sport	54
        'Tr Area 6B:9 Net	55
        'NT Area 10 Sport	56
        'NT Area 11 Sport	57
        'NT Area 10:11 Net	58
        'Tr Area 10:11 Net	59
        'NT Area 10A Sport	60
        'Tr Area 10A Net	61
        'NT Area 10E Sport	62
        'Tr Area 10E Net	63
        'NT Area 12 Sport	64
        'NT Hood Canal Net	65
        'Tr Hood Canal Net	66
        'NT Area 13 Sport	67
        'NT SPS Net	68
        'Tr SPS Net	69
        'NT Area 13A Net	70
        'Tr Area 13A Net	71
        'Freshwater Sport	72
        'Freshwater Net	73
        'Escapement 74
        'Stray Escapement	75
        'NT Area 3 Troll	76
        'Tr Area 3 Troll	77
        'NT Area 4 Troll	78
        'Tr Area 4 Troll	79
        'BC NBC Sport AABM	80
        'BC NBC Sport ISBM	81
        'Area 12 NoA Net	82
        'Area 12 SoA Net	83
        'Area 12 NoA Sport	84
        'Area 12 SoA Sport	85


    End Sub


End Class
