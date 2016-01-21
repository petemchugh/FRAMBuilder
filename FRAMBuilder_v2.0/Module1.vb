Imports System.Data.SqlClient
Imports System.IO

Public Module Module1




    'Broad scope database or dataset variables.
    Public CWTdb As New OleDb.OleDbConnection
    Public CWTdatabasename As String
    Public CWTshortname As String
    Public CWTdatabasepath As String
    Public ChooseBY As String
    Public dsWireTagCodedisplay As New DataSet   'Dataset for storing initial view of codes in db for selection
    Public dsWireTagCodemerge As New DataSet    'Secondary WireTagCode Table dataset for merging multiple codes within stock/by/prod combination
    Public chk As New DataGridViewCheckBoxColumn
    Public dsRecoveries As New DataSet   'Dataset for storing the selection of recoveries
    Public dsFRAMlkup As New DataSet  'CAS to FRAM fishery lookup table from CAS database
    Public dsFRAMlist As New DataSet 'List of FRAM Fisheries for file writing purposes
    Public dsCatSampDat As New DataSet 'This will contain a bunch of goodies to sum catches where possible...
    Public dtWeights As New DataTable 'A datatable that will contain weighting specs for tag codes selected for analysis
    Public wtresult As New Boolean
    Public dtPivCat As New DataTable 'A datatable that will contain the sums of catches by fishery and model year
    Public FRAMlist() As String 'Array
    Public TSNames() As String
    Public dsSpecialCodes As New DataSet   'Dataset for listing codes subject to special rules and flags for rules
    Public dsFWSportRatios As New DataSet    'Catch/Esc Ratios used to generate FW sport cwt recs


    'File I/O goods
    Public fpath As String 'File path for writing *.cwt files


    'Variables for tracking stock name, code, and brood year
    Public CWTStock() As String 'Array with stock codes
    Public CWTcode() As String 'Array with CWT codes
    Public CWTbrood() As Integer 'Array with brood years
    Public FRAMStockName() As String 'Array list with StockNames for output files
    Public FRAMStockNum() As String 'Array list with StockNums for output files
    Public CWTruntime() As Integer 'Place to store run timing information (for shifting escapement across timesteps)
    Public CWTout(,,) As Double 'Array for storing, summing estimated CWT recoveries.
    Public CWToutN(,,) As Integer 'Array for storing, summing counts of estimated CWT recoveries (~proxy for tags in hand).
    Public numCodes As Integer  'Integer value for number of codes to be included in summary run (i.e., as separate files)
    Public FishNum As Integer 'Number of FRAM fisheries to which CWTs must be mapped
    Public FramName As String 'FRAM Fishery Name
    Public TSNum As Integer 'Number of Model timesteps to which CWTs must be mapped
    Public MaxAge As Integer 'Maximum Age to reference in any mapping process
    Public MinAge As Integer ' Minimum Age to reference in any mapping process
    Public dtCWTout As New DataSet 'Primary dataset for exporting mapped CWT recoveries
    Public dtLengthOut As New DataSet 'Primary DataTable for exporting processed length data
    Public dtProcessOut As New DataSet 'pRimary DataTable for exporting processing details
    Public dtSizeLimits As New DataTable 'DataTable with Length Limits for Year, TS, FIshery
    Public dtMarkType As New DataTable 'DataTable Lookup for mark type

    Public runID As New Double




    Public Sub ExportToExcel(ByVal FileName As String, ByVal SavePath As String, ByVal objDataReader As DataTable)
        'Dim i As Integer
        Dim sb As New System.Text.StringBuilder
        Try
            Dim intColumn, intColumnValue As Integer
            Dim row As DataRow
            For intColumn = 0 To objDataReader.Columns.Count - 1
                sb.Append(objDataReader.Columns(intColumn).ColumnName)
                If intColumnValue <> objDataReader.Columns.Count - 1 Then
                    sb.Append(vbTab)
                End If
            Next
            sb.Append(vbCrLf)
            For Each row In objDataReader.Rows
                For intColumnValue = 0 To objDataReader.Columns.Count - 1
                    sb.Append(StrConv(IIf(IsDBNull(row.Item(intColumnValue)), "", row.Item(intColumnValue)), VbStrConv.ProperCase))
                    If intColumnValue <> objDataReader.Columns.Count - 1 Then
                        sb.Append(vbTab)
                    End If
                Next
                sb.Append(vbCrLf)
            Next
            SaveExcel(SavePath & "\" & FileName & ".txt", sb)
        Catch ex As Exception
            Throw
        Finally
            objDataReader = Nothing
            sb = Nothing
        End Try
    End Sub

    Private Sub SaveExcel(ByVal fpath As String, ByVal sb As System.Text.StringBuilder)
        Dim fsFile As New FileStream(fpath, FileMode.Create, FileAccess.Write)
        Dim strWriter As New StreamWriter(fsFile)
        Try
            With strWriter
                .BaseStream.Seek(0, SeekOrigin.End)
                .WriteLine(sb)
                .Close()
            End With
        Catch e As Exception
            Throw
        Finally
            sb = Nothing
            strWriter = Nothing
            fsFile = Nothing
        End Try
    End Sub



End Module
